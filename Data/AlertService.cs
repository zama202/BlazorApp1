using BlazorApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data
{
    public class AlertService
    {

        protected List<Alert> alerts = new List<Alert>();
        public List<Alert> GetAlertsAsync()
        {
            if (alerts.Count == 0)
            {
                for (int x = 0; x < 3; x++)
                {
                    Alert alert = new Alert();
                    alert.AlertCode = "" + (8000 + (x * x * 3));
                    alert.AlertDescription = "Alarm description";
                    alert.AlertCount = 1;
                    alert.AlertDate = DateTime.Now;
                    alert.AlertLevel = "WARNING";
                    alerts.Add(alert);
                }
            }
            
            return alerts;
        }

        public void Delete(string alertCode) {
            for (int i = 0; i < alerts.Count; i++) {
                if (alerts.ElementAt(i).AlertCode.Equals(alertCode))
                    alerts.RemoveAt(i);
            }
        }

        public void DoAction(string action, Event eventArgs)
        {
            if (action.Equals("update") || action.Equals("add"))
            {
                bool found = false;
                if (eventArgs.Contains("AlertCode")) {
                    foreach (var alert in alerts) {
                        if (alert.AlertCode.Equals(eventArgs.Get("AlertCode"))) {
                            Console.WriteLine(eventArgs.Get("AlertCode") + " found");
                            alert.AlertCount++;
                            found = true;
                        }
                    }
                    if (!found) {
                        Alert alert = new Alert();
                        alert.AlertCode = eventArgs.Get("AlertCode");
                        alert.AlertDescription = eventArgs.Get("AlertDescription");
                        alert.AlertCount = 1;
                        alert.AlertDate = DateTime.Parse(eventArgs.Get("AlertDate"));
                        alert.AlertLevel = eventArgs.Get("AlertLevel");
                        alerts.Add(alert);
                    }
                }
            }

            if (action.Equals("reset"))
            {
                alerts = new List<Alert>();
            }
        }

        internal void Init()
        {
            DoAction("reset", null);
        }

    }
}
