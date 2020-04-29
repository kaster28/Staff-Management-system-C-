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
using HRIS.Database;
using HRIS.Teaching;

namespace HRIS.View
{
    /// <summary>
    /// Interaction logic for StaffView.xaml
    /// </summary>
    public partial class StaffView : UserControl
    {
        private StaffController staffcontroller;
        private const string Key = "staffList";
        public StaffView()
        {
            InitializeComponent();
            staffcontroller = (StaffController)(Application.Current.FindResource(Key) as ObjectDataProvider).ObjectInstance;
        }

        private void StaffListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                StaffDetail.DataContext = e.AddedItems[0];
            }
        }

        private void UserControl1_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
