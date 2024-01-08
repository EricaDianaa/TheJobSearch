using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SitoCercaLavoro.Models;

namespace SitoCercaLavoro.Controllers
{
    public class CompetenzeController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        [Authorize(Roles = "Admin,Azienda")]
        public ActionResult Index()
        {
            var competenze = db.Competenze.Include(c => c.Profili);
            return View(competenze.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competenze competenze = db.Competenze.Find(id);
            if (competenze == null)
            {
                return HttpNotFound();
            }
            return View(competenze);
        }

        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCompetenza,NomeCompetenza,Descrizione,IdProfilo")] Competenze competenze)
        {
            if (ModelState.IsValid)
            {
                if (Session["Profilo"] != null)
                {
                    competenze.IdProfilo = (int)Session["Profilo"];
                }
                db.Competenze.Add(competenze);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome", competenze.IdProfilo);
            return View(competenze);
        }
        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competenze competenze = db.Competenze.Find(id);
            if (competenze == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome", competenze.IdProfilo);
            return View(competenze);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCompetenza,NomeCompetenza,Descrizione,IdProfilo")] Competenze competenze)
        {
            if (ModelState.IsValid)
            {
                db.Entry(competenze).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome", competenze.IdProfilo);
            return View(competenze);
        }

        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competenze competenze = db.Competenze.Find(id);
            if (competenze == null)
            {
                return HttpNotFound();
            }
            return View(competenze);
        }

   
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Competenze competenze = db.Competenze.Find(id);
            db.Competenze.Remove(competenze);
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
