
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Hospital_Management_System.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Patient Relations
            // the relation between patient and prescription.
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Prescriptions)
                .WithOne(p => p.Patient)
                .HasForeignKey(p => p.PatientId);

            // the relation between patient and bills.  
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Bills)
                .WithOne(b => b.Patient)
                .HasForeignKey(b => b.PatientId);


            // the relation between patient and diagnoses.
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.DiagnosisPatient)
                .WithOne(d => d.Patient)
                .HasForeignKey(pd => pd.PatientId);

            // the relation between patient and doctors.
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Doctors)
                .WithMany(d => d.Patients);

            // the relation between patient and emergency contacts.
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.EmergencyContacts)
                .WithOne(e => e.Patient)
                .HasForeignKey(e => e.PatientId);

            // the relation between patient and room.
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Room)
                .WithMany(r => r.Patients);
            #endregion

            #region Doctor Relations
            modelBuilder.Entity<Doctor>()
                .ToTable("Doctors");

            // the relation between doctor and Prescriptions.
            modelBuilder.Entity<Doctor>()
                .HasMany(d=>d.Prescriptions)
                .WithOne(p=>p.Doctor)
                .HasForeignKey(p => p.DoctorId);

            //the relation between doctor and appoinments.
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Appointments)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorId);

            // the relation between doctor and laboratory screenings.
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.LaboratoryScreenings)
                .WithOne(l => l.Doctor)
                .HasForeignKey(l => l.DoctorId);

            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Diagnoses)
                .WithOne(di => di.Doctor)
                .HasForeignKey(di => di.DoctorSSN);


            #endregion



            #region Stuff Relations
            // the relation between stuff and payroll.
            modelBuilder.Entity<Stuff>()
                .HasMany(s => s.Payrolls)
                .WithOne(p => p.Staff)
                .HasForeignKey(p => p.StaffSSN);

            // the relation between stuff technician and laboratory screenings.
            modelBuilder.Entity<Stuff>()
                .HasMany(s => s.LaboratoryScreenings)
                .WithOne(l => l.Technician)
                .HasForeignKey(l => l.TechnicianSSN);
            #endregion

            #region Bill Relations

            // the relation between bill and room.
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bills)
                .HasForeignKey(b => b.RoomId);

            // the relation between bill and medicines.
            modelBuilder.Entity<Bill>()
                .HasMany(b => b.Prescriptions)
                .WithOne(m => m.Bill)
                .HasForeignKey(pm => pm.BillID);

            #endregion

            #region Medicine Prescription
            modelBuilder.Entity<PrescriptionMedicine>()
                .HasKey(pm => new { pm.PrescriptionId, pm.MedicineId });

            modelBuilder.Entity<PrescriptionMedicine>()
                .HasOne(pm => pm.Medicine)
                .WithMany(m => m.PrescriptionMedicines)
                .HasForeignKey(pm => pm.MedicineId);

            modelBuilder.Entity<PrescriptionMedicine>()
                .HasOne(pm => pm.Prescription)
                .WithMany(p => p.PrescriptionMedicines)
                .HasForeignKey(pm => pm.PrescriptionId);
            
            #endregion

            #region Patient Diagnosis
            modelBuilder.Entity<DiagnosisPatient>()
                .HasKey(pd => new { pd.PatientId, pd.DiagnosisId });
            
            modelBuilder.Entity<DiagnosisPatient>()
                .HasOne(pd => pd.Patient)
                .WithMany(p => p.DiagnosisPatient)
                .HasForeignKey(pd => pd.PatientId);

            modelBuilder.Entity<DiagnosisPatient>()
                .HasOne(pd => pd.Diagnosis)
                .WithMany(d => d.DiagnosisPatient)
                .HasForeignKey(pd => pd.DiagnosisId);
            #endregion

            #region Prescription Screening
            modelBuilder.Entity<LaboratoryScreeningPrescription>()
                .HasKey(lp => new { lp.PrescriptionId, lp.LaboratoryScreeningId });


            modelBuilder.Entity<LaboratoryScreeningPrescription>()
                .HasOne(lp => lp.Prescription)
                .WithMany(p => p.LaboratoryScreeningPrescriptions)
                .HasForeignKey(lp => lp.PrescriptionId);

            modelBuilder.Entity<LaboratoryScreeningPrescription>()
                .HasOne(lp => lp.LaboratoryScreening)
                .WithMany(ls => ls.LaboratoryScreeningPrescriptions)
                .HasForeignKey(lp => lp.LaboratoryScreeningId);
            #endregion

        }


        public DbSet<PrescriptionMedicine> PrescriptionMedicines { set; get; }
        public DbSet<LaboratoryScreeningPrescription> LaboratoryScreeningPrescription { set; get; }
        public DbSet<DiagnosisPatient> DiagnosisPatient { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Stuff> Staffs { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<LaboratoryScreening> LaboratoryScreenings { get; set; }
        
    }
}
