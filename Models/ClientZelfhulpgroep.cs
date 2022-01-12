using System;

namespace wdpr_h.Models
{
    public class ClientZelfhulpgroep
    {
        //Tussentabel tussen Cliênt - Zelfhulpgroep
        public Guid IdClient {get;set;} // Client ID 
        public Guid IdGroep {get; set;} // Groep ID
    }
}