using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SandMax1.Models;
using System.Windows.Forms;

namespace SandMax1.Controllers
{
    public class tblSearch_HMS03_Team7Controller : Controller
    {
        private DB09AO114_1718Entities1 db = new DB09AO114_1718Entities1();

       
        [Authorize]
        public ActionResult Index()
        {
            return View(db.tblSearch_HMS03_Team7.ToList());
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSearch_HMS03_Team7 tblSearch_HMS03_Team7 = db.tblSearch_HMS03_Team7.Find(id);
            if (tblSearch_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            return View(tblSearch_HMS03_Team7);
        }

       
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SearchID,Source,Destination,Departure,Class,NoOfPassengers_Adult,NoOfPassengers_Child")] tblSearch_HMS03_Team7 tblSearch_HMS03_Team7)
        {
            if (ModelState.IsValid)
            {
                if (tblSearch_HMS03_Team7.Source.Equals(tblSearch_HMS03_Team7.Destination))
                {
                    MessageBox.Show("Source and destination can't be same");
                     return RedirectToAction("Create");
                }

                if (DateTime.Parse(tblSearch_HMS03_Team7.Departure.ToString()).Subtract(DateTime.Now).Days<0)
                {
                    MessageBox.Show("Departure should be in FUTURE");
                    return RedirectToAction("Create");
                }
                Session["SerID"] = tblSearch_HMS03_Team7.SearchID;
                Session["Source"] = tblSearch_HMS03_Team7.Source;
                Session["Dest"] = tblSearch_HMS03_Team7.Destination;
                Session["Depr"] = DateTime.Parse(tblSearch_HMS03_Team7.Departure.ToString());
                Session["Class"] = tblSearch_HMS03_Team7.Class;
               
                try
                {
                    int ad = int.Parse(Request["noofadult"].ToString());
                    Session["NOofADULT"] = int.Parse(Request["noofadult"].ToString());
                    int ch = int.Parse(Request["noofchild"].ToString());
                    Session["NOofCHILD"] = int.Parse(Request["noofchild"].ToString());
                }
                catch
                {
                    MessageBox.Show("Sorry! you have entered invalid format");
                }
                db.tblSearch_HMS03_Team7.Add(tblSearch_HMS03_Team7);
                db.SaveChanges();
                return RedirectToAction("VeiwScheduleToCustomer","tblSchedule_HMS03_Team7");
            }

            return View(tblSearch_HMS03_Team7);
        }



        [Authorize]
        public ActionResult Edit(int? id)
        { 

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSearch_HMS03_Team7 tblSearch_HMS03_Team7 = db.tblSearch_HMS03_Team7.Find(id);
            if (tblSearch_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            return View(tblSearch_HMS03_Team7);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SearchID,Source,Destination,Departure,Class,NoOfPassengers_Adult,NoOfPassengers_Child")] tblSearch_HMS03_Team7 tblSearch_HMS03_Team7)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblSearch_HMS03_Team7).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblSearch_HMS03_Team7);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSearch_HMS03_Team7 tblSearch_HMS03_Team7 = db.tblSearch_HMS03_Team7.Find(id);
            if (tblSearch_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            return View(tblSearch_HMS03_Team7);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblSearch_HMS03_Team7 tblSearch_HMS03_Team7 = db.tblSearch_HMS03_Team7.Find(id);
            db.tblSearch_HMS03_Team7.Remove(tblSearch_HMS03_Team7);
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
