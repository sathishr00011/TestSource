using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReleaseManifests
{
    public partial class ManifestParent : Form
    {
        public ManifestParent()
        {
            InitializeComponent();
        }

        private void componentManofestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
                this.ActiveMdiChild.Close();
            ComponentManifest childComponentManifestForm = new ComponentManifest();
            childComponentManifestForm.MdiParent = this;
            childComponentManifestForm.Show();
        }

        private void masterManifestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
                this.ActiveMdiChild.Close();
            MasterManifest childComponentManifestForm = new MasterManifest();
            childComponentManifestForm.MdiParent = this;
            childComponentManifestForm.Show();
        }

        private void ManifestParent_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.ActiveMdiChild != null)
                this.ActiveMdiChild.Close();
        }

        private void ManifestParent_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
