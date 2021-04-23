using BlogMusic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMusic.Controllers
{
    public class SanatciController : Controller
    {
        MusicContext context = new MusicContext();
        public ActionResult Sanatcilar()
        {
            try
            {
                SanatciViewModel model = new SanatciViewModel();
                model.Sanatcilar = context.Sanatcis.ToList();
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("/Home/Error/");
            }

        }

        [HttpGet]
        public ActionResult SanatciDetay(int? id)
        {
            try
            {
                SanatciViewModel model = new SanatciViewModel();
                var secili = context.Sanatcis.FirstOrDefault(i => i.Id == id);
                if (secili != null)
                {
                    model.Sanatci = secili;
                    model.Sarkilar = context.Musics.Where(i => i.SanatciId == id).OrderByDescending(i => i.CikisTarihi).ToList();
                    model.Comments = context.Comments.Where(i => i.FormName == "Sanatci" && i.BaslikId == id && i.Activated == true);
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
        public ActionResult SanatciDetay(Comment comment, int id)
        {

            try
            {
            if (ModelState.IsValid)
            {
                    comment.Activated = false;
                    comment.YorumTarihi = DateTime.Now;
                    comment.FormName = "Sanatci";
                    comment.BaslikId = id;
                context.Comments.Add(comment);
                context.SaveChanges();
                TempData["SanatciYorumMessage"] = "Yorumunuz başarıyla gönderildi. Onaylandıktan sonra görünecektir.";
                return RedirectToAction("SanatciDetay");
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
