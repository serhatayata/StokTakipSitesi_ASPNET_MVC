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
    public class MusteriController : Controller
    {
        StokTakipMVC_DbEntities db = new StokTakipMVC_DbEntities();
        // GET: Musteri
        public ActionResult MusterilerListesi()
        {
            //var degerler = db.Tbl_Musteriler.ToList();
            var degerler = db.Tbl_Musteriler.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(Tbl_Musteriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.Tbl_Musteriler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("MusterilerListesi");
        }
        public ActionResult MusteriSil(int id)
        {
            var mstr = db.Tbl_Musteriler.Find(id);
            db.Tbl_Musteriler.Remove(mstr);
            db.SaveChanges();
            return RedirectToAction("MusterilerListesi");
        }
        public ActionResult MusteriGetir(int id)
        {
            var mst = db.Tbl_Musteriler.Find(id);
            return View(mst);
        }
        public ActionResult MusteriGuncelle(Tbl_Musteriler p1)
        {
            var musteri = db.Tbl_Musteriler.Find(p1.MUSTERIID);
            musteri.MUSTERIAD = p1.MUSTERIAD;
            musteri.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("MusterilerListesi");
        }
     
    }
}