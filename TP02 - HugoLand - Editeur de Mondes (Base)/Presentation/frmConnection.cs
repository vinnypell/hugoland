using System;
using System.Windows.Forms;
using TP01_Library.Controllers;

namespace HugoLandEditeur.Presentation
{
    public partial class frmConnection : Form
    {
        public frmConnection()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
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
                MessageBox.Show("USERNAME IS INVALID", "Connection error",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (reponse == "SUCCESS")
            {
                Outil.SetActiveUser(controller.TrouverJoueur(username));
                if (controller.TrouverJoueur(username).TypeUtilisateur == 2)
                {
                    var form2 = new frmMain();
                    form2.Closed += (s, args) => this.Close();
                    form2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("YOU DO NOT HAVE PERMISSIONS TO LOG IN", "Connection error",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if (reponse == "INCORRECT")
            {
                MessageBox.Show("PASSWORD IS INVALID", "Connection error",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}