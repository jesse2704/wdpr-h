using System;
using System.ComponentModel.DataAnnotations;

namespace wdpr_h.Models
{
    public class Chatbericht
    {
        [Key]
        [Required]
        public Guid Id {get; set;}
        [Required]
        public DateTime TimePosted {get;set;}
        [Required]
        public Guid Ontvanger {get;set;}
        [Required]
        public Guid Afzender {get;set;}
        [Required]
        [StringLength(100)]
        public String Bericht {get;set;}
    }
}