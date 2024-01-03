using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Web;
using System.Web.Mvc;
using SitoCercaLavoro.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SitoCercaLavoro.Controllers
{
    public class CandidatureController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Candidature
        public ActionResult Index()
        {
            var candidature = db.Candidature.Include(c => c.Annunci);
            return View(candidature.ToList());
        }

        // GET: Candidature/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidature candidature = db.Candidature.Find(id);
            if (candidature == null)
            {
                return HttpNotFound();
            }
               var p = db.Profili.Where(m => m.IdProfilo == candidature.idProfili).ToList();
            if (p != null ||p.Count!=0)
            {
                ViewBag.Profili = p;
            }
            else
            {
                ViewBag.Profili = null;
            }

            return View(candidature);
        }

 
        public ActionResult Create()
        {
            ViewBag.IdAnnuncio = new SelectList(db.Annunci, "IdAnnuncio", "NomeAnnuncio");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCandidatura,IdAnnuncio,Curriculum,Descrizione,Stato")] Candidature candidature, HttpPostedFileBase Curriculum,int ?id)
        {
            if (ModelState.IsValid)
            {
                if (TempData["Curriculum"] != null)
                {
                    candidature.Curriculum = (string)TempData["Curriculum"];
                }
                else
                {
                    if (Curriculum != null && Curriculum.ContentLength > 0)
                    {
                        string Curriculum1File = Curriculum.FileName;
                        string Curriculum1Path = Path.Combine(Server.MapPath("~/Content/FileCurriculum"), Curriculum1File);
                        Curriculum.SaveAs(Curriculum1Path);
                        candidature.Curriculum = Curriculum1File;
                    }
                }
                
                if (Session["Utente"] != null&& Session["Profilo"] != null)
                {
                candidature.idProfili = (int)Session["Profilo"];
                candidature.IdAnnuncio = (int)(id);
                db.Candidature.Add(candidature);
                db.SaveChanges();
                }
              
                return RedirectToAction("Index");
            }

            ViewBag.IdAnnuncio = new SelectList(db.Annunci, "IdAnnuncio", "NomeAnnuncio", candidature.IdAnnuncio);
            return View(candidature);
        }

 
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidature candidature = db.Candidature.Find(id);
            if (candidature == null)
            {
                return HttpNotFound();
            }
            return View(candidature);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidature candidature = db.Candidature.Find(id);
            db.Candidature.Remove(candidature);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DownloadFile(HttpPostedFileBase Curriculum1, string Curriculum)
        {
            if (Curriculum1 != null && Curriculum1.ContentLength > 0)
            {
                string Curriculum1File = Curriculum1.FileName;
                string Curriculum1Path = Path.Combine(Server.MapPath("~/Content/FileCurriculum"), Curriculum1File);
                Curriculum1.SaveAs(Curriculum1Path);
                TempData["Curriculum"] = Curriculum1File;
            }
            return File("~/Content/FileCurriculum/" + Curriculum1, "application / pdf");
        }

        public ActionResult LeTueCandidature()
        {

            if (Session["Profilo"] != null)
            {    
                int id = (int)Session["Profilo"];
                List<Candidature> c= db.Candidature.Where(m=>m.idProfili==id).Include(m => m.Annunci).ToList();
                return View(c);
            }
            return View();
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
