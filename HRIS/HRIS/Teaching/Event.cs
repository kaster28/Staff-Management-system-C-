using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Teaching
{
    public class Event
    {
        public String Day { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public override string ToString()
        {
            return "'" + Day + "' from " + Start + " to " + End;
        }
    }
}
