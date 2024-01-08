using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SitoCercaLavoro.Models;

namespace SitoCercaLavoro.Controllers
{
    public class ProfiliController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        [Authorize(Roles = "Admin,Azienda")]
        public ActionResult Index()
        {
            var profili = db.Profili.Include(p => p.Utenti);
            return View(profili.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profili profili = db.Profili.Find(id);
            Session["Profilo"] = profili.IdProfilo;
            if (profili == null)
            {
                return HttpNotFound();
            }
            return View(profili);
        }

        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Username");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProfilo,Nome,Cognome,Email,Telefono,Foto,Presentazione")] Profili profili, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto != null && Foto.ContentLength > 0)
                {
                    string Foto1File = Foto.FileName;
                    string Foto1Path = Path.Combine(Server.MapPath("~/Content/FotoProfili"), Foto1File);
                    Foto.SaveAs(Foto1Path);
                    profili.Foto = Foto1File;
                }
                if (Session["Utente"] != null)
                {
                    profili.IdUtente = (int)Session["Utente"];
                db.Profili.Add(profili);
                db.SaveChanges();
                }
                
                
                Profili p = db.Profili.FirstOrDefault(m=>m.Nome==profili.Nome&&m.Cognome==profili.Cognome&&m.Email==profili.Email);
                Session["Profilo"] = p.IdProfilo;
                if (User.IsInRole("Admin")|| User.IsInRole("Azienda"))
                {
                   return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Home");
                }
               
            }

            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Username", profili.IdUtente);
            return View(profili);
        }

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profili profili = db.Profili.Find(id);
            if (profili == null)
            {
                return HttpNotFound();
            }
            //Salvo le immagini in un Tempdata se ci sono (per salvarle poi successivamente)
            if (profili.Foto != null)
            {
                TempData["Foto"] = profili.Foto;
            }
            else
            {
                TempData["Foto"] = "";
            }
            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Username", profili.IdUtente);
            return View(profili);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProfilo,Nome,Cognome,Email,Telefono,Foto,Presentazione,IdUtente")] Profili profili, HttpPostedFileBase Foto )
        {
            if (ModelState.IsValid)
            {
                //Se l'immagine è presente la salvo
                if (Foto != null && Foto.ContentLength > 0)
                {
                    string FotoFile = Foto.FileName;
                    string FotoPath = Path.Combine(Server.MapPath("~/Content/FotoProfili"), FotoFile);
                    Foto.SaveAs(FotoPath);
                    profili.Foto = FotoFile;
                }
                //Altimenti mi salvo la foto che ho salvato nella Get dell'edit
                else
                {
                    profili.Foto = TempData["Foto"].ToString();
                }
                db.Entry(profili).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Username", profili.IdUtente);
            return View(profili);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profili profili = db.Profili.Find(id);
            if (profili == null)
            {
                return HttpNotFound();
            }
            return View(profili);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profili profili = db.Profili.Find(id);
            //Prima di eliminare il profilo devo eliminare tutti gli elementi associati
          List<  Esperienze> e = db.Esperienze.Where(m=>m.IdProfilo==id).ToList();
            if (e.Count!=0)
            {
                foreach (Esperienze es in e)
                {
                        db.Esperienze.Remove(es);
                }
            
            }
            List<Formazione> f = db.Formazione.Where(m => m.IdProfilo == id).ToList();
            if (f.Count!=0)
            {
                foreach (Formazione fo in f)
                {
                      db.Formazione.Remove(fo);
                }
             
            }

            List<AttestatiCertificazioni> a = db.AttestatiCertificazioni.Where(m => m.IdProfilo == id).ToList();
            if (a.Count!=0)
            {
                foreach (AttestatiCertificazioni at in a)
                {
                    db.AttestatiCertificazioni.Remove(at);
                }
                
            }
            List<Competenze> c = db.Competenze.Where(m => m.IdProfilo == id).ToList();
            if (c.Count!=0)
            {
                foreach (Competenze co in c)
                {
                       db.Competenze.Remove(co);
                }
            
            }
            List<Lingue> l = db.Lingue.Where(m => m.IdProfilo == id).ToList();
            if (l.Count!=0)
            {
                foreach (Lingue li in l)
                {
                     db.Lingue.Remove(li);
                }
               
            }
            db.Profili.Remove(profili);
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
