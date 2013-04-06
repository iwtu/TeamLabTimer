/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */


using System;
using System.Windows.Forms;
using TimeTracker.GUI;

namespace TimeTracker.Main
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            LoginForm loginForm = new LoginForm();
            DialogResult dialogResult = loginForm.ShowDialog();
            while (dialogResult == DialogResult.Retry)
            {
                dialogResult = loginForm.ShowDialog();
            }

            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    Application.Run(new TimeTrackerForm());
                }
                catch (Exception)
                {
                    MessageBox.Show("Some odd error has occured. Program don't know how to deal with it, so I will quit. If you see this message more than is healty, you can send me an email. Have a nice day :-)");
                }
            }
            else
            {
                Application.Exit();
            }
        }



    }
}
