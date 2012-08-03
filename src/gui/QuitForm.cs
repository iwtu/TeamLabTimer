/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */
using System.Windows.Forms;

namespace TeamLab.GUI
{
    public partial class FormQuit : Form
    {
        public FormQuit()
        {
            InitializeComponent();
        }

        public void CloseWithoutConnection(string lastUploadTime)
        {
            buttonNoSave.Text = "Just close";
            buttonSave.Enabled = false;
            labelQuestion.Text = "Connection is down. Your the latest update time for this task is " + lastUploadTime;
 
        }

        

        

      
    }
}
