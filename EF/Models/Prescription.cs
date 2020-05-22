using System;
using System.Collections.Generic;

namespace EF.Models
{
    public partial class Prescription
    {
        public Prescription()
        {
            prescriptionMedicament = new HashSet<PrescriptionMedicament>();
        }

        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }

        public virtual Doctor doctor { get; set; }
        public virtual Patient patient{ get; set; }
        public virtual ICollection<PrescriptionMedicament> prescriptionMedicament { get; set; }
    }
}
