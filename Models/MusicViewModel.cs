using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMusic.Models
{
    public class MusicViewModel
    {
        public IEnumerable<Music> YeniEklenen { get; set; }
        public IEnumerable<Music> CokDinlenen { get; set; }
        public IEnumerable<Gundem> TopGundem { get; set; }


    }
}