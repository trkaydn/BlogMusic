using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMusic.Models
{
    public class SanatciViewModel
    {
        public Sanatci Sanatci { get; set; }
        public IEnumerable<Music> Sarkilar { get; set; }

        public IEnumerable<Sanatci> Sanatcilar { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
    }
}