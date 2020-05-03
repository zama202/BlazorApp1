using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Model
{
    public class TwinEntity
    {
        public string OperationalStatus { get; set; }
        public string HealthStatus { get; set; }

        public string Consumption { get; set; }
        
        public void DoAction(string action, Event eventArgs)
        {
            if (action.Equals("update"))
            {
                if (eventArgs.Contains("OperationalStatus"))
                    OperationalStatus = eventArgs.Get("OperationalStatus");

                if (eventArgs.Contains("HealthStatus"))
                    HealthStatus = eventArgs.Get("HealthStatus");

                if (eventArgs.Contains("Consumption"))
                    Consumption = eventArgs.Get("Consumption");
            }

            if (action.Equals("reset"))
            {
                OperationalStatus = "";
                HealthStatus = "";
                Consumption = "";
            }
        }

        internal void Init()
        {
            DoAction("reset", null);
        }
    }

    

}
