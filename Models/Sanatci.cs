using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMusic.Models
{
    public class Sanatci
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string SanatciAdi { get; set; }
        [StringLength(250)]
        public string ResimUrl { get; set; }
        public string SanatciHakkinda { get; set; }

        public List<Music> Sarkilar { get; set; }

     

    }
}