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
    public class AttestatiCertificazioniController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        [Authorize(Roles = "Admin,Azienda")]
        public ActionResult Index()
        {
            var attestatiCertificazioni = db.AttestatiCertificazioni.Include(a => a.Profili);
            return View(attestatiCertificazioni.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttestatiCertificazioni attestatiCertificazioni = db.AttestatiCertificazioni.Find(id);
            if (attestatiCertificazioni == null)
            {
                return HttpNotFound();
            }
            return View(attestatiCertificazioni);
        }

        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCertificazione,Nome,Descrizione,IdProfilo")] AttestatiCertificazioni attestatiCertificazioni)
        {
            if (ModelState.IsValid)
            {
                if (Session["Profilo"] != null)
                {
                    attestatiCertificazioni.IdProfilo = (int)Session["Profilo"];
                }

                db.AttestatiCertificazioni.Add(attestatiCertificazioni);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome", attestatiCertificazioni.IdProfilo);
            return View(attestatiCertificazioni);
        }

        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttestatiCertificazioni attestatiCertificazioni = db.AttestatiCertificazioni.Find(id);
            if (attestatiCertificazioni == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome", attestatiCertificazioni.IdProfilo);
            return View(attestatiCertificazioni);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCertificazione,Nome,Descrizione,IdProfilo")] AttestatiCertificazioni attestatiCertificazioni)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attestatiCertificazioni).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome", attestatiCertificazioni.IdProfilo);
            return View(attestatiCertificazioni);
        }
        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttestatiCertificazioni attestatiCertificazioni = db.AttestatiCertificazioni.Find(id);
            if (attestatiCertificazioni == null)
            {
                return HttpNotFound();
            }
            return View(attestatiCertificazioni);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AttestatiCertificazioni attestatiCertificazioni = db.AttestatiCertificazioni.Find(id);
            db.AttestatiCertificazioni.Remove(attestatiCertificazioni);
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
