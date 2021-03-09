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
    
    public partial class frmListSelector : Form
    {
        MondeController ctrl = new MondeController();

        public Monde monde { get; set; }
        /// <summary>
        /// Description : Initilise le formulaire de sélection de monde pour loader
        /// Date : 2021-03-08
        /// </summary>
        public frmListSelector()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            lstMondes.DataSource = ctrl.ListerMondes().Select(x =>x.Id + " : " + x.Description).ToArray();
        }

        /// <summary>
        /// Description : annule la sélection
        /// Date : 2021-03-08
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Description : retourne le monde sélectionné
        /// Date : 2021-03-08
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string itemstr = lstMondes.SelectedItem.ToString();
            int id = Int32.Parse(itemstr.Substring(0, itemstr.IndexOf(':')));
            monde = ctrl.GetMonde(id);
            //Outil.SetMondeToEdit(m);
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void frmListSelector_Load(object sender, EventArgs e)
        {

        }

        private void lstMondes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
