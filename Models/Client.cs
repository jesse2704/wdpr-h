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
        [DataType(DataType.Date)]
        [MinimumAge(16, ErrorMessage = "Je moet 16 jaar of ouder zijn om een account aan te maken!")]
        public DateTime Geboortedatum {get;set;}
        [Required]
        public Boolean isKindAccount {get;set;}
        public Guid OuderAccount {get;set;}
        public Guid HulpverlenerId { get; set; }
    }
}