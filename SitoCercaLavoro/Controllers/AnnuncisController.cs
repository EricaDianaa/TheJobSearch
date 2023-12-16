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
    public class AnnuncisController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        public ActionResult Index()
        {
            var annunci = db.Annunci.Include(a => a.TipoContratto1);
            return View(annunci.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annunci annunci = db.Annunci.Find(id);
            if (annunci == null)
            {
                return HttpNotFound();
            }
            return View(annunci);
        }


        public ActionResult Create()
        {
            ViewBag.TipoContratto = new SelectList(db.TipoContratto, "idContratto", "NomeContratto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAnnuncio,NomeAnnuncio,Retribuzione,Descrizione,Categoria,SedeLavoro,Luogo,TipoContratto")] Annunci annunci)
        {
            if (ModelState.IsValid)
            {
                db.Annunci.Add(annunci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoContratto = new SelectList(db.TipoContratto, "idContratto", "NomeContratto", annunci.TipoContratto);
            return View(annunci);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annunci annunci = db.Annunci.Find(id);
            if (annunci == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoContratto = new SelectList(db.TipoContratto, "idContratto", "NomeContratto", annunci.TipoContratto);
            return View(annunci);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAnnuncio,NomeAnnuncio,Retribuzione,Descrizione,Categoria,SedeLavoro,Luogo,TipoContratto")] Annunci annunci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(annunci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoContratto = new SelectList(db.TipoContratto, "idContratto", "NomeContratto", annunci.TipoContratto);
            return View(annunci);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annunci annunci = db.Annunci.Find(id);
            if (annunci == null)
            {
                return HttpNotFound();
            }
            return View(annunci);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Annunci annunci = db.Annunci.Find(id);
         List<   Candidature> c =db.Candidature.Where(m=>m.IdAnnuncio==id).ToList();  
            if(c.Count!=0)
            {
                foreach( Candidature candid in c)
                {
                    db.Candidature.Remove(candid);
                }
            }

            db.Annunci.Remove(annunci);

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
