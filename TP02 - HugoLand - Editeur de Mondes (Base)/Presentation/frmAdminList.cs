using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP01_Library;
using TP01_Library.Controllers;
namespace HugoLandEditeur.Presentation
{
    public partial class frmAdminList : Form
    {
        public List<CompteJoueur> lstPlayers = new List<CompteJoueur>();
        CompteJoueurController controller = new CompteJoueurController();

        /// <summary>
        /// Description : Initialise la liste des administrateurs
        /// Date : 2021-03-08
        /// </summary>
        public frmAdminList()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            listviewAdmins.CheckBoxes = true;
            listviewAdmins.View = View.Details;
            listviewAdmins.GridLines = true;
            listviewAdmins.FullRowSelect = true;

            //Ajout des titles
            this.listviewAdmins.Columns[0].Text = "Username";
            this.listviewAdmins.Columns[0].Width = 200;
            listviewAdmins.Columns[1].Text = "Admin";
            listviewAdmins.Columns[1].Width = 54;

            this.SuspendLayout();

            Loaditems();

        }

        private void frmAdminList_Load(object sender, EventArgs e)
        {
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            foreach (ListViewItem item in listviewAdmins.Items)
            {
                if (item.Checked)
                {
                    item.SubItems[1].Text = "true";
                    item.SubItems[1].BackColor = Color.Green;
                }
                else
                {
                    item.SubItems[1].Text = "false";
                    item.SubItems[1].BackColor = Color.Red;
                }
                item.UseItemStyleForSubItems = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CompteJoueur j;

            foreach (ListViewItem item in listviewAdmins.Items)
            {

                if (item.Checked)
                {
                    j = lstPlayers.FirstOrDefault(x => x.NomJoueur == item.SubItems[0].Text);
                    controller.ModifierJoueur(j.Id, j.NomJoueur, j.Courriel, j.Prenom, j.Nom, 1);
                }
                else
                {
                    j = lstPlayers.FirstOrDefault(x => x.NomJoueur == item.SubItems[0].Text);
                    controller.ModifierJoueur(j.Id, j.NomJoueur, j.Courriel, j.Prenom, j.Nom, 0);
                }
            }

            this.Close();
        }

        /// <summary>
        /// Description : Remplis la liste du formulaire
        /// date : 2021-03-08
        /// </summary>
        public void Loaditems()
        {
            lstPlayers = controller.ListerCompte().Where(x => x.TypeUtilisateur == 0 || x.TypeUtilisateur == 1).ToList();
            string[] array = new string[2];
            ListViewItem itm;

            foreach (var i in lstPlayers)
            {
                array[0] = i.NomJoueur;

                if (i.TypeUtilisateur == 1)
                {
                    array[1] = "true";
                    itm = new ListViewItem(array);
                    itm.Checked = true;
                    itm.SubItems[1].BackColor = Color.Green;
                }
                else
                {
                    array[1] = "false";
                    itm = new ListViewItem(array);
                    itm.SubItems[1].BackColor = Color.Red;

                }

                itm.UseItemStyleForSubItems = false;
                listviewAdmins.Items.Add(itm);
            }
        }
    }
}
