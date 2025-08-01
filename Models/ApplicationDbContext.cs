﻿
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

            // the relation between patient and insurances.
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.insurances)
                .WithOne(i => i.Patient)
                .HasForeignKey(i => i.PatientId);

            // the relation between patient and bills.  
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Bills)
                .WithOne(b => b.Patient)
                .HasForeignKey(b => b.PatientId);

            // the relation between patient and medical history.
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.MedicalHistory)
                .WithOne(m => m.Patient)
                .HasForeignKey(m => m.PatientId);

            // the relation between patient and diagnoses.
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Diagnoses)
                .WithMany(d => d.Patients);

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
                .WithOne(r => r.Patient);
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


            #endregion

            #region Nurse
            modelBuilder.Entity<Nurse>()
                .ToTable("Nurses");

            #endregion

            #region Stuff Relations
            // the relation between stuff and payroll.
            modelBuilder.Entity<Stuff>()
                .HasOne(s => s.Payroll)
                .WithOne(p => p.Staff);

            // the relation between stuff technician and laboratory screenings.
            modelBuilder.Entity<Stuff>()
                .HasMany(s => s.LaboratoryScreenings)
                .WithOne(l => l.Technician)
                .HasForeignKey(l => l.TechnicianSSN);
            #endregion

            #region Bill Relations
            // the relation between bill and insurance.
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Insurance)
                .WithMany(i => i.Bills)
                .HasForeignKey(b => b.PolicyNumber);

            // the relation between bill and room.
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bills)
                .HasForeignKey(b => b.RoomId);

            // the relation between bill and laboratory screenings.
            modelBuilder.Entity<Bill>()
                .HasMany(b => b.LaboratoryScreenings)
                .WithOne(l => l.Bill)
                .HasForeignKey(l => l.BillId);

            // the relation between bill and medicines.
            modelBuilder.Entity<Bill>()
                .HasMany(b => b.Medicine)
                .WithMany(m => m.Bills);

            #endregion

            #region Medicine Relations
            // the relation between medicine and prescriptions.
            modelBuilder.Entity<Medicine>()
                .HasMany(m=>m.Prescriptions)
                .WithMany(p => p.Medicines);
            #endregion

        }

        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Stuff> Staffs { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalHistory> MedicalRecords { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<LaboratoryScreening> LaboratoryScreenings { get; set; }
        
    }
}
