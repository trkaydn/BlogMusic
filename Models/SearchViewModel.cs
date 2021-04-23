using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMusic.Models
{
    public class SearchViewModel
    {
        public IEnumerable<Music> Sarkilar { get; set; }
        public IEnumerable<Sanatci> Sanatcilar { get; set; }
        public IEnumerable<Gundem> Gundemler { get; set; }

    }
}