
namespace HugoLandEditeur.Presentation
{
    partial class frmSave
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSave));
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_ID = new System.Windows.Forms.Label();
            this.txt_Descrip = new System.Windows.Forms.TextBox();
            this.lbl_Descrip = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(44, 159);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(78, 20);
            this.btnYes.TabIndex = 0;
            this.btnYes.Text = "&Yes";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNo.Location = new System.Drawing.Point(128, 159);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(78, 20);
            this.btnNo.TabIndex = 1;
            this.btnNo.Text = "&No";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(44, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "World ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_ID
            // 
            this.lbl_ID.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_ID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_ID.Location = new System.Drawing.Point(128, 79);
            this.lbl_ID.Name = "lbl_ID";
            this.lbl_ID.Size = new System.Drawing.Size(78, 20);
            this.lbl_ID.TabIndex = 3;
            this.lbl_ID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Descrip
            // 
            this.txt_Descrip.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Descrip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Descrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Descrip.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txt_Descrip.Location = new System.Drawing.Point(128, 116);
            this.txt_Descrip.Name = "txt_Descrip";
            this.txt_Descrip.Size = new System.Drawing.Size(78, 20);
            this.txt_Descrip.TabIndex = 4;
            // 
            // lbl_Descrip
            // 
            this.lbl_Descrip.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_Descrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Descrip.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Descrip.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_Descrip.Location = new System.Drawing.Point(44, 115);
            this.lbl_Descrip.Name = "lbl_Descrip";
            this.lbl_Descrip.Size = new System.Drawing.Size(78, 20);
            this.lbl_Descrip.TabIndex = 5;
            this.lbl_Descrip.Text = "Save name";
            this.lbl_Descrip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Title
            // 
            this.lbl_Title.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Title.Location = new System.Drawing.Point(12, 9);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(231, 44);
            this.lbl_Title.TabIndex = 6;
            this.lbl_Title.Text = "Please confirm if that is the map you wish to save.";
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmSave
            // 
            this.AcceptButton = this.btnYes;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnNo;
            this.ClientSize = new System.Drawing.Size(259, 204);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.lbl_Descrip);
            this.Controls.Add(this.txt_Descrip);
            this.Controls.Add(this.lbl_ID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Save game";
            this.Load += new System.EventHandler(this.frmSave_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_ID;
        private System.Windows.Forms.TextBox txt_Descrip;
        private System.Windows.Forms.Label lbl_Descrip;
        private System.Windows.Forms.Label lbl_Title;
    }
}