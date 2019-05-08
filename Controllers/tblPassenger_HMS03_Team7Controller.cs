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
using System.IO;

namespace SandMax1.Controllers
{
    public class tblPassenger_HMS03_Team7Controller : Controller
    {
        private DB09AO114_1718Entities1 db = new DB09AO114_1718Entities1();

        [Authorize]
        public ActionResult Index()
        {
            var tblPassenger_HMS03_Team7 = db.tblPassenger_HMS03_Team7.Include(t => t.tblJourney_HMS03_Team7);
            return View(tblPassenger_HMS03_Team7.ToList());
        }

        [Authorize]
        public ActionResult Viewpassenger()
        {
            var tblPassenger_HMS03_Team7 = db.tblPassenger_HMS03_Team7.Include(t => t.tblJourney_HMS03_Team7);
            return View(tblPassenger_HMS03_Team7.ToList());
        }
        [Authorize]
        public ActionResult PassengerHistory()
        {
            
            var tblPassenger_HMS03_Team7 = db.tblPassenger_HMS03_Team7.Include(t => t.tblJourney_HMS03_Team7);
            return View(tblPassenger_HMS03_Team7.ToList());
        }

        

        //public static Byte[] PdfSharpConvert(String html)
        //{
        //    Byte[] res = null;
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
        //        pdf.Save(ms);
        //        res = ms.ToArray();
        //    }
        //    return res;
        //}

        //public string RenderViewAsString(string viewName, object model)
        //{
        //    // create a string writer to receive the HTML code
        //    StringWriter stringWriter = new StringWriter();

        //    // get the view to render
        //    ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, null);
        //    // create a context to render a view based on a model
        //    ViewContext viewContext = new ViewContext(
        //            ControllerContext,
        //            viewResult.View,
        //            new ViewDataDictionary(model),
        //            new TempDataDictionary(),
        //            stringWriter
        //            );

        //    // render the view to a HTML code
        //    viewResult.View.Render(viewContext, stringWriter);

        //    // return the HTML code
        //    return stringWriter.ToString();
        //}

        //[HttpPost]
        //public ActionResult ConvertThisPageToPdf()
        //{
        //    // get the HTML code of this view
        //    string htmlToConvert = RenderViewAsString("Index", null);

        //    // the base URL to resolve relative images and css
        //    String thisPageUrl = this.ControllerContext.HttpContext.Request.Url.AbsoluteUri;
        //    String baseUrl = thisPageUrl.Substring(0, thisPageUrl.Length - "Home/ConvertThisPageToPdf".Length);

        //    // instantiate the HiQPdf HTML to PDF converter
        //    HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

        //    // hide the button in the created PDF
        //    htmlToPdfConverter.HiddenHtmlElements = new string[] { "#convertThisPageButtonDiv" };

        //    // render the HTML code as PDF in memory
        //    byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlToConvert, baseUrl);

        //    // send the PDF file to browser
        //    FileResult fileResult = new FileContentResult(pdfBuffer, "application/pdf");
        //    fileResult.FileDownloadName = "ThisMvcViewToPdf.pdf";

        //    return fileResult;
        //}

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPassenger_HMS03_Team7 tblPassenger_HMS03_Team7 = db.tblPassenger_HMS03_Team7.Find(id);
            if (tblPassenger_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            
            return View(tblPassenger_HMS03_Team7);
        }

