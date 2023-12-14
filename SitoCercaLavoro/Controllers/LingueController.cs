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
    public class LingueController : Controller
    {
        private ModelDbContext db = new ModelDbContext();


        public ActionResult Index()
        {
            var lingue = db.Lingue.Include(l => l.Profili);
            return View(lingue.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lingue lingue = db.Lingue.Find(id);
            if (lingue == null)
            {
                return HttpNotFound();
            }
            return View(lingue);
        }


        public ActionResult Create()
        {
            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdLingua,Lingua,Conoscenza,IdProfilo")] Lingue lingue)
        {
            if (ModelState.IsValid)
            {
                if (Session["Profilo"] != null)
                {
                    lingue.IdProfilo = (int)Session["Profilo"];
                }
                db.Lingue.Add(lingue);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome", lingue.IdProfilo);
            return View(lingue);
        }

        // GET: Lingue/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lingue lingue = db.Lingue.Find(id);
            if (lingue == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome", lingue.IdProfilo);
            return View(lingue);
        }

        // POST: Lingue/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdLingua,Lingua,Conoscenza,IdProfilo")] Lingue lingue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lingue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProfilo = new SelectList(db.Profili, "IdProfilo", "Nome", lingue.IdProfilo);
            return View(lingue);
        }

        // GET: Lingue/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lingue lingue = db.Lingue.Find(id);
            if (lingue == null)
            {
                return HttpNotFound();
            }
            return View(lingue);
        }

        // POST: Lingue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lingue lingue = db.Lingue.Find(id);
            db.Lingue.Remove(lingue);
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
