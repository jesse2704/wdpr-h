using System;
using System.ComponentModel.DataAnnotations;

namespace wdpr_h.Models
{
    public class Hulpverlener
    {
        [Key]
        [Required]
        public Guid Id {get; set;}
        [Required]
        [StringLength(30)]
        public String Naam {get; set;}
        [Required]
        [StringLength(30)]
        public String Achternaam {get; set;}
        [Required]
        [StringLength(35)]
        public String Specialisme {get; set;}
        [Required]
        [StringLength(30)]
        public String Wachtwoord {get; set;}
    }
}