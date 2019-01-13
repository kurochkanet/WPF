using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for FenetrePrepose.xaml
    /// </summary>
    public partial class FenetrePrepose : Window
    {

        decimal Id_medicin;
        decimal? Id_assurence;

        decimal? Id_parent;
        string numero_lit;
        decimal? type_chirurgie;
        decimal Id_commod;
        public decimal id_type_chambre;
        public decimal qte_patients=0;
        public decimal qte_lit_libre=0;

        decimal? id_departement;

        //  DateTime? date_nais;

        public FenetrePrepose()
        {
            InitializeComponent();
        }

        private void btQuitterPatient_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Quitter le programme sans sauvegarder les données?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                
                MainWindow modmainif = new MainWindow();
                modmainif.ShowDialog();
                this.Close();
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            //// ComboBox Medecin
            cbMedecin.DataContext = MainWindow.myBDD2.Medecin.ToList();
            //// ComboBox Assurence
            cbAssur.DataContext = MainWindow.myBDD2.Compagnie_Assurance.ToList();
            //// ComboBox Parent
            cbParent.DataContext = MainWindow.myBDD2.Parent.ToList();
            //// ComboBox TypeChambre
            //cbTypeChambre.DataContext = MainWindow.myBDD2.TypeChambre.ToList();


            //// ComboBox Chambre
            cbChambre_Sel.DataContext = MainWindow.myBDD2.Chambre.ToList();


            //// ComboBox Lit
            //cbTypeLit.DataContext = MainWindow.myBDD2.TypeLit.ToList();
            //// ComboBox Commodite
            cbComm.DataContext = MainWindow.myBDD2.TypeCommodite.ToList();
            //// ComboBox Chirurgie programme
            cbChirurProgramm.DataContext = MainWindow.myBDD2.TypeChirurgie.ToList();
            //// ComboBox Chirurgie programme
            //cbNumeroLit.DataContext = MainWindow.myBDD2.Lit.ToList();

            cbParent.Visibility = Visibility.Hidden;
            label14.Visibility = Visibility.Hidden;
            btAjPar.Visibility = Visibility.Hidden;


           


        }



        private void btReinitialiserPatient_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Vous voulez réinitialiser toutes les données?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                tbNSS.Text = string.Empty;
                tbNom.Text = string.Empty;
                tbPrenom.Text = string.Empty;
                tbAdress.Text = string.Empty;
                tbVille.Text = string.Empty;
                tbProvince.Text = string.Empty;
                tbCodePostal.Text = string.Empty;
                tbTeleph.Text = string.Empty;
                DateNaiss.SelectedDate = null;
             
                  cbParent.SelectedIndex = -1;
                  cbMedecin.SelectedIndex = -1;
                  cbAssur.SelectedIndex = -1;

                if (2019 - DateNaiss.DisplayDate.Year <= 18)
                {
                    cbParent.Visibility = Visibility.Visible;
                    label14.Visibility = Visibility.Visible;
                    btAjPar.Visibility = Visibility.Visible;
                }
                else
                {
                    cbParent.Visibility = Visibility.Hidden;
                    label14.Visibility = Visibility.Hidden;
                    btAjPar.Visibility = Visibility.Hidden;
                    label26_Copy9.Visibility = Visibility.Hidden;
                }
            }

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

        private void tbTeleph_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void btRechercherPatient_Click(object sender, RoutedEventArgs e)
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

                if (query.SingleOrDefault() == null)
                {
                    MessageBox.Show("Ce SNN est introuvable. Re-entrez-le!");

                }               
                else
                {
                    Patient p = MainWindow.myBDD2.Patient.Single(a => a.NSS == snn);


                    if (p.ID_Medicin != null)
                    {
                        Medecin m = MainWindow.myBDD2.Medecin.Single(a => a.ID_Medicin == p.ID_Medicin);
                        cbMedecin.Text = m.Specialite;
                    }

                    if (p.Ref_parent != null)
                    {
                        Parent r = MainWindow.myBDD2.Parent.Single(a => a.Ref_parent == p.Ref_parent);
                        cbParent.Text = r.Nom;

                    }
                    else
                    {
                        cbParent.SelectedIndex = -1;
                    }
                    if (p.Compagnie_Assurance != null)
                    {
                        Compagnie_Assurance c = MainWindow.myBDD2.Compagnie_Assurance.Single(a => a.ID_Compagnie == p.ID_Compagnie);
                        cbAssur.Text = c.Nom;
                    }
                    else
                    {
                        cbAssur.SelectedIndex = -1;
                    }

                    tbNom.Text = p.Nom;
                    tbPrenom.Text = p.Prenom;
                    DateNaiss.SelectedDate = p.Date_Naissance;
                    tbAdress.Text = p.Adresse;
                    tbVille.Text = p.Ville;
                    tbProvince.Text = p.Province;
                    tbCodePostal.Text = p.Code_postal;
                    tbTeleph.Text = Convert.ToString(p.Telephone);
                   



                    lbPatient.Content = p.Nom + " " + p.Prenom + " ( NSS: " + p.NSS + ")";
                    lbPatDoss.Content = p.Nom + " " + p.Prenom + " ( NSS: " + p.NSS + ")";

                    if (2019 - DateNaiss.DisplayDate.Year <= 18)
                    {
                        cbParent.Visibility = Visibility.Visible;
                        label14.Visibility = Visibility.Visible;
                        btAjPar.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        cbParent.Visibility = Visibility.Hidden;
                        label14.Visibility = Visibility.Hidden;
                        btAjPar.Visibility = Visibility.Hidden;
                        label26_Copy9.Visibility = Visibility.Hidden;
                    }

                    Medecin TMed = cbMedecin.SelectedItem as Medecin;
                    id_departement = TMed.ID_Departement;

                  //
                    var queryChambre =
                        from fac in MainWindow.myBDD2.Chambre
                        join f in MainWindow.myBDD2.Lit on fac.ID_chambre equals f.ID_Chambre
                        join tc in MainWindow.myBDD2.TypeChambre on fac.ID_Type equals tc.ID_Type
                        where fac.ID_Departement == id_departement && f.Occupe == 0
                        group tc by tc.Description into r

                        select r;
                    cbTypeChambre.ItemsSource = queryChambre.ToList();

                  //
                    var queryLit =
                     from fac in MainWindow.myBDD2.Chambre
                     join f in MainWindow.myBDD2.Lit on fac.ID_chambre equals f.ID_Chambre
                     where fac.ID_Departement == id_departement && f.Occupe == 0
                     select f;
                     cbNumeroLit.ItemsSource = queryLit.ToList();


                    //
                 


                    // DataGrid Formulaire

                    var queryForm =
                        from fac in MainWindow.myBDD2.Facture
                        join f in MainWindow.myBDD2.Frais on fac.ID_Frais equals f.ID_frais
                        join tcom in MainWindow.myBDD2.TypeCommodite on f.ID_Commodite equals tcom.ID_Commodite
                        where fac.NSS == snn

                        select new { Nom__ = f.Nom, tcom.Nom, fac.Qte, f.Prix };
                    dgCommodites.ItemsSource = queryForm.ToList();

                }
            }
        }

        private void btSauvegarderPatient_Click_1(object sender, RoutedEventArgs e)
        {

            decimal snn = Convert.ToDecimal(tbNSS.Text);
            Patient maPatient = MainWindow.myBDD2.Patient.Single(a => a.NSS == snn);

            maPatient.NSS = Convert.ToDecimal(tbNSS.Text);
            maPatient.Date_Naissance = (DateTime)DateNaiss.SelectedDate; 
            maPatient.Nom = tbNom.Text;
            maPatient.Prenom = tbPrenom.Text;
            maPatient.Adresse = tbAdress.Text;
            maPatient.Ville = tbVille.Text;
            maPatient.Province = tbProvince.Text;
            maPatient.Code_postal = tbCodePostal.Text;        
            maPatient.Telephone = Convert.ToDecimal(tbTeleph.Text);
            Medecin maMedecin = cbMedecin.SelectedItem as Medecin;
            maPatient.ID_Medicin = maMedecin.ID_Medicin;
            Compagnie_Assurance maAssur = cbAssur.SelectedItem as Compagnie_Assurance;
            maPatient.ID_Compagnie = maAssur.ID_Compagnie;
            Parent maParent = cbParent.SelectedItem as Parent;
            maPatient.Ref_parent = maParent.Ref_parent;


            MainWindow.myBDD2.SaveChanges();
            MessageBox.Show("Information de patient changee avec succes!");

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Medecin maMedecin = cbMedecin.SelectedItem as Medecin;
            Id_medicin = maMedecin.ID_Medicin;
        }

        
        private void cbAssur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Compagnie_Assurance maAssur = cbAssur.SelectedItem as Compagnie_Assurance;
            Id_assurence = maAssur.ID_Compagnie;
        }

        private void cbParent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Parent maParent = cbParent.SelectedItem as Parent;
            Id_parent = maParent.Ref_parent;
        }

        private void btQuitterForm_Click(object sender, RoutedEventArgs e)
        {
            MainWindow modmainif = new MainWindow();
            modmainif.ShowDialog();
            this.Close();
        }

        private void cbChirurProgramm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TypeChirurgie TypeCh= cbChirurProgramm.SelectedItem as TypeChirurgie;
            type_chirurgie = TypeCh.ID_TypeChirurgie;
        }

        private void btAjouterAdmis_Click(object sender, RoutedEventArgs e)
        {


            if (
                  string.IsNullOrWhiteSpace(tbNSS.Text) ||
                  cbChirurProgramm.SelectedIndex == -1 ||
                  cbNumeroLit.SelectedIndex == -1 ||
                  DateAdm.SelectedDate == null ||
                  DateChirur.SelectedDate == null ||
                  DateConge.SelectedDate == null)
            {
                MessageBox.Show("Tous les champs doivent être remplis!");
            }
            else
            {

                DossierAdmission newDossierAdmission = new DossierAdmission();
                //   Lit maLit = MainWindow.myBDD2.Lit.Single(a => a.Numero_lit == newDossierAdmission.Numero_lit);
                Lit updLit = cbNumeroLit.SelectedItem as Lit;

                newDossierAdmission.Date_admission = (DateTime)DateAdm.SelectedDate;
                newDossierAdmission.Date_chirurgie = (DateTime)DateChirur.SelectedDate;
                newDossierAdmission.Date_conge = (DateTime)DateConge.SelectedDate;
                newDossierAdmission.NSS = Convert.ToDecimal(tbNSS.Text);
                newDossierAdmission.Numero_lit = updLit.Numero_lit;
                newDossierAdmission.ID_TypeChirurgie = type_chirurgie;


                MainWindow.myBDD2.DossierAdmission.Add(newDossierAdmission);


                updLit.Occupe = 1;

                MainWindow.myBDD2.SaveChanges();
                MessageBox.Show("Dossier ajoutee avec succes!");
            }
        }

        private void cbNumeroLit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Lit NLit = cbNumeroLit.SelectedItem as Lit;
            numero_lit = NLit.Numero_lit;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow modmainif = new MainWindow();
            modmainif.ShowDialog();
            this.Close();
        }

        private void btAjPar_Click(object sender, RoutedEventArgs e)
        {
            AjouterParent par= new AjouterParent();
            par.ShowDialog();
           
        }

        private void btAjoutPatient_Click(object sender, RoutedEventArgs e)
        {
            Medecin TMed = cbMedecin.SelectedItem as Medecin;
            id_departement = TMed.ID_Departement;

            //type chambre
            cbTypeChambre.DataContext = (from fac in MainWindow.myBDD2.Chambre
                                         join f in MainWindow.myBDD2.Lit on fac.ID_chambre equals f.ID_Chambre
                                         join tc in MainWindow.myBDD2.TypeChambre on fac.ID_Type equals tc.ID_Type
                                         where fac.ID_Departement == id_departement && f.Occupe == 0
                                         group tc by tc.Description into r

                                         select r).ToList();

            var queryLit =
             from fac in MainWindow.myBDD2.Chambre
             join f in MainWindow.myBDD2.Lit on fac.ID_chambre equals f.ID_Chambre
             where fac.ID_Departement == id_departement && f.Occupe == 0
             select f;
            cbNumeroLit.ItemsSource = queryLit.ToList();
            
            string position = "";
            var queryQteLit = from ch in MainWindow.myBDD2.Chambre
                              join lit in MainWindow.myBDD2.Lit on ch.ID_chambre equals lit.ID_Chambre
                              where ch.ID_Departement == id_departement && lit.Occupe == 0
                              group ch by ch.ID_Departement into g

                              select new { Qty_Lit = g.Count() };

            var entity = queryQteLit.FirstOrDefault();
            if (entity == null)
            {
                MessageBox.Show("Pas de lits libres dans ce departement.!");

            }
            else

            if (cbParent.Visibility == Visibility.Visible)
            {
                if (tbNSS.Text == "" ||
                   string.IsNullOrWhiteSpace(tbNom.Text) ||
                   string.IsNullOrWhiteSpace(tbPrenom.Text) ||
                   string.IsNullOrWhiteSpace(tbAdress.Text) ||
                   string.IsNullOrWhiteSpace(tbVille.Text) ||
                   string.IsNullOrWhiteSpace(tbTeleph.Text) ||
                   cbMedecin.SelectedIndex == -1 ||
                   cbParent.SelectedIndex == -1 ||
                   DateNaiss.SelectedDate == null)
                {
                    label26.Visibility = Visibility.Visible;
                    label26_Copy.Visibility = Visibility.Visible;
                    label26_Copy1.Visibility = Visibility.Visible;
                    label26_Copy2.Visibility = Visibility.Visible;
                    label26_Copy3.Visibility = Visibility.Visible;
                    label26_Copy4.Visibility = Visibility.Visible;
                    label26_Copy7.Visibility = Visibility.Visible;
                    label26_Copy8.Visibility = Visibility.Visible;
                    label26_Copy9.Visibility = Visibility.Visible;
                    MessageBox.Show("Tous les champs marqués d'un " + " '*' " + " doivent être remplis!");
                }

                else
                {
                    Patient newPatient = new Patient();
                    newPatient.NSS = Convert.ToDecimal(tbNSS.Text);
                    newPatient.Date_Naissance = (DateTime)DateNaiss.SelectedDate;
                    newPatient.Nom = tbNom.Text;
                    newPatient.Prenom = tbPrenom.Text;
                    newPatient.Adresse = tbAdress.Text;
                    newPatient.Ville = tbVille.Text;
                    newPatient.Province = tbProvince.Text;
                    newPatient.Code_postal = tbCodePostal.Text;
                    newPatient.Telephone = Convert.ToDecimal(tbTeleph.Text);
                    Medecin maMedecin = cbMedecin.SelectedItem as Medecin;
                    newPatient.ID_Medicin = maMedecin.ID_Medicin;
                    Parent maParent = cbParent.SelectedItem as Parent;
                    newPatient.Ref_parent = maParent.Ref_parent;                    
                    newPatient.ID_Compagnie = Id_assurence; 

                    MainWindow.myBDD2.Patient.Add(newPatient);
                    MainWindow.myBDD2.SaveChanges();
                    MessageBox.Show("Patient ajoutee avec succes!");

                    lbPatient.Content = tbNom.Text + " " + tbPrenom.Text + " ( NSS: " + tbNSS.Text + ")";
                    lbPatDoss.Content = tbNom.Text + " " + tbPrenom.Text + " ( NSS: " + tbNSS.Text + ")";

                    decimal nss = Convert.ToDecimal(tbNSS.Text);
                    // DataGrid Formulaire
                    var queryForm =
                        from fac in MainWindow.myBDD2.Facture
                        join f in MainWindow.myBDD2.Frais on fac.ID_Frais equals f.ID_frais
                        join tcom in MainWindow.myBDD2.TypeCommodite on f.ID_Commodite equals tcom.ID_Commodite
                        where fac.NSS == nss

                        select new { Nom__ = f.Nom, tcom.Nom, fac.Qte, f.Prix, Somme = fac.Qte * f.Prix };
                    dgCommodites.ItemsSource = queryForm.ToList();

                    //tbNSS.Text = string.Empty;
                    //DateNaiss.SelectedDate = null;
                    //tbNom.Text = string.Empty;
                    //tbPrenom.Text = string.Empty;
                    //tbAdress.Text = string.Empty;
                    //tbVille.Text = string.Empty;
                    //tbProvince.Text = string.Empty;
                    //tbCodePostal.Text = string.Empty;
                    //tbTeleph.Text = string.Empty;
                    //cbParent.SelectedIndex = -1;
                    //cbMedecin.SelectedIndex = -1;
                    //cbAssur.SelectedIndex = -1;

                    //Id_medicin = 0;
                    label26.Visibility = Visibility.Hidden;
                    label26_Copy.Visibility = Visibility.Hidden;
                    label26_Copy1.Visibility = Visibility.Hidden;
                    label26_Copy2.Visibility = Visibility.Hidden;
                    label26_Copy3.Visibility = Visibility.Hidden;
                    label26_Copy4.Visibility = Visibility.Hidden;
                    label26_Copy7.Visibility = Visibility.Hidden;
                    label26_Copy8.Visibility = Visibility.Hidden;
                    label26_Copy9.Visibility = Visibility.Hidden;
                    cbParent.Visibility = Visibility.Hidden;
                    label14.Visibility = Visibility.Hidden;
                    btAjPar.Visibility = Visibility.Hidden;

                }
            }
            else if (tbNSS.Text == "" ||
                string.IsNullOrWhiteSpace(tbNom.Text) ||
                string.IsNullOrWhiteSpace(tbPrenom.Text) ||
                string.IsNullOrWhiteSpace(tbAdress.Text) ||
                string.IsNullOrWhiteSpace(tbVille.Text) ||
                string.IsNullOrWhiteSpace(tbTeleph.Text) ||
                cbMedecin.SelectedIndex == -1 ||
                DateNaiss.SelectedDate == null)
            {
                label26.Visibility = Visibility.Visible;
                label26_Copy.Visibility = Visibility.Visible;
                label26_Copy1.Visibility = Visibility.Visible;
                label26_Copy2.Visibility = Visibility.Visible;
                label26_Copy3.Visibility = Visibility.Visible;
                label26_Copy4.Visibility = Visibility.Visible;
                label26_Copy7.Visibility = Visibility.Visible;
                label26_Copy8.Visibility = Visibility.Visible;
                MessageBox.Show("Tous les champs marqués d'un " + " '*' " + " doivent être remplis!");
            }

            else
            {
                Patient newPatient = new Patient();
                newPatient.NSS = Convert.ToDecimal(tbNSS.Text);
                newPatient.Date_Naissance = (DateTime)DateNaiss.SelectedDate;
                newPatient.Nom = tbNom.Text;
                newPatient.Prenom = tbPrenom.Text;
                newPatient.Adresse = tbAdress.Text;
                newPatient.Ville = tbVille.Text;
                newPatient.Province = tbProvince.Text;
                newPatient.Code_postal = tbCodePostal.Text;
                newPatient.Telephone = Convert.ToDecimal(tbTeleph.Text);
                Medecin maMedecin = cbMedecin.SelectedItem as Medecin;
                newPatient.ID_Medicin = maMedecin.ID_Medicin;      
                newPatient.ID_Compagnie = Id_assurence;
                newPatient.Ref_parent = null;



                MainWindow.myBDD2.Patient.Add(newPatient);
                MainWindow.myBDD2.SaveChanges();
                MessageBox.Show("Patient ajoutee avec succes!");


                lbPatient.Content = tbNom.Text + " " + tbPrenom.Text + " ( NSS: " + tbNSS.Text + ")";
                lbPatDoss.Content = tbNom.Text + " " + tbPrenom.Text + " ( NSS: " + tbNSS.Text + ")";

                // DataGrid Formulaire
                decimal nss = Convert.ToDecimal(tbNSS.Text);
                var queryForm =
                    from fac in MainWindow.myBDD2.Facture
                    join f in MainWindow.myBDD2.Frais on fac.ID_Frais equals f.ID_frais
                    join tcom in MainWindow.myBDD2.TypeCommodite on f.ID_Commodite equals tcom.ID_Commodite
                    where fac.NSS == nss

                    select new { Nom__ = f.Nom, tcom.Nom, fac.Qte, f.Prix, Somme = fac.Qte * f.Prix };
                dgCommodites.ItemsSource = queryForm.ToList();

                //tbNSS.Text = string.Empty;
                //DateNaiss.SelectedDate = null;
                //tbNom.Text = string.Empty;
                //tbPrenom.Text = string.Empty;
                //tbAdress.Text = string.Empty;
                //tbVille.Text = string.Empty;
                //tbProvince.Text = string.Empty;
                //tbCodePostal.Text = string.Empty;
                //tbTeleph.Text = string.Empty;
                //cbParent.SelectedIndex = -1;
                //cbMedecin.SelectedIndex = -1;
                //cbAssur.SelectedIndex = -1;

                Id_medicin = 0;
                label26.Visibility = Visibility.Hidden;
                label26_Copy.Visibility = Visibility.Hidden;
                label26_Copy1.Visibility = Visibility.Hidden;
                label26_Copy2.Visibility = Visibility.Hidden;
                label26_Copy3.Visibility = Visibility.Hidden;
                label26_Copy4.Visibility = Visibility.Hidden;
                label26_Copy7.Visibility = Visibility.Hidden;
                label26_Copy8.Visibility = Visibility.Hidden;
                label26_Copy9.Visibility = Visibility.Hidden;
                cbParent.Visibility = Visibility.Hidden;
                label14.Visibility = Visibility.Hidden;
                btAjPar.Visibility = Visibility.Hidden;
            }

            }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            if (lbPatient.Content == ""||tbQteComm.Text == "" ||  cbComm.SelectedIndex == -1 )
            {               
                MessageBox.Show("Pas asser information(Nom de patient,type commodite ou quantité!",
                "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else
            {
                Facture newFacture = new Facture();
                Frais maFrais = MainWindow.myBDD2.Frais.Single(a => a.ID_Commodite == Id_commod);

                newFacture.NSS = Convert.ToDecimal(tbNSS.Text);
                newFacture.ID_Frais = maFrais.ID_frais;
                newFacture.Qte = Convert.ToDecimal(tbQteComm.Text);

                MainWindow.myBDD2.Facture.Add(newFacture);
                MainWindow.myBDD2.SaveChanges();
                MessageBox.Show("Commodite ajoutee avec succes!");             


                decimal nss = Convert.ToDecimal(tbNSS.Text);

                var queryForm =
                  from fac in MainWindow.myBDD2.Facture
                  join f in MainWindow.myBDD2.Frais on fac.ID_Frais equals f.ID_frais
                  join tcom in MainWindow.myBDD2.TypeCommodite on f.ID_Commodite equals tcom.ID_Commodite
                  where fac.NSS == nss

                  select new { Nom__ = f.Nom, tcom.Nom, fac.Qte, f.Prix, Somme = fac.Qte * f.Prix };
                dgCommodites.ItemsSource = queryForm.ToList();

            }
        }

        private void cbComm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TypeCommodite TCommod = cbComm.SelectedItem as TypeCommodite;
            Id_commod = TCommod.ID_Commodite;
           
           
        }

        private void btSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (dgCommodites.SelectedIndex != -1)
            {



            }
            else
                MessageBox.Show("Aucun article n'est selectioné!","Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

      

        private void tbQteComm_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key < Key.D0 || e.Key > Key.D9)
            {
                //Détermine si la séquence de touches est un nombre du clavier.
                if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)
                {
                    if ((e.Key != Key.Back) && (e.Key != Key.Tab) && (e.Key != Key.OemComma))
                    {
                        e.Handled = true;
                        MessageBox.Show("Accepte uniquement les chiffres, désolé.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

      
        private void DateNaiss_CalendarClosed(object sender, RoutedEventArgs e)
        {

            if (2019 - DateNaiss.DisplayDate.Year <= 18)
            {
                cbParent.Visibility = Visibility.Visible;
                label14.Visibility = Visibility.Visible;
                btAjPar.Visibility = Visibility.Visible;
            }
            else
            {
                cbParent.Visibility = Visibility.Hidden;
                label14.Visibility = Visibility.Hidden;
                btAjPar.Visibility = Visibility.Hidden;
                label26_Copy9.Visibility = Visibility.Hidden;
            }
        }

     


    }
    }

