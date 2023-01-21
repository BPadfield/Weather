using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    public class ReturnLocation
    {
        public string? Key { get; set; }
        public string? Type { get; set; }
        public string? LocalizedName { get; set; }
        public Country Country { get; set; } = new Country();
        public int Rank { get; set; }

        public string? Message { get; set; }
        public override string ToString()
        {
            var returnval = this.LocalizedName + ", " + this.Country.LocalizedName;
            return returnval;
        }
    }

    public class Country
    {
        public string? LocalizedName { get; set; }
        public string? ID { get; set; }
    }


}
