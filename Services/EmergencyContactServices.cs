
using Hospital_Management_System.Repository;
using Hospital_Management_System.UnitOfWork;
using System.Threading.Tasks;

namespace Hospital_Management_System.Services
{
    public class EmergencyContactServices : IEmergencyContactServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EmergencyContactServices> _logger;

        public EmergencyContactServices(IMapper mapper,
            ILogger<EmergencyContactServices> logger,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(EmergencyContactCreationDto emergencyContact)
        {
            if (emergencyContact == null)
            {
                _logger.LogError("Attempted to add a null emergency contact");
                throw new ArgumentNullException(nameof(emergencyContact), "Emergency contact cannot be null");
            }
            var emergencyContactEntity = _mapper.Map<EmergencyContact>(emergencyContact);
            _logger.LogInformation("Adding emergency contact for patient with ID {PatientId} in database", emergencyContactEntity.PatientId);
            await _unitOfWork.EmergencyContacts.AddAsync(emergencyContactEntity);
            await _unitOfWork.EmergencyContacts.SaveAsync();
            _logger.LogInformation("Emergency contact for patient with ID {PatientId} added successfully", emergencyContactEntity.PatientId);
        }

        public async Task DeleteAllPatientEmergencyContacts(int patientId)
        {
            _logger.LogInformation("Retrieving all emergency contacts of patient that has ID {patientId}", patientId);
            var EContactDtos = _unitOfWork.EmergencyContacts.GetAllPatientEmergencyContacts(patientId);
            _logger.LogInformation("Mapping the Emergency Contact Dtos to Emergency Contact");
            var EContacts = _mapper.Map<IEnumerable<EmergencyContact>>(EContactDtos);
            _logger.LogInformation("Deleting all ptient emergency contacts");
            _unitOfWork.EmergencyContacts.DeleteAllPatientEmergencyContacts(EContacts);
            await _unitOfWork.Complete();
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0 )
            {
                _logger.LogError("Attempted to delete an emergency contact with invalid ID {Id}", id);
                throw new ArgumentException("Invalid emergency contact ID", nameof(id));
            }
            var dto = await _unitOfWork.EmergencyContacts.GetByIdAsync(id);
            var emergencyContact = _mapper.Map<EmergencyContact>(dto); 
            if (emergencyContact == null)
            {
                _logger.LogWarning("Emergency contact with ID {Id} not found for deletion", id);
                throw new KeyNotFoundException($"Emergency contact with ID {id} not found");
            }
            _logger.LogInformation("Deleting emergency contact with ID {Id} from database", id);
            _unitOfWork.EmergencyContacts.Delete(emergencyContact);
            await _unitOfWork.Complete();
        }

        public IEnumerable<EmergencyContactDisplayDto> GetAllPatientEmergencyContactsAsync(int patientId)
        {
            _logger.LogInformation("Retrieving all emergency contacts for patient with ID {PatientId}", patientId);
            var emergencyContacts = _unitOfWork.EmergencyContacts.GetAllPatientEmergencyContacts(patientId)
                .AsEnumerable();
            if(emergencyContacts == null || !emergencyContacts.Any())
            {
                _logger.LogWarning("No emergency contacts found for patient with ID {PatientId}", patientId);
                return Enumerable.Empty<EmergencyContactDisplayDto>();
            }
                var emergencyContactDtos = _mapper.Map<IEnumerable<EmergencyContactDisplayDto>>(emergencyContacts);
            return emergencyContactDtos;
        }

        public async Task<EmergencyContactDisplayDto> GetById(int id)
        {
            _logger.LogInformation("Retrieving Emergency Contact's Data From Database with ID {id}", id);
            var EContact = await _unitOfWork.EmergencyContacts.GetByIdAsync(id);
            if (EContact == null)
            {
                _logger.LogError("Emergency Contact With Id {id} is not exist!", id);
                throw new NullReferenceException($"Emergency Contact With Id {id} is not exist!");
            }
            _logger.LogInformation("Mapping Emergency Contact to Emergency Contact Dto");
            var dto = _mapper.Map<EmergencyContactDisplayDto>(EContact);
            return dto;
        }
    }
}
