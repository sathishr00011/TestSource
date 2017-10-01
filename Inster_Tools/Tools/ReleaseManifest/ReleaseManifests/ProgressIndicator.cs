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
    public partial class ProgressIndicator : Form
    {
        public ProgressIndicator()
        {
            InitializeComponent();
        }

        private int count = 0;

        private void ProgressIndicator_Load(object sender, EventArgs e)
        {
            progressTimer.Tick += new EventHandler(progressTimer_Tick);
        }

        private void ProgressIndicator_FormClosing(object sender, FormClosingEventArgs e)
        {
            progressTimer.Stop();
        }

        private void progressTimer_Tick(object sender, EventArgs e)
        {
            count = count + 1;
            progressBar.Increment(count);
        }

        private void ProgressIndicator_Shown(object sender, EventArgs e)
        {
            progressTimer.Start();
        }
    }
}
