namespace WebAccessor
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.commentViewTab = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.processCommentsPanel = new System.Windows.Forms.Panel();
            this.deleteSettingPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.deleteCountTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.threadCountTextBox = new System.Windows.Forms.TextBox();
            this.newsTypeComboBox = new System.Windows.Forms.ComboBox();
            this.activityPanel = new System.Windows.Forms.Panel();
            this.updateButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.unprocessCommentsPanel = new System.Windows.Forms.Panel();
            this.stopCommentsButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.commentLogBox = new System.Windows.Forms.TextBox();
            this.progressBarPanel = new System.Windows.Forms.Panel();
            this.commentsProgressBar = new System.Windows.Forms.ProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.commentStateLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.currentCommentsLabel = new System.Windows.Forms.Label();
            this.totalCommentsLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.commentTable = new System.Windows.Forms.DataGridView();
            this.idx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.content = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.board = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newsViewTab = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.newsSettingPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.newsEndDatePicker = new System.Windows.Forms.DateTimePicker();
            this.newsBeginDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.commentContentTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.newsStateLabel = new System.Windows.Forms.Label();
            this.loadNewsTypeComboBox = new System.Windows.Forms.ComboBox();
            this.processNewsPanel = new System.Windows.Forms.Panel();
            this.autoCommentButton = new System.Windows.Forms.Button();
            this.updateNewsButton = new System.Windows.Forms.Button();
            this.unprocessNewsPanel = new System.Windows.Forms.Panel();
            this.stopNewsButton = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.newsLogBox = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.newsProgressBar = new System.Windows.Forms.ProgressBar();
            this.newsTable = new System.Windows.Forms.DataGridView();
            this.newsTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newsDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.commentViewTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.processCommentsPanel.SuspendLayout();
            this.deleteSettingPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.activityPanel.SuspendLayout();
            this.unprocessCommentsPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.progressBarPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commentTable)).BeginInit();
            this.newsViewTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.newsSettingPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.processNewsPanel.SuspendLayout();
            this.unprocessNewsPanel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.commentViewTab);
            this.tabControl1.Controls.Add(this.newsViewTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(844, 501);
            this.tabControl1.TabIndex = 0;
            // 
            // commentViewTab
            // 
            this.commentViewTab.Controls.Add(this.splitContainer1);
            this.commentViewTab.Location = new System.Drawing.Point(4, 22);
            this.commentViewTab.Name = "commentViewTab";
            this.commentViewTab.Padding = new System.Windows.Forms.Padding(3);
            this.commentViewTab.Size = new System.Drawing.Size(836, 475);
            this.commentViewTab.TabIndex = 0;
            this.commentViewTab.Text = "댓글";
            this.commentViewTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.commentTable);
            this.splitContainer1.Size = new System.Drawing.Size(830, 469);
            this.splitContainer1.SplitterDistance = 359;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.processCommentsPanel);
            this.splitContainer2.Panel1.Controls.Add(this.unprocessCommentsPanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel1);
            this.splitContainer2.Size = new System.Drawing.Size(359, 469);
            this.splitContainer2.SplitterDistance = 199;
            this.splitContainer2.TabIndex = 0;
            // 
            // processCommentsPanel
            // 
            this.processCommentsPanel.Controls.Add(this.deleteSettingPanel);
            this.processCommentsPanel.Controls.Add(this.activityPanel);
            this.processCommentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processCommentsPanel.Location = new System.Drawing.Point(0, 0);
            this.processCommentsPanel.Name = "processCommentsPanel";
            this.processCommentsPanel.Size = new System.Drawing.Size(359, 174);
            this.processCommentsPanel.TabIndex = 4;
            // 
            // deleteSettingPanel
            // 
            this.deleteSettingPanel.Controls.Add(this.tableLayoutPanel2);
            this.deleteSettingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deleteSettingPanel.Location = new System.Drawing.Point(0, 0);
            this.deleteSettingPanel.Name = "deleteSettingPanel";
            this.deleteSettingPanel.Size = new System.Drawing.Size(359, 149);
            this.deleteSettingPanel.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.deleteCountTextBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.threadCountTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.newsTypeComboBox, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(359, 149);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 61);
            this.label10.Margin = new System.Windows.Forms.Padding(7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "News type";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 7);
            this.label8.Margin = new System.Windows.Forms.Padding(7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "Thread count";
            // 
            // deleteCountTextBox
            // 
            this.deleteCountTextBox.Location = new System.Drawing.Point(110, 30);
            this.deleteCountTextBox.Name = "deleteCountTextBox";
            this.deleteCountTextBox.Size = new System.Drawing.Size(100, 21);
            this.deleteCountTextBox.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 34);
            this.label9.Margin = new System.Windows.Forms.Padding(7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "Delete count";
            // 
            // threadCountTextBox
            // 
            this.threadCountTextBox.Location = new System.Drawing.Point(110, 3);
            this.threadCountTextBox.Name = "threadCountTextBox";
            this.threadCountTextBox.Size = new System.Drawing.Size(100, 21);
            this.threadCountTextBox.TabIndex = 1;
            this.threadCountTextBox.Text = "5";
            // 
            // newsTypeComboBox
            // 
            this.newsTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.newsTypeComboBox.FormattingEnabled = true;
            this.newsTypeComboBox.Location = new System.Drawing.Point(110, 57);
            this.newsTypeComboBox.Name = "newsTypeComboBox";
            this.newsTypeComboBox.Size = new System.Drawing.Size(121, 20);
            this.newsTypeComboBox.TabIndex = 3;
            // 
            // activityPanel
            // 
            this.activityPanel.Controls.Add(this.updateButton);
            this.activityPanel.Controls.Add(this.deleteButton);
            this.activityPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.activityPanel.Location = new System.Drawing.Point(0, 149);
            this.activityPanel.Name = "activityPanel";
            this.activityPanel.Size = new System.Drawing.Size(359, 25);
            this.activityPanel.TabIndex = 5;
            // 
            // updateButton
            // 
            this.updateButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.updateButton.Location = new System.Drawing.Point(209, 0);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 25);
            this.updateButton.TabIndex = 2;
            this.updateButton.Text = "업데이트";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.deleteButton.Location = new System.Drawing.Point(284, 0);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 25);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "삭제";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // unprocessCommentsPanel
            // 
            this.unprocessCommentsPanel.Controls.Add(this.stopCommentsButton);
            this.unprocessCommentsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.unprocessCommentsPanel.Enabled = false;
            this.unprocessCommentsPanel.Location = new System.Drawing.Point(0, 174);
            this.unprocessCommentsPanel.Name = "unprocessCommentsPanel";
            this.unprocessCommentsPanel.Size = new System.Drawing.Size(359, 25);
            this.unprocessCommentsPanel.TabIndex = 3;
            // 
            // stopCommentsButton
            // 
            this.stopCommentsButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.stopCommentsButton.Location = new System.Drawing.Point(284, 0);
            this.stopCommentsButton.Name = "stopCommentsButton";
            this.stopCommentsButton.Size = new System.Drawing.Size(75, 25);
            this.stopCommentsButton.TabIndex = 0;
            this.stopCommentsButton.Text = "중지";
            this.stopCommentsButton.UseVisualStyleBackColor = true;
            this.stopCommentsButton.Click += new System.EventHandler(this.stopCommentsButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(359, 266);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.commentLogBox);
            this.panel3.Controls.Add(this.progressBarPanel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 78);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(359, 188);
            this.panel3.TabIndex = 3;
            // 
            // commentLogBox
            // 
            this.commentLogBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commentLogBox.Location = new System.Drawing.Point(0, 0);
            this.commentLogBox.Multiline = true;
            this.commentLogBox.Name = "commentLogBox";
            this.commentLogBox.ReadOnly = true;
            this.commentLogBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commentLogBox.Size = new System.Drawing.Size(359, 168);
            this.commentLogBox.TabIndex = 2;
            // 
            // progressBarPanel
            // 
            this.progressBarPanel.Controls.Add(this.commentsProgressBar);
            this.progressBarPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarPanel.Location = new System.Drawing.Point(0, 168);
            this.progressBarPanel.Name = "progressBarPanel";
            this.progressBarPanel.Size = new System.Drawing.Size(359, 20);
            this.progressBarPanel.TabIndex = 1;
            // 
            // commentsProgressBar
            // 
            this.commentsProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commentsProgressBar.Location = new System.Drawing.Point(0, 0);
            this.commentsProgressBar.Name = "commentsProgressBar";
            this.commentsProgressBar.Size = new System.Drawing.Size(359, 20);
            this.commentsProgressBar.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.commentStateLabel);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.currentCommentsLabel);
            this.panel2.Controls.Add(this.totalCommentsLabel);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(359, 78);
            this.panel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "총 댓글";
            // 
            // commentStateLabel
            // 
            this.commentStateLabel.AutoSize = true;
            this.commentStateLabel.Location = new System.Drawing.Point(76, 52);
            this.commentStateLabel.Name = "commentStateLabel";
            this.commentStateLabel.Size = new System.Drawing.Size(0, 12);
            this.commentStateLabel.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "읽은 댓글";
            // 
            // currentCommentsLabel
            // 
            this.currentCommentsLabel.AutoSize = true;
            this.currentCommentsLabel.Location = new System.Drawing.Point(76, 31);
            this.currentCommentsLabel.Name = "currentCommentsLabel";
            this.currentCommentsLabel.Size = new System.Drawing.Size(0, 12);
            this.currentCommentsLabel.TabIndex = 1;
            // 
            // totalCommentsLabel
            // 
            this.totalCommentsLabel.AutoSize = true;
            this.totalCommentsLabel.Location = new System.Drawing.Point(76, 10);
            this.totalCommentsLabel.Name = "totalCommentsLabel";
            this.totalCommentsLabel.Size = new System.Drawing.Size(0, 12);
            this.totalCommentsLabel.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "현재 상태";
            // 
            // commentTable
            // 
            this.commentTable.AllowUserToAddRows = false;
            this.commentTable.AllowUserToDeleteRows = false;
            this.commentTable.AllowUserToResizeRows = false;
            this.commentTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.commentTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.commentTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idx,
            this.content,
            this.board,
            this.date});
            this.commentTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commentTable.EnableHeadersVisualStyles = false;
            this.commentTable.Location = new System.Drawing.Point(0, 0);
            this.commentTable.Name = "commentTable";
            this.commentTable.ReadOnly = true;
            this.commentTable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.commentTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.commentTable.RowTemplate.Height = 23;
            this.commentTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.commentTable.Size = new System.Drawing.Size(467, 469);
            this.commentTable.TabIndex = 0;
            this.commentTable.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.commentTable_CellContentDoubleClick);
            // 
            // idx
            // 
            this.idx.HeaderText = "번호";
            this.idx.Name = "idx";
            this.idx.ReadOnly = true;
            // 
            // content
            // 
            this.content.HeaderText = "내용";
            this.content.Name = "content";
            this.content.ReadOnly = true;
            // 
            // board
            // 
            this.board.HeaderText = "기사";
            this.board.Name = "board";
            this.board.ReadOnly = true;
            // 
            // date
            // 
            this.date.HeaderText = "작성일";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            // 
            // newsViewTab
            // 
            this.newsViewTab.Controls.Add(this.splitContainer3);
            this.newsViewTab.Location = new System.Drawing.Point(4, 22);
            this.newsViewTab.Name = "newsViewTab";
            this.newsViewTab.Padding = new System.Windows.Forms.Padding(3);
            this.newsViewTab.Size = new System.Drawing.Size(836, 475);
            this.newsViewTab.TabIndex = 1;
            this.newsViewTab.Text = "기사";
            this.newsViewTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.newsTable);
            this.splitContainer3.Size = new System.Drawing.Size(830, 469);
            this.splitContainer3.SplitterDistance = 393;
            this.splitContainer3.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.newsSettingPanel);
            this.splitContainer4.Panel1.Controls.Add(this.processNewsPanel);
            this.splitContainer4.Panel1.Controls.Add(this.unprocessNewsPanel);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.panel4);
            this.splitContainer4.Panel2.Controls.Add(this.panel5);
            this.splitContainer4.Size = new System.Drawing.Size(393, 469);
            this.splitContainer4.SplitterDistance = 295;
            this.splitContainer4.TabIndex = 0;
            // 
            // newsSettingPanel
            // 
            this.newsSettingPanel.Controls.Add(this.tableLayoutPanel1);
            this.newsSettingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newsSettingPanel.Location = new System.Drawing.Point(0, 0);
            this.newsSettingPanel.Name = "newsSettingPanel";
            this.newsSettingPanel.Size = new System.Drawing.Size(393, 245);
            this.newsSettingPanel.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.8117F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.1883F));
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.newsEndDatePicker, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.newsBeginDatePicker, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.commentContentTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.newsStateLabel, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.loadNewsTypeComboBox, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(393, 245);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 167);
            this.label11.Margin = new System.Windows.Forms.Padding(7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 4;
            this.label11.Text = "현재상태";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 34);
            this.label5.Margin = new System.Windows.Forms.Padding(7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "종료날짜";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 61);
            this.label7.Margin = new System.Windows.Forms.Padding(7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "댓글내용";
            // 
            // newsEndDatePicker
            // 
            this.newsEndDatePicker.Location = new System.Drawing.Point(72, 30);
            this.newsEndDatePicker.Name = "newsEndDatePicker";
            this.newsEndDatePicker.Size = new System.Drawing.Size(200, 21);
            this.newsEndDatePicker.TabIndex = 1;
            this.newsEndDatePicker.Value = new System.DateTime(2005, 12, 31, 0, 0, 0, 0);
            // 
            // newsBeginDatePicker
            // 
            this.newsBeginDatePicker.Location = new System.Drawing.Point(72, 3);
            this.newsBeginDatePicker.Name = "newsBeginDatePicker";
            this.newsBeginDatePicker.Size = new System.Drawing.Size(200, 21);
            this.newsBeginDatePicker.TabIndex = 0;
            this.newsBeginDatePicker.Value = new System.DateTime(2005, 1, 1, 0, 0, 0, 0);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 141);
            this.label6.Margin = new System.Windows.Forms.Padding(7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "분류";
            // 
            // commentContentTextBox
            // 
            this.commentContentTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commentContentTextBox.Location = new System.Drawing.Point(72, 57);
            this.commentContentTextBox.Multiline = true;
            this.commentContentTextBox.Name = "commentContentTextBox";
            this.commentContentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commentContentTextBox.Size = new System.Drawing.Size(318, 74);
            this.commentContentTextBox.TabIndex = 2;
            this.commentContentTextBox.Text = "안녕하세요 ^_^\r\n한국산업기술대학교 화이팅!!\r\n이 기사 너무 좋네요 잘 보고 갑니다.\r\n아 배고파 죽겠다\r\n나는 커서 뭐가 될까? 내 인생은 " +
    "망했어.\r\n평범한 보통 노멀사람이 되고 싶다.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 7);
            this.label4.Margin = new System.Windows.Forms.Padding(7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "시작날짜";
            // 
            // newsStateLabel
            // 
            this.newsStateLabel.AutoSize = true;
            this.newsStateLabel.Location = new System.Drawing.Point(76, 167);
            this.newsStateLabel.Margin = new System.Windows.Forms.Padding(7);
            this.newsStateLabel.Name = "newsStateLabel";
            this.newsStateLabel.Size = new System.Drawing.Size(29, 12);
            this.newsStateLabel.TabIndex = 3;
            this.newsStateLabel.Text = "대기";
            // 
            // loadNewsTypeComboBox
            // 
            this.loadNewsTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loadNewsTypeComboBox.FormattingEnabled = true;
            this.loadNewsTypeComboBox.Location = new System.Drawing.Point(72, 137);
            this.loadNewsTypeComboBox.Name = "loadNewsTypeComboBox";
            this.loadNewsTypeComboBox.Size = new System.Drawing.Size(121, 20);
            this.loadNewsTypeComboBox.TabIndex = 5;
            // 
            // processNewsPanel
            // 
            this.processNewsPanel.Controls.Add(this.autoCommentButton);
            this.processNewsPanel.Controls.Add(this.updateNewsButton);
            this.processNewsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.processNewsPanel.Location = new System.Drawing.Point(0, 245);
            this.processNewsPanel.Name = "processNewsPanel";
            this.processNewsPanel.Size = new System.Drawing.Size(393, 25);
            this.processNewsPanel.TabIndex = 7;
            // 
            // autoCommentButton
            // 
            this.autoCommentButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.autoCommentButton.Location = new System.Drawing.Point(243, 0);
            this.autoCommentButton.Name = "autoCommentButton";
            this.autoCommentButton.Size = new System.Drawing.Size(75, 25);
            this.autoCommentButton.TabIndex = 4;
            this.autoCommentButton.Text = "댓글 공작";
            this.autoCommentButton.UseVisualStyleBackColor = true;
            this.autoCommentButton.Click += new System.EventHandler(this.autoCommentButton_Click);
            // 
            // updateNewsButton
            // 
            this.updateNewsButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.updateNewsButton.Location = new System.Drawing.Point(318, 0);
            this.updateNewsButton.Name = "updateNewsButton";
            this.updateNewsButton.Size = new System.Drawing.Size(75, 25);
            this.updateNewsButton.TabIndex = 5;
            this.updateNewsButton.Text = "기사 로드";
            this.updateNewsButton.UseVisualStyleBackColor = true;
            this.updateNewsButton.Click += new System.EventHandler(this.updateNewsButton_Click);
            // 
            // unprocessNewsPanel
            // 
            this.unprocessNewsPanel.Controls.Add(this.stopNewsButton);
            this.unprocessNewsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.unprocessNewsPanel.Enabled = false;
            this.unprocessNewsPanel.Location = new System.Drawing.Point(0, 270);
            this.unprocessNewsPanel.Name = "unprocessNewsPanel";
            this.unprocessNewsPanel.Size = new System.Drawing.Size(393, 25);
            this.unprocessNewsPanel.TabIndex = 6;
            // 
            // stopNewsButton
            // 
            this.stopNewsButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.stopNewsButton.Location = new System.Drawing.Point(318, 0);
            this.stopNewsButton.Name = "stopNewsButton";
            this.stopNewsButton.Size = new System.Drawing.Size(75, 25);
            this.stopNewsButton.TabIndex = 6;
            this.stopNewsButton.Text = "중지";
            this.stopNewsButton.UseVisualStyleBackColor = true;
            this.stopNewsButton.Click += new System.EventHandler(this.stopNewsButton_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.newsLogBox);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(393, 150);
            this.panel4.TabIndex = 1;
            // 
            // newsLogBox
            // 
            this.newsLogBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newsLogBox.Location = new System.Drawing.Point(0, 0);
            this.newsLogBox.Multiline = true;
            this.newsLogBox.Name = "newsLogBox";
            this.newsLogBox.ReadOnly = true;
            this.newsLogBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.newsLogBox.Size = new System.Drawing.Size(393, 150);
            this.newsLogBox.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.newsProgressBar);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 150);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(393, 20);
            this.panel5.TabIndex = 0;
            // 
            // newsProgressBar
            // 
            this.newsProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newsProgressBar.Location = new System.Drawing.Point(0, 0);
            this.newsProgressBar.Name = "newsProgressBar";
            this.newsProgressBar.Size = new System.Drawing.Size(393, 20);
            this.newsProgressBar.TabIndex = 0;
            // 
            // newsTable
            // 
            this.newsTable.AllowUserToAddRows = false;
            this.newsTable.AllowUserToDeleteRows = false;
            this.newsTable.AllowUserToResizeRows = false;
            this.newsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.newsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.newsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.newsTitle,
            this.newsDate});
            this.newsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newsTable.EnableHeadersVisualStyles = false;
            this.newsTable.Location = new System.Drawing.Point(0, 0);
            this.newsTable.Name = "newsTable";
            this.newsTable.ReadOnly = true;
            this.newsTable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.newsTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.newsTable.RowTemplate.Height = 23;
            this.newsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.newsTable.Size = new System.Drawing.Size(433, 469);
            this.newsTable.TabIndex = 0;
            this.newsTable.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.newsTable_CellContentDoubleClick);
            // 
            // newsTitle
            // 
            this.newsTitle.HeaderText = "기사";
            this.newsTitle.Name = "newsTitle";
            this.newsTitle.ReadOnly = true;
            // 
            // newsDate
            // 
            this.newsDate.HeaderText = "작성일";
            this.newsDate.Name = "newsDate";
            this.newsDate.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 501);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "과거청산 합시다 ^_^";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.commentViewTab.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.processCommentsPanel.ResumeLayout(false);
            this.deleteSettingPanel.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.activityPanel.ResumeLayout(false);
            this.unprocessCommentsPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.progressBarPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commentTable)).EndInit();
            this.newsViewTab.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.newsSettingPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.processNewsPanel.ResumeLayout(false);
            this.unprocessNewsPanel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.newsTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage commentViewTab;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel progressBarPanel;
        private System.Windows.Forms.ProgressBar commentsProgressBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label commentStateLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label currentCommentsLabel;
        private System.Windows.Forms.Label totalCommentsLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView commentTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn idx;
        private System.Windows.Forms.DataGridViewTextBoxColumn content;
        private System.Windows.Forms.DataGridViewTextBoxColumn board;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.TabPage newsViewTab;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView newsTable;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.DataGridViewTextBoxColumn newsTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn newsDate;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ProgressBar newsProgressBar;
        private System.Windows.Forms.Panel processCommentsPanel;
        private System.Windows.Forms.Panel unprocessCommentsPanel;
        private System.Windows.Forms.Button stopCommentsButton;
        private System.Windows.Forms.Panel newsSettingPanel;
        private System.Windows.Forms.Label newsStateLabel;
        private System.Windows.Forms.Panel processNewsPanel;
        private System.Windows.Forms.Button autoCommentButton;
        private System.Windows.Forms.Button updateNewsButton;
        private System.Windows.Forms.Panel unprocessNewsPanel;
        private System.Windows.Forms.Button stopNewsButton;
        private System.Windows.Forms.TextBox newsLogBox;
        private System.Windows.Forms.Panel deleteSettingPanel;
        private System.Windows.Forms.Panel activityPanel;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.TextBox deleteCountTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox threadCountTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox commentLogBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker newsEndDatePicker;
        private System.Windows.Forms.DateTimePicker newsBeginDatePicker;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox commentContentTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox newsTypeComboBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox loadNewsTypeComboBox;
    }
}

