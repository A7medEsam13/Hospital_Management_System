
using Hospital_Management_System.Repository;
using Hospital_Management_System.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Hospital_Management_System.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<PrescriptionService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;


        public PrescriptionService(IPrescriptionRepository prescriptionRepository,
            ILogger<PrescriptionService> logger,
            IPrescriptionMedicineRepository prescriptionMedicineRepository,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IStuffRepository stuffRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
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
            await _unitOfWork.PrescriptionMedicines.CreatePrescriptionMedicine(prescriptionMedicine);
            await _unitOfWork.Complete();
        }

        public async Task CreateNewPrescription(PrescriptionCreationDto dto)
        {
            _logger.LogInformation("Create and mapping the data to the entity object");
            var userID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var doctorID = await _unitOfWork.Stuffs.GetStuffSSN(userID);
            var date = DateOnly.FromDateTime(DateTime.Today);
            var prescription = new Prescription()
            {
                PatientId = dto.PatientID,
                DoctorId = doctorID,
                Date = date
            };
            _logger.LogInformation("Adding and saving the prescription to the repository");
            await _unitOfWork.Prescriptions.CreateNewPrescriptionAsync(prescription);
            await _unitOfWork.Complete();
        }

        public async Task DeletePrescriptionAsync(int prescriptionID)
        {
            if (prescriptionID <= 0)
            {
                _logger.LogError("The id is invalid");
                throw new InvalidDataException();
            }

            _logger.LogInformation("Deleting the prescription with ID {id}", prescriptionID);
            await _unitOfWork.Prescriptions.DeletePrescriptionAsync(prescriptionID);
        }

        public IEnumerable<PrescriptionDisplayDto> GetAllDoctorPrescriptions(string doctorSSN)
        {
            _logger.LogInformation("Getting Prescriptions of the doctor with ssn {ssn}", doctorSSN);
            var prescriptions = _unitOfWork.Prescriptions.GetAllDoctorPrescriptions(doctorSSN);
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
            var prescriptions = _unitOfWork.Prescriptions.GetAllPatientPrescriptions(patientID);

            if (prescriptions == null)
            {
                _logger.LogWarning("the patient with id {id} does not has ant prescriptions", patientID);
                throw new NullReferenceException();
            }

            return prescriptions.Select(p => new PrescriptionDisplayDto()
            {
                PatientId = patientID,
                DoctorSSN = p.DoctorId,
                Medicines = GetPrescriptionMedicines(p.Id)
            });
        }

        public IEnumerable<PrescriptionMedicineDisplayDTO> GetAllPrescriptionMedicines(int prescriptionID)
        {
            _logger.LogInformation("Getting the medicines of prescription {id}", prescriptionID);
            return GetPrescriptionMedicines(prescriptionID);
        }

        public async Task RemoveMedicine(int medicineID, int prescriptionID)
        {
            _logger.LogInformation("Deleting the prescription medicine");
            await _unitOfWork.PrescriptionMedicines.Delete(prescriptionID, medicineID);
        }

        public async Task UpdatePrescription(int prescriptionID, int medicineID, string? newDosage, string? newDuration)
        {
            _logger.LogInformation("Updating the medicine with ID {id}", medicineID);
            var prescriptionMedicine = await _unitOfWork.PrescriptionMedicines.GetPrescriptionMedicine(prescriptionID, medicineID);

            if(prescriptionMedicine is null)
            {
                _logger.LogWarning("the medicine with id {id} is not exist in the prescription {pid}", medicineID, prescriptionID);
                throw new KeyNotFoundException("this medicine not found in the prescription");
            }
            if (newDosage is not null)
                prescriptionMedicine.Dosage = newDosage;

            if (newDuration is not null)
                prescriptionMedicine.Duration = newDuration;


            await _unitOfWork.PrescriptionMedicines.UpdatePrescriptionMedicine(prescriptionMedicine);
            
        }


        //////////////////////////////////////////////////////
        private List<PrescriptionMedicineDisplayDTO> GetPrescriptionMedicines(int prescriptionID)
        {
            var medicines = _unitOfWork.PrescriptionMedicines.GetMedicinesOfPrescription(prescriptionID);
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
