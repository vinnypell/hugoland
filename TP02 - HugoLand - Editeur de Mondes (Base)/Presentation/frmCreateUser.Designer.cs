
namespace HugoLandEditeur.Presentation
{
    partial class frmCreateUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_Username = new System.Windows.Forms.Label();
            this.lbl_firstname = new System.Windows.Forms.Label();
            this.lbl_lastname = new System.Windows.Forms.Label();
            this.lbl_email = new System.Windows.Forms.Label();
            this.lbl_password = new System.Windows.Forms.Label();
            this.lbl_passwordconfirm = new System.Windows.Forms.Label();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.txt_firstname = new System.Windows.Forms.TextBox();
            this.txt_lastname = new System.Windows.Forms.TextBox();
            this.txt_email = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_passwordconfig = new System.Windows.Forms.TextBox();
            this.lbl_usertype = new System.Windows.Forms.Label();
            this.lbl_accountcreation = new System.Windows.Forms.Label();
            this.btn = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.listbx_usertype = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbl_Username
            // 
            this.lbl_Username.AutoSize = true;
            this.lbl_Username.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_Username.Location = new System.Drawing.Point(45, 178);
            this.lbl_Username.Name = "lbl_Username";
            this.lbl_Username.Size = new System.Drawing.Size(102, 25);
            this.lbl_Username.TabIndex = 0;
            this.lbl_Username.Text = "Username";
            // 
            // lbl_firstname
            // 
            this.lbl_firstname.AutoSize = true;
            this.lbl_firstname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_firstname.Location = new System.Drawing.Point(41, 229);
            this.lbl_firstname.Name = "lbl_firstname";
            this.lbl_firstname.Size = new System.Drawing.Size(106, 25);
            this.lbl_firstname.TabIndex = 1;
            this.lbl_firstname.Text = "First Name";
            this.lbl_firstname.Click += new System.EventHandler(this.lbl_firstname_Click);
            // 
            // lbl_lastname
            // 
            this.lbl_lastname.AutoSize = true;
            this.lbl_lastname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_lastname.Location = new System.Drawing.Point(41, 274);
            this.lbl_lastname.Name = "lbl_lastname";
            this.lbl_lastname.Size = new System.Drawing.Size(106, 25);
            this.lbl_lastname.TabIndex = 2;
            this.lbl_lastname.Text = "Last Name";
            // 
            // lbl_email
            // 
            this.lbl_email.AutoSize = true;
            this.lbl_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_email.Location = new System.Drawing.Point(41, 330);
            this.lbl_email.Name = "lbl_email";
            this.lbl_email.Size = new System.Drawing.Size(138, 25);
            this.lbl_email.TabIndex = 3;
            this.lbl_email.Text = "Email Address";
            // 
            // lbl_password
            // 
            this.lbl_password.AutoSize = true;
            this.lbl_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_password.Location = new System.Drawing.Point(41, 424);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(98, 25);
            this.lbl_password.TabIndex = 4;
            this.lbl_password.Text = "Password";
            // 
            // lbl_passwordconfirm
            // 
            this.lbl_passwordconfirm.AutoSize = true;
            this.lbl_passwordconfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_passwordconfirm.Location = new System.Drawing.Point(41, 474);
            this.lbl_passwordconfirm.Name = "lbl_passwordconfirm";
            this.lbl_passwordconfirm.Size = new System.Drawing.Size(171, 25);
            this.lbl_passwordconfirm.TabIndex = 5;
            this.lbl_passwordconfirm.Text = "Password Confirm";
            // 
            // txt_username
            // 
            this.txt_username.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_username.Location = new System.Drawing.Point(218, 178);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(277, 28);
            this.txt_username.TabIndex = 7;
            // 
            // txt_firstname
            // 
            this.txt_firstname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_firstname.Location = new System.Drawing.Point(218, 226);
            this.txt_firstname.Name = "txt_firstname";
            this.txt_firstname.Size = new System.Drawing.Size(277, 28);
            this.txt_firstname.TabIndex = 8;
            // 
            // txt_lastname
            // 
            this.txt_lastname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_lastname.Location = new System.Drawing.Point(218, 274);
            this.txt_lastname.Name = "txt_lastname";
            this.txt_lastname.Size = new System.Drawing.Size(277, 28);
            this.txt_lastname.TabIndex = 9;
            // 
            // txt_email
            // 
            this.txt_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_email.Location = new System.Drawing.Point(218, 330);
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(277, 28);
            this.txt_email.TabIndex = 10;
            // 
            // txt_password
            // 
            this.txt_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_password.Location = new System.Drawing.Point(218, 421);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(277, 28);
            this.txt_password.TabIndex = 11;
            this.txt_password.UseSystemPasswordChar = true;
            // 
            // txt_passwordconfig
            // 
            this.txt_passwordconfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_passwordconfig.Location = new System.Drawing.Point(218, 474);
            this.txt_passwordconfig.Name = "txt_passwordconfig";
            this.txt_passwordconfig.Size = new System.Drawing.Size(277, 28);
            this.txt_passwordconfig.TabIndex = 12;
            this.txt_passwordconfig.UseSystemPasswordChar = true;
            // 
            // lbl_usertype
            // 
            this.lbl_usertype.AutoSize = true;
            this.lbl_usertype.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_usertype.Location = new System.Drawing.Point(41, 373);
            this.lbl_usertype.Name = "lbl_usertype";
            this.lbl_usertype.Size = new System.Drawing.Size(103, 25);
            this.lbl_usertype.TabIndex = 13;
            this.lbl_usertype.Text = "User Type";
            // 
            // lbl_accountcreation
            // 
            this.lbl_accountcreation.AutoSize = true;
            this.lbl_accountcreation.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.lbl_accountcreation.Location = new System.Drawing.Point(61, 49);
            this.lbl_accountcreation.Name = "lbl_accountcreation";
            this.lbl_accountcreation.Size = new System.Drawing.Size(409, 58);
            this.lbl_accountcreation.TabIndex = 14;
            this.lbl_accountcreation.Text = "Account Creation";
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(46, 527);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(165, 47);
            this.btn.TabIndex = 15;
            this.btn.Text = "Add";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(218, 527);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(165, 47);
            this.btn_cancel.TabIndex = 16;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // listbx_usertype
            // 
            this.listbx_usertype.FormattingEnabled = true;
            this.listbx_usertype.ItemHeight = 16;
            this.listbx_usertype.Items.AddRange(new object[] {
            "Admin",
            "User"});
            this.listbx_usertype.Location = new System.Drawing.Point(218, 373);
            this.listbx_usertype.Name = "listbx_usertype";
            this.listbx_usertype.Size = new System.Drawing.Size(107, 36);
            this.listbx_usertype.TabIndex = 17;
            // 
            // frmCreateUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 586);
            this.Controls.Add(this.listbx_usertype);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.lbl_accountcreation);
            this.Controls.Add(this.lbl_usertype);
            this.Controls.Add(this.txt_passwordconfig);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_email);
            this.Controls.Add(this.txt_lastname);
            this.Controls.Add(this.txt_firstname);
            this.Controls.Add(this.txt_username);
            this.Controls.Add(this.lbl_passwordconfirm);
            this.Controls.Add(this.lbl_password);
            this.Controls.Add(this.lbl_email);
            this.Controls.Add(this.lbl_lastname);
            this.Controls.Add(this.lbl_firstname);
            this.Controls.Add(this.lbl_Username);
            this.Name = "frmCreateUser";
            this.Text = "User Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Username;
        private System.Windows.Forms.Label lbl_firstname;
        private System.Windows.Forms.Label lbl_lastname;
        private System.Windows.Forms.Label lbl_email;
        private System.Windows.Forms.Label lbl_password;
        private System.Windows.Forms.Label lbl_passwordconfirm;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.TextBox txt_firstname;
        private System.Windows.Forms.TextBox txt_lastname;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_passwordconfig;
        private System.Windows.Forms.Label lbl_usertype;
        private System.Windows.Forms.Label lbl_accountcreation;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.ListBox listbx_usertype;
    }
}