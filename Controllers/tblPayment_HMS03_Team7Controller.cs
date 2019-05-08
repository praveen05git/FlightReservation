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
    public class tblPayment_HMS03_Team7Controller : Controller
    {
        private DB09AO114_1718Entities1 db = new DB09AO114_1718Entities1();

        [Authorize]
        public ActionResult Index()
        {
            var tblPayment_HMS03_Team7 = db.tblPayment_HMS03_Team7.Include(t => t.tblCustomer_HMS03_Team7).Include(t => t.tblJourney_HMS03_Team7).Include(t => t.tblSchedule_HMS03_Team7);
            return View(tblPayment_HMS03_Team7.ToList());
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPayment_HMS03_Team7 tblPayment_HMS03_Team7 = db.tblPayment_HMS03_Team7.Find(id);
            if (tblPayment_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            return View(tblPayment_HMS03_Team7);
        }

        [Authorize]
        public ActionResult Create()
        {
            
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionID,JourneyID,CardType,CardNumber,CVV,ExpiryDate,PaymentDate,PaymentType,CustomerID,ScheduleID")] tblPayment_HMS03_Team7 tblPayment_HMS03_Team7)
        {
            if (ModelState.IsValid)
            {
                //if (DateTime.Parse(tblPayment_HMS03_Team7.PaymentDate.ToString()).Subtract(DateTime.Now).TotalDays < 0)
                if(tblPayment_HMS03_Team7.ExpiryDate < DateTime.Now)
                {
                    MessageBox.Show("Expire Date should be in FUTURE");
                    return RedirectToAction("Create");
                }
                tblPayment_HMS03_Team7.JourneyID = int.Parse(Session["JourneyID"].ToString());
                tblPayment_HMS03_Team7.CustomerID = int.Parse(Session["cid"].ToString());//int.Parse(Session["custid"].ToString());
                tblPayment_HMS03_Team7.PaymentDate = DateTime.Now;
                tblPayment_HMS03_Team7.PaymentType = "Successful";
                //
                tblJourney_HMS03_Team7 tj = db.tblJourney_HMS03_Team7.Find(int.Parse(Session["JourneyID"].ToString()));
                tj.PaymentStatus = "Successful";
                //
                db.tblPayment_HMS03_Team7.Add(tblPayment_HMS03_Team7);
                db.SaveChanges();
                Session["TransactionID"] = tblPayment_HMS03_Team7.TransactionID;
                return RedirectToAction("changeseat","tblSchedule_HMS03_Team7");
            }

            ViewBag.CustomerID = new SelectList(db.tblCustomer_HMS03_Team7, "CustomerID", "Title", tblPayment_HMS03_Team7.CustomerID);
            ViewBag.JourneyID = new SelectList(db.tblJourney_HMS03_Team7, "JourneyID", "PaymentStatus", tblPayment_HMS03_Team7.JourneyID);
            ViewBag.ScheduleID = new SelectList(db.tblSchedule_HMS03_Team7, "ScheduleID", "Source", tblPayment_HMS03_Team7.ScheduleID);
            return View(tblPayment_HMS03_Team7);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPayment_HMS03_Team7 tblPayment_HMS03_Team7 = db.tblPayment_HMS03_Team7.Find(id);
            if (tblPayment_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.tblCustomer_HMS03_Team7, "CustomerID", "Title", tblPayment_HMS03_Team7.CustomerID);
            ViewBag.JourneyID = new SelectList(db.tblJourney_HMS03_Team7, "JourneyID", "PaymentStatus", tblPayment_HMS03_Team7.JourneyID);
            ViewBag.ScheduleID = new SelectList(db.tblSchedule_HMS03_Team7, "ScheduleID", "Source", tblPayment_HMS03_Team7.ScheduleID);
            return View(tblPayment_HMS03_Team7);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionID,JourneyID,CardType,CardNumber,CVV,ExpiryDate,PaymentDate,PaymentType,CustomerID,ScheduleID")] tblPayment_HMS03_Team7 tblPayment_HMS03_Team7)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblPayment_HMS03_Team7).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Viewpassenger");
            }
            ViewBag.CustomerID = new SelectList(db.tblCustomer_HMS03_Team7, "CustomerID", "Title", tblPayment_HMS03_Team7.CustomerID);
            ViewBag.JourneyID = new SelectList(db.tblJourney_HMS03_Team7, "JourneyID", "PaymentStatus", tblPayment_HMS03_Team7.JourneyID);
            ViewBag.ScheduleID = new SelectList(db.tblSchedule_HMS03_Team7, "ScheduleID", "Source", tblPayment_HMS03_Team7.ScheduleID);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPayment_HMS03_Team7 tblPayment_HMS03_Team7 = db.tblPayment_HMS03_Team7.Find(id);
            if (tblPayment_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            return View(tblPayment_HMS03_Team7);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblPayment_HMS03_Team7 tblPayment_HMS03_Team7 = db.tblPayment_HMS03_Team7.Find(id);
            db.tblPayment_HMS03_Team7.Remove(tblPayment_HMS03_Team7);
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
