using Microsoft.EntityFrameworkCore;


namespace Wyklad10.Models
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {

        }

        public virtual DbSet<Doctor> doctor { get; set; }
        public virtual DbSet<Patient> patient { get; set; }
        public virtual DbSet<Prescription> prescription { get; set; }
        public virtual DbSet<PrescriptionMedicament> prescriptionMedicament { get; set; }
        public virtual DbSet<Medicament> medicament { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>(e =>
            {
                e.HasKey(x => x.IdDoctor).HasName("Doctor_PK");
                //.ValueGeneratedNever(); aby nie było indetity
                //Nie podajac dalej nic wszystkie parametry moga byc null i maja MAX value
                e.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
                e.Property(x => x.LastName).HasMaxLength(100).IsRequired();
                e.Property(x => x.Email).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Patient>(e =>
            {
                e.HasKey(x => x.IdPatient).HasName("Patient_PK");
                e.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
                e.Property(x => x.LastName).HasMaxLength(100).IsRequired();
                e.Property(x => x.Birthdate).IsRequired();
            });

            modelBuilder.Entity<Prescription>(e =>
            {
                e.HasKey(x => x.IdPrescription).HasName("Prescription_PK");
                e.Property(x => x.Date).IsRequired();
                e.Property(x => x.DueDate).IsRequired();


                e.HasOne(x => x.patient)
               .WithMany(x => x.Prescription)
               .HasForeignKey(x => x.IdPatient)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("Prescription_Patient");

                e.HasOne(x => x.doctor)
               .WithMany(x => x.Prescription)
               .HasForeignKey(x => x.IdDoctor)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("Prescription_Doctor");
            });

            modelBuilder.Entity<Medicament>(e =>
            {
                e.HasKey(x => x.IdMedicament).HasName("Medicament_PK");
                e.Property(x => x.Name).HasMaxLength(100).IsRequired();
                e.Property(x => x.Description).HasMaxLength(100).IsRequired();
                e.Property(x => x.Type).HasMaxLength(100).IsRequired();

            });

            modelBuilder.Entity<PrescriptionMedicament>(e =>
            {
                e.HasKey(e => new { e.IdMedicament, e.IdPrescription })
                        .HasName("Prescription_Medicament_PK");
                //Dose is null nie trzeba pisac
                e.Property(x => x.Details).HasMaxLength(100).IsRequired();

                e.HasOne(x => x.medicament)
               .WithMany(x => x.prescriptionMedicament)
               .HasForeignKey(x => x.IdMedicament)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("Prescription_Medicament_Medicament");

                e.HasOne(x => x.prescription)
               .WithMany(x => x.prescriptionMedicament)
               .HasForeignKey(x => x.IdPrescription)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("Prescription_Medicament_Prescription");

            });
            //base.OnModelCreating(modelBuilder);
        }
    }
}
