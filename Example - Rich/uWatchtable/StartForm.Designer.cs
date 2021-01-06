namespace uWatchtable
{
    partial class StartForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TopBar = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonMinimize = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.button_GE_Disconnect = new System.Windows.Forms.Button();
            this.button_GE_Connect = new System.Windows.Forms.Button();
            this.label_Odczyt = new System.Windows.Forms.Label();
            this.label_max = new System.Windows.Forms.Label();
            this.label_min = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_GE_valid = new System.Windows.Forms.Label();
            this.label_GE_Status = new System.Windows.Forms.Label();
            this.textBox_GE_IP = new System.Windows.Forms.TextBox();
            this.trackBar_Refresh = new System.Windows.Forms.TrackBar();
            this.panelContent = new System.Windows.Forms.Panel();
            this.labelStat = new System.Windows.Forms.Label();
            this.dataTable = new System.Windows.Forms.DataGridView();
            this.timerRW = new System.Windows.Forms.Timer(this.components);
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.RValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TopBar.SuspendLayout();
            this.LeftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Refresh)).BeginInit();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable)).BeginInit();
            this.SuspendLayout();
            // 
            // TopBar
            // 
            this.TopBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TopBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TopBar.Controls.Add(this.labelTitle);
            this.TopBar.Controls.Add(this.buttonMinimize);
            this.TopBar.Controls.Add(this.buttonClose);
            this.TopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopBar.Location = new System.Drawing.Point(0, 0);
            this.TopBar.Name = "TopBar";
            this.TopBar.Size = new System.Drawing.Size(795, 24);
            this.TopBar.TabIndex = 0;
            this.TopBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopBar_MouseDown);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.Silver;
            this.labelTitle.Location = new System.Drawing.Point(3, 2);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(153, 19);
            this.labelTitle.TabIndex = 4;
            this.labelTitle.Text = "Ultra Watchtable";
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseDown);
            // 
            // buttonMinimize
            // 
            this.buttonMinimize.BackColor = System.Drawing.Color.Transparent;
            this.buttonMinimize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMinimize.BackgroundImage")));
            this.buttonMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMinimize.ForeColor = System.Drawing.Color.Gray;
            this.buttonMinimize.Location = new System.Drawing.Point(754, 4);
            this.buttonMinimize.Name = "buttonMinimize";
            this.buttonMinimize.Size = new System.Drawing.Size(16, 16);
            this.buttonMinimize.TabIndex = 3;
            this.buttonMinimize.UseVisualStyleBackColor = false;
            this.buttonMinimize.Click += new System.EventHandler(this.buttonMinimize_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.Transparent;
            this.buttonClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonClose.BackgroundImage")));
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.ForeColor = System.Drawing.Color.Gray;
            this.buttonClose.Location = new System.Drawing.Point(776, 4);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(16, 16);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // LeftPanel
            // 
            this.LeftPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.LeftPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LeftPanel.Controls.Add(this.button_GE_Disconnect);
            this.LeftPanel.Controls.Add(this.button_GE_Connect);
            this.LeftPanel.Controls.Add(this.label_Odczyt);
            this.LeftPanel.Controls.Add(this.label_max);
            this.LeftPanel.Controls.Add(this.label_min);
            this.LeftPanel.Controls.Add(this.label5);
            this.LeftPanel.Controls.Add(this.label_GE_valid);
            this.LeftPanel.Controls.Add(this.label_GE_Status);
            this.LeftPanel.Controls.Add(this.textBox_GE_IP);
            this.LeftPanel.Controls.Add(this.trackBar_Refresh);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 24);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(156, 478);
            this.LeftPanel.TabIndex = 1;
            // 
            // button_GE_Disconnect
            // 
            this.button_GE_Disconnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_GE_Disconnect.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button_GE_Disconnect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_GE_Disconnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.button_GE_Disconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_GE_Disconnect.ForeColor = System.Drawing.Color.Silver;
            this.button_GE_Disconnect.Location = new System.Drawing.Point(7, 85);
            this.button_GE_Disconnect.Name = "button_GE_Disconnect";
            this.button_GE_Disconnect.Size = new System.Drawing.Size(78, 23);
            this.button_GE_Disconnect.TabIndex = 10;
            this.button_GE_Disconnect.Text = "DISCONNECT";
            this.button_GE_Disconnect.UseVisualStyleBackColor = false;
            this.button_GE_Disconnect.Visible = false;
            this.button_GE_Disconnect.Click += new System.EventHandler(this.button_GE_Disconnect_Click);
            // 
            // button_GE_Connect
            // 
            this.button_GE_Connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_GE_Connect.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button_GE_Connect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_GE_Connect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.button_GE_Connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_GE_Connect.ForeColor = System.Drawing.Color.Silver;
            this.button_GE_Connect.Location = new System.Drawing.Point(7, 56);
            this.button_GE_Connect.Name = "button_GE_Connect";
            this.button_GE_Connect.Size = new System.Drawing.Size(78, 23);
            this.button_GE_Connect.TabIndex = 10;
            this.button_GE_Connect.Text = "CONNECT";
            this.button_GE_Connect.UseVisualStyleBackColor = false;
            this.button_GE_Connect.Click += new System.EventHandler(this.button_GE_Connect_Click);
            // 
            // label_Odczyt
            // 
            this.label_Odczyt.AutoSize = true;
            this.label_Odczyt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label_Odczyt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label_Odczyt.Location = new System.Drawing.Point(3, 453);
            this.label_Odczyt.Name = "label_Odczyt";
            this.label_Odczyt.Size = new System.Drawing.Size(14, 15);
            this.label_Odczyt.TabIndex = 9;
            this.label_Odczyt.Text = "#";
            // 
            // label_max
            // 
            this.label_max.AutoSize = true;
            this.label_max.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label_max.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label_max.Location = new System.Drawing.Point(110, 133);
            this.label_max.Name = "label_max";
            this.label_max.Size = new System.Drawing.Size(38, 15);
            this.label_max.TabIndex = 9;
            this.label_max.Text = "#max";
            // 
            // label_min
            // 
            this.label_min.AutoSize = true;
            this.label_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label_min.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label_min.Location = new System.Drawing.Point(3, 133);
            this.label_min.Name = "label_min";
            this.label_min.Size = new System.Drawing.Size(35, 15);
            this.label_min.TabIndex = 9;
            this.label_min.Text = "#min";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label5.Location = new System.Drawing.Point(12, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "GE PLC Address:";
            // 
            // label_GE_valid
            // 
            this.label_GE_valid.AutoSize = true;
            this.label_GE_valid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label_GE_valid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label_GE_valid.Location = new System.Drawing.Point(119, 31);
            this.label_GE_valid.Name = "label_GE_valid";
            this.label_GE_valid.Size = new System.Drawing.Size(21, 15);
            this.label_GE_valid.TabIndex = 9;
            this.label_GE_valid.Text = "##";
            this.label_GE_valid.Visible = false;
            // 
            // label_GE_Status
            // 
            this.label_GE_Status.AutoSize = true;
            this.label_GE_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label_GE_Status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label_GE_Status.Location = new System.Drawing.Point(119, 76);
            this.label_GE_Status.Name = "label_GE_Status";
            this.label_GE_Status.Size = new System.Drawing.Size(21, 15);
            this.label_GE_Status.TabIndex = 9;
            this.label_GE_Status.Text = "##";
            this.label_GE_Status.Visible = false;
            // 
            // textBox_GE_IP
            // 
            this.textBox_GE_IP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBox_GE_IP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_GE_IP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox_GE_IP.Location = new System.Drawing.Point(7, 29);
            this.textBox_GE_IP.Name = "textBox_GE_IP";
            this.textBox_GE_IP.Size = new System.Drawing.Size(105, 20);
            this.textBox_GE_IP.TabIndex = 8;
            this.textBox_GE_IP.Text = "192.168.50.100";
            this.textBox_GE_IP.TextChanged += new System.EventHandler(this.textBox_GE_IP_TextChanged);
            // 
            // trackBar_Refresh
            // 
            this.trackBar_Refresh.Location = new System.Drawing.Point(20, 112);
            this.trackBar_Refresh.Maximum = 300;
            this.trackBar_Refresh.Minimum = 30;
            this.trackBar_Refresh.Name = "trackBar_Refresh";
            this.trackBar_Refresh.Size = new System.Drawing.Size(104, 45);
            this.trackBar_Refresh.TabIndex = 12;
            this.trackBar_Refresh.Value = 30;
            this.trackBar_Refresh.ValueChanged += new System.EventHandler(this.trackBar_Refresh_ValueChanged);
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelContent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContent.Controls.Add(this.labelStat);
            this.panelContent.Controls.Add(this.dataTable);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 24);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(795, 478);
            this.panelContent.TabIndex = 2;
            // 
            // labelStat
            // 
            this.labelStat.AutoSize = true;
            this.labelStat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.labelStat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelStat.Location = new System.Drawing.Point(173, 450);
            this.labelStat.Name = "labelStat";
            this.labelStat.Size = new System.Drawing.Size(0, 15);
            this.labelStat.TabIndex = 10;
            // 
            // dataTable
            // 
            this.dataTable.AllowUserToResizeColumns = false;
            this.dataTable.AllowUserToResizeRows = false;
            this.dataTable.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle31.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle31;
            this.dataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Address,
            this.Tag,
            this.Comment,
            this.Type,
            this.RValue,
            this.WValue});
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle38.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle38.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle38.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataTable.DefaultCellStyle = dataGridViewCellStyle38;
            this.dataTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataTable.Location = new System.Drawing.Point(159, 11);
            this.dataTable.Name = "dataTable";
            this.dataTable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle39.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle39.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle39.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle39.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle39.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle39.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle39;
            this.dataTable.RowHeadersVisible = false;
            this.dataTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle40.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle40.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle40.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataTable.RowsDefaultCellStyle = dataGridViewCellStyle40;
            this.dataTable.RowTemplate.Height = 20;
            this.dataTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataTable.Size = new System.Drawing.Size(611, 415);
            this.dataTable.TabIndex = 0;
            this.dataTable.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataTable_CellBeginEdit);
            this.dataTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataTable_CellEndEdit);
            // 
            // timerRW
            // 
            this.timerRW.Interval = 30;
            this.timerRW.Tick += new System.EventHandler(this.timerRW_Tick);
            // 
            // Address
            // 
            dataGridViewCellStyle32.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle32.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle32.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle32.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Address.DefaultCellStyle = dataGridViewCellStyle32;
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Address.Width = 80;
            // 
            // Tag
            // 
            dataGridViewCellStyle33.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Tag.DefaultCellStyle = dataGridViewCellStyle33;
            this.Tag.HeaderText = "Name";
            this.Tag.Name = "Tag";
            this.Tag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Comment
            // 
            dataGridViewCellStyle34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle34.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Comment.DefaultCellStyle = dataGridViewCellStyle34;
            this.Comment.HeaderText = "Comment";
            this.Comment.Name = "Comment";
            this.Comment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Comment.Width = 180;
            // 
            // Type
            // 
            dataGridViewCellStyle35.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Type.DefaultCellStyle = dataGridViewCellStyle35;
            this.Type.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Type.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Type.HeaderText = "Type";
            this.Type.Items.AddRange(new object[] {
            "BIN",
            "DEC",
            "HEX",
            "FLOAT"});
            this.Type.Name = "Type";
            this.Type.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Type.Width = 68;
            // 
            // RValue
            // 
            dataGridViewCellStyle36.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RValue.DefaultCellStyle = dataGridViewCellStyle36;
            this.RValue.HeaderText = "Value(Read)";
            this.RValue.Name = "RValue";
            this.RValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RValue.Width = 90;
            // 
            // WValue
            // 
            dataGridViewCellStyle37.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.WValue.DefaultCellStyle = dataGridViewCellStyle37;
            this.WValue.HeaderText = "Value(Write)";
            this.WValue.Name = "WValue";
            this.WValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WValue.Width = 90;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 502);
            this.ControlBox = false;
            this.Controls.Add(this.LeftPanel);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.TopBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartForm";
            this.ShowIcon = false;
            this.Text = "mainApp";
            this.TopBar.ResumeLayout(false);
            this.TopBar.PerformLayout();
            this.LeftPanel.ResumeLayout(false);
            this.LeftPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Refresh)).EndInit();
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopBar;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Button buttonMinimize;
        private System.Windows.Forms.Label labelTitle;
        public System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Button button_GE_Connect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_GE_Status;
        private System.Windows.Forms.TextBox textBox_GE_IP;
        private System.Windows.Forms.Button button_GE_Disconnect;
        private System.Windows.Forms.Label label_GE_valid;
        private System.Windows.Forms.DataGridView dataTable;
        private System.Windows.Forms.Timer timerRW;
        private System.Windows.Forms.TrackBar trackBar_Refresh;
        private System.Windows.Forms.Label labelStat;
        private System.Windows.Forms.Label label_max;
        private System.Windows.Forms.Label label_min;
        private System.Windows.Forms.Label label_Odczyt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tag;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewComboBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn RValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn WValue;
    }
}

