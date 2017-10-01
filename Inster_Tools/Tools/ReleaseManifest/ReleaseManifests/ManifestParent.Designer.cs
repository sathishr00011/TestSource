namespace ReleaseManifests
{
    partial class ManifestParent
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManifestParent));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.componentManofestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterManifestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.componentManofestToolStripMenuItem,
            this.masterManifestToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1170, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // componentManofestToolStripMenuItem
            // 
            this.componentManofestToolStripMenuItem.Name = "componentManofestToolStripMenuItem";
            this.componentManofestToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.componentManofestToolStripMenuItem.Text = "Component Manifest";
            this.componentManofestToolStripMenuItem.Click += new System.EventHandler(this.componentManofestToolStripMenuItem_Click);
            // 
            // masterManifestToolStripMenuItem
            // 
            this.masterManifestToolStripMenuItem.Name = "masterManifestToolStripMenuItem";
            this.masterManifestToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.masterManifestToolStripMenuItem.Text = "Master Manifest";
            this.masterManifestToolStripMenuItem.Click += new System.EventHandler(this.masterManifestToolStripMenuItem_Click);
            // 
            // ManifestParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 481);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "ManifestParent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManifestParent_FormClosing);
            this.Shown += new System.EventHandler(this.ManifestParent_Shown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem componentManofestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterManifestToolStripMenuItem;
    }
}



