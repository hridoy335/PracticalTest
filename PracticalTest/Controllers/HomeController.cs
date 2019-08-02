using System;
using System.Collections.Generic;
using System.Linq;
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
                    tblEmployee.DegId =Convert.ToInt32(DegId);
                    db.TblEmployees.Add(tblEmployee);
                    db.SaveChanges();
                    ViewBag.Employee = "Employee Registration Successfully";
                    return RedirectToAction("Index","Home");
                }

            }

            ViewBag.DegId = new SelectList(db.TblDesignations, "DegId", "DesignationName");
            return View();
        }

        [HttpGet]
        public ActionResult Designation()
        {
            return View();
        }
        public ActionResult Designation( TblDesignation tblDesignation)
        {
            if (ModelState.IsValid)
            {
                db.TblDesignations.Add(tblDesignation);
                db.SaveChanges();
                return RedirectToAction("DesignationAdd");
            }
            return RedirectToAction("Index","Home");
          
        }

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