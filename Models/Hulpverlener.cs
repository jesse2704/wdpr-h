using System;


namespace wdpr_h.Models
{
    public class Hulpverlener
    {
        public Guid Id {get;set;}
        public String Naam {get;set;}
        public String Achternaam {get;set;}
        public String Specialisme {get;set;}
        public String Wachtwoord {get;set;}
        public String Email {get;set;}
    }
}