using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMusic.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50), Required]
        public string AdSoyad { get; set; }
        [StringLength(50), Required]
        public string Mail { get; set; }

        [StringLength(50), Required]
        public string UserName { get; set; }

        [StringLength(50), Required]
        public string Password { get; set; }
        [StringLength(250), Required]
        public string ResimUrl { get; set; }

        public string AboutMe { get; set; }
    }
}