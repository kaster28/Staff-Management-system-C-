using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Teaching
{
    public class UnitClass
    {
        public string Unit_Code { get; set; }
        public string Room { get; set; }
        public Campus Campus { get; set; }
        public string Day { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string Type { get; set; }
        public int Staff { get; set; }
        public string StartAndEnd { get { return Start + "-" + End; } }
        public override string ToString()
        {
            return " Day: " + Day + " Duration: " + Start + " - " + End + " Type: " + Type + "Room: " + Room + " Campus: " + Campus + "Staff Member: " + Staff;
        }
    }
}
