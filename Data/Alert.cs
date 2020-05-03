using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Model
{
    public class Alert
    {
        public string AlertCode { get; set; } 
        public string AlertDescription { get; set; }
        public DateTime AlertDate { get; set; }
        public string AlertLevel { get; set;  }
        public int AlertCount { get; set; }


    }
}
