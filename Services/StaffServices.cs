
using Hospital_Management_System.Repository;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Hospital_Management_System.Services
{
    public class StaffServices : IStaffServices
    {
        private readonly IStuffRepository _stuffRepository;
        private readonly IMapper _mapper;

        public StaffServices(IStuffRepository stuffRepository,
            IMapper mapper)
        {
            _stuffRepository = stuffRepository;
            _mapper = mapper;
        }

        public async Task Create(StuffCreateDto dto)
        {
            var stuff = _mapper.Map<Stuff>(dto);
            await _stuffRepository.Add(stuff);
            await _stuffRepository.SaveAsync();
        }

        public async Task<IEnumerable<StuffDisplayDto>> GetAll()
        {
            var stuffsFromDB = await _stuffRepository.GetAll();
            var stuffDtos = _mapper.Map<IEnumerable<StuffDisplayDto>>(stuffsFromDB);
            return stuffDtos;
        }

        public async Task<Stuff> GetById(string ssn)
        {
            return await _stuffRepository.GetById(ssn);
        }

        public async Task Terminate(string ssn)
        {
            var stuff = await _stuffRepository.GetById(ssn);
            if(stuff != null)
            {
                await _stuffRepository.Remove(ssn);
                stuff.IsTerminated = true;
                stuff.SeparationDate = DateOnly.FromDateTime(DateTime.Now);
                await _stuffRepository.SaveAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Staff with SSN {ssn} not found.");
            }
        }

        public async Task Update(StuffUpdateDto staff)
        {
            var stuff = _mapper.Map<Stuff>(staff);
            if (!await _stuffRepository.IsExists(stuff.SSN))
            {
                throw new KeyNotFoundException($"Staff with SSN {staff.SSN} not found.");
            }
            else
            {
                _stuffRepository.Update(stuff);
                await _stuffRepository.SaveAsync();
            }

        }

    }
}
