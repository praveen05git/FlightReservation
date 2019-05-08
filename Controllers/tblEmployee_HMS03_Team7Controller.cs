using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SandMax1.Models;

namespace SandMax1.Controllers
{
    public class tblEmployee_HMS03_Team7Controller : Controller
    {
        private DB09AO114_1718Entities1 db = new DB09AO114_1718Entities1();

        // GET: tblEmployee_HMS03_Team7
        public ActionResult Index()
        {
            var tblEmployee_HMS03_Team7 = db.tblEmployee_HMS03_Team7.Include(t => t.tblLogin_HMS03_Team7);
            return View(tblEmployee_HMS03_Team7.ToList());
        }

        // GET: tblEmployee_HMS03_Team7/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployee_HMS03_Team7 tblEmployee_HMS03_Team7 = db.tblEmployee_HMS03_Team7.Find(id);
            if (tblEmployee_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            return View(tblEmployee_HMS03_Team7);
        }

        // GET: tblEmployee_HMS03_Team7/Create
        public ActionResult Create()
        {
            ViewBag.LoginID = new SelectList(db.tblLogin_HMS03_Team7, "LoginID", "uname");
            return View();
        }

        // POST: tblEmployee_HMS03_Team7/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,LoginID,Employee_Name,Gender,DateofBirth,Age,Designation,Airlines,Work_location,Contact_Number,EmailID,Address")] tblEmployee_HMS03_Team7 tblEmployee_HMS03_Team7)
        {
            if (ModelState.IsValid)
            {
                db.tblEmployee_HMS03_Team7.Add(tblEmployee_HMS03_Team7);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoginID = new SelectList(db.tblLogin_HMS03_Team7, "LoginID", "uname", tblEmployee_HMS03_Team7.LoginID);
            return View(tblEmployee_HMS03_Team7);
        }

        // GET: tblEmployee_HMS03_Team7/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployee_HMS03_Team7 tblEmployee_HMS03_Team7 = db.tblEmployee_HMS03_Team7.Find(id);
            if (tblEmployee_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoginID = new SelectList(db.tblLogin_HMS03_Team7, "LoginID", "uname", tblEmployee_HMS03_Team7.LoginID);
            return View(tblEmployee_HMS03_Team7);
        }

        // POST: tblEmployee_HMS03_Team7/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,LoginID,Employee_Name,Gender,DateofBirth,Age,Designation,Airlines,Work_location,Contact_Number,EmailID,Address")] tblEmployee_HMS03_Team7 tblEmployee_HMS03_Team7)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEmployee_HMS03_Team7).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoginID = new SelectList(db.tblLogin_HMS03_Team7, "LoginID", "uname", tblEmployee_HMS03_Team7.LoginID);
            return View(tblEmployee_HMS03_Team7);
        }

        // GET: tblEmployee_HMS03_Team7/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployee_HMS03_Team7 tblEmployee_HMS03_Team7 = db.tblEmployee_HMS03_Team7.Find(id);
            if (tblEmployee_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            return View(tblEmployee_HMS03_Team7);
        }

        // POST: tblEmployee_HMS03_Team7/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEmployee_HMS03_Team7 tblEmployee_HMS03_Team7 = db.tblEmployee_HMS03_Team7.Find(id);
            db.tblEmployee_HMS03_Team7.Remove(tblEmployee_HMS03_Team7);
            db.SaveChanges();
            return RedirectToAction("Index");
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
