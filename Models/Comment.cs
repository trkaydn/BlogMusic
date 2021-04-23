using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMusic.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50),Required(ErrorMessage ="İsim zorunludur.")]
        public string Name { get; set; }
        [StringLength(50), Required(ErrorMessage="Mail zorunludur."), EmailAddress(ErrorMessage = "Doğru bir mail adresi girin.")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Yorum boş gönderilemez.")]
        public string YorumIcerik { get; set; }
        public DateTime YorumTarihi { get; set; }
        [StringLength(10)]
        public string FormName { get; set; }
        public int BaslikId { get; set; }

        public bool Activated { get; set; }

    }
}