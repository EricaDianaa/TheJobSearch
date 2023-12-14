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
            return View(candidature);
        }

        // GET: Candidature/Create
        public ActionResult Create()
        {
            ViewBag.IdAnnuncio = new SelectList(db.Annunci, "IdAnnuncio", "NomeAnnuncio");
            return View();
        }

        // POST: Candidature/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCandidatura,IdAnnuncio,Curriculum,Descrizione,Stato")] Candidature candidature)
        {
            if (ModelState.IsValid)
            {
                db.Candidature.Add(candidature);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAnnuncio = new SelectList(db.Annunci, "IdAnnuncio", "NomeAnnuncio", candidature.IdAnnuncio);
            return View(candidature);
        }

        // GET: Candidature/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.IdAnnuncio = new SelectList(db.Annunci, "IdAnnuncio", "NomeAnnuncio", candidature.IdAnnuncio);
            return View(candidature);
        }

        // POST: Candidature/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCandidatura,IdAnnuncio,Curriculum,Descrizione,Stato")] Candidature candidature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAnnuncio = new SelectList(db.Annunci, "IdAnnuncio", "NomeAnnuncio", candidature.IdAnnuncio);
            return View(candidature);
        }

        // GET: Candidature/Delete/5
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

        // POST: Candidature/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidature candidature = db.Candidature.Find(id);
            db.Candidature.Remove(candidature);
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
