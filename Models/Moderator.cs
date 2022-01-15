using System;
using System.ComponentModel.DataAnnotations;

namespace wdpr_h.Models
{
    public class Moderator
    {
        [Required]
        public Guid Id {get; set;}
        [Required]
        [StringLength(30)]
        public String Naam {get; set;}
        [Required]
        [StringLength(40)]
        public String Wachtwoord {get; set;}
    }
}