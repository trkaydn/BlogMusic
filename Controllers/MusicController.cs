using BlogMusic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMusic.Controllers
{
    public class MusicController : Controller
    {
        MusicContext context = new MusicContext();

        [HttpGet]
        public ActionResult MusicDetay(int? id)
        {
            try
            {
                MusicDetayViewModel model = new MusicDetayViewModel();
                var secili = context.Musics.FirstOrDefault(i => i.Id == id);
                if (secili != null)
                {
                    model.Music = secili;
                    model.Sanatci = context.Sanatcis.FirstOrDefault(i => i.Id == model.Music.SanatciId).SanatciAdi;
                    model.Kategori = context.Kategoris.FirstOrDefault(i => i.Id == model.Music.KategoriId).KategoriAdi;
                    model.Comments = context.Comments.Where(i => i.FormName == "Music" && i.BaslikId == id && i.Activated == true);
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
        public ActionResult MusicDetay(Comment comment, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    comment.Activated = false;
                    comment.YorumTarihi = DateTime.Now;
                    comment.FormName = "Music";
                    comment.BaslikId = id;
                    context.Comments.Add(comment);
                    context.SaveChanges();
                    TempData["MusicYorumMessage"] = "Yorumuz başarıyla gönderildi. Onaylandıktan sonra görünecektir.";
                    return RedirectToAction("MusicDetay");
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