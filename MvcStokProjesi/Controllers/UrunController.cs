﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokProjesi.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStokProjesi.Controllers
{
    public class UrunController : Controller
    {
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Index(int sayfa = 1)
        {
            //var degerler = db.TblUrunler.ToList();
            var degerler = db.TblUrunler.ToList().ToPagedList(sayfa,4);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.TblKategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriId.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(TblUrunler p1)
        {
            var ktg = db.TblKategoriler.Where(m => m.KategoriId == p1.TblKategoriler.KategoriId).FirstOrDefault();
            p1.TblKategoriler = ktg;
            db.TblUrunler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var urun = db.TblUrunler.Find(id);
            db.TblUrunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var urun = db.TblUrunler.Find(id);
            List<SelectListItem> degerler = (from i in db.TblKategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriId.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);
        }

        public ActionResult Guncelle(TblUrunler p)
        {
            var urun = db.TblUrunler.Find(p.UrunId);
            urun.UrunAd = p.UrunAd;
            urun.Marka = p.Marka;
            urun.Stok = p.Stok;
            urun.Fiyat = p.Fiyat;
            //urun.UrunKategori = p.UrunKategori;
            var ktg = db.TblKategoriler.Where(m => m.KategoriId == p.TblKategoriler.KategoriId).FirstOrDefault();
            urun.UrunKategori = ktg.KategoriId;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}