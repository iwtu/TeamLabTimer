namespace TimeTracker.GUI
{
    partial class TimeTrackerForm
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
            if (disposing && (components != null)) {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeTrackerForm));
            this.labelTime = new System.Windows.Forms.Label();
            this.comboBoxProjets = new System.Windows.Forms.ComboBox();
            this.comboBoxTasks = new System.Windows.Forms.ComboBox();
            this.buttonTime = new System.Windows.Forms.Button();
            this.labelProject = new System.Windows.Forms.Label();
            this.labelTask = new System.Windows.Forms.Label();
            this.checkBoxSeconds = new System.Windows.Forms.CheckBox();
            this.labelPortal = new System.Windows.Forms.Label();
            this.timerTaskTimer = new System.Windows.Forms.Timer(this.components);
            this.timerUpdateTaskTimeOnServer = new System.Windows.Forms.Timer(this.components);
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.labelNote = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxTasks = new System.Windows.Forms.CheckBox();
            this.linkLabelAbout = new System.Windows.Forms.LinkLabel();
            this.timerTasksUpdate = new System.Windows.Forms.Timer(this.components);
            this.labelHours = new System.Windows.Forms.Label();
            this.linkLabelClearTime = new System.Windows.Forms.LinkLabel();
            this.buttonMainTask = new System.Windows.Forms.Button();
            this.linkLabelSetMainTask = new System.Windows.Forms.LinkLabel();
            this.labelMainTask = new System.Windows.Forms.Label();
            this.linkLabelClearMainTask = new System.Windows.Forms.LinkLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // labelTime
            // 
            this.labelTime.BackColor = System.Drawing.SystemColors.Control;
            this.labelTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 35.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelTime.Location = new System.Drawing.Point(0, 0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(321, 64);
            this.labelTime.TabIndex = 0;
            this.labelTime.Text = "00:00:00";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxProjets
            // 
            this.comboBoxProjets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProjets.Location = new System.Drawing.Point(9, 127);
            this.comboBoxProjets.Name = "comboBoxProjets";
            this.comboBoxProjets.Size = new System.Drawing.Size(301, 21);
            this.comboBoxProjets.TabIndex = 1;
            this.comboBoxProjets.SelectedIndexChanged += new System.EventHandler(this.comboBoxProjets_SelectedIndexChanged);
            // 
            // comboBoxTasks
            // 
            this.comboBoxTasks.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxTasks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTasks.FormattingEnabled = true;
            this.comboBoxTasks.Location = new System.Drawing.Point(9, 167);
            this.comboBoxTasks.Name = "comboBoxTasks";
            this.comboBoxTasks.Size = new System.Drawing.Size(301, 21);
            this.comboBoxTasks.TabIndex = 3;
            this.comboBoxTasks.SelectedIndexChanged += new System.EventHandler(this.comboBoxTasks_SelectedIndexChanged);
            // 
            // buttonTime
            // 
            this.buttonTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonTime.Location = new System.Drawing.Point(117, 71);
            this.buttonTime.Name = "buttonTime";
            this.buttonTime.Size = new System.Drawing.Size(75, 23);
            this.buttonTime.TabIndex = 3;
            this.buttonTime.Text = "Start";
            this.buttonTime.UseVisualStyleBackColor = true;
            this.buttonTime.Click += new System.EventHandler(this.buttonTime_Click);
            // 
            // labelProject
            // 
            this.labelProject.Location = new System.Drawing.Point(1, 108);
            this.labelProject.Name = "labelProject";
            this.labelProject.Size = new System.Drawing.Size(123, 14);
            this.labelProject.TabIndex = 4;
            this.labelProject.Text = "Participated projects";
            this.labelProject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTask
            // 
            this.labelTask.Location = new System.Drawing.Point(2, 151);
            this.labelTask.Name = "labelTask";
            this.labelTask.Size = new System.Drawing.Size(80, 13);
            this.labelTask.TabIndex = 5;
            this.labelTask.Text = "Accepted Task";
            this.labelTask.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxSeconds
            // 
            this.checkBoxSeconds.AutoSize = true;
            this.checkBoxSeconds.Checked = true;
            this.checkBoxSeconds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSeconds.Location = new System.Drawing.Point(217, 75);
            this.checkBoxSeconds.Name = "checkBoxSeconds";
            this.checkBoxSeconds.Size = new System.Drawing.Size(96, 17);
            this.checkBoxSeconds.TabIndex = 6;
            this.checkBoxSeconds.Text = "Show seconds";
            this.checkBoxSeconds.UseVisualStyleBackColor = true;
            this.checkBoxSeconds.CheckedChanged += new System.EventHandler(this.checkBoxSeconds_CheckedChanged);
            // 
            // labelPortal
            // 
            this.labelPortal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPortal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPortal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelPortal.Location = new System.Drawing.Point(1, 354);
            this.labelPortal.Name = "labelPortal";
            this.labelPortal.Size = new System.Drawing.Size(324, 17);
            this.labelPortal.TabIndex = 7;
            this.labelPortal.Text = "Portal";
            this.labelPortal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelPortal.MouseEnter += new System.EventHandler(this.labelPortal_MouseEnter);
            // 
            // timerTaskTimer
            // 
            this.timerTaskTimer.Interval = 1000;
            this.timerTaskTimer.Tick += new System.EventHandler(this.timerTaskTimer_Tick);
            // 
            // timerUpdateTaskTimeOnServer
            // 
            this.timerUpdateTaskTimeOnServer.Interval = 30000;
            this.timerUpdateTaskTimeOnServer.Tick += new System.EventHandler(this.timerUpdateTaskTimeOnServer_Tick);
            // 
            // textBoxNote
            // 
            this.textBoxNote.Location = new System.Drawing.Point(9, 269);
            this.textBoxNote.Multiline = true;
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(304, 73);
            this.textBoxNote.TabIndex = 8;
            this.textBoxNote.TextChanged += new System.EventHandler(this.textBoxNote_TextChanged);
            // 
            // labelNote
            // 
            this.labelNote.Location = new System.Drawing.Point(6, 255);
            this.labelNote.Name = "labelNote";
            this.labelNote.Size = new System.Drawing.Size(30, 11);
            this.labelNote.TabIndex = 9;
            this.labelNote.Text = "Note";
            // 
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // checkBoxTasks
            // 
            this.checkBoxTasks.AutoSize = true;
            this.checkBoxTasks.Location = new System.Drawing.Point(5, 197);
            this.checkBoxTasks.Name = "checkBoxTasks";
            this.checkBoxTasks.Size = new System.Drawing.Size(111, 17);
            this.checkBoxTasks.TabIndex = 12;
            this.checkBoxTasks.Text = "All tasks in project";
            this.checkBoxTasks.UseVisualStyleBackColor = true;
            this.checkBoxTasks.CheckedChanged += new System.EventHandler(this.checkBoxTasks_CheckedChanged);
            // 
            // linkLabelAbout
            // 
            this.linkLabelAbout.AutoSize = true;
            this.linkLabelAbout.Location = new System.Drawing.Point(278, 384);
            this.linkLabelAbout.Name = "linkLabelAbout";
            this.linkLabelAbout.Size = new System.Drawing.Size(35, 13);
            this.linkLabelAbout.TabIndex = 13;
            this.linkLabelAbout.TabStop = true;
            this.linkLabelAbout.Text = "About";
            this.linkLabelAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAbout_LinkClicked);
            // 
            // timerTasksUpdate
            // 
            this.timerTasksUpdate.Interval = 5000;
            this.timerTasksUpdate.Tick += new System.EventHandler(this.timerTasksUpdate_Tick);
            // 
            // labelHours
            // 
            this.labelHours.AutoSize = true;
            this.labelHours.Location = new System.Drawing.Point(6, 64);
            this.labelHours.Name = "labelHours";
            this.labelHours.Size = new System.Drawing.Size(41, 13);
            this.labelHours.TabIndex = 14;
            this.labelHours.Text = "Hours: ";
            // 
            // linkLabelClearTime
            // 
            this.linkLabelClearTime.AutoSize = true;
            this.linkLabelClearTime.Location = new System.Drawing.Point(6, 77);
            this.linkLabelClearTime.Name = "linkLabelClearTime";
            this.linkLabelClearTime.Size = new System.Drawing.Size(53, 13);
            this.linkLabelClearTime.TabIndex = 15;
            this.linkLabelClearTime.TabStop = true;
            this.linkLabelClearTime.Text = "Clear time";
            this.linkLabelClearTime.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelClearTime_LinkClicked);
            this.linkLabelClearTime.MouseEnter += new System.EventHandler(this.linkLabelClearTime_MouseEnter);
            // 
            // buttonMainTask
            // 
            this.buttonMainTask.Location = new System.Drawing.Point(121, 199);
            this.buttonMainTask.Name = "buttonMainTask";
            this.buttonMainTask.Size = new System.Drawing.Size(75, 23);
            this.buttonMainTask.TabIndex = 16;
            this.buttonMainTask.Text = "Main Task";
            this.buttonMainTask.UseVisualStyleBackColor = true;
            this.buttonMainTask.Click += new System.EventHandler(this.buttonMainTask_Click);
            this.buttonMainTask.MouseHover += new System.EventHandler(this.buttonMainTask_MouseHover);
            // 
            // linkLabelSetMainTask
            // 
            this.linkLabelSetMainTask.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkLabelSetMainTask.AutoSize = true;
            this.linkLabelSetMainTask.Enabled = false;
            this.linkLabelSetMainTask.Location = new System.Drawing.Point(225, 198);
            this.linkLabelSetMainTask.Name = "linkLabelSetMainTask";
            this.linkLabelSetMainTask.Size = new System.Drawing.Size(85, 13);
            this.linkLabelSetMainTask.TabIndex = 17;
            this.linkLabelSetMainTask.TabStop = true;
            this.linkLabelSetMainTask.Text = "Set as main task";
            this.linkLabelSetMainTask.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSetMainTask_LinkClicked);
            // 
            // labelMainTask
            // 
            this.labelMainTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMainTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMainTask.Location = new System.Drawing.Point(0, 225);
            this.labelMainTask.Name = "labelMainTask";
            this.labelMainTask.Size = new System.Drawing.Size(325, 18);
            this.labelMainTask.TabIndex = 18;
            this.labelMainTask.Text = "MainTask";
            this.labelMainTask.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelMainTask.MouseEnter += new System.EventHandler(this.labelMainTask_MouseEnter);
            // 
            // linkLabelClearMainTask
            // 
            this.linkLabelClearMainTask.AutoSize = true;
            this.linkLabelClearMainTask.Location = new System.Drawing.Point(2, 384);
            this.linkLabelClearMainTask.Name = "linkLabelClearMainTask";
            this.linkLabelClearMainTask.Size = new System.Drawing.Size(84, 13);
            this.linkLabelClearMainTask.TabIndex = 19;
            this.linkLabelClearMainTask.TabStop = true;
            this.linkLabelClearMainTask.Text = "Clear Main Task";
            this.linkLabelClearMainTask.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelClearMainTask_LinkClicked);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // TimeTrackerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 401);
            this.Controls.Add(this.linkLabelClearMainTask);
            this.Controls.Add(this.labelMainTask);
            this.Controls.Add(this.linkLabelSetMainTask);
            this.Controls.Add(this.buttonMainTask);
            this.Controls.Add(this.linkLabelClearTime);
            this.Controls.Add(this.labelHours);
            this.Controls.Add(this.linkLabelAbout);
            this.Controls.Add(this.checkBoxTasks);
            this.Controls.Add(this.labelNote);
            this.Controls.Add(this.textBoxNote);
            this.Controls.Add(this.labelPortal);
            this.Controls.Add(this.checkBoxSeconds);
            this.Controls.Add(this.labelTask);
            this.Controls.Add(this.labelProject);
            this.Controls.Add(this.buttonTime);
            this.Controls.Add(this.comboBoxTasks);
            this.Controls.Add(this.comboBoxProjets);
            this.Controls.Add(this.labelTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TimeTrackerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TeamLab Timer ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TimeTrackerForm_FormClosing);
            this.Load += new System.EventHandler(this.TimeTrackerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.ComboBox comboBoxProjets;
        private System.Windows.Forms.ComboBox comboBoxTasks;
        private System.Windows.Forms.Button buttonTime;
        private System.Windows.Forms.Label labelProject;
        private System.Windows.Forms.Label labelTask;
        private System.Windows.Forms.CheckBox checkBoxSeconds;
        private System.Windows.Forms.Label labelPortal;
        private System.Windows.Forms.Timer timerTaskTimer;
        private System.Windows.Forms.Timer timerUpdateTaskTimeOnServer;
        private System.Windows.Forms.TextBox textBoxNote;
        private System.Windows.Forms.Label labelNote;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox checkBoxTasks;
        private System.Windows.Forms.LinkLabel linkLabelAbout;
        private System.Windows.Forms.Timer timerTasksUpdate;
        private System.Windows.Forms.Label labelHours;
        private System.Windows.Forms.LinkLabel linkLabelClearTime;
        private System.Windows.Forms.Button buttonMainTask;
        private System.Windows.Forms.LinkLabel linkLabelSetMainTask;
        private System.Windows.Forms.Label labelMainTask;
        private System.Windows.Forms.LinkLabel linkLabelClearMainTask;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}