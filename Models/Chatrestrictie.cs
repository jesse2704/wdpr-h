using System;

namespace wdpr_h.Models
{
    public class Chatrestrictie
    {
        public Guid User {get;set;}
        public DateTime BeginTijd {get;set;}
        public DateTime EindTijd {get;set;}
        public String Reden {get;set;}
        public Guid ZelfhulpgroepId {get;set;}
    }
}