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
    public class EsperienzeController : Controller
    {
        private ModelDbContext db = new ModelDbContext();
        [Authorize(Roles = "Admin,Azienda")]
        public ActionResult Index()
        {
            var esperienze = db.Esperienze.Include(e => e.Profili).Include(e => e.TipoContratto);
            return View(esperienze.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Esperienze esperienze = db.Esperienze.Find(id);
            if (esperienze == null)
            {
                return HttpNotFound();
            }
            return View(esperienze);
        }
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome");
            ViewBag.Contratto = new SelectList(db.TipoContratto, "idContratto", "NomeContratto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEsperienza,Qualifica,Contratto,NomeAzienda,Localita,SedeLavoro,DataInizio,DataFine,IdProfilo")] Esperienze esperienze)
        {
            if (ModelState.IsValid)
            {
                if (Session["Profilo"] != null)
                {
                    esperienze.IdProfilo = (int)Session["Profilo"];
                }
                db.Esperienze.Add(esperienze);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome", esperienze.IdProfilo);
            ViewBag.Contratto = new SelectList(db.TipoContratto, "idContratto", "NomeContratto", esperienze.Contratto);
            return View(esperienze);
        }

        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Esperienze esperienze = db.Esperienze.Find(id);
            if (esperienze == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome", esperienze.IdProfilo);
            ViewBag.Contratto = new SelectList(db.TipoContratto, "idContratto", "NomeContratto", esperienze.Contratto);
            return View(esperienze);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEsperienza,Qualifica,Contratto,NomeAzienda,Localita,SedeLavoro,DataInizio,DataFine,IdProfilo")] Esperienze esperienze)
        {
            if (ModelState.IsValid)
            {
                db.Entry(esperienze).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome", esperienze.IdProfilo);
            ViewBag.Contratto = new SelectList(db.TipoContratto, "idContratto", "NomeContratto", esperienze.Contratto);
            return View(esperienze);
        }

        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Esperienze esperienze = db.Esperienze.Find(id);
            if (esperienze == null)
            {
                return HttpNotFound();
            }
            return View(esperienze);
        }

        // POST: Esperienze/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Esperienze esperienze = db.Esperienze.Find(id);
            db.Esperienze.Remove(esperienze);
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
