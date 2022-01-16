using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace wdpr_h.Models
{
    public class Moderator : IdentityUser
    {
        [Required]
        [StringLength(30)]
        public String Naam {get; set;}
        [Required]
        [StringLength(40)]
        public String Wachtwoord {get; set;}
    }
}