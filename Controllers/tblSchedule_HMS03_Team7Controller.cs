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
    public class tblSchedule_HMS03_Team7Controller : Controller
    {
        private DB09AO114_1718Entities1 db = new DB09AO114_1718Entities1();

        [Authorize]
        public ActionResult Index()
        {
            var tblSchedule_HMS03_Team7 = db.tblSchedule_HMS03_Team7.Include(t => t.tblAeroplane_HMS03_Team7);
            return View(tblSchedule_HMS03_Team7.ToList());
        }

       
        public ActionResult VeiwScheduleToCustomer()
        {

            var tblSchedule_HMS03_Team7 = db.tblSchedule_HMS03_Team7.Include(t => t.tblAeroplane_HMS03_Team7);
            return View(tblSchedule_HMS03_Team7.ToList());
        }

        [Authorize]
        public ActionResult VeiwScheduleToScheduler()
        {
            var tblSchedule_HMS03_Team7 = db.tblSchedule_HMS03_Team7.Include(t => t.tblAeroplane_HMS03_Team7);
            return View(tblSchedule_HMS03_Team7.ToList());
        }


     
        public ActionResult FareDetails(int? id)
        {
            Session["ScheduleID"] = id;
            tblSchedule_HMS03_Team7 sch = db.tblSchedule_HMS03_Team7.Find(id);
            if (sch == null)
            {
                return HttpNotFound();
            }
            decimal? TotalBookingFare;
            int ano = int.Parse(Session["NOofADULT"].ToString());
            int cno = int.Parse(Session["NOofCHILD"].ToString());
            string cl = Session["Class"].ToString();
            if (cl == "Premium")
            {
                decimal? adultfare = sch.Fare_Premium_Adult;
                decimal? childfare = sch.Fare_Premium_Child;

                TotalBookingFare = (ano * adultfare) + (cno * childfare);

            }

            else if (cl == "FirstClass")
            {
                decimal? adultfare = sch.Fare_Firstclass_Adult;
                decimal? childfare = sch.Fare_Firstclass_Child;

                TotalBookingFare = (ano * adultfare) + (cno * childfare); 

            }

            else if (cl == "Economy")
            {
                decimal? adultfare = sch.Fare_Economy_Adult;
                decimal? childfare = sch.Fare_Economy_Child;

                TotalBookingFare = (ano * adultfare) + (cno * childfare);

            }

            //
            /*Session["TotalFare"] = 12345;*///int.Parse(Session["NOofADULT"].ToString()) * adultFare + int.Parse(Session["NOofCHILD"].ToString()) * childFare;
            return View(sch);

            //var tblSchedule_HMS03_Team7 = db.tblSchedule_HMS03_Team7.Include(t => t.tblAeroplane_HMS03_Team7);
            //return View(tblSchedule_HMS03_Team7.ToList());
        }


        // GET: tblSchedule_HMS03_Team7
        //public ActionResult CheckAvailability()
        //{
        //    var tblSchedule_HMS03_Team7 = db.tblSchedule_HMS03_Team7.Include(t => t.tblAeroplane_HMS03_Team7);
        //    return View(tblSchedule_HMS03_Team7.ToList());
        //}

        //public ActionResult BookTicket(int? id)
        //{
        //    if (ModelState.IsValid)
        //    {





        //        return RedirectToAction("changeseat");
        //    }


        //    return View("CheckAvailability");
        //}
        [Authorize]
        public ActionResult changeseat()
        {
            if (ModelState.IsValid)
            {

                tblSchedule_HMS03_Team7 Sched = db.tblSchedule_HMS03_Team7.Find(int.Parse(Session["ScheduleID"].ToString()));
                string CLASS = (string)Session["Class"].ToString();
                int NoOfAdult = int.Parse((string)Session["NOofADULT"].ToString());
                int NoOfChild = int.Parse((string)Session["NOofCHILD"].ToString());
                if (CLASS == "Premium")
                {
                    try { 
                    int avail =Convert.ToInt16( Sched.Availability_Premium);
                    Sched.Availability_Premium =avail - (NoOfAdult + NoOfChild);
                       
                        
                        db.SaveChanges();
                    }
                    catch
                    {

                    }
                }
                else if (CLASS == "Firstclass")
                {
                    Sched.Availability_Firstclass = Sched.Availability_Firstclass-(NoOfAdult + NoOfChild);
                    db.SaveChanges();
                }
                else if (CLASS == "Economy")
                {
                    Sched.Availability_Economy = Sched.Availability_Economy - (NoOfAdult + NoOfChild);
                    db.SaveChanges();
                }



                db.Entry(Sched).State = EntityState.Modified;




                return RedirectToAction("ViewTicket", "tblPassenger_HMS03_Team7");
            }


            return View("CheckAvailability");

        }


        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSchedule_HMS03_Team7 tblSchedule_HMS03_Team7 = db.tblSchedule_HMS03_Team7.Find(id);
            if (tblSchedule_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            return View(tblSchedule_HMS03_Team7);
        }

        [Authorize]
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSchedule_HMS03_Team7 tblSchedule_HMS03_Team7 = db.tblSchedule_HMS03_Team7.Find(id);
            if (tblSchedule_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            return View(tblSchedule_HMS03_Team7);
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.PlaneRegistrationID = new SelectList(db.tblAeroplane_HMS03_Team7, "PlaneRegistrationID", "AirplaneSeries");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScheduleID,PlaneRegistrationID,Source,Destination,Departure,Arrival,Fare_Premium_Adult,Fare_Economy_Adult,Fare_Firstclass_Adult,Fare_Premium_Child,Fare_Firstclass_Child,Fare_Economy_Child,Availability_Premium,Availability_Firstclass,Availability_Economy,TravelDistance,StatusofFlight,BonusMiles,BonusMilesPoints")] tblSchedule_HMS03_Team7 tblSchedule_HMS03_Team7)
        {
            if (ModelState.IsValid)
            {
                tblSchedule_HMS03_Team7.StatusofFlight = "Scheduled";
                if (tblSchedule_HMS03_Team7.Source.Equals(tblSchedule_HMS03_Team7.Destination))
                {
                    MessageBox.Show("Source and destination can't be same");
                   // return RedirectToAction("Create");
                }
                if (tblSchedule_HMS03_Team7.Arrival < tblSchedule_HMS03_Team7.Departure)
                {
                    MessageBox.Show("Arrival should be after departure!");
                    return RedirectToAction("VeiwScheduleToScheduler");
                }
                if (tblSchedule_HMS03_Team7.Departure < DateTime.Now)
                {
                    MessageBox.Show("Departure should be in future!");
                    return RedirectToAction("VeiwScheduleToScheduler");
                }
                if (tblSchedule_HMS03_Team7.Departure.Subtract(DateTime.Now).TotalDays < 1)
                {
                    MessageBox.Show("Departure should be after 24 hours!");
                    return RedirectToAction("VeiwScheduleToScheduler");
                }
                var busy = db.tblSchedule_HMS03_Team7.Where(x => x.PlaneRegistrationID ==tblSchedule_HMS03_Team7.PlaneRegistrationID  && ((x.Departure <= tblSchedule_HMS03_Team7.Arrival &&  x.Arrival >= tblSchedule_HMS03_Team7.Arrival) || (x.Departure <= tblSchedule_HMS03_Team7.Departure && x.Arrival >= tblSchedule_HMS03_Team7.Departure))).FirstOrDefault();
                if (busy != null)
                {
                    MessageBox.Show("Flight already Scheduled!");
                    return RedirectToAction("VeiwScheduleToScheduler");
                }
                var duplicate = db.tblSchedule_HMS03_Team7.Where(x => x.Departure == tblSchedule_HMS03_Team7.Departure && x.Arrival == tblSchedule_HMS03_Team7.Arrival && x.Source == tblSchedule_HMS03_Team7.Source && x.Destination == tblSchedule_HMS03_Team7.Destination).FirstOrDefault();
                if (duplicate != null)
                {
                    MessageBox.Show("Duplicate Schedule!");
                    return RedirectToAction("VeiwScheduleToScheduler");
                }
                List<tblAeroplane_HMS03_Team7> alist = db.tblAeroplane_HMS03_Team7.ToList();
                foreach (var i in alist)
                {
                    if (i.PlaneRegistrationID == tblSchedule_HMS03_Team7.PlaneRegistrationID)
                    {

                        tblSchedule_HMS03_Team7 tr = new tblSchedule_HMS03_Team7();
                        i.ScheduleStatus = tblSchedule_HMS03_Team7.StatusofFlight;
                        db.SaveChanges();
                    }
                }

                //To increment the schedule count for the scheduled airplane
                tblAeroplane_HMS03_Team7 airplane = db.tblAeroplane_HMS03_Team7.Find(tblSchedule_HMS03_Team7.PlaneRegistrationID);
                /*airplane.SchedulerID = int.Parse(Session["FSID"].ToString());*///Change it to int.Parse(Session["EmployeeID"].ToString());
                                                                                 //airplane.ScheduleStatus = "Scheduled";
                                                                                 
                tblSchedule_HMS03_Team7.Availability_Economy = airplane.Capacity_Economy;
                tblSchedule_HMS03_Team7.Availability_Premium = airplane.Capacity_Premium;
                tblSchedule_HMS03_Team7.Availability_Firstclass = airplane.Capacity_FirstClass;

                db.tblSchedule_HMS03_Team7.Add(tblSchedule_HMS03_Team7);
                db.SaveChanges();
                return RedirectToAction("VeiwScheduleToScheduler");
            }

            ViewBag.PlaneRegistrationID = new SelectList(db.tblAeroplane_HMS03_Team7, "PlaneRegistrationID", "AirplaneSeries", tblSchedule_HMS03_Team7.PlaneRegistrationID);
            return View(tblSchedule_HMS03_Team7);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSchedule_HMS03_Team7 tblSchedule_HMS03_Team7 = db.tblSchedule_HMS03_Team7.Find(id);
            if (tblSchedule_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            if (tblSchedule_HMS03_Team7.Departure.Date.Equals(DateTime.Today.Date) && (tblSchedule_HMS03_Team7.Departure.TimeOfDay.Subtract(DateTime.Now.TimeOfDay)).TotalHours < 4.0)
            {
                MessageBox.Show("Can't Edit! Flight to depart in " + tblSchedule_HMS03_Team7.Departure.TimeOfDay.Subtract(DateTime.Now.TimeOfDay).Hours + " : " + tblSchedule_HMS03_Team7.Departure.TimeOfDay.Subtract(DateTime.Now.TimeOfDay).Minutes + " hours");
                return RedirectToAction("VeiwScheduleToScheduler");


            }

            ViewBag.PlaneRegistrationID = new SelectList(db.tblAeroplane_HMS03_Team7, "PlaneRegistrationID", "AirplaneSeries", tblSchedule_HMS03_Team7.PlaneRegistrationID);

            return View(tblSchedule_HMS03_Team7);


        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScheduleID,PlaneRegistrationID,Source,Destination,Departure,Arrival,Fare_Premium_Adult,Fare_Economy_Adult,Fare_Firstclass_Adult,Fare_Premium_Child,Fare_Firstclass_Child,Fare_Economy_Child,Availability_Premium,Availability_Firstclass,Availability_Economy,TravelDistance,StatusofFlight,BonusMiles,BonusMilesPoints")] tblSchedule_HMS03_Team7 tblSchedule_HMS03_Team7)
        {

            //if (tblSchedule_HMS03_Team7.Departure.Date.Equals(DateTime.Today.Date) && (tblSchedule_HMS03_Team7.Departure.TimeOfDay.Subtract(DateTime.Now.TimeOfDay)).TotalHours < 4)
            //{
            //    MessageBox.Show("you can't update flight!!sorry boss!!");
            //    return RedirectToAction("Index");
            //}
            if (ModelState.IsValid)
            {
                Session["s1"] = tblSchedule_HMS03_Team7.PlaneRegistrationID;
                tblSchedule_HMS03_Team7.StatusofFlight = "Scheduled";
                tblAeroplane_HMS03_Team7 airplane = db.tblAeroplane_HMS03_Team7.Find(tblSchedule_HMS03_Team7.PlaneRegistrationID);


                tblSchedule_HMS03_Team7.Availability_Economy = airplane.Capacity_Economy;
                tblSchedule_HMS03_Team7.Availability_Premium = airplane.Capacity_Premium;
                tblSchedule_HMS03_Team7.Availability_Firstclass = airplane.Capacity_FirstClass;
                db.Entry(tblSchedule_HMS03_Team7).State = EntityState.Modified;
                db.SaveChanges();
                MessageBox.Show("Flight Schedule edited successfully" + Session["s1"].ToString());
                return RedirectToAction("VeiwScheduleToScheduler");
            }
            ViewBag.PlaneRegistrationID = new SelectList(db.tblAeroplane_HMS03_Team7, "PlaneRegistrationID", "AirplaneSeries", tblSchedule_HMS03_Team7.PlaneRegistrationID);
            return View(tblSchedule_HMS03_Team7);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSchedule_HMS03_Team7 tblSchedule_HMS03_Team7 = db.tblSchedule_HMS03_Team7.Find(id);
            if (tblSchedule_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            if (tblSchedule_HMS03_Team7.Departure.Date.Equals(DateTime.Today.Date) && (tblSchedule_HMS03_Team7.Departure.TimeOfDay.Subtract(DateTime.Now.TimeOfDay)).TotalHours < 4)
            {
                MessageBox.Show("Can't Cancel! Flight to depart in " + tblSchedule_HMS03_Team7.Departure.TimeOfDay.Subtract(DateTime.Now.TimeOfDay).Hours + " : " + tblSchedule_HMS03_Team7.Departure.TimeOfDay.Subtract(DateTime.Now.TimeOfDay).Minutes + " hours");
                return RedirectToAction("VeiwScheduleToScheduler");

            }
            return View(tblSchedule_HMS03_Team7);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            tblSchedule_HMS03_Team7 tblSchedule_HMS03_Team7 = db.tblSchedule_HMS03_Team7.Find(id);
            tblSchedule_HMS03_Team7.StatusofFlight = "Cancelled";
            db.Entry(tblSchedule_HMS03_Team7).State = EntityState.Modified;
            //db.tblSchedule_HMS03_Team7.Remove(tblSchedule_HMS03_Team7);
            db.SaveChanges();
            return RedirectToAction("VeiwScheduleToScheduler");
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
