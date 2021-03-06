﻿using System;
using System.Collections.Generic;

namespace Wyklad10.Models
{
    public partial class Medicament
    {
        public Medicament()
        {
            prescriptionMedicament = new HashSet<PrescriptionMedicament>();
        }

        public int IdMedicament { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public virtual ICollection<PrescriptionMedicament> prescriptionMedicament { get; set; }
    }
}
