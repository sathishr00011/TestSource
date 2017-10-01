using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace Deployment
{
    public partial class DeploymentTrigger : Form

    {
        public string DeploymentFlag;
        public DeploymentTrigger()
        {
            InitializeComponent();
        }
       


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)

        {
           DeploymentFlag=comboBox1.SelectedItem.ToString();
            if (DeploymentFlag!=null)
            {
                Util.JenkinsURL = ConfigurationManager.AppSettings[DeploymentFlag];
                Util.TriggerJenkinsJob("IGHS");
                this.Close();


            }
        }
    }
}
