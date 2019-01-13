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
    /// Logique d'interaction pour MedecinDossier.xaml
    /// </summary>
    public partial class MedecinDossier : Window
    {
        decimal cb_Conge;
        public MedecinDossier()
        {
            InitializeComponent();
        }

        private void btCherche_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNSS.Text))
            {

                MessageBox.Show("Ce NSS est incorrect. Re-entrez-le!");
            }
            else

            {
                decimal snn = Convert.ToDecimal(tbNSS.Text);


                var query = from u in MainWindow.myBDD2.Patient
                            where u.NSS == snn
                            select u;

                var queryD = from f in MainWindow.myBDD2.DossierAdmission
                            where f.NSS == snn
                            select f;



                if (query.SingleOrDefault() == null)
                {
                    MessageBox.Show("Ce SNN est introuvable. Re-entrez-le!");

                }
                else
                 if (queryD.SingleOrDefault() == null)
                {
                    MessageBox.Show("Dossier de cet patient est introuvable. Re-entrez-le!");

                }
                else
                {
                    DossierAdmission p = MainWindow.myBDD2.DossierAdmission.Single(a => a.NSS == snn);
                    if (p.TypeChirurgie != null)
                    {
                        TypeChirurgie m = MainWindow.myBDD2.TypeChirurgie.Single(a => a.ID_TypeChirurgie == p.ID_TypeChirurgie);
                        tbProgramm.Text = m.Titre;
                        DateChir.SelectedDate = p.Date_chirurgie;
                        DateConge.SelectedDate = p.Date_conge;
                    }
                    else {
                        DateChir.SelectedDate = p.Date_chirurgie;
                        DateConge.SelectedDate = p.Date_conge;
                    }


                }
            }
        }

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            decimal snn = Convert.ToDecimal(tbNSS.Text);
            DossierAdmission maDoss = MainWindow.myBDD2.DossierAdmission.Single(a => a.NSS == snn);
            Lit maLit = MainWindow.myBDD2.Lit.Single(a => a.Numero_lit == maDoss.Numero_lit);

            maDoss.Date_chirurgie = (DateTime)DateChir.SelectedDate;
            maDoss.Date_conge = (DateTime)DateConge.SelectedDate;

            MainWindow.myBDD2.SaveChanges();
            MessageBox.Show("Information de dossier de patient changee avec succes!");

            if (cb_Conge == 1)
            {
                maLit.Occupe = 0;
            }


            MainWindow.myBDD2.SaveChanges();
            MessageBox.Show("List est libre!");
        }

        private void cbConge_Checked(object sender, RoutedEventArgs e)
        {
            cb_Conge = 1;
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

        private void tbNSS_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key < Key.D0 || e.Key > Key.D9)
            {
                //Détermine si la séquence de touches est un nombre du clavier.
                if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)
                {
                    if ((e.Key != Key.Back) && (e.Key != Key.Tab) && (e.Key != Key.OemComma))
                    {
                        e.Handled = true;
                        MessageBox.Show("J'accepte uniquement les chiffres, désolé.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
