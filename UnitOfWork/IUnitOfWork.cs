using Hospital_Management_System.Repository;

namespace Hospital_Management_System.UnitOfWork
{
    public interface IUnitOfWork : IDisposable 
    {
        ILaboratoryScreeningRepository LaboratoryScreenings { get; }
        IAppointmentRepository Appointments { get;  }
        IDiagnosisPatientRepository DiagnosisPatient { get; }
        IDiagnosisRepository Diagnoses { get; }
        IDoctorRepository Doctors { get; }
        IEmergencyContactRepository EmergencyContacts { get; }
        IPatientRepository Patients { get; }
        IPrescriptionMedicineRepository PrescriptionMedicines { get; }
        IPrescriptionRepository Prescriptions { get; }
        IRoomRepository Rooms { get;  }
        IStuffRepository Stuffs { get;  }
        IPayrollRepository Payrolls { get; }
        IMedicineRepository Medicines { get; }
        IBillRepository Bills { get; }

        public Task<int> Complete();
    }
}
