using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMusic.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50), Required(ErrorMessage = "İsim zorunludur.")]
        public string Name { get; set; }

        [StringLength(50), Required(ErrorMessage = "Mail zorunludur."), EmailAddress(ErrorMessage = "Doğru bir mail adresi girin.")]
        public string Mail { get; set; }

        [StringLength(250), Required(ErrorMessage = "Konu alanı zorunludur.")]
        public string Konu { get; set; }

        [Required(ErrorMessage = "Boş mesaj gönderilemez.")]
        public string Mesaj { get; set; }

        public DateTime Tarih { get; set; }



    }
}