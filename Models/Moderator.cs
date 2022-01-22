using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace wdpr_h.Models
{
    public class Moderator : IdentityUser
    {
        [StringLength(30)]
        public String Naam {get; set;}
        [StringLength(40)]
        public String Wachtwoord {get; set;}
    }
}