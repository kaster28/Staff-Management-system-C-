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
using HRIS.View;
namespace HRIS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       public void Tag_Changed(object sender, SelectionChangedEventArgs e)
        {
            TabItem a = TagControl.SelectedItem as TabItem;
            string key = a.Header.ToString();
            if (key == "Staff")
            {
                ViewContent.Content = new StaffView();
            }

            if (key == "Unit")
            {
                ViewContent.Content = new UnitView();
            }
        }
    }
}
