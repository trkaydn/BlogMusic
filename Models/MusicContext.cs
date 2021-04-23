using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogMusic.Models
{
    public class MusicContext : DbContext
    {
        public MusicContext() : base("Context")
        {

        }

        public DbSet<Music> Musics { get; set; }
        public DbSet<Sanatci> Sanatcis { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Gundem> Gundems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}