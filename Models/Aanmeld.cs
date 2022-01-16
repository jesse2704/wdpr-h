using System;
using System.ComponentModel;
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
        [MinimumAge(16, ErrorMessage = "Je moet 16 jaar of ouder zijn om een account aan te maken!")]
        public DateTime Geboortedatum {get;set;}
        [Required]
        [StringLength(150)]
        public String Aandoening {get;set;}
        
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public String Email {get;set;}
        [Required]
        [Display(Name = "Hulpverlener")]
        public Guid hulpVerlenerId {get;set;}
        [Display(Name = "Account voor uw kind?")]
        public Boolean voorKind {get;set;}
        [DisplayName("In bezit van een doorverwijzing?")]
        public Boolean heeftVerwijzing {get;set;}
    
    }
}