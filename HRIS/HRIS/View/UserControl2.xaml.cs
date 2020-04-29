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
    /// Interaction logic for UserControl2.xaml
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        private UnitController unitcontroller;
        private const string Key = "unitList";
        public UserControl2()
        {
            InitializeComponent();
            unitcontroller = (UnitController)(Application.Current.FindResource(Key) as ObjectDataProvider).ObjectInstance;
        }

        private void search_KeyChanged(object sender, TextChangedEventArgs e)//search
        {
            if (unitcontroller != null)
            {
                //string key = null;
                string name = searchKey.Text;
                //key = combol_campus.SelectedItem.ToString();
                //Campus campus = ParseEnum<Campus>(key);

                unitcontroller.FilterBy(name);
            }
        }

    }
}
