
using Hospital_Management_System.Repository;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Hospital_Management_System.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IPrescriptionMedicineRepository _prescriptionMedicineRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<PrescriptionService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStuffRepository _stuffRepository;



        public PrescriptionService(IPrescriptionRepository prescriptionRepository,
            ILogger<PrescriptionService> logger,
            IPrescriptionMedicineRepository prescriptionMedicineRepository,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IStuffRepository stuffRepository)
        {
            _prescriptionRepository = prescriptionRepository;
            _logger = logger;
            _prescriptionMedicineRepository = prescriptionMedicineRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _stuffRepository = stuffRepository;
        }

        public async Task AddNewMedicine(PrescriptionMedicineCreationDTO dto)
        {
            _logger.LogInformation("Mapping the dto");
            var prescriptionMedicine = new PrescriptionMedicine()
            {
                PrescriptionId = dto.PrescriptionID,
                MedicineId = dto.MedicineID,
                Dosage = dto.Dosage,
                Duration = dto.Duration
            };

            _logger.LogInformation("Adding and saving the Medicine {mid} to the Prescription {pid}", dto.MedicineID, dto.PrescriptionID);
            await _prescriptionMedicineRepository.CreatePrescriptionMedicine(prescriptionMedicine);
            await _prescriptionRepository.SaveAsync();
        }

        public async Task CreateNewPrescription(PrescriptionCreationDto dto)
        {
            _logger.LogInformation("Create and mapping the data to the entity object");
            var userID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var doctorID = await _stuffRepository.GetStuffSSN(userID);
            var date = DateOnly.FromDateTime(DateTime.Today);
            var prescription = new Prescription()
            {
                PatientId = dto.PatientID,
                DoctorId = doctorID,
                Date = date
            };
            _logger.LogInformation("Adding and saving the prescription to the repository");
            await _prescriptionRepository.CreateNewPrescriptionAsync(prescription);
            await _prescriptionRepository.SaveAsync();
        }

        public async Task DeletePrescriptionAsync(int prescriptionID)
        {
            if (prescriptionID <= 0)
            {
                _logger.LogError("The id is invalid");
                throw new InvalidDataException();
            }

            _logger.LogInformation("Deleting the prescription with ID {id}", prescriptionID);
            await _prescriptionRepository.DeletePrescriptionAsync(prescriptionID);
        }

        public IEnumerable<PrescriptionDisplayDto> GetAllDoctorPrescriptions(string doctorSSN)
        {
            _logger.LogInformation("Getting Prescriptions of the doctor with ssn {ssn}", doctorSSN);
            var prescriptions = _prescriptionRepository.GetAllDoctorPrescriptions(doctorSSN);
            return prescriptions.Select(p => new PrescriptionDisplayDto()
            {
                PatientId = p.PatientId,
                Medicines = GetPrescriptionMedicines(p.Id),
                DoctorSSN = p.DoctorId
            });
        }

        public IEnumerable<PrescriptionDisplayDto> GetAllPatientPrescriptions(int patientID)
        {
            _logger.LogInformation("Getting all prescriptions of the patient with id {id} from database", patientID);
            var prescriptions = _prescriptionRepository.GetAllPatientPrescriptions(patientID);

            if (prescriptions == null)
            {
                _logger.LogWarning("the patient with id {id} does not has ant prescriptions", patientID);
                throw new NullReferenceException();
            }

            return prescriptions.Select(p => new PrescriptionDisplayDto()
            {
                PatientId = patientID,
                DoctorSSN = p.DoctorId,
                Medicines = GetPrescriptionMedicines(patientID)
            });
        }

        public IEnumerable<PrescriptionMedicineDisplayDTO> GetAllPrescriptionMedicines(int prescriptionID)
        {
            return GetPrescriptionMedicines(prescriptionID);
        }

        public Task RemoveMedicine(int medicineID)
        {
            
        }

        public Task UpdatePrescription(PrescriptionDisplayDto dto)
        {

        }


        //////////////////////////////////////////////////////
        private List<PrescriptionMedicineDisplayDTO> GetPrescriptionMedicines(int prescriptionID)
        {
            var medicines = _prescriptionMedicineRepository.GetMedicinesOfPrescription(prescriptionID);
            return medicines.Select(m => new PrescriptionMedicineDisplayDTO()
            {
                Dosage = m.Dosage,
                Duration = m.Duration,
                PrescriptionId = m.PrescriptionId,
                MedicineId = m.MedicineId,
                MedicineName = m.Medicine.Name
            }).ToList();
                
        }
    }
}
