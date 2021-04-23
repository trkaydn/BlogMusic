using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMusic.Models
{
    public class GundemViewModel
    {
        public Gundem Gundem { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
    }
}