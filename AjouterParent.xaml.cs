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
    /// Interaction logic for AjouterParent.xaml
    /// </summary>
    public partial class AjouterParent : Window
    {
        public AjouterParent()
        {
            InitializeComponent();
        }

        private void btReinitialiserPatient_Click(object sender, RoutedEventArgs e)
        {
            tbNom.Text = string.Empty;
            tbPrenom.Text = string.Empty;
            tbAdress.Text = string.Empty;
            tbVille.Text = string.Empty;
            tbProvince.Text = string.Empty;
            tbCodePostal.Text = string.Empty;
            tbTeleph.Text = string.Empty;
        }

        private void btQuitterPatient_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Quitter le programme sans sauvegarder les données?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                FenetrePrepose modmainif = new FenetrePrepose();
                modmainif.cbParent.DataContext = MainWindow.myBDD2.Parent.ToList();
                modmainif.ShowDialog();
                this.Close();
            }
        }

        private void btAjParent_Click(object sender, RoutedEventArgs e)
        {
            if (
               string.IsNullOrWhiteSpace(tbNom.Text) ||
               string.IsNullOrWhiteSpace(tbPrenom.Text) ||
               string.IsNullOrWhiteSpace(tbAdress.Text) ||
               string.IsNullOrWhiteSpace(tbVille.Text) ||
               string.IsNullOrWhiteSpace(tbTeleph.Text)  )
            {
                label26.Visibility = Visibility.Visible;
                label26_Copy.Visibility = Visibility.Visible;
                label26_Copy1.Visibility = Visibility.Visible;
                label26_Copy2.Visibility = Visibility.Visible;
                label26_Copy3.Visibility = Visibility.Visible;
            
                MessageBox.Show("Tous les champs marqués d'un " + " '*' " + " doivent être remplis!");
            }

            else
            {

                Parent newParent = new Parent();
                newParent.Nom = tbNom.Text;
                newParent.Prenom = tbPrenom.Text;
                newParent.Adresse = tbAdress.Text;
                newParent.Ville = tbVille.Text;
                newParent.Province = tbProvince.Text;
                newParent.Code_postal = tbCodePostal.Text;
                newParent.Telephone = tbTeleph.Text;
              
                MainWindow.myBDD2.Parent.Add(newParent);
                MainWindow.myBDD2.SaveChanges();
                MessageBox.Show("Parent ajoutee avec succes!");

                label26.Visibility = Visibility.Hidden;
                label26_Copy.Visibility = Visibility.Hidden;
                label26_Copy1.Visibility = Visibility.Hidden;
                label26_Copy2.Visibility = Visibility.Hidden;
                label26_Copy3.Visibility = Visibility.Hidden;

                tbNom.Text = string.Empty;
                tbPrenom.Text = string.Empty;
                tbAdress.Text = string.Empty;
                tbVille.Text = string.Empty;
                tbProvince.Text = string.Empty;
                tbCodePostal.Text = string.Empty;
                tbTeleph.Text = string.Empty;

                

            }
        }

  
        private void btRechercherParent_Click(object sender, RoutedEventArgs e)
        {
            string nomp = tbNom.Text;

            var query = from c in MainWindow.myBDD2.Parent
                        where c.Nom == nomp
                        select c;

            if (query.FirstOrDefault() == null)
            {
              MessageBox.Show("Ce nom est introuvable. Re-entrez-le!");
            }
            else
            {

                tbNom.Text = query.FirstOrDefault().Nom;
                tbPrenom.Text = query.FirstOrDefault().Prenom;
                tbAdress.Text = query.FirstOrDefault().Adresse;
                tbVille.Text = query.FirstOrDefault().Ville;
                tbProvince.Text = query.FirstOrDefault().Province;
                tbCodePostal.Text = query.FirstOrDefault().Code_postal;
                tbTeleph.Text = Convert.ToString(query.FirstOrDefault().Telephone);
            }
        }

        private void btSauvegarderPar_Click(object sender, RoutedEventArgs e)
        {
            string nom = tbNom.Text;
            Parent maPatient = MainWindow.myBDD2.Parent.Single(a => a.Nom == nom);

            maPatient.Nom = tbNom.Text;
            maPatient.Prenom = tbPrenom.Text;
            maPatient.Adresse = tbAdress.Text;
            maPatient.Ville = tbVille.Text;
            maPatient.Province = tbProvince.Text;
            maPatient.Code_postal = tbCodePostal.Text;
            maPatient.Telephone = tbTeleph.Text;

            MainWindow.myBDD2.SaveChanges();
            MessageBox.Show("Information de parent changee avec succes!");
        }
    }

}