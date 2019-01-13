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
    /// Interaction logic for Departement.xaml
    /// </summary>
    public partial class Departement : Window
    {
        public Departement()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var queryDep =
          from r in MainWindow.myBDD2.Departemente

          select new { r.Nom };
            dgDep.ItemsSource = queryDep.ToList();

        }
    }
}
