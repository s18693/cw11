using System;
using System.Collections.Generic;

namespace EF.Models
{
    public partial class PrescriptionMedicament
    {
        public int IdMedicament { get; set; }
        public int IdPrescription { get; set; }
        public int Dose { get; set; }
        public string Details { get; set; }

        public virtual Medicament medicament { get; set; }
        public virtual Prescription prescription { get; set; }
    }
}
