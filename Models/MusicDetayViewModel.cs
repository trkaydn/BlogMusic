using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMusic.Models
{
    public class MusicDetayViewModel
    {
        public Music Music { get; set; }
        public string Kategori { get; set; }
        public string Sanatci { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
    }
}