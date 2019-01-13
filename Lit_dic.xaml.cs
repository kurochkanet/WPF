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
using System.Windows.Shapes;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for Lit_dic.xaml
    /// </summary>
    public partial class Lit_dic : Window
    {
        public Lit_dic()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var queryLit =
 from l in MainWindow.myBDD2.TypeLit

 select new { l.Description };
            dgLit.ItemsSource = queryLit.ToList();
        }
    }
}
