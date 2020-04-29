using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRIS.Control;

namespace HRIS.Teaching
{
    public enum Campus
    {
        All,Hobart,Launceston
    }

    public enum Availability
    {
        Free,Consulting,Teaching
    }
    public enum Category
    {
        All,Academic,Technical,Admin,Casual
    }
    public class Staff
    {
        public int ID { get; set; }
        public string Family_name { get; set; }
        public string Given_name {get; set; }
        public string Title {get; set;}
        public string Phone{get; set;}
        public string Room{get; set;}      
        public string Email{get; set;}     
        public string Photo{get; set;}
        public Category Category { get; set; }
        public Campus Campus { get; set; }
        public string AvilInfo { get; set; }
        public List<Event> eventList { get; set; } // save consultation events for this staff
        public List<Unit> classList { get; set; } // save classes taught by this staff
        public override string ToString()
        {
            return Family_name + "," + Given_name + " (" + Title + ")";
        }

        public string Name
        {
            get
            {
                return this.Title + " "+ Given_name + " " + Family_name;
            }
        }

    }
}
