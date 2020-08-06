using Microsoft.AspNetCore.Mvc;
using DoctorOffice.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoctorOffice.Controllers
{
  public class DoctorsController : Controller
  {
    private readonly DoctorOfficeContext _db;

    public  DoctorsController(DoctorOfficeContext db)
    {
      _db = db;
    } 

    public ActionResult Index()
    {
      return View(_db.Doctors.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    // [HttpPost]
    // public ActionResult Create(Doctor doctor, int PatientId)
    // {
    //   _db.Doctors.Add(doctor);
    //   if(PatientId != 0)
    //   {
    //     _db.DoctorPatient.Add(new DoctorPatient() {PatientId = PatientId, DoctorId = doctorId.DoctorId});
    //   }
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }

    public ActionResult Details(int id)
    {
      Doctor thisDoctor = _db.Doctors
        .Include(doctor => doctor.Patients)
        .ThenInclude(join => join.Patient)
        .FirstOrDefault(doctors => doctors.DoctorId == id);
        return View(thisDoctor);
    }

    public ActionResult Edit(int id)
    {
      var thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
      return View(thisDoctor);
    }

    [HttpPost]
    public ActionResult Edit(Doctor doctor)
    {
      _db.Entry(doctor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult AddPatient(int id)
    {
      var thisDoctor = _db.Doctors.FirstOrDefault(doctors => doctors.DoctorId == id);
      ViewBag.PatientId = new SelectList(_db.Patients, "PatientId", "Name");
      return View(thisDoctor);
    }
    [HttpPost]
    public ActionResult AddPatient(Doctor doctor, int PatientId)
    {
      if(PatientId != 0)
      {
        _db.DoctorPatient.Add(new DoctorPatient() {PatientId = PatientId, DoctorId = doctor.DoctorId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisDoctor = _db.Doctors.FirstOrDefault(doctors => doctors.DoctorId == id);
      return View(thisDoctor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisDoctor = _db.Doctors.FirstOrDefault(doctors => doctors.DoctorId == id);
      _db.Doctors.Remove(thisDoctor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    //  [HttpPost]
    // public ActionResult DeletePatient(int joinId)
    // {
    //   var joinEntry = _db.FirstOrDefault(entry => entry.DoctorPatientId ==joinId);
    //   _db.DoctorPatient.Remove(joinEntry);
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }
  }
} 