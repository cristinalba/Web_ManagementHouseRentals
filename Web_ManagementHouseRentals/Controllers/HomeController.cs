using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Models;

namespace Web_ManagementHouseRentals.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(EmailForm sendMail)
        {
            //if (!ModelState.IsValid) return View();

            //try
            //{
                SmtpClient sc = new SmtpClient();
                MailMessage mail = new MailMessage();


                mail.From = new MailAddress("dinocinel3@gmail.com");
                mail.To.Add(new MailAddress("dinocinel3@gmail.com"));
                mail.Subject = sendMail.Subject;

                mail.IsBodyHtml = true;

                mail.Body = "<br/><br/><b>This customer contacted us:</b><br/>" +
                    $"<b>Name:</b> {sendMail.Name}<br/>" +
                    $"<b>E-mail:</b> {sendMail.Email}<br/><br/>" +
                    "<b>With the following message:</b><br/>" +
                    $"{sendMail.Message}<br/><br/>" +
                    $"On {DateTime.Now}<br/>";

                sc.Host = "smtp.gmail.com";
                sc.Port = 587;

                sc.Credentials = new NetworkCredential("dinocinel3@gmail.com", "dinoPass3");
                sc.EnableSsl = true;

                sc.Send(mail);

                ViewBag.Message = "Message sent!";

                ModelState.Clear();
            //}
            ////catch (Exception ex)
            ////{
            ////    ViewBag.Message = ex.Message.ToString();
            ////}

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }

    }
}
