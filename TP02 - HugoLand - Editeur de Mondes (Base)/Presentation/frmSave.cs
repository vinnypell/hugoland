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

namespace HugoLandEditeur.Presentation
{
    public partial class frmSave : Form
    {
        public string Description { get; set; }
        public frmSave(Monde m)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterParent;
            this.DialogResult = DialogResult.Cancel;
            this.lbl_ID.Text = m.Id.ToString();
            this.txt_Descrip.Text = m.Description;
            Description = m.Description;
        }

        private void frmSave_Load(object sender, EventArgs e)
        {

        }
        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (txt_Descrip.Text != Description)
            {
                Description = txt_Descrip.Text;
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}
