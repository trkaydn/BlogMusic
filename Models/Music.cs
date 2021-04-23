using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMusic.Models
{
    public class Music
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string SarkiAd { get; set; }
        public int KategoriId { get; set; }
        public int SanatciId { get; set; }
        [StringLength(150)]
        public string ResimUrl { get; set; }
        public string SarkiSozleri { get; set; }
        public DateTime CikisTarihi { get; set; }
        public int Puan { get; set; }
        [StringLength(250)]
        public string YoutubeUrl { get; set; }

      
    }
}