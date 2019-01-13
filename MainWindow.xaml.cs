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

namespace Hospital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static HospytalEntities2 myBDD2;

        public MainWindow()
        {
            InitializeComponent();
            tbLogin.Text = "prepose";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            myBDD2 = new HospytalEntities2();
        }



        private void button_OK(object sender, RoutedEventArgs e)
        {
            if (
             string.IsNullOrWhiteSpace(tbLogin.Text) ||
             string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                MessageBox.Show("Tous les champs doivent être remplis!");
            }

            else
            {
                string login = tbLogin.Text;
                string password = passwordBox.Password;
                string position = "";

                var query = from p in MainWindow.myBDD2.Employe
                            where (p.Login == login && p.MotDePass == password)
                select p;
         
                var entity =  query.FirstOrDefault();

                if (entity == null)
                {
                    tbLogin.Text = string.Empty;
                    passwordBox.Password = string.Empty;
                    MessageBox.Show("Votre login ou mot de passe sont incorrects. Re-entrez-les.!");
                }
                else
                {
                    position = query.FirstOrDefault().Position;

                    if (position == "Prepose")
                    {
                        FenetrePrepose fPrep = new FenetrePrepose();
                        fPrep.Show();
                        this.Close();
                    }
                    if (position == "Admin")
                    {
                        FenetreAdmin fAdmin = new FenetreAdmin();
                        fAdmin.Show();
                        this.Close();
                    }
                    if (position == "Medecin")
                    {
                        MedecinDossier fMed = new MedecinDossier();
                        fMed.Show();
                        this.Close();
                    }
                }
            }
        }

        private void button1_Annuler(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
