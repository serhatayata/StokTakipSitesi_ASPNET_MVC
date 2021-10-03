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
    public class SatisController : Controller
    {
        // GET: Satis
        StokTakipMVC_DbEntities db = new StokTakipMVC_DbEntities();
        public ActionResult SatisListesi()
        {
            var degerler = db.Tbl_Satislar.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(Tbl_Satislar p1)
        {
            db.Tbl_Satislar.Add(p1);
            db.SaveChanges();
            return RedirectToAction("SatisListesi");
        }
        public ActionResult SatisSil(int id)
        {
            var satis = db.Tbl_Satislar.Find(id);
            db.Tbl_Satislar.Remove(satis);
            db.SaveChanges();
            return RedirectToAction("SatisListesi");
        }
        public ActionResult SatisGetir(int id)
        {
            var satis = db.Tbl_Satislar.Find(id);
            List<SelectListItem> degerler =
               (
                from i in db.Tbl_Urunler.ToList()
                select new SelectListItem
                {
                    Text = i.URUNAD,
                    Value = i.URUNID.ToString()
                }).ToList();
            List<SelectListItem> degerler2 =
               (
                from i in db.Tbl_Musteriler.ToList()
                select new SelectListItem
                {
                    Text = i.MUSTERIAD + " " + i.MUSTERISOYAD,
                    Value = i.MUSTERIID.ToString()
                }).ToList();
            ViewBag.dgr2 = degerler2;
            ViewBag.dgr = degerler;
            return View("SatisGetir", satis);
        }
        public ActionResult SatisGuncelle(Tbl_Satislar p1)
        {
            var satis = db.Tbl_Satislar.Find(p1.SATISID);
            var urn = db.Tbl_Urunler.Where(m => m.URUNID == p1.Tbl_Urunler.URUNID).FirstOrDefault();
            satis.URUN = urn.URUNID;
            var mst = db.Tbl_Musteriler.Where(m => m.MUSTERIID == p1.Tbl_Musteriler.MUSTERIID).FirstOrDefault();
            satis.MUSTERI = mst.MUSTERIID;
            satis.URUNADET = p1.URUNADET;
            satis.URUNFIYAT = p1.URUNFIYAT;
            db.SaveChanges();
            return RedirectToAction("SatisListesi");
        }

       

    }
}