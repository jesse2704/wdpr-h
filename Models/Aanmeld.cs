using System;
using System.ComponentModel.DataAnnotations;

namespace wdpr_h.Models
{
    public class Aanmeld
    {
        [Key]
        [Required]
        public Guid Id {get; set;}
        [Required]
        [StringLength(25)]
        public String Naam {get;set;}
        [Required]
        [StringLength(25)]
        public String Achternaam {get;set;}
        [Required]
        public DateTime Geboortedatum {get;set;}
        [Required]
        [StringLength(150)]
        public String Aandoening {get;set;}
        public Boolean voorKind {get;set;}
        public Boolean heeftVerwijzing {get;set;}
    
    }
}