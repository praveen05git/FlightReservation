using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Windows.Forms;
using SandMax1.Models;

namespace SandMax1.Controllers
{
    public class tblLogin_HMS03_Team7Controller : Controller
    {
        private DB09AO114_1718Entities1 db = new DB09AO114_1718Entities1();

        // GET: tblLogin_HMS03_Team7
        public ActionResult Index()
        {
            return View(db.tblLogin_HMS03_Team7.ToList());
        }

        // GET: tblLogin_HMS03_Team7/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLogin_HMS03_Team7 tblLogin_HMS03_Team7 = db.tblLogin_HMS03_Team7.Find(id);
            if (tblLogin_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            return View(tblLogin_HMS03_Team7);
        }

        // GET: tblLogin_HMS03_Team7/Create
        public ActionResult Log()
        {
            return View();
        }

        // POST: tblLogin_HMS03_Team7/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Log([Bind(Include = "LoginID,uname,pwd,roles")] tblLogin_HMS03_Team7 tblLogin_HMS03_Team7)
        {
            List<tblLogin_HMS03_Team7> ulist = db.tblLogin_HMS03_Team7.ToList();

            foreach (var i in ulist)
            {
                if (i.uname.Equals(tblLogin_HMS03_Team7.uname) && i.pwd.Equals(tblLogin_HMS03_Team7.pwd) && i.roles.Equals("Customer"))
                {
                    FormsAuthentication.SetAuthCookie(i.uname, false);
                    MessageBox.Show("Logged in successfully as: " + tblLogin_HMS03_Team7.uname);
                    Session["username"] = i.uname;
                    Session["userid"] = i.LoginID;
                    ViewBag.uname = i.uname;

                    List<tblCustomer_HMS03_Team7> clist = db.tblCustomer_HMS03_Team7.ToList();

                    foreach(var customer in clist)
                    {
                        if (i.LoginID == customer.LoginID)
                        {
                            Session["cid"] = customer.CustomerID;
                        }
                    }

                    if (Session["ScheduleId"] == null)
                    {
                        return RedirectToAction("Create", "tblSearch_HMS03_Team7");
                    }
                    else
                    {
                        int jid = int.Parse(Session["jour"].ToString());
                        List<tblJourney_HMS03_Team7> jlist = db.tblJourney_HMS03_Team7.ToList();

                        int cust = int.Parse((Session["cid"]).ToString());
                        tblJourney_HMS03_Team7 journeys = db.tblJourney_HMS03_Team7.Find(jid);
                        journeys.CustomerID = cust;
                        db.SaveChanges();

                       
                        return RedirectToAction("Details", "tblJourney_HMS03_Team7", new { id = jid });
                }

                }


                else if (i.uname.Equals(tblLogin_HMS03_Team7.uname) && i.pwd.Equals(tblLogin_HMS03_Team7.pwd) && i.roles.Equals("Manager"))
                {
                    FormsAuthentication.SetAuthCookie(i.uname, false);
                   
                    List<tblEmployee_HMS03_Team7> elist = db.tblEmployee_HMS03_Team7.ToList();
                    foreach (var item in elist)
                    {
                        if (i.LoginID == item.LoginID)
                        {
                            Session["FMID"] = item.EmployeeID;
                            Session["FMAirline"] = item.Airlines;

                        }
                    }

                    MessageBox.Show("Logged in successfully as: " + tblLogin_HMS03_Team7.uname);
                    return RedirectToAction("Index", "tblAeroplane_HMS03_Team7");
                }

                else if (i.uname.Equals(tblLogin_HMS03_Team7.uname) && i.pwd.Equals(tblLogin_HMS03_Team7.pwd) && i.roles.Equals("Flight"))
                {
                    FormsAuthentication.SetAuthCookie(i.uname, false);
                   
                    List<tblEmployee_HMS03_Team7> elist = db.tblEmployee_HMS03_Team7.ToList();
                    foreach(var item in elist)
                    {
                        if (i.LoginID == item.LoginID)
                        {
                            Session["FSID"]= item.EmployeeID;
                          
                        }
                    }

                    MessageBox.Show("Logged in successfully as: " + tblLogin_HMS03_Team7.uname);
                    return RedirectToAction("VeiwScheduleToScheduler", "tblSchedule_HMS03_Team7");
                }
            }

            MessageBox.Show("Invalid");
            return RedirectToAction("Log");
            //}
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("progress","Home");
        }

      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLogin_HMS03_Team7 tblLogin_HMS03_Team7 = db.tblLogin_HMS03_Team7.Find(id);
            if (tblLogin_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            return View(tblLogin_HMS03_Team7);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginID,uname,pwd,roles")] tblLogin_HMS03_Team7 tblLogin_HMS03_Team7)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblLogin_HMS03_Team7).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblLogin_HMS03_Team7);
        }

       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLogin_HMS03_Team7 tblLogin_HMS03_Team7 = db.tblLogin_HMS03_Team7.Find(id);
            if (tblLogin_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            return View(tblLogin_HMS03_Team7);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblLogin_HMS03_Team7 tblLogin_HMS03_Team7 = db.tblLogin_HMS03_Team7.Find(id);
            db.tblLogin_HMS03_Team7.Remove(tblLogin_HMS03_Team7);
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
