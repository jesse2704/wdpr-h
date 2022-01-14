using System;
using System.ComponentModel.DataAnnotations;

namespace wdpr_h.Models
{
    public class ClientZelfhulpgroep
    {
        [Key]
        public int Id {get;set;}

        //Tussentabel tussen Cliênt - Zelfhulpgroep
        [Required]
        public Guid IdClient {get;set;} // Client ID 
        [Required]
        public Guid IdGroep {get; set;} // Groep ID
    }
}