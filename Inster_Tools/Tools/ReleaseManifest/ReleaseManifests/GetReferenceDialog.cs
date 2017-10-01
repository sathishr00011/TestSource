using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReleaseManifests
{
    public partial class GetReferenceDialog : Form
    {
        public GetReferenceDialog()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
           var result= this.ofdRef.ShowDialog();
           if (result == DialogResult.OK)
           {
               tbReferencePath.Text = this.ofdRef.FileName;
           }
        }

        public DialogResult ShowDialog(out string referencePath)
        {
            referencePath = string.Empty;
            var result=this.ShowDialog();
            if (result == DialogResult.OK)
            {
                referencePath = tbReferencePath.Text;
            }
            return result;
        }

    }
}
