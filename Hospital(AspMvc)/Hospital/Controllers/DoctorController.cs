using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hospital.Models;

namespace Hospital.Controllers
{
    public class DoctorController : Controller
    {
        private HospitalContext db = new HospitalContext();

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doctor = db.Doctors.Find(id);
            ViewBag.Arrived = doctor.Patients.SelectMany(pat => pat.Sickness).ToList().Count(sickn => sickn.Status == StateOfHealth.Arrived).ToString();
            ViewBag.Sick = doctor.Patients.SelectMany(pat => pat.Sickness).ToList().Count(sickn => sickn.Status == StateOfHealth.Sick).ToString();
            ViewBag.Healthy = doctor.Patients.SelectMany(pat => pat.Sickness).ToList().Count(sickn => sickn.Status == StateOfHealth.Healthy).ToString();
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        [HttpPost]
        public ActionResult FindAPatient(string patientName,int id)
        {

            if (patientName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Patient> patient = db.Doctors.First(w=>w.Id == id).Patients.Where(pat => pat.Name == patientName).ToList();
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewData["IdDoc"] = id;
            return View(patient);
        }
       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }
        
        public ActionResult CreateDiseaseRecord(int? id)
        {
            ViewBag.DoctorId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDiseaseRecord(DiseaseRecord diseaseRecord)
        {
            if (ModelState.IsValid)
            {
                if (diseaseRecord.IsOutpatientCatd())
                {
                    Patient patient = db.Patients.First(pat => pat.OutpatientCard == diseaseRecord.OutpatientCard);
                    patient.Sickness = new List<DiseaseRecord>() { diseaseRecord };
                    return addDiseaseRecord(patient, diseaseRecord);
                }
                else
                {
                    Patient patient = new Patient { OutpatientCard = diseaseRecord.OutpatientCard, Login = diseaseRecord.OutpatientCard, Password = diseaseRecord.OutpatientCard, Name = diseaseRecord.Name };
                    patient.Sickness = new List<DiseaseRecord>() { diseaseRecord };
                    db.Patients.Add(patient);
                    return addDiseaseRecord(patient, diseaseRecord);
                }
            }
            return View(new { id = diseaseRecord.DoctorId });
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }

            return View(doctor);
        }
       
        [HttpPost]
        public ActionResult Edit(Doctor doctor)
        {
            ModelState.Remove("Login");
            if (ModelState.IsValid)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = doctor.Id });
            }
            return View(doctor);
        }
        private ActionResult addDiseaseRecord(Patient patient, DiseaseRecord diseaseRecord)
        {
            db.BurnDiseases.Add(diseaseRecord);
            db.Doctors.Find(diseaseRecord.DoctorId).Patients = new List<Patient>() { patient };
            db.SaveChanges();
            return RedirectToAction("Index", new { id = diseaseRecord.DoctorId });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
