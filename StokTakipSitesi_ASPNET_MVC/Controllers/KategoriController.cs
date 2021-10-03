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
    public class KategoriController : Controller
    {
        // GET: Kategori
        StokTakipMVC_DbEntities db = new StokTakipMVC_DbEntities();
        public ActionResult KategoriListesi()
        {
            //var degerler = db.Tbl_Kategoriler.ToList();
            var degerler = db.Tbl_Kategoriler.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKategori() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(Tbl_Kategoriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.Tbl_Kategoriler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("KategoriListesi");
        }
        public ActionResult KategoriSil(int id)
        {
            var kategori = db.Tbl_Kategoriler.Find(id);
            db.Tbl_Kategoriler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("KategoriListesi");

        }
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.Tbl_Kategoriler.Find(id);
            return View("KategoriGetir", ktgr);
        }
        public ActionResult KategoriGuncelle(Tbl_Kategoriler p1)
        {
            var ktg = db.Tbl_Kategoriler.Find(p1.KATEGORIID);
            ktg.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("KategoriListesi");
        }
    }
}