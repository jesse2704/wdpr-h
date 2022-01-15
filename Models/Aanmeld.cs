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
        [MinimumAge(18, ErrorMessage = "Je moet ouder of gelijk aan 16 zijn om zelf een account aan te maken!")]
        public DateTime Geboortedatum {get;set;}
        [Required]
        [StringLength(150)]
        public String Aandoening {get;set;}
        
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public String Email {get;set;}
        [Required]
        public Guid hulpVerlenerId {get;set;}
        public Boolean voorKind {get;set;}
        public Boolean heeftVerwijzing {get;set;}
    
    }
}