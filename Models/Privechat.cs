using System;
using System.ComponentModel.DataAnnotations;

namespace wdpr_h.Models
{
    public class Privechat
    {
        [Required]
        public Guid Id {get; set;}
        [Required]
        public DateTime DateTime {get; set;}
        [Required]
        public Guid Ontvanger {get; set;}
        [Required]
        public Guid Afzender {get; set;}
    }
}