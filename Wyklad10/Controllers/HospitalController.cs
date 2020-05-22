using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wyklad10.Models;

namespace Wyklad10.Controllers
{
    [Route("hospital")]
    [ApiController]
    public class HospitalController : ControllerBase
    {

        private readonly HospitalContext context;

        public HospitalController(HospitalContext hospital)
        {
            context = hospital;

            //Add datebase on start
            context.doctor.Add(new Doctor { IdDoctor = 1, FirstName = "Trudne", LastName = "Sprawy", Email = "SprawaJestTrudna@tvp.pl"});
            context.doctor.Add(new Doctor { IdDoctor = 2, FirstName = "Dlaczego", LastName = "Ja", Email = "ZnowuOna@tvn.pl"});
            context.patient.Add(new Patient { IdPatient = 1, FirstName = "Moda", LastName = "Nasukces", Birthdate = new DateTime(1989,9,10) });
            context.patient.Add(new Patient { IdPatient = 1, FirstName = "Mjak", LastName = "Milosc", Birthdate = new DateTime(1999,1,5) });
            context.medicament.Add(new Medicament { IdMedicament = 1, Name = "Prostamol", Description = "Zyj dlugo i szczesliwie", Type = "Tabletki"});
            context.medicament.Add(new Medicament { IdMedicament = 2, Name = "Partekol", Description = "Zadbaj o siebie", Type = "Syrop"});
            context.prescription.Add(new Prescription { IdPrescription = 1,Date = new DateTime(2020,1,1), DueDate = new DateTime(2020,2,2), IdPatient = 1, IdDoctor = 2});
            context.prescription.Add(new Prescription { IdPrescription = 2,Date = new DateTime(2020,3,5), DueDate = new DateTime(2020,4,8), IdPatient = 2, IdDoctor = 1});
            context.prescriptionMedicament.Add(new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 2, Details = "Po zuzyciu leku wywal opakowanie do worka na papier"});
            context.prescriptionMedicament.Add(new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 1, Dose = 10 , Details = "Nie wypij wszystkiego pierwszego dnia"});
            context.SaveChanges();
        }

        //Get doctor
        [HttpGet("doctor/{id}")]
        public IActionResult getDoctor(int? id)
        {
            if (id == null)
                return NotFound();

            Doctor d = context.doctor.Find(id);

            if (d == null)
                return NotFound();

            return Ok(d);
        }

        //Update doctor
        [HttpPost("doctor/up")]
        public IActionResult updateDoctor(Doctor doctor)
        {
            Doctor d = context.doctor.Find(doctor.IdDoctor);
            if (d == null)
                return NotFound("Doctor o takim id nieistnieje");
            context.Database.BeginTransaction();
            context.Entry(d).CurrentValues.SetValues(doctor);
            context.SaveChanges();
            context.Database.CommitTransaction();
            return Ok(doctor);
        }

        //Add doctor
        [HttpPost("doctor/add")]
        public IActionResult addDoctor(Doctor doctor)
        {
            context.Database.BeginTransaction();
            context.doctor.Add(doctor);
            context.SaveChanges();
            context.Database.CommitTransaction();
            return Created("Add new",doctor);
        }

        //Delete doctor
        [HttpDelete("student/{id}")]
        public IActionResult deleteDoctor(int? id)
        {
            if (id == null)
                return NotFound();
            Doctor d = context.doctor.Find(id);
            if (d == null)
                return NotFound("Doctor o takim id nieistnieje");
            context.Database.BeginTransaction();
            context.doctor.Remove(d);
            context.SaveChanges();
            context.Database.CommitTransaction();
            return Ok();
        }

    }
}
