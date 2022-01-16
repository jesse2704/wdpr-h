using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace wdpr_h.Models
{
    public class Client : IdentityUser
    {
        [Required]
        [StringLength(15)]
        public String Nicknaam { get; set; }
        [Required]
        public String LeeftijdsCategorie { get; set; }
        [Required]
        [StringLength(30)]
        public String Naam { get; set; }
        [Required]
        [StringLength(30)]
        public String Achternaam { get; set; }
        [Required]
        public Boolean isKindAccount {get;set;}
        public Guid OuderAccount {get;set;}
        [Required]
        public int HulpverlenerId { get; set; }
        [Required]
        public String Wachtwoord { get; set; }
    }
}