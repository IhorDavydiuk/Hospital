using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class PatientController : Controller
    {
        private HospitalContext dbContext = new HospitalContext();
        public ActionResult Index(int id)
        {
            Patient patient = dbContext.Patients.Find(id);
            ViewBag.Doctors = patient.Doctors.ToList();
            return View(patient);
        }
        
        public ActionResult DetailsPatient(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = dbContext.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = dbContext.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(patient).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Index",new { id = patient.Id });
            }
            return View(patient);
        }
    }
}