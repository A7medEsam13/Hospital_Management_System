using Hospital_Management_System.Repository;
using Hospital_Management_System.UnitOfWork;

namespace Hospital_Management_System.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPatientServices, PatientServices>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IAppointmentServices, AppointmentServices>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IDoctorServices, DoctorServices>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IStaffServices, StaffServices>();
            services.AddScoped<IStuffRepository, StuffRepository>();
            services.AddScoped<IEmergencyContactRepository, EmergencyContactRepository>();
            services.AddScoped<IEmergencyContactServices, EmergencyContactServices>();
            services.AddScoped<IDiagnosisServices, DiagnosisServices>();
            services.AddScoped<IDiagnosisRepository, DiagnosisRepository>();
            services.AddScoped<IDiagnosisPatientRepository, DiagnosisPatientRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPrescriptionMedicineRepository, PrescriptionMedicineRepository>();
            services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            services.AddScoped<IPayrollRepository, PayrollRepository>();
            services.AddScoped<IPayrollServices, PayrollServices>();
            services.AddScoped<IMedicineRepository, MedicineRepository>();
            services.AddScoped<ILaboratoryScreeningRepository, LaboratoryScreeningRepository>();
            services.AddScoped<ILaboratoryScreeningServices, LaboratoryScreeningServices>();
            services.AddScoped<IBillServices, BillServices>();
            services.AddScoped<IBillRepository, BillRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}
