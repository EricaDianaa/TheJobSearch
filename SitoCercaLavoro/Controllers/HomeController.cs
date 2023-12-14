using SitoCercaLavoro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SitoCercaLavoro.Controllers
{
    public class HomeController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        public ActionResult Index()
        {

            return View();
        }

        //Autentificazione
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Username, Password,Indirizzo,Email,Telefono,Ruolo,PartitaIva,CodiceFiscale")] Utenti u, bool IsAzienda, string Username, string CodiceFiscale, string Email)
        {
            if (ModelState.IsValid)
            {
                //Validazione Username/CodiceFiscale/Email 
                Utenti utente = db.Utenti.FirstOrDefault(m => m.Username == Username);
                Utenti utente1 = db.Utenti.FirstOrDefault(m => m.CodiceFiscale == CodiceFiscale);
                Utenti utente2 = db.Utenti.FirstOrDefault(m => m.Email == Email);
                //Messaggio di errore in caso Username sia già presenete nel database
                if (utente1 != null)
                {
                    ViewBag.Username = "Username non disponibile";

                }
                //Messaggio di errore in caso Email sia già presenete nel database
                if (utente2 != null)
                {
                    ViewBag.Email = "Email non disponibile";

                }
                //Messaggio di errore in caso CodiceFiscale sia già presenete nel database
                if (utente1 != null)
                {
                    ViewBag.CodiceFiscale = "Codice fiscale non disponibile";

                }
                try
                {
                    //Cripto la password 
                    using (var context = new ModelDbContext())
                    {
                        var chkUser = (from s in context.Utenti where s.Username == u.Username || s.Email == u.Email select s).FirstOrDefault();
                        if (chkUser == null)
                        {
                            var keyNew = hash.GeneratePassword(10);
                            var password = hash.EncodePassword(u.Password, keyNew);

                            //Se l'utente non è un admin assegno il ruolo User o Azienda
                            if (!User.IsInRole("Admin") || !User.IsInRole("Azienda"))
                            {
                                // Se è un azienda assegna ruolo come Azienda 
                                if (IsAzienda == true)
                                {
                                    u.Ruolo = "Azienda";
                                    u.IsAzienda = IsAzienda;
                                }
                                //Altrimenti come User
                                else
                                {
                                    u.Ruolo = "User";
                                    u.IsAzienda = IsAzienda;
                                }
                            }
                            //Sicurezza password
                            if (!Regex.IsMatch(u.Password, @"\d"))
                            {
                                ViewBag.ErroreNumerico = "la password deve contenere un carattere numerico";

                            }

                            if (!Regex.IsMatch(u.Password, @"[@#$%^&+=]"))
                            {
                                ViewBag.ErroreCarattereSpeciale = "la password deve contenere un carattere speciale";

                            }
                            if (!Regex.IsMatch(u.Password, @"[A-Z]"))
                            {
                                ViewBag.ErroreletteraMaiuscola = "la password deve contenere una lettera maiuscola";

                            }
                            if (Regex.IsMatch(u.Password, @"\d") && Regex.IsMatch(u.Password, @"[@#$%^&+=]") && Regex.IsMatch(u.Password, @"[A-Z]"))
                            {
                                //Se Username/Email/CodiceFiscale non sono presenti nel database salvo l'utente
                                if (utente == null && utente1 == null && utente2 == null)
                                {
                                    u.Password = password;
                                    u.Vcode = keyNew;
                                    db.Utenti.Add(u);
                                    db.SaveChanges();
                                    ModelState.Clear();
                                    return RedirectToAction("Login", "Home");

                                }
                            }




                            //Altrimenti rimando la pagin messaggi errore
                            else
                            {
                                return View();
                            }

                        }
                        ViewBag.ErrorMessage = "Username gia esistente";
                        return View();
                    }
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = "Some exception occured" + e;
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "IdUtente,Username, Password")] Utenti u)
        {
            try
            {
                using (var context = new ModelDbContext())
                {

                    var getUser = (from s in context.Utenti where s.Username == u.Username || s.Email == u.Username select s).FirstOrDefault();
                    if (getUser != null)
                    {
                        // Controllo se Username e password concidono
                        var hashCode = getUser.Vcode;
                        var encodingPasswordString = hash.EncodePassword(u.Password, hashCode);
                        var query = (from s in context.Utenti where (s.Username == u.Username || s.Email == u.Username) && s.Password.Equals(encodingPasswordString) select s).FirstOrDefault();
                        if (query != null)
                        {
                            //Se concidono creo il cookie per l'autentificazione 
                            FormsAuthentication.SetAuthCookie(u.Username, false);
                            Session["Utente"] = query.IdUtente;
                            Session.Timeout = 90;
                            return RedirectToAction("Index", "Home");
                        }
                        ViewBag.ErrorMessage = "Username o password non validi";
                        return View();
                    }
                    ViewBag.ErrorMessage = "Username o password non validi";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "Errore";
                return View();
            }
        }
        public ActionResult Logout()
        {
            //elimino il cookie dell'autentificazione per il logout
            Session["Cliente"] = null;
            FormsAuthentication.SignOut();
            Session["Utente"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
