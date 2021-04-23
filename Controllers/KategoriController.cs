using BlogMusic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMusic.Controllers
{
    public class KategoriController : Controller
    {
        MusicContext context = new MusicContext();

        public ActionResult Index()
        {
            try
            {
                var kategoriler = context.Kategoris.ToList();
                return View(kategoriler);
            }
            catch (Exception)
            {
                return RedirectToAction("/Home/Error/");
            }

        }

        public ActionResult Sarkilar(int? id)
        {
            try
            {
                var secilictg = context.Kategoris.FirstOrDefault(i => i.Id == id);
                var sarkilar = context.Musics.Where(i => i.KategoriId == id).ToList();

                if (sarkilar != null && secilictg!=null)
                {
                    ViewBag.KategoriAd = secilictg.KategoriAdi;
                    return View(sarkilar);
                }
                return RedirectToAction("/Home/Error/");
            }
            catch (Exception)
            {
                return RedirectToAction("/Home/Error/");

            }
        }
    }
}