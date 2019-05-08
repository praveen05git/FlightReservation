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
    public class tblJourney_HMS03_Team7Controller : Controller
    {
        private DB09AO114_1718Entities1 db = new DB09AO114_1718Entities1();
        [Authorize]
        public ActionResult Index()
        {
            var tblJourney_HMS03_Team7 = db.tblJourney_HMS03_Team7.Include(t => t.tblCustomer_HMS03_Team7).Include(t => t.tblSchedule_HMS03_Team7);
            return View(tblJourney_HMS03_Team7.ToList());
        }

        [Authorize]
        public ActionResult JourneyHistory()
        {
            var tblJourney_HMS03_Team7 = db.tblJourney_HMS03_Team7.Include(t => t.tblCustomer_HMS03_Team7).Include(t => t.tblSchedule_HMS03_Team7);
            return View(tblJourney_HMS03_Team7.ToList());
        }

        [Authorize]
        public ActionResult PassengerHistory(int? id)
        {
            Session["JID"] = id;
            return RedirectToAction("PassengerHistory", "tblPassenger_HMS03_Team7");
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblJourney_HMS03_Team7 tblJourney_HMS03_Team7 = db.tblJourney_HMS03_Team7.Find(id);
            if (tblJourney_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            Session["JourneyID"] = id;
           
            return View(tblJourney_HMS03_Team7);
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.tblCustomer_HMS03_Team7, "CustomerID", "Title");
            ViewBag.ScheduleID = new SelectList(db.tblSchedule_HMS03_Team7, "ScheduleID", "Source");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JourneyID,CustomerID,ScheduleID,TotalBookingFare,NoOfPassengers_Adult,NoOfPassengers_Child,PaymentStatus,BonusRequstStatus,BookingDate,AdditionalBaggageCharge")] tblJourney_HMS03_Team7 tblJourney_HMS03_Team7)
        {
            if (ModelState.IsValid)
            {

                db.tblJourney_HMS03_Team7.Add(tblJourney_HMS03_Team7);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.tblCustomer_HMS03_Team7, "CustomerID", "Title", tblJourney_HMS03_Team7.CustomerID);
            ViewBag.ScheduleID = new SelectList(db.tblSchedule_HMS03_Team7, "ScheduleID", "Source", tblJourney_HMS03_Team7.ScheduleID);
            return View(tblJourney_HMS03_Team7);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblJourney_HMS03_Team7 tblJourney_HMS03_Team7 = db.tblJourney_HMS03_Team7.Find(id);
            if (tblJourney_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.tblCustomer_HMS03_Team7, "CustomerID", "Title", tblJourney_HMS03_Team7.CustomerID);
            ViewBag.ScheduleID = new SelectList(db.tblSchedule_HMS03_Team7, "ScheduleID", "Source", tblJourney_HMS03_Team7.ScheduleID);
            return View(tblJourney_HMS03_Team7);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JourneyID,CustomerID,ScheduleID,TotalBookingFare,NoOfPassengers_Adult,NoOfPassengers_Child,PaymentStatus,BonusRequstStatus,BookingDate,AdditionalBaggageCharge")] tblJourney_HMS03_Team7 tblJourney_HMS03_Team7)
        {
            if (ModelState.IsValid)
            {
                tblSchedule_HMS03_Team7 sch = db.tblSchedule_HMS03_Team7.Find(int.Parse(Session["ScheduleID"].ToString()));

                if (tblJourney_HMS03_Team7.BonusRequstStatus == "Premium")
                {
                    decimal? adultfare = sch.Fare_Premium_Adult;
                    decimal? childfare = sch.Fare_Premium_Child;

                    tblJourney_HMS03_Team7.TotalBookingFare = (tblJourney_HMS03_Team7.NoOfPassengers_Adult * adultfare) + (tblJourney_HMS03_Team7.NoOfPassengers_Child * childfare);

                }

                else if (tblJourney_HMS03_Team7.BonusRequstStatus == "FirstClass")
                {
                    decimal? adultfare = sch.Fare_Firstclass_Adult;
                    decimal? childfare = sch.Fare_Firstclass_Child;

                    tblJourney_HMS03_Team7.TotalBookingFare = (tblJourney_HMS03_Team7.NoOfPassengers_Adult * adultfare) + (tblJourney_HMS03_Team7.NoOfPassengers_Child * childfare);

                }

                else if (tblJourney_HMS03_Team7.BonusRequstStatus == "Economy")
                {
                    decimal? adultfare = sch.Fare_Economy_Adult;
                    decimal? childfare = sch.Fare_Economy_Child;

                    tblJourney_HMS03_Team7.TotalBookingFare = (tblJourney_HMS03_Team7.NoOfPassengers_Adult * adultfare) + (tblJourney_HMS03_Team7.NoOfPassengers_Child * childfare);

                }

                db.Entry(tblJourney_HMS03_Team7).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.tblCustomer_HMS03_Team7, "CustomerID", "Title", tblJourney_HMS03_Team7.CustomerID);
            ViewBag.ScheduleID = new SelectList(db.tblSchedule_HMS03_Team7, "ScheduleID", "Source", tblJourney_HMS03_Team7.ScheduleID);
            return View(tblJourney_HMS03_Team7);
        }
        //[Authorize]
        public ActionResult AddJourney()
        {
            tblSchedule_HMS03_Team7 sch = db.tblSchedule_HMS03_Team7.Find(int.Parse(Session["ScheduleID"].ToString()));
            tblJourney_HMS03_Team7 jr = new tblJourney_HMS03_Team7();
           

           

            jr.ScheduleID = sch.ScheduleID;
            jr.NoOfPassengers_Adult = int.Parse(Session["NOofADULT"].ToString());
            jr.NoOfPassengers_Child = int.Parse((Session["NOofCHILD"]).ToString());
            jr.BonusRequstStatus = Session["Class"].ToString(); //class premium
            if (jr.BonusRequstStatus == "Premium")
            {
                decimal adultfare = Convert.ToDecimal(sch.Fare_Premium_Adult);
                decimal childfare = Convert.ToDecimal(sch.Fare_Premium_Child);

                jr.TotalBookingFare = (jr.NoOfPassengers_Adult * adultfare) + (jr.NoOfPassengers_Child * childfare);

            }
            else if (jr.BonusRequstStatus == "FirstClass")
            {
                decimal adultfare = Convert.ToDecimal(sch.Fare_Firstclass_Adult);
                decimal childfare = Convert.ToDecimal(sch.Fare_Firstclass_Child);

                jr.TotalBookingFare = (jr.NoOfPassengers_Adult * adultfare) + (jr.NoOfPassengers_Child * childfare);

            }

            else if (jr.BonusRequstStatus == "Economy")
            {
                decimal adultfare = Convert.ToDecimal(sch.Fare_Economy_Adult);
                decimal childfare = Convert.ToDecimal(sch.Fare_Economy_Child);

                jr.TotalBookingFare = (jr.NoOfPassengers_Adult * adultfare) + (jr.NoOfPassengers_Child * childfare);

            }

            jr.NoOfPassengers_Adult = int.Parse(Session["NOofADULT"].ToString());
            jr.NoOfPassengers_Child = int.Parse((Session["NOofCHILD"]).ToString());
            
            jr.PaymentStatus = "Pending";
            jr.BookingDate = DateTime.Now;
            db.tblJourney_HMS03_Team7.Add(jr);
            db.SaveChanges();
            Session["count"] = 1;
            Session["jour"] = jr.JourneyID;
            
            Session["TotalFare"] = jr.TotalBookingFare;
            try
            {
                jr.CustomerID = int.Parse(Session["cid"].ToString());
                db.SaveChanges();
            }
            catch
            {
                if (jr.CustomerID == null)
                {
                    
                    MessageBox.Show("You haven't Signed-In");
                    return RedirectToAction("Log", "tblLogin_HMS03_Team7");
                }
            }
            return RedirectToAction("Details", new {id=jr.JourneyID });


        }
        [Authorize]
        public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        tblJourney_HMS03_Team7 tblJourney_HMS03_Team7 = db.tblJourney_HMS03_Team7.Find(id);
        if (tblJourney_HMS03_Team7 == null)
        {
            return HttpNotFound();
        }
        return View(tblJourney_HMS03_Team7);
    }

        [Authorize]
        [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        tblJourney_HMS03_Team7 tblJourney_HMS03_Team7 = db.tblJourney_HMS03_Team7.Find(id);
        db.tblJourney_HMS03_Team7.Remove(tblJourney_HMS03_Team7);
        db.SaveChanges();
        return RedirectToAction("Index");
    }
        [Authorize]
        public ActionResult ViewTicket()
        {
            
            tblJourney_HMS03_Team7 tblJourney_HMS03_Team7 = db.tblJourney_HMS03_Team7.Find(int.Parse(Session["JourneyID"].ToString()));
            if (tblJourney_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            return View(tblJourney_HMS03_Team7);
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
