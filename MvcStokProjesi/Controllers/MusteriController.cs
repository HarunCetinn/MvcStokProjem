using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokProjesi.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStokProjesi.Controllers
{
    public class MusteriController : Controller
    {
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Index(int sayfa = 1)
        {
            //var degerler2 = from d in db.TblMusteriler select d;
            //return View(degerler2.ToList()); //Arama panelinin gereksiz olduğunu düşünüyorum, yapmayacağım.
            //var degerler = db.TblMusteriler.ToList();
            var degerler = db.TblMusteriler.ToList().ToPagedList(sayfa,4);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(TblMusteriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TblMusteriler.Add(p1);
            db.SaveChanges();
            return View();
        }

        public ActionResult Sil(int id)
        {
            var musteri = db.TblMusteriler.Find(id);
            db.TblMusteriler.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.TblMusteriler.Find(id);
            return View("MusteriGetir", musteri);
        }

        public ActionResult Guncelle(TblMusteriler p1)
        {
            var musteri = db.TblMusteriler.Find(p1.MusteriId);
            musteri.MusteriAd = p1.MusteriAd;
            musteri.MusteriSoyad = p1.MusteriSoyad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}