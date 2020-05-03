using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Model
{
    public class Event
    {

        /// DA CANCELLARE
        public string User { get; set; }
        public string Message { get; set; }
        /// DA CANCELLARE
        public Dictionary<string, string> Properties { get; set; }

        public bool Contains(string key) {
            return Properties.ContainsKey(key);
        }

        public string Get(string key) {
            string value;
            Properties.TryGetValue(key, out value);
            return value;
        }

        public void Set(string key, string value) {
            Properties.TryAdd(key, value);
        }
    }
}
