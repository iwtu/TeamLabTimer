namespace TeamLab.GUI
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.buttonSingIn = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelPass = new System.Windows.Forms.Label();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.textBoxPortal = new System.Windows.Forms.TextBox();
            this.labelPortal = new System.Windows.Forms.Label();
            this.labelCaption = new System.Windows.Forms.Label();
            this.checkBoxPass = new System.Windows.Forms.CheckBox();
            this.labelWrongCredentials = new System.Windows.Forms.Label();
            this.linkLabelAbout = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSingIn
            // 
            this.buttonSingIn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSingIn.Location = new System.Drawing.Point(160, 184);
            this.buttonSingIn.Name = "buttonSingIn";
            this.buttonSingIn.Size = new System.Drawing.Size(75, 23);
            this.buttonSingIn.TabIndex = 5;
            this.buttonSingIn.Text = "Sign in";
            this.buttonSingIn.UseVisualStyleBackColor = true;
            this.buttonSingIn.Click += new System.EventHandler(this.btnSingIn_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(74, 184);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 6;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(40, 91);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(33, 13);
            this.labelLogin.TabIndex = 2;
            this.labelLogin.Text = "Login";
            // 
            // labelPass
            // 
            this.labelPass.AutoSize = true;
            this.labelPass.Location = new System.Drawing.Point(21, 121);
            this.labelPass.Name = "labelPass";
            this.labelPass.Size = new System.Drawing.Size(53, 13);
            this.labelPass.TabIndex = 3;
            this.labelPass.Text = "Password";
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(74, 88);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(203, 20);
            this.textBoxLogin.TabIndex = 2;
            // 
            // textBoxPass
            // 
            this.textBoxPass.Location = new System.Drawing.Point(74, 118);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.PasswordChar = '*';
            this.textBoxPass.Size = new System.Drawing.Size(203, 20);
            this.textBoxPass.TabIndex = 3;
            // 
            // textBoxPortal
            // 
            this.textBoxPortal.Location = new System.Drawing.Point(38, 6);
            this.textBoxPortal.Name = "textBoxPortal";
            this.textBoxPortal.Size = new System.Drawing.Size(134, 20);
            this.textBoxPortal.TabIndex = 1;
            // 
            // labelPortal
            // 
            this.labelPortal.AutoSize = true;
            this.labelPortal.Location = new System.Drawing.Point(3, 9);
            this.labelPortal.Name = "labelPortal";
            this.labelPortal.Size = new System.Drawing.Size(34, 13);
            this.labelPortal.TabIndex = 7;
            this.labelPortal.Text = "Portal";
            // 
            // labelCaption
            // 
            this.labelCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCaption.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelCaption.Location = new System.Drawing.Point(0, 0);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(315, 43);
            this.labelCaption.TabIndex = 8;
            this.labelCaption.Text = "TeamLab Timer";
            this.labelCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxPass
            // 
            this.checkBoxPass.AutoSize = true;
            this.checkBoxPass.Location = new System.Drawing.Point(90, 144);
            this.checkBoxPass.Name = "checkBoxPass";
            this.checkBoxPass.Size = new System.Drawing.Size(187, 17);
            this.checkBoxPass.TabIndex = 4;
            this.checkBoxPass.Text = "Remember password (in plain text)";
            this.checkBoxPass.UseVisualStyleBackColor = true;
            // 
            // labelWrongCredentials
            // 
            this.labelWrongCredentials.AutoSize = true;
            this.labelWrongCredentials.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWrongCredentials.ForeColor = System.Drawing.Color.Red;
            this.labelWrongCredentials.Location = new System.Drawing.Point(12, 223);
            this.labelWrongCredentials.Name = "labelWrongCredentials";
            this.labelWrongCredentials.Size = new System.Drawing.Size(0, 13);
            this.labelWrongCredentials.TabIndex = 9;
            // 
            // linkLabelAbout
            // 
            this.linkLabelAbout.AutoSize = true;
            this.linkLabelAbout.Location = new System.Drawing.Point(264, 214);
            this.linkLabelAbout.Name = "linkLabelAbout";
            this.linkLabelAbout.Size = new System.Drawing.Size(35, 13);
            this.linkLabelAbout.TabIndex = 10;
            this.linkLabelAbout.TabStop = true;
            this.linkLabelAbout.Text = "About";
            this.linkLabelAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAbout_LinkClicked);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(169, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = ".teamlab.com";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBoxPortal);
            this.panel1.Controls.Add(this.labelPortal);
            this.panel1.Location = new System.Drawing.Point(37, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 37);
            this.panel1.TabIndex = 12;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.buttonSingIn;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(315, 245);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.linkLabelAbout);
            this.Controls.Add(this.labelWrongCredentials);
            this.Controls.Add(this.checkBoxPass);
            this.Controls.Add(this.labelCaption);
            this.Controls.Add(this.textBoxPass);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.labelPass);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSingIn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TeamLab Timer - Authentification";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSingIn;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelPass;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.TextBox textBoxPass;
        private System.Windows.Forms.TextBox textBoxPortal;
        private System.Windows.Forms.Label labelPortal;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.CheckBox checkBoxPass;
        private System.Windows.Forms.Label labelWrongCredentials;
        private System.Windows.Forms.LinkLabel linkLabelAbout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}