using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMusic.Models
{
    public class Gundem
    {
        [Key]
        public int Id { get; set; }

        [StringLength(150)]
        public string Baslik { get; set; }

        [StringLength(250)]
        public string GundemResim { get; set; }
        public string Icerik { get; set; }

        public DateTime Tarih { get; set; }

      
    }
}