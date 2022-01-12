using System;

namespace wdpr_h.Models
{
    public class Chatbericht
    {
        public Guid Id {get; set;}
        public DateTime TimePosted {get;set;}
        public Guid Ontvanger {get;set;}
        public Guid Afzender {get;set;}
        public String Bericht {get;set;}
    }
}