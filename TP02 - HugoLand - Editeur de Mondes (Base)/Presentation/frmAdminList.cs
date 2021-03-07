﻿using System;
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
        public List<CompteJoueur> lstadmins = new List<CompteJoueur>();
        CompteJoueurController controller = new CompteJoueurController();
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

            foreach(ListViewItem item in listviewAdmins.Items)
            {
                if (item.Checked)
                {
                    item.SubItems[1].Text = "false";
                    item.SubItems[1].BackColor = Color.Red;
                    item.UseItemStyleForSubItems = false;
                }
                else
                {
                    item.SubItems[1].Text = "true";
                    item.SubItems[1].BackColor = Color.White;
                    item.UseItemStyleForSubItems = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CompteJoueur j;

            foreach (ListViewItem item in listviewAdmins.Items)
            {
                if (item.Checked)
                {
                    j = lstadmins.FirstOrDefault(x => x.NomJoueur == item.SubItems[0].Text);
                    controller.ModifierJoueur(j.Id, j.NomJoueur, j.Courriel, j.Prenom, j.Nom, 0);
                }
            }

            listviewAdmins.Items.Clear();
            Loaditems();
        }

        private void btn_Revert_Click(object sender, EventArgs e)
        {

        }

        public void Loaditems()
        {
            lstadmins = controller.ListerCompte().Where(x => x.TypeUtilisateur.Equals(1)).ToList();
            string[] array = new string[2];
            ListViewItem itm;

            foreach (var i in lstadmins)
            {
                array[0] = i.NomJoueur;
                array[1] = "true";
                itm = new ListViewItem(array);
                listviewAdmins.Items.Add(itm);
            }
        }
    }
}
