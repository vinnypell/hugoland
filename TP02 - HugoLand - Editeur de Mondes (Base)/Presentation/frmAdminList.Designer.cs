
namespace HugoLandEditeur.Presentation
{
    partial class frmAdminList
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
            this.listviewAdmins = new System.Windows.Forms.ListView();
            this.Username = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Admin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_Apply = new System.Windows.Forms.Button();
            this.btn_Confirm = new System.Windows.Forms.Button();
            this.Description = new System.Windows.Forms.Label();
            this.lbl_Error = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listviewAdmins
            // 
            this.listviewAdmins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Username,
            this.Admin});
            this.listviewAdmins.HideSelection = false;
            this.listviewAdmins.Location = new System.Drawing.Point(12, 69);
            this.listviewAdmins.Name = "listviewAdmins";
            this.listviewAdmins.Size = new System.Drawing.Size(341, 323);
            this.listviewAdmins.TabIndex = 0;
            this.listviewAdmins.UseCompatibleStateImageBehavior = false;
            this.listviewAdmins.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // btn_Apply
            // 
            this.btn_Apply.Location = new System.Drawing.Point(12, 427);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(341, 56);
            this.btn_Apply.TabIndex = 1;
            this.btn_Apply.Text = "Apply changes";
            this.btn_Apply.UseVisualStyleBackColor = true;
            this.btn_Apply.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Confirm
            // 
            this.btn_Confirm.Location = new System.Drawing.Point(12, 489);
            this.btn_Confirm.Name = "btn_Confirm";
            this.btn_Confirm.Size = new System.Drawing.Size(341, 56);
            this.btn_Confirm.TabIndex = 3;
            this.btn_Confirm.Text = "Confirm changes";
            this.btn_Confirm.UseVisualStyleBackColor = true;
            this.btn_Confirm.Click += new System.EventHandler(this.button3_Click);
            // 
            // Description
            // 
            this.Description.AutoSize = true;
            this.Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.Description.Location = new System.Drawing.Point(65, 8);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(222, 58);
            this.Description.TabIndex = 4;
            this.Description.Text = "User List\r\n";
            // 
            // lbl_Error
            // 
            this.lbl_Error.AutoSize = true;
            this.lbl_Error.Location = new System.Drawing.Point(14, 403);
            this.lbl_Error.Name = "lbl_Error";
            this.lbl_Error.Size = new System.Drawing.Size(0, 17);
            this.lbl_Error.TabIndex = 5;
            // 
            // frmAdminList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 565);
            this.Controls.Add(this.lbl_Error);
            this.Controls.Add(this.Description);
            this.Controls.Add(this.btn_Confirm);
            this.Controls.Add(this.btn_Apply);
            this.Controls.Add(this.listviewAdmins);
            this.MaximizeBox = false;
            this.Name = "frmAdminList";
            this.Text = "AdminList";
            this.Load += new System.EventHandler(this.frmAdminList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listviewAdmins;
        private System.Windows.Forms.ColumnHeader Username;
        private System.Windows.Forms.ColumnHeader Admin;
        private System.Windows.Forms.Button btn_Apply;
        private System.Windows.Forms.Button btn_Confirm;
        private System.Windows.Forms.Label Description;
        private System.Windows.Forms.Label lbl_Error;
    }
}