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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private StaffController staffcontroller;
        private const string Key = "staffList";
        public UserControl1()
        {
            InitializeComponent();
            staffcontroller = (StaffController)(Application.Current.FindResource(Key) as ObjectDataProvider).ObjectInstance;
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        private void search_KeyChanged(object sender, TextChangedEventArgs e)//search
        {
            if (staffcontroller != null)
            {
                string key = null;
                string name = searchKey.Text;
                key = combol_category.SelectedItem.ToString();
                Category category = ParseEnum<Category>(key);

                staffcontroller.FilterBy(category, name);
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)//filter
        {
           if (staffcontroller!=null)
            {
                string key = null;
                string name = searchKey.Text;
                key = combol_category.SelectedItem.ToString();
                Category category = ParseEnum<Category>(key);

                staffcontroller.FilterBy(category, name);

            }
        }

       
    }
}
