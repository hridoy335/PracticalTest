using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PracticalTest.Models;

namespace PracticalTest.Controllers
{
    public class HomeController : Controller
    {
        TestEntities db = new TestEntities();
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Employee insert form
        /// </summary>
        [HttpGet]
        public ActionResult Employee()  
        {
            ViewBag.DegId = new SelectList(db.TblDesignations, "DegId", "DesignationName");
            return View();
        }

        [HttpPost]
        public ActionResult Employee(TblEmployee tblEmployee, int? DegId,  string Employee_Password)
        {
            int er = 0;
            if (DegId == null)
            {
                er++;
                ViewBag.Designation = "Select One Item";
            }
            if (Employee_Password == "")
            {
                er++;
                ViewBag.password = "Password Required";
            }

            bool isThisIdExist = db.TblEmployees.ToList().Exists(a => a.EmployeeContactNo == tblEmployee.EmployeeContactNo);
            if (isThisIdExist)
            {
                er++;
                ViewBag.idnumber = "This cotact number is already here";
            }
            if (er > 0)
            {
                ViewBag.DegId = new SelectList(db.TblDesignations, "DegId", "DesignationName");
                return View();
            }
            else
            {

                if (ModelState.IsValid)
                {
                    tblEmployee.MakeDate = DateTime.Now;
                    tblEmployee.DegId =Convert.ToInt32(DegId);
                    db.TblEmployees.Add(tblEmployee);
                    db.SaveChanges();
                    ViewBag.Employee = "Employee Registration Successfully";
                    return RedirectToAction("EmployeeView", "Home");
                }

            }

            ViewBag.DegId = new SelectList(db.TblDesignations, "DegId", "DesignationName");
            return View();
        }

        /// <summary>
        /// Employee information view
        /// </summary>
        public ActionResult EmployeeView()
        {
            return View(db.TblEmployees.ToList());
        }

        //employee information update
        [HttpGet]
        public ActionResult EmpUpdate(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //TblDesignation tblDesignation = db.TblDesignations.Find(id);
            TblEmployee tblEmployee = db.TblEmployees.Find(id);
            if (tblEmployee == null)
            {
                return HttpNotFound();
            }
            return View(tblEmployee);
        }


        // Employee information delete
        [HttpGet]
        public ActionResult EmpDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        
            TblEmployee tblEmployee = db.TblEmployees.Find(id);
            if (tblEmployee == null)
            {
                return HttpNotFound();
            }
            return View(tblEmployee);
        }

        [HttpPost]
        public ActionResult EmpDelete(int id)
        {
            TblEmployee tblEmployee = db.TblEmployees.Find(id);
            db.TblEmployees.Remove(tblEmployee);
            db.SaveChanges();
            return RedirectToAction("EmployeeView","Home");
        }

        //Insert designation form
        [HttpGet]
        public ActionResult Designation()
        {
            return View();
        }
        public ActionResult Designation( TblDesignation tblDesignation)
        {
            if (ModelState.IsValid)
            {
                tblDesignation.MakeDate = DateTime.Now;
                db.TblDesignations.Add(tblDesignation);
                db.SaveChanges();
                return RedirectToAction("DesignationAdd");
            }
            return RedirectToAction("Index","Home");
          
        }

        // Designation information view 
        [HttpGet]
        public ActionResult Designationview()
        {
            return View(db.TblDesignations.ToList());
        }

        // update designation information 
        public ActionResult Update(int? id, DateTime? date)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
      

            TblDesignation tblDesignation = db.TblDesignations.Find(id);
            if (tblDesignation == null)
            {
                return HttpNotFound();
            }
            return View(tblDesignation);
        }

        [HttpPost]

        public ActionResult Update(DateTime? date, TblDesignation tblDesignation)
        {

            if (ModelState.IsValid)
            {
                tblDesignation.MakeDate =Convert.ToDateTime(date);
                db.Entry(tblDesignation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Designationview");
            }
            return View(tblDesignation);
        }

        // login page
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TblEmployee tblEmployee)
        {
            string contact = tblEmployee.EmployeeContactNo;
            string password = tblEmployee.Password;
            int er = 0;
            if (contact == "")
            {
                er++;
                ViewBag.username = "Username required";
            }
            if (password == "")
            {
                er++;
                ViewBag.password = "Password required";
            }
            if (er > 0)
            {
                return View();
            }
            var Login = db.TblEmployees.Where(x => x.EmployeeContactNo == contact && x.Password == password).FirstOrDefault();
            if (Login == null)
            {

                ViewBag.message = "Login fail";
                return View("Login");
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }


    }
}