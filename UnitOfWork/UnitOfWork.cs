using Hospital_Management_System.Repository;
using System.Threading.Tasks;

namespace Hospital_Management_System.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBillRepository Bills { get; private set; }

        public ILaboratoryScreeningRepository LaboratoryScreenings { get; private set; }


        public IAppointmentRepository Appointments { get; private set; }

        public IDiagnosisRepository Diagnoses { get; private set; }

        public IDiagnosisPatientRepository DiagnosisPatient { get; private set; }

        public IDoctorRepository Doctors { get; private set; }

        public IEmergencyContactRepository EmergencyContacts { get; private set; }

        public IPatientRepository Patients { get; private set; }

        public IPrescriptionMedicineRepository PrescriptionMedicines { get; private set; }

        public IPrescriptionRepository Prescriptions { get; private set; }

        public IRoomRepository Rooms { get; private set; }

        public IStuffRepository Stuffs { get; private set; }

        public IMedicineRepository Medicines { get; private set; }


        public UnitOfWork(IAppointmentRepository appointments,
            IDiagnosisRepository diagnoses,
            IDiagnosisPatientRepository diagnosisPatient,
            IDoctorRepository doctors,
            IEmergencyContactRepository emergencyContacts,
            IPatientRepository patients,
            IPrescriptionMedicineRepository prescriptionMedicines,
            IPrescriptionRepository prescriptions,
            IRoomRepository rooms,
            IStuffRepository stuffs,
            ApplicationDbContext context,
            IMedicineRepository medicines,
            ILaboratoryScreeningRepository laboratoryScreening,
            IBillRepository bills)
        {
            Appointments = appointments;
            Diagnoses = diagnoses;
            DiagnosisPatient = diagnosisPatient;
            Doctors = doctors;
            EmergencyContacts = emergencyContacts;
            Patients = patients;
            PrescriptionMedicines = prescriptionMedicines;
            Prescriptions = prescriptions;
            Rooms = rooms;
            Stuffs = stuffs;
            _context = context;
            Medicines = medicines;
            LaboratoryScreenings = laboratoryScreening;
            Bills = bills;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }


        void IDisposable.Dispose()
        {
             _context.Dispose();
        }
    }
}
