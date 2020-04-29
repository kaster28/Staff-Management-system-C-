using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using HRIS.Teaching;
using HRIS.Database;

namespace HRIS.Control
{
    public class UnitController
    {
        private List<Unit> unitList;
        private List<UnitClass> classList;
        public List<Unit> Course { get { return unitList; } }

        private ObservableCollection<Unit> viewableUnit;
        public ObservableCollection<Unit> VisibleCourse { get { return viewableUnit; } set { } }
        private ObservableCollection<UnitClass> viewableClass;
        public ObservableCollection<UnitClass> VisibleTimetable { get { return viewableClass; } set { } }

        public UnitController()
        {
            unitList = SchoolDBAAdapter.FetchAllUnit();//load all units to local
            viewableUnit = new ObservableCollection<Unit>(unitList);
            foreach (Unit i in unitList)
            {
                i.classList = SchoolDBAAdapter.FetchClassByCode(i.Code);    
            }
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public void FilterByCampus(string code, Campus cam)
        {
            Campus all = ParseEnum<Campus>("All");
            classList = SchoolDBAAdapter.FetchClassByCode(code);
            viewableClass = new ObservableCollection<UnitClass>(classList);
            if (cam != all)
            {
                var SearchtName = from UnitClass uc in classList
                                  where (uc.Campus==cam)
                                  select uc;
                viewableClass.Clear();
                SearchtName.ToList().ForEach(viewableClass.Add);
            }
        }

        public void FilterBy(string name)
        {
            var SearchtName = from Unit u in unitList
                              where (u.UnitName.ToLower()).Contains(name.ToLower())
                              select u;
            viewableUnit.Clear();
            SearchtName.ToList().ForEach(viewableUnit.Add);
           /* Campus all = ParseEnum<Campus>("All");
            if (campus != all)
            {
                FilterByCampus(code,campus);
                List<UnitClass> view = viewableClass.ToList();
                foreach (UnitClass uc in viewableClass)

                {
                    if (campus != uc.Campus)
                    {
                        viewableClass.Remove(uc);
                    }
                }
            }*/
            //MessageBox.Show("filtered ");
        }

        public ObservableCollection<Unit> GetViewableUnit()
        {
            return VisibleCourse;
        }

        public ObservableCollection<UnitClass> GetViewableTimetable()
        {
            return VisibleTimetable;
        }
    }
}
