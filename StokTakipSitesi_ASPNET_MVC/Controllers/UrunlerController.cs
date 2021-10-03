using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakipSitesi_ASPNET_MVC.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace StokTakipSitesi_ASPNET_MVC.Controllers
{
    public class UrunlerController : Controller
    {
        // GET: Urunler
        StokTakipMVC_DbEntities db = new StokTakipMVC_DbEntities();
        public ActionResult UrunlerListesi()
        {
            var degerler = db.Tbl_Urunler.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler =
                (
                 from i in db.Tbl_Kategoriler.ToList()
                 select new SelectListItem
                 {
                     Text = i.KATEGORIAD,
                     Value = i.KATEGORIID.ToString()
                 }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Tbl_Urunler p1)
        {
            var ktg = db.Tbl_Kategoriler.Where(m => m.KATEGORIID == p1.Tbl_Kategoriler.KATEGORIID).FirstOrDefault();
            p1.Tbl_Kategoriler = ktg;
            db.Tbl_Urunler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("UrunlerListesi");
        }
        public ActionResult UrunSil(int id)
        {
            var urun = db.Tbl_Urunler.Find(id);
            db.Tbl_Urunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("UrunlerListesi");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.Tbl_Urunler.Find(id);
            List<SelectListItem> degerler =
               (
                from i in db.Tbl_Kategoriler.ToList()
                select new SelectListItem
                {
                    Text = i.KATEGORIAD,
                    Value = i.KATEGORIID.ToString()
                }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir",urun);
        }
        public ActionResult UrunGuncelle(Tbl_Urunler p1)
        {
            var urun = db.Tbl_Urunler.Find(p1.URUNID);
            urun.URUNAD = p1.URUNAD;
            urun.URUNMARKA = p1.URUNMARKA;
            var ktg = db.Tbl_Kategoriler.Where(m => m.KATEGORIID == p1.Tbl_Kategoriler.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;
            urun.URUNSTOK = p1.URUNSTOK;
            urun.URUNFIYAT = p1.URUNFIYAT;
            //urun.URUNKATEGORI = p1.URUNKATEGORI;
            db.SaveChanges();
            return RedirectToAction("UrunlerListesi");
        }
    }
}