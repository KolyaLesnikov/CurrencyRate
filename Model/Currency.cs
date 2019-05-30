using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate
{
   public class Currency
    {
        public string Title { get; set; }
        public double Value { get; set; }
        public DateTime LastUpdate { get; set; }
        public Currency(string title, double value, DateTime lastUpdate)
        {
            this.Value = value;
            this.Title = title;
            this.LastUpdate = lastUpdate;
        }
    }
}
