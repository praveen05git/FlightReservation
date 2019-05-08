using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using SandMax1.Models;

namespace SandMax1.Controllers
{
    public class tblCustomer_HMS03_Team7Controller : Controller
    {
        private DB09AO114_1718Entities1 db = new DB09AO114_1718Entities1();

        [Authorize]
        public ActionResult Index()
        {
            var tblCustomer_HMS03_Team7 = db.tblCustomer_HMS03_Team7.Include(t => t.tblLogin_HMS03_Team7);
            return View(tblCustomer_HMS03_Team7.ToList());
        }
        public ActionResult all()
        {
            var tblCustomer_HMS03_Team7 = db.tblCustomer_HMS03_Team7.Include(t => t.tblLogin_HMS03_Team7);
            return View(tblCustomer_HMS03_Team7.ToList());
        }
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCustomer_HMS03_Team7 tblCustomer_HMS03_Team7 = db.tblCustomer_HMS03_Team7.Find(id);
            if (tblCustomer_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            return View(tblCustomer_HMS03_Team7);
        }

        // GET: tblCustomer_HMS03_Team7/Create
        public ActionResult Create()
        {
            ViewBag.LoginID = new SelectList(db.tblLogin_HMS03_Team7, "LoginID", "uname");
            return View();
        }

        public ActionResult testing()
        {
         
            return View();
        }

        // POST: tblCustomer_HMS03_Team7/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,LoginID,Title,FirstName,LastName,DateOfBirth,Gender,StreetAddress,City,State,ZipCode,Nationality,MobileNumber,AlternateNumber,PhoneNumber,Email,CompanyName,OfficeAddress,BonusMilePoints")] tblCustomer_HMS03_Team7 tblCustomer_HMS03_Team7)
        {
            if (ModelState.IsValid)
            {
                tblCustomer_HMS03_Team7.LoginID =int.Parse (Session["newID"].ToString());
                tblCustomer_HMS03_Team7.Nationality = "Indian";
                db.tblCustomer_HMS03_Team7.Add(tblCustomer_HMS03_Team7);
                db.SaveChanges();
                Session["custid"] = tblCustomer_HMS03_Team7.CustomerID;
                return RedirectToAction("Log","tblLogin_HMS03_Team7");
            }

            ViewBag.LoginID = new SelectList(db.tblLogin_HMS03_Team7, "LoginID", "uname", tblCustomer_HMS03_Team7.LoginID);
            return View(tblCustomer_HMS03_Team7);
        }

        public ActionResult credit()
        {
            
            return View();
        }

        // POST: tblCustomer_HMS03_Team7/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult credit([Bind(Include = "CustomerID,LoginID,Title,FirstName,LastName,DateOfBirth,Gender,StreetAddress,City,State,ZipCode,Nationality,MobileNumber,AlternateNumber,PhoneNumber,Email,CompanyName,OfficeAddress,BonusMilePoints")] tblCustomer_HMS03_Team7 tblCustomer_HMS03_Team7)
        {
            //if (ModelState.IsValid)
            //{
                tblLogin_HMS03_Team7 lg = new tblLogin_HMS03_Team7();
                lg.uname = Request["username"];
                lg.pwd = Request["password"];
                lg.roles = "Customer";
                db.tblLogin_HMS03_Team7.Add(lg);
                db.SaveChanges();
                MessageBox.Show("Proceeding to Register");
                Session["newID"] = lg.LoginID;
             
                return RedirectToAction("Create", "tblCustomer_HMS03_Team7");
            //}

            //ViewBag.LoginID = new SelectList(db.tblLogin_HMS03_Team7, "LoginID", "uname", tblCustomer_HMS03_Team7.LoginID);
            //return RedirectToAction("Create", "tblCustomer_HMS03_Team7");
        }


        [Authorize]
        public ActionResult Edit(int? id)
        { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCustomer_HMS03_Team7 tblCustomer_HMS03_Team7 = db.tblCustomer_HMS03_Team7.Find(id);
            if (tblCustomer_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoginID = new SelectList(db.tblLogin_HMS03_Team7, "LoginID", "uname", tblCustomer_HMS03_Team7.LoginID);
            return View(tblCustomer_HMS03_Team7);
        }

        // POST: tblCustomer_HMS03_Team7/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,LoginID,Title,FirstName,LastName,DateOfBirth,Gender,StreetAddress,City,State,ZipCode,Nationality,MobileNumber,AlternateNumber,PhoneNumber,Email,CompanyName,OfficeAddress,BonusMilePoints")] tblCustomer_HMS03_Team7 tblCustomer_HMS03_Team7)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCustomer_HMS03_Team7).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoginID = new SelectList(db.tblLogin_HMS03_Team7, "LoginID", "uname", tblCustomer_HMS03_Team7.LoginID);
            return View(tblCustomer_HMS03_Team7);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCustomer_HMS03_Team7 tblCustomer_HMS03_Team7 = db.tblCustomer_HMS03_Team7.Find(id);
            if (tblCustomer_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            return View(tblCustomer_HMS03_Team7);
        }

        // POST: tblCustomer_HMS03_Team7/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCustomer_HMS03_Team7 tblCustomer_HMS03_Team7 = db.tblCustomer_HMS03_Team7.Find(id);
            db.tblCustomer_HMS03_Team7.Remove(tblCustomer_HMS03_Team7);
            db.SaveChanges();
            return RedirectToAction("all");
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
