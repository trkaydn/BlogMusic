﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMusic.Models
{
    public class Kategori
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string KategoriAdi { get; set; }

        public List<Music> Muzikler { get; set; }

    }
}