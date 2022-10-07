using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservaHotel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Nuestra Mision.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Logout()
        {
            ViewBag.Message = "Cerrando Sesion";

            Session.Clear();

            return RedirectToAction("Authenticate", "Cliente");
        }

        public ActionResult Galeria()
        {
            ViewBag.Message = "Galeria Fotografica";
            return View();
        }
    }
}