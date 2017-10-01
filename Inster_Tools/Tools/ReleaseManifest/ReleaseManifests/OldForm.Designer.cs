namespace ReleaseManifests
{
    partial class OldForm
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
            this.lblError = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.button2 = new System.Windows.Forms.Button();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlRegion = new System.Windows.Forms.ComboBox();
            this.openReferenceFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.chkComponents = new System.Windows.Forms.CheckedListBox();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.lblReference = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(42, 394);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 13);
            this.lblError.TabIndex = 25;
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(41, 149);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(612, 208);
            this.listView1.TabIndex = 24;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(516, 373);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "Generate Manifest";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(215, 30);
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
            this.radioButton2.Location = new System.Drawing.Point(162, 30);
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
            this.radioButton1.Location = new System.Drawing.Point(111, 30);
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
            this.label2.Location = new System.Drawing.Point(482, 32);
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
            this.ddlRegion.Location = new System.Drawing.Point(532, 29);
            this.ddlRegion.Name = "ddlRegion";
            this.ddlRegion.Size = new System.Drawing.Size(121, 21);
            this.ddlRegion.TabIndex = 18;
            // 
            // openReferenceFileDialog
            // 
            this.openReferenceFileDialog.FileName = "openReferenceFileDialog";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Environment";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(576, 77);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // chkComponents
            // 
            this.chkComponents.FormattingEnabled = true;
            this.chkComponents.Location = new System.Drawing.Point(41, 114);
            this.chkComponents.Name = "chkComponents";
            this.chkComponents.Size = new System.Drawing.Size(612, 19);
            this.chkComponents.TabIndex = 15;
            this.chkComponents.Visible = false;
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(99, 79);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(471, 20);
            this.txtReference.TabIndex = 14;
            // 
            // lblReference
            // 
            this.lblReference.AutoSize = true;
            this.lblReference.Location = new System.Drawing.Point(39, 82);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(57, 13);
            this.lblReference.TabIndex = 13;
            this.lblReference.Text = "Reference";
            // 
            // OldForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 436);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ddlRegion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkComponents);
            this.Controls.Add(this.txtReference);
            this.Controls.Add(this.lblReference);
            this.Name = "OldForm";
            this.Text = "OldForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddlRegion;
        private System.Windows.Forms.OpenFileDialog openReferenceFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckedListBox chkComponents;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.Label lblReference;
    }
}