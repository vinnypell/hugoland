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
        public frmListSelector()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            lstMondes.DataSource = ctrl.ListerMondes().Select(x =>x.Id + " : " + x.Description).ToArray();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string itemstr = lstMondes.SelectedItem.ToString();
            int id = Int32.Parse(itemstr.Substring(0, itemstr.IndexOf(':')));
            monde = ctrl.GetMonde(id);
            //Outil.SetMondeToEdit(m);
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
