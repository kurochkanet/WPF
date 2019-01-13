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
    /// Interaction logic for FenetreAdmin.xaml
    /// </summary>
    public partial class FenetreAdmin : Window
    {

        decimal? id_dep;

        public FenetreAdmin()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult key = MessageBox.Show(
              "Voulez-vous vraiment quitter ?",
              "Confirmez",
              MessageBoxButton.YesNo,
              MessageBoxImage.Question,
              MessageBoxResult.No);
            e.Cancel = (key == MessageBoxResult.No);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow modmainif = new MainWindow();
            modmainif.ShowDialog();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            label.Visibility = Visibility.Hidden;
            tbNomMed.Visibility = Visibility.Hidden;
            label1.Visibility = Visibility.Hidden;
            tbPrenomMed.Visibility = Visibility.Hidden;
            label2.Visibility = Visibility.Hidden;
            tbSpecialite.Visibility = Visibility.Hidden;
            btOKMedecin.Visibility = Visibility.Hidden;
            label3.Visibility = Visibility.Hidden;
            cbDep.Visibility = Visibility.Hidden;
            gbActMed.Visibility = Visibility.Hidden;
            rbAc.Visibility = Visibility.Hidden;
            rbNA.Visibility = Visibility.Hidden;
            btModifMed.Visibility = Visibility.Hidden;



            label4.Visibility = Visibility.Hidden;
            tbNomEmp.Visibility = Visibility.Hidden;
            label5.Visibility = Visibility.Hidden;
            tbPrenomEmp.Visibility = Visibility.Hidden;
            label6.Visibility = Visibility.Hidden;
            tbLogin.Visibility = Visibility.Hidden;
            btAjouterEmploye.Visibility = Visibility.Hidden;
            label7.Visibility = Visibility.Hidden;
            tbMotdeP.Visibility = Visibility.Hidden;
            label8.Visibility = Visibility.Hidden;
            tbPosition.Visibility = Visibility.Hidden;
            gbActMed_Copy.Visibility = Visibility.Hidden;
            rbAc_Copy.Visibility = Visibility.Hidden;
            rbNA_Copy.Visibility = Visibility.Hidden;
            btModifEmp.Visibility = Visibility.Hidden;


            //// ComboBox Departement
            cbDep.DataContext = MainWindow.myBDD2.Departemente.ToList();
        }

        private void btAjTC_Click(object sender, RoutedEventArgs e)
        {

       
        }

        private void btDeparte_Click(object sender, RoutedEventArgs e)
        {
            Departement modDep= new Departement();
            modDep.ShowDialog();
        }

        private void btLit_Click(object sender, RoutedEventArgs e)
        {
            Lit_dic modLit= new Lit_dic();
            modLit.ShowDialog();
        }

        private void btAjoutMed_Click(object sender, RoutedEventArgs e)
        {
            label.Visibility = Visibility.Visible;
            tbNomMed.Visibility = Visibility.Visible;
            label1.Visibility = Visibility.Visible;
            tbPrenomMed.Visibility = Visibility.Visible;
            label2.Visibility = Visibility.Visible;
            tbSpecialite.Visibility = Visibility.Visible;
            btOKMedecin.Visibility = Visibility.Visible;
            label3.Visibility = Visibility.Visible;
            cbDep.Visibility = Visibility.Visible;
            rbAc.Visibility = Visibility.Hidden;
            rbNA.Visibility = Visibility.Hidden;
            btModifMed.Visibility = Visibility.Hidden;
            gbActMed.Visibility = Visibility.Hidden;
        }

        private void btOKMedecin_Click(object sender, RoutedEventArgs e)
        {
            if (
              string.IsNullOrWhiteSpace(tbNomMed.Text) ||
              string.IsNullOrWhiteSpace(tbPrenomMed.Text) ||
              string.IsNullOrWhiteSpace(tbSpecialite.Text) )
            { 
                MessageBox.Show("Tous les champs doivent être remplis!");
            }

            else
            {

                Medecin newMed = new Medecin();
                newMed.Nom = tbNomMed.Text;
                newMed.Prenom = tbPrenomMed.Text;
                newMed.Specialite = tbSpecialite.Text;
                newMed.ID_Departement =id_dep;
               

                MainWindow.myBDD2.Medecin.Add(newMed);
                MainWindow.myBDD2.SaveChanges();
                MessageBox.Show("Medecin ajoutee avec succes!");

                
                tbNomMed.Text = string.Empty;
                tbPrenomMed.Text = string.Empty;
                tbSpecialite.Text = string.Empty;
                cbDep.SelectedIndex = -1;
                

            }
        }

        private void cbDep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Departemente maDepartement= cbDep.SelectedItem as Departemente;
            id_dep= maDepartement.ID_Departement;
        }

        private void btAjouterEmploye_Click(object sender, RoutedEventArgs e)
        {
            if (
           string.IsNullOrWhiteSpace(tbNomEmp.Text) ||
           string.IsNullOrWhiteSpace(tbPrenomEmp.Text) ||
           string.IsNullOrWhiteSpace(tbLogin.Text) ||
            string.IsNullOrWhiteSpace(tbMotdeP.Text) ||
            string.IsNullOrWhiteSpace(tbPosition.Text)
           )
            {
                MessageBox.Show("Tous les champs doivent être remplis!");
            }

            else
            {

                Employe newEmp = new Employe();
                newEmp.Nom = tbNomEmp.Text;
                newEmp.Prenom = tbPrenomEmp.Text;
                newEmp.Login = tbLogin.Text;
                newEmp.MotDePass = tbMotdeP.Text;
                newEmp.Position = tbPosition.Text;
                newEmp.Active = "1";

                MainWindow.myBDD2.Employe.Add(newEmp);
                MainWindow.myBDD2.SaveChanges();
                MessageBox.Show("Employe ajoutee avec succes!");



                tbNomEmp.Text = string.Empty;
                tbPrenomEmp.Text = string.Empty;
                tbLogin.Text = string.Empty;
                tbMotdeP.Text = string.Empty;
                tbPosition.Text = string.Empty;


            }
        }

        private void btAjoutEmploye_Click(object sender, RoutedEventArgs e)
        {
            label4.Visibility = Visibility.Visible;
            tbNomEmp.Visibility = Visibility.Visible;
            label5.Visibility = Visibility.Visible;
            tbPrenomEmp.Visibility = Visibility.Visible;
            label6.Visibility = Visibility.Visible;
            tbLogin.Visibility = Visibility.Visible;
            btAjouterEmploye.Visibility = Visibility.Visible;
            label7.Visibility = Visibility.Visible;
            tbMotdeP.Visibility = Visibility.Visible;
            label8.Visibility = Visibility.Visible;
            tbPosition.Visibility = Visibility.Visible;
            rbAc_Copy.Visibility = Visibility.Hidden;
            rbNA_Copy.Visibility = Visibility.Hidden;
            gbActMed_Copy.Visibility = Visibility.Hidden;
            btModifEmp.Visibility = Visibility.Hidden;
        }

        private void btModifierMed_Click(object sender, RoutedEventArgs e)
        {
            label.Visibility = Visibility.Visible;
            tbNomMed.Visibility = Visibility.Visible;
            label1.Visibility = Visibility.Visible;
            tbPrenomMed.Visibility = Visibility.Visible;
            label2.Visibility = Visibility.Visible;
            tbSpecialite.Visibility = Visibility.Visible;
            label3.Visibility = Visibility.Visible;
            cbDep.Visibility = Visibility.Visible;
            btOKMedecin.Visibility = Visibility.Hidden;
            btModifMed.Visibility = Visibility.Visible;
            gbActMed.Visibility = Visibility.Visible;
            rbAc.Visibility = Visibility.Visible;
            rbNA.Visibility = Visibility.Visible;
            btModifMed.Visibility = Visibility.Visible;
            btAjouterEmploye.Visibility = Visibility.Hidden;

        }

        private void btQuitterMed_Click(object sender, RoutedEventArgs e)
        {
             MainWindow modmain = new MainWindow();
            modmain.ShowDialog();
                this.Close();
        }       

        private void btModifMed_Click(object sender, RoutedEventArgs e)
        {
            double activeMedecin = 0;
            if (rbAc.IsChecked == true)
            {
                activeMedecin = 1;
            }
            else
                activeMedecin = 0;


            //Medecin maMed = (Medecin)cbPatient.SelectedItem;
            //maMed.Nom = tbNomMed.Text;
            //maMed.Prenom = tbPrenomMed.Text;
            //maMed.Specialite = tbSpecialite.Text;
          
            ////passer la valeur de la liste déroulante à la table
            ////1//            maPatient.ID_Medicin = cbMedecin.SelectedItem;
            ////2//
            //Departemente maDep = cbDep.SelectedItem as Departemente;
            //maDep.ID_Departement = maMed.ID_Departement;

            MainWindow.myBDD2.SaveChanges();
            MessageBox.Show("Information de medecin changee avec succes!");
        }

        private void btModifEmp_Click(object sender, RoutedEventArgs e)
        {
            double activeEmploye = 0;
            if (rbAc_Copy.IsChecked == true)
            {
                activeEmploye = 1;
            }
            else
                activeEmploye = 0;
        }

        private void btModEmp_Click_1(object sender, RoutedEventArgs e)
        {
            rbAc_Copy.Visibility = Visibility.Visible;
            rbNA_Copy.Visibility = Visibility.Visible;
            btModifEmp.Visibility = Visibility.Visible;
            btAjouterEmploye.Visibility = Visibility.Hidden;
            gbActMed_Copy.Visibility = Visibility.Visible;

        }


    }
}
