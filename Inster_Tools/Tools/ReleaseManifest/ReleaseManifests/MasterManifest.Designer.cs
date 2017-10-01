namespace ReleaseManifests
{
    partial class MasterManifest
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterManifest));
            this.ManifestTabContainer = new System.Windows.Forms.TabControl();
            this.Grocery = new System.Windows.Forms.TabPage();
            this.dgvGrocery = new System.Windows.Forms.DataGridView();
            this.Component = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.lstDropdownGrocery = new System.Windows.Forms.Panel();
            this.btnGrocery = new System.Windows.Forms.Button();
            this.chkListGrocery = new System.Windows.Forms.CheckedListBox();
            this.AppStore = new System.Windows.Forms.TabPage();
            this.dgvAppStore = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewComboBoxColumn4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btnAppStore = new System.Windows.Forms.Button();
            this.OMS = new System.Windows.Forms.TabPage();
            this.dgvOms = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.chkListOms = new System.Windows.Forms.CheckedListBox();
            this.lstDropdownOms = new System.Windows.Forms.Panel();
            this.btnOms = new System.Windows.Forms.Button();
            this.Tibco = new System.Windows.Forms.TabPage();
            this.dgvTibco = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ddlTibcoRegions = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstDropdownTibco = new System.Windows.Forms.Panel();
            this.chkListTibco = new System.Windows.Forms.CheckedListBox();
            this.btnTibco = new System.Windows.Forms.Button();
            this.dataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.lblMessage = new System.Windows.Forms.Label();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ddlReleaseVerison = new System.Windows.Forms.ComboBox();
            this.lblReleaseVersion = new System.Windows.Forms.Label();
            this.ManifestTabContainer.SuspendLayout();
            this.Grocery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrocery)).BeginInit();
            this.AppStore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppStore)).BeginInit();
            this.OMS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOms)).BeginInit();
            this.Tibco.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTibco)).BeginInit();
            this.SuspendLayout();
            // 
            // ManifestTabContainer
            // 
            this.ManifestTabContainer.Controls.Add(this.Grocery);
            this.ManifestTabContainer.Controls.Add(this.AppStore);
            this.ManifestTabContainer.Controls.Add(this.OMS);
            this.ManifestTabContainer.Controls.Add(this.Tibco);
            this.ManifestTabContainer.Location = new System.Drawing.Point(12, 39);
            this.ManifestTabContainer.Name = "ManifestTabContainer";
            this.ManifestTabContainer.SelectedIndex = 0;
            this.ManifestTabContainer.Size = new System.Drawing.Size(435, 392);
            this.ManifestTabContainer.TabIndex = 4;
            this.ManifestTabContainer.SelectedIndexChanged += new System.EventHandler(this.ManifestTabContainer_SelectedIndexChanged);
            // 
            // Grocery
            // 
            this.Grocery.Controls.Add(this.dgvGrocery);
            this.Grocery.Controls.Add(this.lstDropdownGrocery);
            this.Grocery.Controls.Add(this.btnGrocery);
            this.Grocery.Controls.Add(this.chkListGrocery);
            this.Grocery.Location = new System.Drawing.Point(4, 22);
            this.Grocery.Name = "Grocery";
            this.Grocery.Padding = new System.Windows.Forms.Padding(3);
            this.Grocery.Size = new System.Drawing.Size(427, 366);
            this.Grocery.TabIndex = 0;
            this.Grocery.Text = "Grocery";
            this.Grocery.UseVisualStyleBackColor = true;
            // 
            // dgvGrocery
            // 
            this.dgvGrocery.AllowUserToAddRows = false;
            this.dgvGrocery.AllowUserToDeleteRows = false;
            this.dgvGrocery.AllowUserToResizeColumns = false;
            this.dgvGrocery.AllowUserToResizeRows = false;
            this.dgvGrocery.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGrocery.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGrocery.ColumnHeadersVisible = false;
            this.dgvGrocery.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Component,
            this.Version});
            this.dgvGrocery.Location = new System.Drawing.Point(6, 6);
            this.dgvGrocery.Name = "dgvGrocery";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGrocery.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvGrocery.RowHeadersVisible = false;
            this.dgvGrocery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvGrocery.Size = new System.Drawing.Size(415, 324);
            this.dgvGrocery.TabIndex = 6;
            this.dgvGrocery.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dgvGrocery.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView_CurrentCellDirtyStateChanged);
            // 
            // Component
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.NullValue = false;
            this.Component.DefaultCellStyle = dataGridViewCellStyle2;
            this.Component.FalseValue = "0";
            this.Component.HeaderText = "Component";
            this.Component.Name = "Component";
            this.Component.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Component.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Component.TrueValue = "1";
            this.Component.Width = 300;
            // 
            // Version
            // 
            this.Version.HeaderText = "Version";
            this.Version.Name = "Version";
            this.Version.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Version.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Version.Width = 110;
            // 
            // lstDropdownGrocery
            // 
            this.lstDropdownGrocery.AutoScroll = true;
            this.lstDropdownGrocery.Location = new System.Drawing.Point(693, 24);
            this.lstDropdownGrocery.Name = "lstDropdownGrocery";
            this.lstDropdownGrocery.Size = new System.Drawing.Size(150, 256);
            this.lstDropdownGrocery.TabIndex = 6;
            this.lstDropdownGrocery.Visible = false;
            // 
            // btnGrocery
            // 
            this.btnGrocery.Location = new System.Drawing.Point(270, 336);
            this.btnGrocery.Name = "btnGrocery";
            this.btnGrocery.Size = new System.Drawing.Size(151, 23);
            this.btnGrocery.TabIndex = 1;
            this.btnGrocery.Text = "Generate Master Manifest";
            this.btnGrocery.UseVisualStyleBackColor = true;
            this.btnGrocery.Click += new System.EventHandler(this.btnGenerateComponentManifest_Click);
            // 
            // chkListGrocery
            // 
            this.chkListGrocery.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkListGrocery.FormattingEnabled = true;
            this.chkListGrocery.Location = new System.Drawing.Point(435, 24);
            this.chkListGrocery.Name = "chkListGrocery";
            this.chkListGrocery.Size = new System.Drawing.Size(252, 268);
            this.chkListGrocery.TabIndex = 0;
            this.chkListGrocery.Visible = false;
            // 
            // AppStore
            // 
            this.AppStore.Controls.Add(this.dgvAppStore);
            this.AppStore.Controls.Add(this.btnAppStore);
            this.AppStore.Location = new System.Drawing.Point(4, 22);
            this.AppStore.Name = "AppStore";
            this.AppStore.Padding = new System.Windows.Forms.Padding(3);
            this.AppStore.Size = new System.Drawing.Size(427, 366);
            this.AppStore.TabIndex = 1;
            this.AppStore.Text = "AppStore";
            this.AppStore.UseVisualStyleBackColor = true;
            // 
            // dgvAppStore
            // 
            this.dgvAppStore.AllowUserToAddRows = false;
            this.dgvAppStore.AllowUserToDeleteRows = false;
            this.dgvAppStore.AllowUserToResizeColumns = false;
            this.dgvAppStore.AllowUserToResizeRows = false;
            this.dgvAppStore.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAppStore.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAppStore.ColumnHeadersVisible = false;
            this.dgvAppStore.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn5,
            this.dataGridViewComboBoxColumn4});
            this.dgvAppStore.Location = new System.Drawing.Point(6, 6);
            this.dgvAppStore.Name = "dgvAppStore";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAppStore.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAppStore.RowHeadersVisible = false;
            this.dgvAppStore.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvAppStore.Size = new System.Drawing.Size(415, 324);
            this.dgvAppStore.TabIndex = 8;
            this.dgvAppStore.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dgvAppStore.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView_CurrentCellDirtyStateChanged);
            // 
            // dataGridViewCheckBoxColumn5
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.NullValue = false;
            this.dataGridViewCheckBoxColumn5.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewCheckBoxColumn5.FalseValue = "0";
            this.dataGridViewCheckBoxColumn5.HeaderText = "Component";
            this.dataGridViewCheckBoxColumn5.Name = "dataGridViewCheckBoxColumn5";
            this.dataGridViewCheckBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn5.TrueValue = "1";
            this.dataGridViewCheckBoxColumn5.Width = 270;
            // 
            // dataGridViewComboBoxColumn4
            // 
            this.dataGridViewComboBoxColumn4.HeaderText = "Version";
            this.dataGridViewComboBoxColumn4.Name = "dataGridViewComboBoxColumn4";
            this.dataGridViewComboBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewComboBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewComboBoxColumn4.Width = 110;
            // 
            // btnAppStore
            // 
            this.btnAppStore.Location = new System.Drawing.Point(270, 336);
            this.btnAppStore.Name = "btnAppStore";
            this.btnAppStore.Size = new System.Drawing.Size(151, 23);
            this.btnAppStore.TabIndex = 7;
            this.btnAppStore.Text = "Generate Master Manifest";
            this.btnAppStore.UseVisualStyleBackColor = true;
            this.btnAppStore.Click += new System.EventHandler(this.btnGenerateComponentManifest_Click);
            // 
            // OMS
            // 
            this.OMS.Controls.Add(this.dgvOms);
            this.OMS.Controls.Add(this.chkListOms);
            this.OMS.Controls.Add(this.lstDropdownOms);
            this.OMS.Controls.Add(this.btnOms);
            this.OMS.Location = new System.Drawing.Point(4, 22);
            this.OMS.Name = "OMS";
            this.OMS.Padding = new System.Windows.Forms.Padding(3);
            this.OMS.Size = new System.Drawing.Size(427, 366);
            this.OMS.TabIndex = 2;
            this.OMS.Text = "OMS";
            this.OMS.UseVisualStyleBackColor = true;
            // 
            // dgvOms
            // 
            this.dgvOms.AllowUserToAddRows = false;
            this.dgvOms.AllowUserToDeleteRows = false;
            this.dgvOms.AllowUserToResizeColumns = false;
            this.dgvOms.AllowUserToResizeRows = false;
            this.dgvOms.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOms.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvOms.ColumnHeadersVisible = false;
            this.dgvOms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn2,
            this.dataGridViewComboBoxColumn1});
            this.dgvOms.Location = new System.Drawing.Point(6, 6);
            this.dgvOms.Name = "dgvOms";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOms.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvOms.RowHeadersVisible = false;
            this.dgvOms.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvOms.Size = new System.Drawing.Size(415, 324);
            this.dgvOms.TabIndex = 11;
            this.dgvOms.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dgvOms.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView_CurrentCellDirtyStateChanged);
            // 
            // dataGridViewCheckBoxColumn2
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.NullValue = false;
            this.dataGridViewCheckBoxColumn2.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewCheckBoxColumn2.FalseValue = "0";
            this.dataGridViewCheckBoxColumn2.HeaderText = "Component";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn2.TrueValue = "1";
            this.dataGridViewCheckBoxColumn2.Width = 270;
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.HeaderText = "Version";
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            this.dataGridViewComboBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewComboBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewComboBoxColumn1.Width = 110;
            // 
            // chkListOms
            // 
            this.chkListOms.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkListOms.FormattingEnabled = true;
            this.chkListOms.Location = new System.Drawing.Point(458, 20);
            this.chkListOms.Name = "chkListOms";
            this.chkListOms.Size = new System.Drawing.Size(252, 246);
            this.chkListOms.TabIndex = 10;
            this.chkListOms.Visible = false;
            // 
            // lstDropdownOms
            // 
            this.lstDropdownOms.AutoScroll = true;
            this.lstDropdownOms.Location = new System.Drawing.Point(718, 22);
            this.lstDropdownOms.Name = "lstDropdownOms";
            this.lstDropdownOms.Size = new System.Drawing.Size(150, 254);
            this.lstDropdownOms.TabIndex = 9;
            this.lstDropdownOms.Visible = false;
            // 
            // btnOms
            // 
            this.btnOms.Location = new System.Drawing.Point(270, 336);
            this.btnOms.Name = "btnOms";
            this.btnOms.Size = new System.Drawing.Size(151, 23);
            this.btnOms.TabIndex = 1;
            this.btnOms.Text = "Generate Master Manifest";
            this.btnOms.UseVisualStyleBackColor = true;
            this.btnOms.Click += new System.EventHandler(this.btnGenerateComponentManifest_Click);
            // 
            // Tibco
            // 
            this.Tibco.Controls.Add(this.dgvTibco);
            this.Tibco.Controls.Add(this.ddlTibcoRegions);
            this.Tibco.Controls.Add(this.label1);
            this.Tibco.Controls.Add(this.lstDropdownTibco);
            this.Tibco.Controls.Add(this.chkListTibco);
            this.Tibco.Controls.Add(this.btnTibco);
            this.Tibco.Location = new System.Drawing.Point(4, 22);
            this.Tibco.Name = "Tibco";
            this.Tibco.Padding = new System.Windows.Forms.Padding(3);
            this.Tibco.Size = new System.Drawing.Size(427, 366);
            this.Tibco.TabIndex = 3;
            this.Tibco.Text = "Tibco";
            this.Tibco.UseVisualStyleBackColor = true;
            // 
            // dgvTibco
            // 
            this.dgvTibco.AllowUserToAddRows = false;
            this.dgvTibco.AllowUserToDeleteRows = false;
            this.dgvTibco.AllowUserToResizeColumns = false;
            this.dgvTibco.AllowUserToResizeRows = false;
            this.dgvTibco.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTibco.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvTibco.ColumnHeadersVisible = false;
            this.dgvTibco.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn3,
            this.dataGridViewComboBoxColumn2});
            this.dgvTibco.Location = new System.Drawing.Point(6, 34);
            this.dgvTibco.Name = "dgvTibco";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTibco.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvTibco.RowHeadersVisible = false;
            this.dgvTibco.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvTibco.Size = new System.Drawing.Size(415, 299);
            this.dgvTibco.TabIndex = 31;
            this.dgvTibco.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dgvTibco.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView_CurrentCellDirtyStateChanged);
            // 
            // dataGridViewCheckBoxColumn3
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.NullValue = false;
            this.dataGridViewCheckBoxColumn3.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewCheckBoxColumn3.FalseValue = "0";
            this.dataGridViewCheckBoxColumn3.HeaderText = "Component";
            this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
            this.dataGridViewCheckBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn3.TrueValue = "1";
            this.dataGridViewCheckBoxColumn3.Width = 300;
            // 
            // dataGridViewComboBoxColumn2
            // 
            this.dataGridViewComboBoxColumn2.HeaderText = "Version";
            this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
            this.dataGridViewComboBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewComboBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewComboBoxColumn2.Width = 110;
            // 
            // ddlTibcoRegions
            // 
            this.ddlTibcoRegions.FormattingEnabled = true;
            this.ddlTibcoRegions.Location = new System.Drawing.Point(54, 7);
            this.ddlTibcoRegions.Name = "ddlTibcoRegions";
            this.ddlTibcoRegions.Size = new System.Drawing.Size(121, 21);
            this.ddlTibcoRegions.TabIndex = 30;
            this.ddlTibcoRegions.SelectedIndexChanged += new System.EventHandler(this.ddlTibcoRegions_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Region";
            // 
            // lstDropdownTibco
            // 
            this.lstDropdownTibco.AutoScroll = true;
            this.lstDropdownTibco.Location = new System.Drawing.Point(731, 47);
            this.lstDropdownTibco.Name = "lstDropdownTibco";
            this.lstDropdownTibco.Size = new System.Drawing.Size(150, 235);
            this.lstDropdownTibco.TabIndex = 28;
            this.lstDropdownTibco.Visible = false;
            // 
            // chkListTibco
            // 
            this.chkListTibco.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkListTibco.FormattingEnabled = true;
            this.chkListTibco.Location = new System.Drawing.Point(473, 47);
            this.chkListTibco.Name = "chkListTibco";
            this.chkListTibco.Size = new System.Drawing.Size(252, 224);
            this.chkListTibco.TabIndex = 27;
            this.chkListTibco.Visible = false;
            // 
            // btnTibco
            // 
            this.btnTibco.Location = new System.Drawing.Point(270, 337);
            this.btnTibco.Name = "btnTibco";
            this.btnTibco.Size = new System.Drawing.Size(151, 23);
            this.btnTibco.TabIndex = 1;
            this.btnTibco.Text = "Generate Master Manifest";
            this.btnTibco.UseVisualStyleBackColor = true;
            this.btnTibco.Click += new System.EventHandler(this.btnGenerateComponentManifest_Click);
            // 
            // dataGridViewComboBoxColumn3
            // 
            this.dataGridViewComboBoxColumn3.HeaderText = "Version";
            this.dataGridViewComboBoxColumn3.Name = "dataGridViewComboBoxColumn3";
            this.dataGridViewComboBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewComboBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewComboBoxColumn3.Width = 110;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(16, 440);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 5;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "Component";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.NullValue = false;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridViewCheckBoxColumn1.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewCheckBoxColumn1.FalseValue = "0";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Component";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn1.TrueValue = "1";
            this.dataGridViewCheckBoxColumn1.Width = 180;
            // 
            // dataGridViewCheckBoxColumn4
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.NullValue = false;
            this.dataGridViewCheckBoxColumn4.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewCheckBoxColumn4.FalseValue = "0";
            this.dataGridViewCheckBoxColumn4.HeaderText = "Component";
            this.dataGridViewCheckBoxColumn4.Name = "dataGridViewCheckBoxColumn4";
            this.dataGridViewCheckBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn4.TrueValue = "1";
            this.dataGridViewCheckBoxColumn4.Width = 200;
            // 
            // ddlReleaseVerison
            // 
            this.ddlReleaseVerison.FormattingEnabled = true;
            this.ddlReleaseVerison.Location = new System.Drawing.Point(118, 12);
            this.ddlReleaseVerison.Name = "ddlReleaseVerison";
            this.ddlReleaseVerison.Size = new System.Drawing.Size(121, 21);
            this.ddlReleaseVerison.TabIndex = 7;
            this.ddlReleaseVerison.SelectedIndexChanged += new System.EventHandler(this.ddlReleaseVerison_SelectedIndexChanged);
            // 
            // lblReleaseVersion
            // 
            this.lblReleaseVersion.AutoSize = true;
            this.lblReleaseVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReleaseVersion.Location = new System.Drawing.Point(15, 15);
            this.lblReleaseVersion.Name = "lblReleaseVersion";
            this.lblReleaseVersion.Size = new System.Drawing.Size(99, 13);
            this.lblReleaseVersion.TabIndex = 6;
            this.lblReleaseVersion.Text = "Release Version";
            // 
            // MasterManifest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 460);
            this.Controls.Add(this.ddlReleaseVerison);
            this.Controls.Add(this.lblReleaseVersion);
            this.Controls.Add(this.ManifestTabContainer);
            this.Controls.Add(this.lblMessage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MasterManifest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MasterManifest";
            this.Load += new System.EventHandler(this.MasterManifest_Load);
            this.ManifestTabContainer.ResumeLayout(false);
            this.Grocery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrocery)).EndInit();
            this.AppStore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppStore)).EndInit();
            this.OMS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOms)).EndInit();
            this.Tibco.ResumeLayout(false);
            this.Tibco.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTibco)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl ManifestTabContainer;
        private System.Windows.Forms.TabPage Grocery;
        private System.Windows.Forms.Button btnGrocery;
        private System.Windows.Forms.CheckedListBox chkListGrocery;
        private System.Windows.Forms.TabPage AppStore;
        private System.Windows.Forms.TabPage OMS;
        private System.Windows.Forms.Button btnOms;
        private System.Windows.Forms.TabPage Tibco;
        private System.Windows.Forms.CheckedListBox chkListTibco;
        private System.Windows.Forms.Button btnTibco;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel lstDropdownGrocery;
        private System.Windows.Forms.CheckedListBox chkListOms;
        private System.Windows.Forms.Panel lstDropdownOms;
        private System.Windows.Forms.ComboBox ddlTibcoRegions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel lstDropdownTibco;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridView dgvGrocery;
        private System.Windows.Forms.DataGridView dgvOms;
        private System.Windows.Forms.DataGridView dgvTibco;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn4;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn3;
        private System.Windows.Forms.Button btnAppStore;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Component;
        private System.Windows.Forms.DataGridViewComboBoxColumn Version;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridView dgvAppStore;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn5;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn4;
        private System.Windows.Forms.ComboBox ddlReleaseVerison;
        private System.Windows.Forms.Label lblReleaseVersion;
    }
}
