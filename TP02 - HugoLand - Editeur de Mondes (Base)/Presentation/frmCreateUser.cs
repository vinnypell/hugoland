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
using System.Text.RegularExpressions;

namespace HugoLandEditeur.Presentation
{
    public partial class frmCreateUser : Form
    {
        public frmCreateUser()
        {
            InitializeComponent();
        }

        private void lbl_firstname_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Description : Tente de créer un nouvel utilisateur avec le formulaire
        /// Date : 2021-03-08
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Click(object sender, EventArgs e)
        {

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            string UserName = txt_username.Text;
            string FirstName = txt_firstname.Text;
            string LastName = txt_lastname.Text;
            string Email = txt_email.Text;
            int Type = 0;
            string Password = txt_password.Text;
            string PasswordConfig = txt_passwordconfig.Text;
            switch (listbx_usertype.Text)
            {
                case "Admin":
                    Type = 1;
                    break;
                case "User":
                    Type = 0;
                    break;
            }

            CompteJoueurController controller = new CompteJoueurController();

            Match match = regex.Match(Email);

            string Error = null;
            string Error1 = null;
            string Error2 = null;
            string Error3 = null;

            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    if (textBox.Text == string.Empty)
                    {
                        Error = "All textbox must be filled";
                        if (Error != null)
                        {
                            break;
                        }
                    }
                }
            }


            if (!match.Success)
            {
                Error1 = "The email format is not valid";
            }

            if (controller.TrouverJoueur(UserName) != null)
            {
                Error2 = "The username is already taken";
            }

            if (Password != PasswordConfig)
            {
                Error3 = "The confirmed password does not match the original password";
            }

            if (Error !=null)
            {
                MessageBox.Show(Error, "ERROR",
   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Error1 != null || Error2 != null || Error3 != null)
            {
                MessageBox.Show(Error1 + "\n\r" + Error2 + "\r\n" + Error3 + "\r\n", "ERROR",
   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string Validation = controller.CreerJoueur(UserName, Email, FirstName, LastName, Type, PasswordConfig);
                if (Validation == "SUCCESS")
                {
                    this.Close(); 
                }
                else
                    MessageBox.Show(Validation, "ERROR",
       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
