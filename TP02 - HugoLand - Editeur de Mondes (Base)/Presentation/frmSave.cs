using System;
using System.Windows.Forms;
using TP01_Library;

namespace HugoLandEditeur.Presentation
{
    public partial class frmSave : Form
    {
        public string Description { get; set; }

        /// <summary>
        /// Description : Initialise le formulaire pour la sauvegarde du monde
        /// </summary>
        /// <param name="m"></param>
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