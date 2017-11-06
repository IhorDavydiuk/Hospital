using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class HospitalController : Controller
    {
        HospitalContext db = new HospitalContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(InputData data)
        {
            if (ModelState.IsValid)
            {
                int idPerson = data.GetId();
                if (data.IsDoctor)
                {
                    return RedirectToAction("Index", "Doctor", new { Id = idPerson });
                }
                else
                {
                    return RedirectToAction("Index", "Patient", new { Id = idPerson });
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult SignUpDoctor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUpDoctor([Bind(Exclude ="Id,IsDoctor")]Doctor doctor)
        {
            doctor.IsDoctor = true;
            if (ModelState.IsValid)
            {
                db.Doctors.Add(doctor);
                db.SaveChanges();
                return View("Index");
            }
            return View(doctor);
        }
       
        [HttpGet]
        public ActionResult ViewAllDoctors()
        {
            List<Doctor> allDoctors = db.Doctors.ToList();
            return View(allDoctors);
        }

        [HttpPost]
        public ActionResult ViewDoctorsByName(string doctorName)
        {
            if (doctorName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Doctor> doctors = db.Doctors.Where(doc => doc.Name == doctorName).ToList();
            if (doctors == null)
            {
                return HttpNotFound();
            }
            
            return View(doctors);
        }
    }
}