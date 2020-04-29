using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Teaching
{
    public class Unit
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Coordinator { get; set; }
        public List<UnitClass> classList { get; set; }//save the classes for this unit
        public override string ToString()
        {
            return Code + "    " + Title;
        }

        public string UnitName
        {
            get
            {
                return this.Code + " " + Title;
            }
        }
    }
}
