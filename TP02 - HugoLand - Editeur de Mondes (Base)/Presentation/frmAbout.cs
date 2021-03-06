﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HugoLandEditeur
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void frmAbout_Load(object sender, System.EventArgs e)
        {
           lblVersion.Text = "Version: " + Application.ProductVersion;
        }
    }
}
