using System;

namespace wdpr_h.Models
{
    public class Privechat
    {
        public Guid Id {get; set;}
        public DateTime DateTime {get; set;}
        public Guid Ontvanger {get; set;}
        public Guid Afzender {get; set;}
    }
}