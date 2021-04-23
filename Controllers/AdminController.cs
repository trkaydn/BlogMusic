using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BlogMusic.Models;

namespace BlogMusic.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        MusicContext context = new MusicContext();
        //static Admin aktifadmin = new Admin();

        [HttpGet, OverrideAuthorization]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, OverrideAuthorization]
        public ActionResult Login(Admin admin)
        {
            var adminbilgi = context.Admins.FirstOrDefault(i => i.UserName == admin.UserName && i.Password == admin.Password);
            if (adminbilgi != null)
            {
                //aktifadmin = adminbilgi;                                        
                Session["admin"] = adminbilgi.UserName;
                FormsAuthentication.SetAuthCookie(adminbilgi.UserName, false);
                return RedirectToAction("Index");
            }
            ViewBag.LoginMesage = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("admin");
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


        public ActionResult Index()
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                string username = Session["admin"].ToString();
                Admin admin = context.Admins.FirstOrDefault(i => i.UserName == username);
                AdminViewModel model = new AdminViewModel();
                //ViewBag.AdminAdi = aktifadmin.AdSoyad;             
                //ViewBag.AdminResim = aktifadmin.ResimUrl;                  
                ViewBag.AdminAdi = admin.AdSoyad;
                ViewBag.AdminResim = admin.ResimUrl;
                model.Comments = context.Comments;
                model.Gundems = context.Gundems;
                model.Kategoris = context.Kategoris;
                model.Messages = context.Messages;
                model.Musics = context.Musics;
                model.Sanatcis = context.Sanatcis;
                return View(model);

            }
            catch (Exception)
            {
                return RedirectToAction("/Home/Error/");
            }
        }

        [HttpGet]
        public ActionResult BilgiGuncelle()
        {
            if (Session.Count == 0)
                return RedirectToAction("Logout");
            string username = Session["admin"].ToString();
            Admin admin = context.Admins.FirstOrDefault(i => i.UserName == username);
            return View(admin);

        }

        [HttpPost]
        public ActionResult BilgiGuncelle(Admin admin)
        {
            try
            {
                var dbadmin = context.Admins.FirstOrDefault(i => i.Id == admin.Id);
                dbadmin.AdSoyad = admin.AdSoyad;
                dbadmin.Mail = admin.Mail;
                dbadmin.Password = admin.Password;
                dbadmin.ResimUrl = admin.ResimUrl;
                dbadmin.UserName = admin.UserName;
                dbadmin.AboutMe = admin.AboutMe;
                //aktifadmin = admin;
                TempData["AdminMessage"] = "Bilgileriniz başarıyla güncellendi.";
                context.SaveChanges();
                return View(dbadmin);
                //return View(Session["login"]);
            }
            catch (Exception)
            {
                TempData["AdminMessage"] = "İşlem sırasında hata oluştu.";
                string username = Session["admin"].ToString();
                Admin admin1 = context.Admins.FirstOrDefault(i => i.UserName == username);
                return View(admin1);
                //return View(Session["login"]);
            }

        }

        [HttpGet]
        public ActionResult AddGundem()
        {
            if (Session.Count == 0)
                return RedirectToAction("Logout");
            return View();

        }

        [HttpPost]
        public ActionResult AddGundem(Gundem gundem)
        {
            try
            {
                TempData["GundemMessage"] = "Haber başarıyla yayınlandı.";
                context.Gundems.Add(gundem);
                context.SaveChanges();
                return RedirectToAction("TumHaberler");
            }
            catch (Exception)
            {
                TempData["GundemMessage"] = "İşlem sırasında hata oluştu.";
                return RedirectToAction("TumHaberler");

            }
        }

        [HttpGet]
        public ActionResult UpdateGundem(int? id)
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                Gundem gundem = context.Gundems.FirstOrDefault(i => i.Id == id);
                return View(gundem);
            }
            catch (Exception)
            {

                return RedirectToAction("/Home/Error/");

            }
        }

        [HttpPost]
        public ActionResult UpdateGundem(Gundem gundem)
        {
            try
            {
                TempData["GundemMessage"] = "Haber başarıyla güncellendi.";
                var secili = context.Gundems.FirstOrDefault(i => i.Id == gundem.Id);
                secili.Baslik = gundem.Baslik;
                secili.Icerik = gundem.Icerik;
                secili.GundemResim = gundem.GundemResim;
                context.SaveChanges();
                return RedirectToAction("TumHaberler");
            }
            catch (Exception)
            {
                TempData["GundemMessage"] = "İşlem sırasında hata oluştu.";
                return RedirectToAction("TumHaberler");

            }

        }

        public ActionResult DeleteGundem(int? id)
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                TempData["GundemMessage"] = "Haber başarıyla silindi.";
                var haber = context.Gundems.FirstOrDefault(i => i.Id == id);
                context.Gundems.Remove(haber);
                context.SaveChanges();
                return RedirectToAction("TumHaberler");
            }
            catch (Exception)
            {
                TempData["GundemMessage"] = "İşlem sırasında hata oluştu.";
                return RedirectToAction("TumHaberler");

            }

        }

        [HttpGet]
        public ActionResult AddMusic()
        {
            if (Session.Count == 0)
                return RedirectToAction("Logout");
            ViewBag.Sanatcilar = new SelectList(context.Sanatcis, "Id", "SanatciAdi");
            ViewBag.Kategoriler = new SelectList(context.Kategoris, "Id", "KategoriAdi");
            return View();
        }

        [HttpPost]
        public ActionResult AddMusic(Music music)
        {
            try
            {
                TempData["MusicMessage"] = "Şarkı başarıyla eklendi.";
                context.Musics.Add(music);
                context.SaveChanges();
                return RedirectToAction("TumSarkilar");
            }
            catch (Exception)
            {
                TempData["MusicMessage"] = "İşlem sırasında hata oluştu.";
                return RedirectToAction(nameof(AdminController.TumSarkilar));

            }

        }

        [HttpGet]
        public ActionResult UpdateMusic(int? id)
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                ViewBag.Sanatcilar = new SelectList(context.Sanatcis, "Id", "SanatciAdi");
                ViewBag.Kategoriler = new SelectList(context.Kategoris, "Id", "KategoriAdi");
                Music music = context.Musics.FirstOrDefault(i => i.Id == id);
                return View(music);
            }
            catch (Exception)
            {

                return RedirectToAction("/Home/Error/");
            }
        }

        [HttpPost]
        public ActionResult UpdateMusic(Music music)
        {
            try
            {
                TempData["MusicMessage"] = "Şarkı başarıyla güncellendi";
                var secili = context.Musics.FirstOrDefault(i => i.Id == music.Id);
                secili.CikisTarihi = music.CikisTarihi;
                secili.KategoriId = music.KategoriId;
                secili.Puan = music.Puan;
                secili.ResimUrl = music.ResimUrl;
                secili.SanatciId = music.SanatciId;
                secili.SarkiAd = music.SarkiAd;
                secili.SarkiSozleri = music.SarkiSozleri;
                secili.YoutubeUrl = music.YoutubeUrl;
                context.SaveChanges();
                return RedirectToAction("TumSarkilar");
            }
            catch (Exception)
            {
                TempData["MusicMessage"] = "İşlem sırasında hata oluştu.";
                return RedirectToAction("TumSarkilar");
            }


        }

        [HttpGet]
        public ActionResult AddKategori()
        {
            if (Session.Count == 0)
                return RedirectToAction("Logout");
            return View();
        }

        [HttpPost]
        public ActionResult AddKategori(Kategori kategori)
        {
            try
            {
                TempData["KategoriMessage"] = "Kategori başarıyla eklendi.";
                context.Kategoris.Add(kategori);
                context.SaveChanges();
                return RedirectToAction("TumKategoriler");
            }
            catch (Exception)
            {
                TempData["KategoriMessage"] = "İşlem sırasında hata oluştu.";
                return RedirectToAction("TumKategoriler");

            }
        }
        [HttpGet]
        public ActionResult UpdateKategori(int? id)
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                Kategori ctg = context.Kategoris.FirstOrDefault(i => i.Id == id);
                return View(ctg);
            }
            catch (Exception)
            {
                return RedirectToAction("/Home/Error/");

            }

        }
        [HttpPost]
        public ActionResult UpdateKategori(Kategori ctg)
        {
            try
            {
                TempData["KategoriMessage"] = "Kategori başarıyla güncellendi.";
                var secili = context.Kategoris.FirstOrDefault(i => i.Id == ctg.Id);
                secili.KategoriAdi = ctg.KategoriAdi;
                context.SaveChanges();
                return RedirectToAction("TumKategoriler");
            }
            catch (Exception)
            {
                TempData["KategoriMessage"] = "İşlem sırasında hata oluştu.";
                return RedirectToAction("TumKategoriler");

            }
        }

        [HttpGet]
        public ActionResult AddSanatci()
        {
            if (Session.Count == 0)
                return RedirectToAction("Logout");
            return View();
        }
        [HttpPost]
        public ActionResult AddSanatci(Sanatci sanatci)
        {
            try
            {
                TempData["SanatciMessage"] = "Sanatçı başarıyla eklendi.";
                context.Sanatcis.Add(sanatci);
                context.SaveChanges();
                return RedirectToAction("TumSanatcilar");
            }
            catch (Exception)
            {
                TempData["SanatciMessage"] = "İşlem sırasında hata oluştu.";
                return RedirectToAction("TumSanatcilar");

            }

        }

        [HttpGet]
        public ActionResult UpdateSanatci(int? id)
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                Sanatci sanatci = context.Sanatcis.FirstOrDefault(i => i.Id == id);
                return View(sanatci);
            }
            catch (Exception)
            {

                return RedirectToAction("/Home/Error/");
            }

        }

        [HttpPost]
        public ActionResult UpdateSanatci(Sanatci sanatci)
        {
            try
            {
                TempData["SanatciMessage"] = "Sanatçı bilgileri başarıyla güncellendi.";
                var secili = context.Sanatcis.FirstOrDefault(i => i.Id == sanatci.Id);
                secili.ResimUrl = sanatci.ResimUrl;
                secili.SanatciAdi = sanatci.SanatciAdi;
                secili.SanatciHakkinda = sanatci.SanatciHakkinda;
                context.SaveChanges();
                return RedirectToAction("TumSanatcilar");
            }
            catch (Exception)
            {
                TempData["SanatciMessage"] = "İşlem sırasında hata oluştu.";
                return RedirectToAction("TumSanatcilar");

            }
        }

        public ActionResult YorumOnay()
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                AdminViewModel model = new AdminViewModel();
                model.Comments = context.Comments;
                model.Gundems = context.Gundems;
                model.Kategoris = context.Kategoris;
                model.Messages = context.Messages;
                model.Musics = context.Musics;
                model.Sanatcis = context.Sanatcis;
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("/Home/Error/");

            }
        }

        public ActionResult AcceptYorum(int? id)
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                TempData["YorumMessage"] = "Yorum başarıyla onaylandı.";
                var yorum = context.Comments.FirstOrDefault(i => i.Id == id);
                yorum.Activated = true;
                context.SaveChanges();
                return RedirectToAction("YorumOnay");
            }
            catch (Exception)
            {

                TempData["YorumMessage"] = "İşlem sırasında hata oluştu.";
                return RedirectToAction("YorumOnay");

            }

        }

        public ActionResult DeleteYorum(int? id)
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                TempData["YorumMessage"] = "Yorum başarıyla silindi.";
                var yorum = context.Comments.FirstOrDefault(i => i.Id == id);
                context.Comments.Remove(yorum);
                context.SaveChanges();
                return RedirectToAction("YorumOnay");
            }
            catch (Exception)
            {

                TempData["YorumMessage2"] = "İşlem sırasında hata oluştu.";
                return RedirectToAction("YorumOnay");

            }

        }

        public ActionResult DeleteYorum2(int? id)
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                TempData["YorumMessage2"] = "Yorum başarıyla silindi.";
                var yorum = context.Comments.FirstOrDefault(i => i.Id == id);
                context.Comments.Remove(yorum);
                context.SaveChanges();
                return RedirectToAction("TumYorumlar");
            }
            catch (Exception)
            {

                TempData["YorumMessage2"] = "İşlem sırasında hata oluştu.";
                return RedirectToAction("TumYorumlar");

            }
        }

        public ActionResult GelenKutusu()
        {
            if (Session.Count == 0)
                return RedirectToAction("Logout");
            AdminViewModel model = new AdminViewModel();
            model.Messages = context.Messages;
            return View(model);
        }
        public ActionResult DeleteMessage(int? id)
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                TempData["DmMessage"] = "Mesaj başarıyla silindi.";
                var mesaj = context.Messages.FirstOrDefault(i => i.Id == id);
                context.Messages.Remove(mesaj);
                context.SaveChanges();
                return RedirectToAction("GelenKutusu");
            }
            catch (Exception)
            {
                TempData["DmMessage"] = "İşlem sırasında hata oluştu.";
                return RedirectToAction("GelenKutusu");

            }

        }

        public ActionResult TumHaberler()
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                List<Gundem> gundems = context.Gundems.OrderByDescending(i => i.Tarih).ToList();
                return View(gundems);
            }
            catch (Exception)
            {

                return RedirectToAction("/Home/Error/");
            }

        }

        public ActionResult TumSarkilar()
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                AdminViewModel model = new AdminViewModel();
                model.Kategoris = context.Kategoris;
                model.Musics = context.Musics;
                model.Sanatcis = context.Sanatcis;
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("/Home/Error/");
            }

        }

        public ActionResult DeleteMusic(int? id)
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                TempData["MusicMessage"] = "Şarkı başarıyla silindi.";
                var secili = context.Musics.FirstOrDefault(i => i.Id == id);
                context.Musics.Remove(secili);
                context.SaveChanges();
                return RedirectToAction("TumSarkilar");
            }
            catch (Exception)
            {

                TempData["MusicMessage"] = "İşlem sırasında hata oluştu.";
                return RedirectToAction("TumSarkilar");

            }


        }

        public ActionResult TumSanatcilar()
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                AdminViewModel model = new AdminViewModel();
                model.Sanatcis = context.Sanatcis;
                model.Musics = context.Musics;
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("/Home/Error/");

            }
        }

        public ActionResult DeleteSanatci(int? id)
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                var muzikler = context.Musics.Where(i => i.SanatciId == id);
                if (muzikler.Count() < 1)
                {
                    TempData["SanatciMessage"] = "Sanatçı başarıyla silindi.";
                    var sanatci = context.Sanatcis.FirstOrDefault(i => i.Id == id);
                    context.Sanatcis.Remove(sanatci);
                    context.SaveChanges();
                    return RedirectToAction("TumSanatcilar");
                }
                else
                {
                    TempData["SanatciMessage"] = "Bu sanatçıyı silmek için önce sanatçıya ait tüm şarkıları silmelisiniz..!";
                    return RedirectToAction("TumSanatcilar");
                }
            }
            catch (Exception)
            {

                TempData["SanatciMessage"] = "İşlem sırasında hata oluştu.";
                return RedirectToAction("TumSanatcilar");
            }

        }

        public ActionResult TumKategoriler()
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                AdminViewModel model = new AdminViewModel();
                model.Kategoris = context.Kategoris;
                model.Musics = context.Musics;
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("/Home/Error");
            }

        }

        public ActionResult DeleteKategori(int? id)
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                var muzikler = context.Musics.Where(i => i.KategoriId == id);
                if (muzikler.Count() < 1)
                {
                    TempData["KategoriMessage"] = "Kategori başarıyla silindi.";
                    var kategori = context.Kategoris.FirstOrDefault(i => i.Id == id);
                    context.Kategoris.Remove(kategori);
                    context.SaveChanges();
                    return RedirectToAction("TumKategoriler");
                }
                else
                {
                    TempData["KategoriMessage"] = "Bu kategoriyi silmek için önce kategoriye ait tüm şarkıları silmelisiniz..!";
                    return RedirectToAction("TumKategoriler");
                }
            }
            catch (Exception)
            {
                TempData["KategoriMessage"] = "İşlem sırasında hata oluştu.";
                return RedirectToAction("TumKategoriler");

            }
        }

        public ActionResult TumYorumlar()
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                AdminViewModel model = new AdminViewModel();
                model.Comments = context.Comments;
                model.Gundems = context.Gundems;
                model.Musics = context.Musics;
                model.Sanatcis = context.Sanatcis;
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("/Home/Error/");
            }
        }

        public ActionResult TumAdminler()
        {
            IEnumerable<Admin> admins = context.Admins.ToList();
            return View(admins);
        }

        public ActionResult DeleteAdmin(int? id)
        {
            try
            {
                if (Session.Count == 0)
                    return RedirectToAction("Logout");
                var seciliadmin = context.Admins.FirstOrDefault(i => i.Id == id);

                string username = Session["admin"].ToString();
                var aktifadmin = context.Admins.FirstOrDefault(i => i.UserName == username);

                if (aktifadmin == seciliadmin)
                {
                    context.Admins.Remove(seciliadmin);
                    context.SaveChanges();
                    return RedirectToAction("Logout");
                }
                context.Admins.Remove(seciliadmin);
                context.SaveChanges();
                TempData["AdminEditMessage"] = "Moderatör başarıyla silindi.";
                return RedirectToAction("TumAdminler");
            }
            catch (Exception)
            {

                TempData["AdminEditMessage"] = "İşlem sırasında hata oluştu.";
                return RedirectToAction("TumSarkilar");

            }
        }

        public ActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(Admin admin)
        {
            context.Admins.Add(admin);
            context.SaveChanges();
            TempData["AdminEditMessage"] = "Moderatör başarıyla eklendi.";
            return RedirectToAction("TumAdminler");
        }

    }
}