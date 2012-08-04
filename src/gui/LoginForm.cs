/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */

using System;
using System.Windows.Forms;
using TeamLab.Exceptions;


namespace TeamLab.GUI
{
    public partial class LoginForm : Form
    {
                
        public LoginForm()
        {
            InitializeComponent();
            textBoxPortal.Text = Properties.Settings.Default.Portal;
            textBoxLogin.Text = Properties.Settings.Default.UserName;
            textBoxPass.Text = Properties.Settings.Default.Password;
            checkBoxPass.Checked = Properties.Settings.Default.SavePassword;
        }

        public LoginForm(String errorMessage) : this()
        {
            labelWrongCredentials.Text = errorMessage;
        }

        private void btnSingIn_Click(object sender, EventArgs e)
        {
            string portal = textBoxPortal.Text;
            string name = textBoxLogin.Text;
            string password = textBoxPass.Text;

            Properties.Settings.Default.Portal = portal;
            Properties.Settings.Default.UserName = name;

            if (checkBoxPass.Checked) {
                Properties.Settings.Default.Password = password;
                Properties.Settings.Default.SavePassword = true;
            } else {
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.SavePassword = false;
            }
            Properties.Settings.Default.Save();

            try {
                TeamLab.Wrapper.Facade.Authentificate(portal, name, password);
            } catch (TeamLabExpception ex) {
                if (ex.Type == TeamLabExpception.TYPE.CredentialsOrPortal ||
                    ex.Type == TeamLabExpception.TYPE.Connection) {
                    labelWrongCredentials.Text = ex.Message;
                    this.DialogResult = System.Windows.Forms.DialogResult.Retry;
                } else if (ex.Type == TeamLabExpception.TYPE.DeleteTimerOnServer) {
                    labelWrongCredentials.Text = "Authentificaton refused. Please, check your inputs.";
                    this.DialogResult = System.Windows.Forms.DialogResult.Retry;
                } else {
                    throw ex;
                }
            }

        }        

        private void linkLabelAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form aboutDialog = new AboutForm();
            aboutDialog.ShowDialog(this);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }      
                
    }
}
