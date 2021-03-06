using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP01_Library.Controllers;
using TP01_Library;

namespace HugoLandEditeur.Presentation
{
    public partial class frmConnection : Form
    {
        public frmConnection()
        {
            InitializeComponent();
        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmConnection_Load(object sender, EventArgs e)
        {

        }

        private void btn_connection_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text;
            string password = txt_password.Text;

            CompteJoueurController controller = new CompteJoueurController();

            string reponse = controller.ValiderConnexion(password, username);

            if (reponse == "INVALIDE")
            {
                MessageBox.Show("PASSWORD OR USERNAME INVALID", "Connection error",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(reponse == "SUCCESS")
            {
                this.Hide();
                var form2 = new frmMain();
                form2.Closed += (s, args) => this.Close();
                form2.Show();
            }
        }
    }
}
