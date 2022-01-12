using System;

namespace wdpr_h.Models
{
    public class Client
    {
        public Guid User { get; set; }
        public String Nicknaam { get; set; }
        public String LeeftijdsCategorie { get; set; }
        public String Naam { get; set; }
        public String Achternaam { get; set; }
        public int HulpverlenerId { get; set; }
        public String Wachtwoord { get; set; }
        public String Email {get;set;}
    }
}