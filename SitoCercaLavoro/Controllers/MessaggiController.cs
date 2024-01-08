using SitoCercaLavoro.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SitoCercaLavoro.Controllers
{
    public class MessaggiController : Controller
    {
        ModelDbContext db=new ModelDbContext();
        public ActionResult Index()
        {
            
            // Leggi le informazioni di configurazione dal file Web.config
            string mittente = ConfigurationManager.AppSettings["EmailMittente"];
            string destinatario = "destinatario@email.com";
            string oggetto = "Candidatura inviata";
            string corpoMessaggio = "Candidatura inviata con sucesso per ";

            // Configura il client SMTP
            SmtpClient clientSmtp = new SmtpClient();

            // Crea il messaggio email
            MailMessage messaggio = new MailMessage(mittente, destinatario, oggetto, corpoMessaggio);
            messaggio.IsBodyHtml = true;

            try
            {
                // Invia l'email
                clientSmtp.Send(messaggio);
                ViewBag.Messaggio = "Email inviata con successo!";
            }
            catch (Exception ex)
            {
                ViewBag.Messaggio = $"Errore durante l'invio dell'email: {ex.Message}";
            }

            return View();

        }
    }
}