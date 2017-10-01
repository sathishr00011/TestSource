namespace ReleaseManifests
{
    partial class TabbedForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabManifest = new System.Windows.Forms.TabPage();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.gbApplications = new System.Windows.Forms.GroupBox();
            this.rbOMS = new System.Windows.Forms.RadioButton();
            this.rbAppStore = new System.Windows.Forms.RadioButton();
            this.rbTibco = new System.Windows.Forms.RadioButton();
            this.rgGrocery = new System.Windows.Forms.RadioButton();
            this.lblReference = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.listComponents = new System.Windows.Forms.ListView();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.tabTools = new System.Windows.Forms.TabPage();
            this.lblTabToolsResult = new System.Windows.Forms.Label();
            this.btnGenerateReference = new System.Windows.Forms.Button();
            this.lblRef = new System.Windows.Forms.Label();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlRegion = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openReferenceFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl.SuspendLayout();
            this.tabManifest.SuspendLayout();
            this.gbApplications.SuspendLayout();
            this.tabTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabManifest);
            this.tabControl.Controls.Add(this.tabTools);
            this.tabControl.Location = new System.Drawing.Point(0, 43);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(639, 416);
            this.tabControl.TabIndex = 0;
            // 
            // tabManifest
            // 
            this.tabManifest.Controls.Add(this.btnAnalyze);
            this.tabManifest.Controls.Add(this.gbApplications);
            this.tabManifest.Controls.Add(this.lblReference);
            this.tabManifest.Controls.Add(this.lblMessage);
            this.tabManifest.Controls.Add(this.listComponents);
            this.tabManifest.Controls.Add(this.btnGenerate);
            this.tabManifest.Controls.Add(this.btnBrowse);
            this.tabManifest.Controls.Add(this.txtReference);
            this.tabManifest.Location = new System.Drawing.Point(4, 22);
            this.tabManifest.Name = "tabManifest";
            this.tabManifest.Padding = new System.Windows.Forms.Padding(3);
            this.tabManifest.Size = new System.Drawing.Size(631, 390);
            this.tabManifest.TabIndex = 0;
            this.tabManifest.Text = "Manifest";
            this.tabManifest.UseVisualStyleBackColor = true;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(541, 10);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(75, 23);
            this.btnAnalyze.TabIndex = 27;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // gbApplications
            // 
            this.gbApplications.Controls.Add(this.rbOMS);
            this.gbApplications.Controls.Add(this.rbAppStore);
            this.gbApplications.Controls.Add(this.rbTibco);
            this.gbApplications.Controls.Add(this.rgGrocery);
            this.gbApplications.Location = new System.Drawing.Point(12, 38);
            this.gbApplications.Name = "gbApplications";
            this.gbApplications.Size = new System.Drawing.Size(610, 68);
            this.gbApplications.TabIndex = 26;
            this.gbApplications.TabStop = false;
            this.gbApplications.Text = "Applications";
            // 
            // rbOMS
            // 
            this.rbOMS.AutoSize = true;
            this.rbOMS.Location = new System.Drawing.Point(220, 23);
            this.rbOMS.Name = "rbOMS";
            this.rbOMS.Size = new System.Drawing.Size(49, 17);
            this.rbOMS.TabIndex = 3;
            this.rbOMS.TabStop = true;
            this.rbOMS.Text = "OMS";
            this.rbOMS.UseVisualStyleBackColor = true;
            // 
            // rbAppStore
            // 
            this.rbAppStore.AutoSize = true;
            this.rbAppStore.Location = new System.Drawing.Point(73, 21);
            this.rbAppStore.Name = "rbAppStore";
            this.rbAppStore.Size = new System.Drawing.Size(69, 17);
            this.rbAppStore.TabIndex = 2;
            this.rbAppStore.TabStop = true;
            this.rbAppStore.Text = "AppStore";
            this.rbAppStore.UseVisualStyleBackColor = true;
            // 
            // rbTibco
            // 
            this.rbTibco.AutoSize = true;
            this.rbTibco.Location = new System.Drawing.Point(155, 23);
            this.rbTibco.Name = "rbTibco";
            this.rbTibco.Size = new System.Drawing.Size(57, 17);
            this.rbTibco.TabIndex = 1;
            this.rbTibco.TabStop = true;
            this.rbTibco.Text = "TIBCO";
            this.rbTibco.UseVisualStyleBackColor = true;
            // 
            // rgGrocery
            // 
            this.rgGrocery.AutoSize = true;
            this.rgGrocery.Location = new System.Drawing.Point(7, 20);
            this.rgGrocery.Name = "rgGrocery";
            this.rgGrocery.Size = new System.Drawing.Size(62, 17);
            this.rgGrocery.TabIndex = 0;
            this.rgGrocery.TabStop = true;
            this.rgGrocery.Text = "Grocery";
            this.rgGrocery.UseVisualStyleBackColor = true;
            // 
            // lblReference
            // 
            this.lblReference.AutoSize = true;
            this.lblReference.Location = new System.Drawing.Point(8, 15);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(57, 13);
            this.lblReference.TabIndex = 13;
            this.lblReference.Text = "Reference";
            // 
            // lblError
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(13, 347);
            this.lblMessage.Name = "lblError";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 25;
            // 
            // listView1
            // 
            this.listComponents.CheckBoxes = true;
            this.listComponents.LabelWrap = false;
            this.listComponents.Location = new System.Drawing.Point(10, 112);
            this.listComponents.Name = "listView1";
            this.listComponents.Size = new System.Drawing.Size(612, 180);
            this.listComponents.TabIndex = 24;
            this.listComponents.UseCompatibleStateImageBehavior = false;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(483, 310);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(137, 23);
            this.btnGenerate.TabIndex = 23;
            this.btnGenerate.Text = "Generate Manifest";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(458, 10);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 16;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(68, 12);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(384, 20);
            this.txtReference.TabIndex = 14;
            // 
            // tabTools
            // 
            this.tabTools.Controls.Add(this.lblTabToolsResult);
            this.tabTools.Controls.Add(this.btnGenerateReference);
            this.tabTools.Controls.Add(this.lblRef);
            this.tabTools.Location = new System.Drawing.Point(4, 22);
            this.tabTools.Name = "tabTools";
            this.tabTools.Padding = new System.Windows.Forms.Padding(3);
            this.tabTools.Size = new System.Drawing.Size(631, 390);
            this.tabTools.TabIndex = 3;
            this.tabTools.Text = "Tools";
            this.tabTools.UseVisualStyleBackColor = true;
            // 
            // lblTabToolsResult
            // 
            this.lblTabToolsResult.AutoSize = true;
            this.lblTabToolsResult.Location = new System.Drawing.Point(260, 56);
            this.lblTabToolsResult.Name = "lblTabToolsResult";
            this.lblTabToolsResult.Size = new System.Drawing.Size(0, 13);
            this.lblTabToolsResult.TabIndex = 2;
            // 
            // btnGenerateReference
            // 
            this.btnGenerateReference.Location = new System.Drawing.Point(167, 51);
            this.btnGenerateReference.Name = "btnGenerateReference";
            this.btnGenerateReference.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateReference.TabIndex = 1;
            this.btnGenerateReference.Text = "Generate";
            this.btnGenerateReference.UseVisualStyleBackColor = true;
            this.btnGenerateReference.Click += new System.EventHandler(this.btnGenerateReference_Click);
            // 
            // lblRef
            // 
            this.lblRef.AutoSize = true;
            this.lblRef.Location = new System.Drawing.Point(37, 56);
            this.lblRef.Name = "lblRef";
            this.lblRef.Size = new System.Drawing.Size(124, 13);
            this.lblRef.TabIndex = 0;
            this.lblRef.Text = "Generate Reference Xml";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(188, 17);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(97, 17);
            this.radioButton3.TabIndex = 22;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "PRODUCTION";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(135, 17);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 17);
            this.radioButton2.TabIndex = 21;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "STG";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(84, 17);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 17);
            this.radioButton1.TabIndex = 20;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "DEV";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(455, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Region";
            // 
            // ddlRegion
            // 
            this.ddlRegion.FormattingEnabled = true;
            this.ddlRegion.Items.AddRange(new object[] {
            "UK",
            "CZ",
            "PL",
            "SK",
            "TH",
            "MY",
            "HU",
            "CN",
            "CH",
            "CS"});
            this.ddlRegion.Location = new System.Drawing.Point(505, 16);
            this.ddlRegion.Name = "ddlRegion";
            this.ddlRegion.Size = new System.Drawing.Size(121, 21);
            this.ddlRegion.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Environment";
            // 
            // openReferenceFileDialog
            // 
            this.openReferenceFileDialog.FileName = "openReferenceFileDialog";
            // 
            // TabbedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 459);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ddlRegion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.radioButton2);
            this.Name = "TabbedForm";
            this.Text = "TabbedForm";
            this.Load += new System.EventHandler(this.TabbedForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabManifest.ResumeLayout(false);
            this.tabManifest.PerformLayout();
            this.gbApplications.ResumeLayout(false);
            this.gbApplications.PerformLayout();
            this.tabTools.ResumeLayout(false);
            this.tabTools.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabManifest;
        private System.Windows.Forms.TabPage tabTools;
        private System.Windows.Forms.Label lblReference;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ListView listComponents;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddlRegion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.OpenFileDialog openReferenceFileDialog;
        private System.Windows.Forms.Label lblRef;
        private System.Windows.Forms.Button btnGenerateReference;
        private System.Windows.Forms.Label lblTabToolsResult;
        private System.Windows.Forms.GroupBox gbApplications;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.RadioButton rbOMS;
        private System.Windows.Forms.RadioButton rbAppStore;
        private System.Windows.Forms.RadioButton rbTibco;
        private System.Windows.Forms.RadioButton rgGrocery;
    }
}