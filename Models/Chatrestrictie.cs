using System;
using System.ComponentModel.DataAnnotations;

namespace wdpr_h.Models
{
    public class Chatrestrictie
    {
        [Key]
        [Required]
        public int Id {get;set;}
        [Required]
        public Guid User {get;set;}
        [Required]
        public DateTime BeginTijd {get;set;}
        [Required]
        public DateTime EindTijd {get;set;}
        [Required]
        [StringLength(100)]
        public String Reden {get;set;}
        [Required]
        public Guid ZelfhulpgroepId {get;set;}
    }
}