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
    public class UtentiController : Controller
    {
        private ModelDbContext db = new ModelDbContext();
        [Authorize(Roles = "Admin,Azienda")]
        public ActionResult Index()
        {
            return View(db.Utenti.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utenti utenti = db.Utenti.Find(id);
            if (utenti == null)
            {
                return HttpNotFound();
            }
            return View(utenti);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUtente,Username,Password,Ruolo,Indirizzo,CodiceFiscale,IsAzienda,PartitaIva,Vcode,Email")] Utenti utenti)
        {
            if (ModelState.IsValid)
            {
                db.Utenti.Add(utenti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(utenti);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utenti utenti = db.Utenti.Find(id);
            if (utenti == null)
            {
                return HttpNotFound();
            }
            return View(utenti);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUtente,Username,Password,Ruolo,Indirizzo,CodiceFiscale,IsAzienda,PartitaIva,Vcode,Email")] Utenti utenti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utenti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utenti);
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
