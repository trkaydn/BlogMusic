using BlogMusic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMusic.Controllers
{
    public class HomeController : Controller
    {
        MusicContext db = new MusicContext();

        public ActionResult Index()
        {

            return View();

        }
        public PartialViewResult PartialIndex()
        {

            MusicViewModel model = new MusicViewModel
            {
                YeniEklenen = db.Musics.OrderByDescending(i => i.Id).Take(3).ToList(),
                CokDinlenen = db.Musics.OrderByDescending(i => i.Puan).Take(3).ToList(),
                TopGundem = db.Gundems.OrderByDescending(i => i.Tarih).Take(3).ToList()
            };
            return PartialView(model);

        }

        [HttpGet]
        public ActionResult BizeUlasin()
        {

            return View();

        }

        [HttpPost]
        public ActionResult BizeUlasin(Message message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    message.Tarih = DateTime.Now;
                    db.Messages.Add(message);
                    db.SaveChanges();
                    TempData["BizeUlasinMessage"] = "Mesajınız başarıyla iletildi.";
                    return View();
                }
                else
                    return RedirectToAction("/Home/Error/");

            }
            catch (Exception)
            {
                return RedirectToAction("/Home/Error/");
            }
        }

        public ActionResult Hakkimizda()
        {
            Admin me = db.Admins.FirstOrDefault(i => i.Id == 1);
            return View(me);
        }


        public ActionResult Search(string text)
        {
            try
            {
                ViewBag.Aranan = text;
                SearchViewModel model = new SearchViewModel();

                var sanatci = db.Sanatcis.FirstOrDefault(i => i.SanatciAdi.Contains(text));
                if (sanatci != null)
                    model.Sarkilar = db.Musics.Where(i => i.SarkiAd.Contains(text) || i.SarkiSozleri.Contains(text) || i.SanatciId == sanatci.Id).ToList();
                else
                    model.Sarkilar = db.Musics.Where(i => i.SarkiAd.Contains(text) || i.SarkiSozleri.Contains(text)).ToList();

                model.Sanatcilar = db.Sanatcis.Where(i => i.SanatciAdi.Contains(text)).ToList();
                model.Gundemler = db.Gundems.Where(i => i.Baslik.Contains(text) || i.Icerik.Contains(text)).ToList();
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("/Home/Error/");

            }

        }

        public ActionResult Error()
        {
            return View();
        }


    }
}