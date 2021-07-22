using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokProjesi.Models.Entity;

namespace MvcStokProjesi.Controllers
{
    public class SatisController : Controller
    {
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(TblSatıslar p)
        {
            db.TblSatıslar.Add(p);
            db.SaveChanges();
            return View("Index");
        }
    }
}