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
    public class FormazioneController : Controller
    {
        private ModelDbContext db = new ModelDbContext();


        public ActionResult Index()
        {
            var formazione = db.Formazione.Include(f => f.Profili);
            return View(formazione.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formazione formazione = db.Formazione.Find(id);
            if (formazione == null)
            {
                return HttpNotFound();
            }
            return View(formazione);
        }


        public ActionResult Create()
        {
            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFormazione,Scuola,TitoloStudio,NomeStudio,DataInizio,DataFine,Votazione,IdProfilo")] Formazione formazione)
        {
            if (ModelState.IsValid)
            {
                if (Session["Profilo"] != null)
                {
                    formazione.IdProfilo = (int)Session["Profilo"];
                }
                db.Formazione.Add(formazione);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome", formazione.IdProfilo);
            return View(formazione);
        }

        // GET: Formazione/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formazione formazione = db.Formazione.Find(id);
            if (formazione == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome", formazione.IdProfilo);
            return View(formazione);
        }

        // POST: Formazione/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFormazione,Scuola,TitoloStudio,NomeStudio,DataInizio,DataFine,Votazione,IdProfilo")] Formazione formazione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formazione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome", formazione.IdProfilo);
            return View(formazione);
        }

        // GET: Formazione/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formazione formazione = db.Formazione.Find(id);
            if (formazione == null)
            {
                return HttpNotFound();
            }
            return View(formazione);
        }

        // POST: Formazione/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Formazione formazione = db.Formazione.Find(id);
            db.Formazione.Remove(formazione);
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
