using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReclamaTche.Models;

namespace ReclamaTche.Controllers
{
    public class HomeController : Controller
    {

        private ReclamaDBContext db = new ReclamaDBContext();

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Reclamacoes");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Sobre o ReclamaPOA";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "ReclamaPOA";

            return View();
        }

    }
}