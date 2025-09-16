
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

        public StaffServices(IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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

        public async Task<Stuff> GetById(string ssn)
        {
            return await _unitOfWork.Stuffs.GetById(ssn);
        }

        public async Task<StuffDisplayDto> GetByUserID(string userID)
        {
            
            var stuff = await _unitOfWork.Stuffs.GetStuffByUserID(userID);

            var dto = _mapper.Map<StuffDisplayDto>(stuff);

            return dto;
        }

        public async Task Terminate(string ssn)
        {
            var stuff = await _unitOfWork.Stuffs.GetById(ssn);
            if(stuff != null)
            {
                await _unitOfWork.Stuffs.Remove(ssn);
                stuff.IsTerminated = true;
                stuff.SeparationDate = DateOnly.FromDateTime(DateTime.Now);
                await _unitOfWork.Complete();
            }
            else
            {
                throw new KeyNotFoundException($"Staff with SSN {ssn} not found.");
            }
        }

        public async Task Update(StuffUpdateDto staff)
        {
            var stuff = _mapper.Map<Stuff>(staff);
            if (!await _unitOfWork.Stuffs.IsExists(stuff.SSN))
            {
                throw new KeyNotFoundException($"Staff with SSN {staff.SSN} not found.");
            }
            else
            {
                _unitOfWork.Stuffs.Update(stuff);
                await _unitOfWork.Complete();
            }

        }

    }
}
