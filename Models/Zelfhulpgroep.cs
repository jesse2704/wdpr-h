using System;
using System.ComponentModel.DataAnnotations;

namespace wdpr_h.Models
{
    public class Zelfhulpgroep
    {
        [Required]
        public Guid Id {get; set;}
        [Required]
        [StringLength(20)]
        public String Titel {get; set;}
        [Required]
        [StringLength(30)]
        public String Onderwerp {get; set;}
        [Required]
        public String LeeftijdsCategorie {get; set;}
    }
}