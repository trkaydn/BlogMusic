using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMusic.Models
{
    public class AdminViewModel
    {
        public IEnumerable<Gundem> Gundems { get; set; }
        public IEnumerable<Music> Musics { get; set; }
        public IEnumerable<Kategori> Kategoris { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Sanatci> Sanatcis { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<Admin> Admins { get; set; }


    }
}