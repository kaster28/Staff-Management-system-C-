using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HRIS.Teaching;
using HRIS.Database;

namespace HRIS.Control
{
    public class StaffController
    {
      //  public string currNameFilter { get; set; }//?
      //  public Category currCategoryFilter { get; set; }//?
        private List<Staff> staffList;
        public List<Staff> Teachers { get { return staffList; } }

        private ObservableCollection<Staff> viewableStaff;

        public static string dt;
        public ObservableCollection<Staff> VisibleWorkers { get { return viewableStaff; } set { } }
        //?consultation observaable??
        
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
        public StaffController()
        {
            staffList = SchoolDBAAdapter.FetchAllStaff();//load all staffs to local
            viewableStaff = new ObservableCollection<Staff>(staffList);

            foreach (Staff s in staffList)
            {
                s.eventList = SchoolDBAAdapter.FetchEvent(s.ID);
                s.classList = SchoolDBAAdapter.FetchUnitByID(s.ID);
                s.AvilInfo = Availability(s.ID);
            }
        }
        public void FilterBy(Category category, string name)
        {
            var SearchtName = from Staff s in staffList
                              where (s.Name.ToLower()).Contains(name.ToLower())
                              select s;
            viewableStaff.Clear();
            SearchtName.ToList().ForEach(viewableStaff.Add);
            Category all = ParseEnum<Category>("All");
            if (category!=all)
            {
                List<Staff> view = viewableStaff.ToList();
                foreach (Staff s in view)

                {
                    if (category != s.Category)
                    {
                        viewableStaff.Remove(s);
                    }
                }
            }
            //MessageBox.Show("filtered ");
        }

        public string Availability(int ID)
        {
            List<UnitClass> classlist=SchoolDBAAdapter.FetchClassByID(ID);
            List<Event> eventlist = SchoolDBAAdapter.FetchEvent(ID);
            string result;
    
            DateTime now = DateTime.Now;
            dt = DateTime.Today.DayOfWeek.ToString();
            TimeSpan time = now.TimeOfDay;

            var cls = from UnitClass uc in classlist
                              where uc.Day==dt && uc.Start<=time && uc.End>=time
                              select uc;
            int classnum = cls.ToList().Count();
            List<UnitClass> teach = cls.ToList<UnitClass>();
            var evt = from Event et in eventlist
                              where et.Day == dt && et.Start <= time && et.End >= time
                              select et;
            int eventnum = evt.ToList().Count();
           
            if (eventnum > 0) { result= "Consulting"; }
            else if (classnum > 0) { result = "Teaching" + " (" + teach[0].Unit_Code + "," + teach[0].Room + ")"; }
            else { result = "Free"; }
            //Availability status = ParseEnum<Availability>(result);
            return result;

        }

        public ObservableCollection<Staff> GetViewableStaff()
        {
            return VisibleWorkers;
        }     

    }
}
