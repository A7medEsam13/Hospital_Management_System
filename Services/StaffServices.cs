
using Hospital_Management_System.Repository;
using Hospital_Management_System.UnitOfWork;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Hospital_Management_System.Services
{
    public class StaffServices : IStaffServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<StaffServices> _logger;

        public StaffServices(IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<StaffServices> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Create(StuffCreateDto dto)
        {
            var stuff = _mapper.Map<Stuff>(dto);
            await _unitOfWork.Stuffs.Add(stuff);
            await _unitOfWork.Complete();
        }

        public async Task<IEnumerable<StuffDisplayDto>> GetAll()
        {
            var stuffsFromDB = await _unitOfWork.Stuffs.GetAll();
            var stuffDtos = _mapper.Map<IEnumerable<StuffDisplayDto>>(stuffsFromDB);
            return stuffDtos;
        }

        public async Task<StuffDisplayDto> GetById(string ssn)
        {
            _logger.LogInformation("Getting the stuff entity from database with ssn{ssn}", ssn);
            var stuffEntity = await _unitOfWork.Stuffs.GetById(ssn);

            _logger.LogInformation("Mapping the entity to display dto");
            var dto = _mapper.Map<StuffDisplayDto>(stuffEntity);

            return dto;
        }

        public async Task<StuffDisplayDto> GetByUserID(string userID)
        {
            
            var stuff = await _unitOfWork.Stuffs.GetStuffByUserID(userID);

            var dto = _mapper.Map<StuffDisplayDto>(stuff);

            return dto;
        }

        public async Task ReturnStuffToWork(string ssn)
        {
            _logger.LogInformation("Getting the stuuf entity from database with ssn {ssn}", ssn);
            var stuff = await _unitOfWork.Stuffs.GetById(ssn);

            _logger.LogInformation("Assign stuff to not terminated and save");
            stuff.IsTerminated = false;
            if (stuff.Role == "Doctor")
            {
                var doctor = new Doctor
                {
                    FirstName = stuff.FirstName,
                    LastName = stuff.LastName,
                    SSN = stuff.SSN,
                    JoinDate = stuff.JoinDate,
                    SeparationDate = stuff.SeparationDate,
                    Role = stuff.Role,
                    Email = stuff.Email,
                    Address = stuff.Address,
                    DepartmentName = stuff.DepartmentName,
                    IsTerminated = stuff.IsTerminated,
                    Salary = stuff.Salary,
                    UserId = stuff.UserId
                };
                await _unitOfWork.Doctors.Add(doctor);
            }
            await _unitOfWork.Complete();
        }

        public async Task Terminate(string ssn)
        {
            var stuff = await _unitOfWork.Stuffs.GetById(ssn);
            if(stuff != null)
            {
                if (stuff.IsTerminated)
                {
                    _logger.LogWarning("stuff with ssn {ssn} had been terminated", stuff.SSN);
                    throw new Exception($"stuff with ssn {ssn} had been terminated");
                }

                await _unitOfWork.Stuffs.Remove(ssn);
                stuff.SeparationDate = DateOnly.FromDateTime(DateTime.Now);
                await _unitOfWork.Complete();
            }
            else
            {
                throw new KeyNotFoundException($"Staff with SSN {ssn} not found.");
            }
        }

        public async Task Update(StuffUpdateDto dto)
        {
            var stuff = await _unitOfWork.Stuffs.GetById(dto.SSN);
            if (!await _unitOfWork.Stuffs.IsExists(stuff.SSN))
            {
                throw new KeyNotFoundException($"Staff with SSN {dto.SSN} not found.");
            }
            else
            {
                if (dto.Email != "string")
                {
                    stuff.Email = dto.Email;
                }

                if (dto.Address != "string")
                {
                    stuff.Address = dto.Address;
                }

                if (dto.DepartmentName != "string")
                {
                    stuff.DepartmentName = dto.DepartmentName;
                }

                if (dto.Salary != 0)
                {
                    stuff.Salary = dto.Salary;
                }
                _unitOfWork.Stuffs.Update(stuff);
                await _unitOfWork.Complete();
            }

        }

    }
}
