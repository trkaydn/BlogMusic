using BlogMusic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMusic.Controllers
{
    public class GundemController : Controller
    {
        MusicContext context = new MusicContext();
        public ActionResult TumHaberler()
        {
            try
            {
                var haberler = context.Gundems.OrderByDescending(i => i.Tarih).ToList();
                return View(haberler);
            }
            catch (Exception)
            {

                return RedirectToAction("/Home/Error/");
            }

        }

        [HttpGet]
        public ActionResult HaberDetay(int? id)
        {
            try
            {
                GundemViewModel model = new GundemViewModel();
                var secili = context.Gundems.FirstOrDefault(i => i.Id == id);
                model.Gundem = secili;
                if (secili != null)
                {
                    model.Comments = context.Comments.Where(i => i.FormName == "Gundem" && i.BaslikId == id && i.Activated == true).OrderByDescending(i => i.YorumTarihi).ToList();
                    return View(model);
                }
                return RedirectToAction("/Home/Error/");

            }
            catch (Exception)
            {
                return RedirectToAction("/Home/Error/");
            }
        }

        [HttpPost]
        public ActionResult HaberDetay(Comment comment, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    comment.Activated = false;
                    comment.YorumTarihi = DateTime.Now;
                    comment.FormName = "Gundem";
                    comment.BaslikId = id;
                    context.Comments.Add(comment);
                    context.SaveChanges();
                    TempData["HaberDetayMessage"] = "Yorumunuz başarıyla gönderildi. Onaylandıktan sonra görünecektir.";

                    return RedirectToAction("HaberDetay");
                }
                else
                    return View();
            }
            catch (Exception)
            {

                return RedirectToAction("/Home/Error/");
            }

        }
    }
}