        [Authorize]
        public ActionResult AddPassenger()
        {
            if((int.Parse(Session["NOofADULT"].ToString()))==0)
                {
                return RedirectToAction("AddChild");
                
            }
            return RedirectToAction("Create");
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.JourneyID = new SelectList(db.tblJourney_HMS03_Team7, "JourneyID", "PaymentStatus");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PassengerID,JourneyID,PassengerName,Gender,Age,NoOfBaggages,TotalWeightOfBaggages,CheckInStatus,CheckOutDate")] tblPassenger_HMS03_Team7 tblPassenger_HMS03_Team7)
        {
            if (ModelState.IsValid)
            {
                if (tblPassenger_HMS03_Team7.Age < 100 && tblPassenger_HMS03_Team7.Age > 12)
                {
                    tblPassenger_HMS03_Team7.JourneyID = int.Parse(Session["JourneyID"].ToString());
                    db.tblPassenger_HMS03_Team7.Add(tblPassenger_HMS03_Team7);
                    db.SaveChanges();
                    if (Request["button1"].ToString().Equals("Proceed"))
                    {

                        Session["count"] = 1;
                        return RedirectToAction("Viewpassenger");
                    }

                    if (int.Parse(Session["count"].ToString()) < (int.Parse(Session["NOofADULT"].ToString()) + int.Parse(Session["NOofCHILD"].ToString())))
                    {
                        if (int.Parse(Session["count"].ToString()) < int.Parse(Session["NOofADULT"].ToString()))
                        {
                            MessageBox.Show("enter" + int.Parse(Session["count"].ToString()));
                            MessageBox.Show("enter" + int.Parse(Session["NOofADULT"].ToString()));


                            Session["count"] = int.Parse(Session["count"].ToString()) + 1;
                            return RedirectToAction("Create");
                        }
                        else
                        {

                            Session["count"] = int.Parse(Session["count"].ToString()) + 1;
                            return RedirectToAction("AddChild");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Enter the Valid Age");
                }


            }
            ViewBag.JourneyID = new SelectList(db.tblJourney_HMS03_Team7, "JourneyID", "PaymentStatus", tblPassenger_HMS03_Team7.JourneyID);
            return View(tblPassenger_HMS03_Team7);
        }

        [Authorize]
        public ActionResult AddChild()
        {
            ViewBag.JourneyID = new SelectList(db.tblJourney_HMS03_Team7, "JourneyID", "PaymentStatus");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddChild([Bind(Include = "PassengerID,JourneyID,PassengerName,Gender,Age,NoOfBaggages,TotalWeightOfBaggages,CheckInStatus,CheckOutDate")] tblPassenger_HMS03_Team7 tblPassenger_HMS03_Team7)
        {
            if (ModelState.IsValid)
            {
                if (tblPassenger_HMS03_Team7.Age <= 12 && tblPassenger_HMS03_Team7.Age > 0)
                { 

                    tblPassenger_HMS03_Team7.JourneyID = int.Parse(Session["JourneyID"].ToString());
                    db.tblPassenger_HMS03_Team7.Add(tblPassenger_HMS03_Team7);
                    db.SaveChanges();
                    if (Request["button1"].ToString().Equals("Proceed"))
                    {

                        Session["count"] = 1;
                        return RedirectToAction("Viewpassenger");
                    }

                    if (int.Parse(Session["count"].ToString()) < int.Parse(Session["NOofADULT"].ToString()) + int.Parse(Session["NOofCHILD"].ToString()))
                    {

                        if (int.Parse(Session["count"].ToString()) < int.Parse(Session["NOofADULT"].ToString()))
                        {
                            Session["count"] = int.Parse(Session["count"].ToString()) + 1;
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            Session["count"] = int.Parse(Session["count"].ToString()) + 1;
                            return RedirectToAction("AddChild");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Enter the Valid Age");
                }
            }

            ViewBag.JourneyID = new SelectList(db.tblJourney_HMS03_Team7, "JourneyID", "PaymentStatus", tblPassenger_HMS03_Team7.JourneyID);
            return View(tblPassenger_HMS03_Team7);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPassenger_HMS03_Team7 tblPassenger_HMS03_Team7 = db.tblPassenger_HMS03_Team7.Find(id);
            if (tblPassenger_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            ViewBag.JourneyID = new SelectList(db.tblJourney_HMS03_Team7, "JourneyID", "PaymentStatus", tblPassenger_HMS03_Team7.JourneyID);
            return View(tblPassenger_HMS03_Team7);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PassengerID,JourneyID,PassengerName,Gender,Age,NoOfBaggages,TotalWeightOfBaggages,CheckInStatus,CheckOutDate")] tblPassenger_HMS03_Team7 tblPassenger_HMS03_Team7)
        {
            if (ModelState.IsValid)
            {
                tblPassenger_HMS03_Team7.JourneyID = int.Parse(Session["JourneyID"].ToString());
                db.Entry(tblPassenger_HMS03_Team7).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Viewpassenger");
            }
            ViewBag.JourneyID = new SelectList(db.tblJourney_HMS03_Team7, "JourneyID", "PaymentStatus", tblPassenger_HMS03_Team7.JourneyID);
            return View(tblPassenger_HMS03_Team7);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPassenger_HMS03_Team7 tblPassenger_HMS03_Team7 = db.tblPassenger_HMS03_Team7.Find(id);
            if (tblPassenger_HMS03_Team7 == null)
            {
                return HttpNotFound();
            }
            return View(tblPassenger_HMS03_Team7);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblPassenger_HMS03_Team7 tblPassenger_HMS03_Team7 = db.tblPassenger_HMS03_Team7.Find(id);
            db.tblPassenger_HMS03_Team7.Remove(tblPassenger_HMS03_Team7);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ViewTicket()
        {

            var tblPassenger_HMS03_Team7 = db.tblPassenger_HMS03_Team7.Include(t => t.tblJourney_HMS03_Team7);
            return View(tblPassenger_HMS03_Team7.ToList());
        }

        [Authorize]
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
