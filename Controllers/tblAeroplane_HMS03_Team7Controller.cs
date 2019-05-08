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
    public class tblAeroplane_HMS03_Team7Controller : Controller
    {
        private DB09AO114_1718Entities1 db = new DB09AO114_1718Entities1();

        [Authorize]
        public ActionResult Index()
        {
            var tblAeroplane_HMS03_Team7 = db.tblAeroplane_HMS03_Team7.Include(t => t.tblEmployee_HMS03_Team7);
            return View(tblAeroplane_HMS03_Team7.ToList());
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAeroplane_HMS03_Team7 tblAeroplane_HMS03_Team7 = db.tblAeroplane_HMS03_Team7.Find(id);
            //tblAeroplane_HMS03_Team7.ScheduleStatus = (string)Session["status"];
            if (tblAeroplane_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            //if(tblAeroplane_HMS03_Team7.PlaneRegistrationID=)
            return View(tblAeroplane_HMS03_Team7);
        }

        [Authorize]
        public ActionResult Searchflights()
        {
            
            var tblAeroplane_HMS03_Team7 = db.tblAeroplane_HMS03_Team7.Include(t => t.tblEmployee_HMS03_Team7);
            return View(tblAeroplane_HMS03_Team7.ToList()); 
        }
        [Authorize]
        public ActionResult Search()
        {
           
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search([Bind(Include = "PlaneRegistrationID,DateOfCommencement,DateOdMAnufacturing,AirplaneSeries,Airline,Capacity_Premium,Capacity_FirstClass,Capacity_Economy,TakeOffWeight,MaximumDistance,LocationOfPlane,SchedulerID,RecentUpdate,ScheduleStatus")] tblAeroplane_HMS03_Team7 flight)
        {
            Session["planeid"] = flight.PlaneRegistrationID;
            Session["flightname"] = flight.AirplaneSeries;
            Session["airlines"] = flight.Airline;
            var result = db.tblAeroplane_HMS03_Team7.Where(x => x.PlaneRegistrationID == flight.PlaneRegistrationID && x.AirplaneSeries == flight.AirplaneSeries && x.Airline == flight.Airline).FirstOrDefault();
            if(result != null)
            {
                return RedirectToAction("Searchflights");
            }
            else
            {
                MessageBox.Show("there are no flights existed for entered details");
                return RedirectToAction("Search");
            }
            
            
        }

        [Authorize]
        public ActionResult Create()
        {
          ViewBag.SchedulerID = new SelectList(db.tblEmployee_HMS03_Team7, "EmployeeID", "Employee_Name");
           
            return View();
            
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlaneRegistrationID,DateOfCommencement,DateOdMAnufacturing,AirplaneSeries,Airline,Capacity_Premium,Capacity_FirstClass,Capacity_Economy,TakeOffWeight,MaximumDistance,LocationOfPlane,SchedulerID,RecentUpdate,ScheduleStatus")] tblAeroplane_HMS03_Team7 tblAeroplane_HMS03_Team7)
        {
            if (ModelState.IsValid)
            {
                if (tblAeroplane_HMS03_Team7.DateOdMAnufacturing > DateTime.Now && tblAeroplane_HMS03_Team7.DateOfCommencement  > DateTime.Now)
                {
                    MessageBox.Show("manufacturing and commencement cannot be in future");
                    return RedirectToAction("Create");
                }
                else if (tblAeroplane_HMS03_Team7.DateOdMAnufacturing > DateTime.Now)
                {
                    MessageBox.Show("date of manufacturing cannot be in future");
                    return RedirectToAction("Create");
                    
                }
                else if (tblAeroplane_HMS03_Team7.DateOfCommencement > DateTime.Now)
                {
                    MessageBox.Show("date of commencement cannot be in future");
                    return RedirectToAction("Create");
                }
                if (db.tblAeroplane_HMS03_Team7.Any(x => x.AirplaneSeries == tblAeroplane_HMS03_Team7.AirplaneSeries))
                {
                    //ModelState.AddModelError("CustomError", "name already exists");
                    MessageBox.Show("name already exists:" + tblAeroplane_HMS03_Team7.AirplaneSeries);
                    return RedirectToAction("Create");
                }

                if (tblAeroplane_HMS03_Team7.DateOfCommencement < tblAeroplane_HMS03_Team7.DateOdMAnufacturing)
                 {
                    MessageBox.Show("can't be same or Date of Commencement should be greater than date of manufacturing");
                    return RedirectToAction("Create");
                }
                
                    tblAeroplane_HMS03_Team7.SchedulerID = int.Parse(Session["FMID"].ToString());
                    tblAeroplane_HMS03_Team7.ScheduleStatus = "Not Scheduled";
                    tblAeroplane_HMS03_Team7.RecentUpdate = DateTime.Now;
                    db.tblAeroplane_HMS03_Team7.Add(tblAeroplane_HMS03_Team7);
                    db.SaveChanges();
                
                
                MessageBox.Show("Created successfully with ID:"+tblAeroplane_HMS03_Team7.PlaneRegistrationID);
                return RedirectToAction("Index");
            }

            ViewBag.SchedulerID = new SelectList(db.tblEmployee_HMS03_Team7, "EmployeeID", "Employee_Name", tblAeroplane_HMS03_Team7.SchedulerID);
            return View(tblAeroplane_HMS03_Team7);
        }
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAeroplane_HMS03_Team7 tblAeroplane_HMS03_Team7 = db.tblAeroplane_HMS03_Team7.Find(id);
            if (tblAeroplane_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }


            var user = db.tblAeroplane_HMS03_Team7.Where(x => x.PlaneRegistrationID == tblAeroplane_HMS03_Team7.PlaneRegistrationID).FirstOrDefault();
            
            if (user != null)
            {
                if(tblAeroplane_HMS03_Team7.ScheduleStatus=="scheduled" || tblAeroplane_HMS03_Team7.ScheduleStatus=="Scheduled")
                {
                    MessageBox.Show("Flight already scheduled!");
                    return RedirectToAction("Index");
                }
                
                
            }

            ViewBag.SchedulerID = new SelectList(db.tblEmployee_HMS03_Team7, "EmployeeID", "Employee_Name", tblAeroplane_HMS03_Team7.SchedulerID);
            return View(tblAeroplane_HMS03_Team7);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlaneRegistrationID,DateOfCommencement,DateOdMAnufacturing,AirplaneSeries,Airline,Capacity_Premium,Capacity_FirstClass,Capacity_Economy,TakeOffWeight,MaximumDistance,LocationOfPlane,SchedulerID,RecentUpdate,ScheduleStatus")] tblAeroplane_HMS03_Team7 tblAeroplane_HMS03_Team7)
        {
            if (ModelState.IsValid)
            {
                if (tblAeroplane_HMS03_Team7.DateOdMAnufacturing > DateTime.Now && tblAeroplane_HMS03_Team7.DateOfCommencement > DateTime.Now)
                {
                    MessageBox.Show("Manufacturing and commencement cannot be in future");
                    return RedirectToAction("Edit");
                }
                if (tblAeroplane_HMS03_Team7.DateOfCommencement < tblAeroplane_HMS03_Team7.DateOdMAnufacturing)
                {
                    MessageBox.Show("Can't be same or Date of Commencement should be greater than date of manufacturing");
                    return RedirectToAction("Edit");
                }
                db.Entry(tblAeroplane_HMS03_Team7).State = EntityState.Modified; 
                db.SaveChanges();
                MessageBox.Show("Updated successfully");
                return RedirectToAction("Index");
            }
            ViewBag.SchedulerID = new SelectList(db.tblEmployee_HMS03_Team7, "EmployeeID", "Employee_Name", tblAeroplane_HMS03_Team7.SchedulerID);
            return View(tblAeroplane_HMS03_Team7);
        }
       [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAeroplane_HMS03_Team7 tblAeroplane_HMS03_Team7 = db.tblAeroplane_HMS03_Team7.Find(id);
            if (tblAeroplane_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }

            var user = db.tblAeroplane_HMS03_Team7.Where(x => x.PlaneRegistrationID == tblAeroplane_HMS03_Team7.PlaneRegistrationID).FirstOrDefault();

            if (user != null)
            {
                if (tblAeroplane_HMS03_Team7.ScheduleStatus == "scheduled" || tblAeroplane_HMS03_Team7.ScheduleStatus == "Scheduled")
                {
                    MessageBox.Show("Flight already scheduled!");
                    return RedirectToAction("Index");
                }


            }
            return View(tblAeroplane_HMS03_Team7);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblAeroplane_HMS03_Team7 tblAeroplane_HMS03_Team7 = db.tblAeroplane_HMS03_Team7.Find(id);
            db.tblAeroplane_HMS03_Team7.Remove(tblAeroplane_HMS03_Team7);
            db.SaveChanges();
            MessageBox.Show("Flight deleted successfully!"+tblAeroplane_HMS03_Team7.AirplaneSeries);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult viewOwnedAeroplanes()
        {
            var tblAeroplane_HMS03_Team7 = db.tblAeroplane_HMS03_Team7.Include(t => t.tblEmployee_HMS03_Team7);
            return View(tblAeroplane_HMS03_Team7.ToList());
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
