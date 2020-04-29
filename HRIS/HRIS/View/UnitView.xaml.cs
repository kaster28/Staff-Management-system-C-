using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HRIS.Control;
using HRIS.Teaching;
using HRIS.Database;

namespace HRIS.View
{
    /// <summary>
    /// Interaction logic for UnitView.xaml
    /// </summary>
    public partial class UnitView : UserControl
    {
        private UnitController unitcontroller;
        private const string Key = "unitList";
        private UnitController classcontroller;
        private const string Key2 = "classList";
        private Unit cls;
        public UnitView()
        {
            InitializeComponent();
            unitcontroller = (UnitController)(Application.Current.FindResource(Key) as ObjectDataProvider).ObjectInstance;
            classcontroller = (UnitController)(Application.Current.FindResource(Key2) as ObjectDataProvider).ObjectInstance;
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        private void UnitListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                CourseDetail.DataContext = e.AddedItems[0];
                cls = unitBox.SelectedItem as Unit;   //Rationalize the selected item to unit
                CourseDetail.ItemsSource = cls.classList;
                 //MessageBox.Show(cls.Code); 
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string key = combol_campus.SelectedItem.ToString();
            Campus campus = ParseEnum<Campus>(key);
            if (cls != null) {
                //MessageBox.Show("xnull ");
                string code = cls.Code;
               
                classcontroller.FilterByCampus(code, campus);
                CourseDetail.ItemsSource = classcontroller.GetViewableTimetable();
            }
        }
    }
}
