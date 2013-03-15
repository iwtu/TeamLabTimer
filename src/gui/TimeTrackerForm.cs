/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */

using System;
using System.Windows.Forms;
using TeamLab.Wrapper;
using TeamLab.Exceptions;

namespace TeamLab.GUI
{
    public partial class TimeTrackerForm : Form
    {

        private Facade facade = new Facade();
        private TLTimer timer;
        private Project[] projects;
        private string Portal;

        public TimeTrackerForm()
        {
            InitializeComponent();
            timer = new TLTimer(facade.GetMyId());

            projects = facade.GetParticiapedProjects();
            foreach (Project p in projects) {
                comboBoxProjets.Items.Add(p);
            }


            if (comboBoxProjets.Items.Count > 0) {
                int index = Properties.Settings.Default.SelectedProjectIndex;
                comboBoxProjets.SelectedIndex = index < comboBoxProjets.Items.Count ? Properties.Settings.Default.SelectedProjectIndex : 0;
                checkBoxTasks.Checked = Properties.Settings.Default.OnlyAcceptedTask;
                labelTask.Text = checkBoxTasks.Checked ? "All tasks" : "My open tasks";
                updateTasksComboBox();
            }
            buttonTime.Enabled = false;

            checkBoxSeconds.Checked = Properties.Settings.Default.ShowSeconds;
            Portal = Properties.Settings.Default.Portal + ".teamlab.com";
            labelPortal.Text = Portal;
            timer.State = TLTimer.STATE.Ready;

            int MainTaskId = Properties.Settings.Default.MainTaskTaskId;
            try {
                if (MainTaskId != -1) labelMainTask.Text = facade.GetTask(MainTaskId).title; else clearMainTask();
            } catch (TeamLabExpception) {
                clearMainTask();
            }
        }


        private void viewTime()
        {
            labelTime.Text = timer.GetTime(checkBoxSeconds.Checked);
            labelHours.Text = "Hours: " + timer.GetHours();
            this.Text = labelTime.Text + " - " + comboBoxTasks.SelectedItem;
            notifyIcon1.Text = labelTime.Text + " - " + comboBoxTasks.SelectedItem;
        }

        /// <summary>
        /// Project change requires update of its tasks.
        /// Except calling updateTasks() methods it does not do any request.
        /// It means list of projects is updated only if application started.
        /// </summary>
        /// <param name="projectSelectedIndex"></param>
        private void changeProject(int projectSelectedIndex)
        {

            comboBoxTasks.Items.Clear();
            comboBoxTasks.Text = "";
            if (comboBoxProjets.Items.Count > 0) {
                int index = projectSelectedIndex;
                if (projectSelectedIndex >= comboBoxProjets.Items.Count) {
                    index = 0;
                }
                updateTasksComboBox();
            }

        }


        /// <summary>
        /// It asks server for list of tasks for singed in user.
        /// </summary>
        private void updateTasksComboBox()
        {

            comboBoxTasks.Items.Clear();
            comboBoxTasks.ResetText();
            textBoxNote.Text = "";

            if (comboBoxProjets.Items.Count > 0) {
                int projectId = ((Project)comboBoxProjets.SelectedItem).id;

                try {
                    Task[] tasks = checkBoxTasks.Checked ? facade.GetAllTasks(projectId) : facade.GetMyOpenTaskByProject(projectId);
                    timerTasksUpdate.Stop();
                    labelPortal.ForeColor = System.Drawing.SystemColors.ControlText;
                    labelPortal.Text = Portal;
                    foreach (Task t in tasks) {
                        comboBoxTasks.Items.Add(t);
                    }
                } catch (ObjectReferenceException) {
                    MessageBox.Show("Timing task or its part has changed or deleted on the server. Please, check the task on the server and close this application.", "Machinations on server - TimeLab", MessageBoxButtons.OK);
                } catch (UnauthorizedException) {
                    Unauthorized();               
                } catch (ConnectionFailedException ex) {
                    labelPortal.ForeColor = System.Drawing.Color.Red;
                    labelPortal.Text = ex.Message;
                }

            }
        }


        /// <summary>
        /// New task restarts timer automatically.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stopTaskTimer();
            UpdateTaskTime(); //upload the latest time
            timer.Reset();
            timer.TaskId = ((Task)comboBoxTasks.SelectedItem).id;
            startTaskTimer();
            buttonTime.Enabled = true;
            buttonTime.Text = "Pause";
            viewTime();
            linkLabelSetMainTask.Enabled = true;
            textBoxNote.Text = "";

            if (timer.ProjectId == Properties.Settings.Default.MainTaskProjectId && timer.TaskId == Properties.Settings.Default.MainTaskTaskId) {
                buttonMainTask.Enabled = false;
            } else {
                if (isMainTaskSetted()) {
                    buttonMainTask.Enabled = true;
                }
            }

        }

        /// <summary>
        /// New project restarts timer automatically
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxProjets_SelectedIndexChanged(object sender, EventArgs e)
        {
            stopTaskTimer();
            UpdateTaskTime();
            buttonTime.Enabled = false;
            timer.Reset();
            timer.ProjectId = ((Project)comboBoxProjets.SelectedItem).id;
            viewTime();
            Properties.Settings.Default.SelectedProjectIndex = comboBoxProjets.SelectedIndex;
            Properties.Settings.Default.Save();
            updateTasksComboBox();
            linkLabelSetMainTask.Enabled = false;
        }

        /// <summary>
        /// This event is called every second if timer is not paused.
        /// It adds one second to the timer and view actual time on timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerTaskTimer_Tick(object sender, EventArgs e)
        {
            timer.AddOneSecond();
            viewTime();
        }

        /// <summary>
        /// It checks if the connection is OK depends purely on this Form.
        /// Probably not best way to do. At least it makes the code more readable.
        /// I was trying to avoid non-necessary server request because It would be called a lot.
        /// </summary>
        /// <returns></returns>
        private bool isConnection()
        {
            if (labelPortal.Text == Portal)
                return true;
            else
                return false;
        }


        private void checkBoxSeconds_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowSeconds = checkBoxSeconds.Checked;
            Properties.Settings.Default.Save();
            viewTime();
        }

        private void checkBoxTasks_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.OnlyAcceptedTask = checkBoxTasks.Checked;
            Properties.Settings.Default.Save();
            if (checkBoxTasks.Checked) {
                labelTask.Text = "All tasks";
            } else {
                labelTask.Text = "My open tasks";
            }
            updateTasksComboBox();
        }

        private void lockProjectAndTask()
        {
            comboBoxProjets.Enabled = false;
            comboBoxTasks.Enabled = false;
            checkBoxTasks.Enabled = false;
        }

        private void unlockProjectAndTask()
        {
            comboBoxProjets.Enabled = true;
            comboBoxTasks.Enabled = true;
            checkBoxTasks.Enabled = true;
        }

        private void startTaskTimer()
        {
            timerTaskTimer.Start();
            timerUpdateTaskTimeOnServer.Start();
            buttonTime.Enabled = true;
        }

        private void stopTaskTimer()
        {
            timerTaskTimer.Stop();
            timerUpdateTaskTimeOnServer.Stop(); ;
        }

        private void buttonTime_Click(object sender, EventArgs e)
        {
            if (buttonTime.Text == "Continue" || buttonTime.Text == "Start") {
                buttonTime.Text = "Pause";
                //lockProjectAndTask();
                timer.State = TLTimer.STATE.Running;
                startTaskTimer();
            } else {
                buttonTime.Text = "Continue";
                //unlockProjectAndTask();
                timer.State = TLTimer.STATE.Paused;
                stopTaskTimer();
                UpdateTaskTime();
            }
        }

        /// <summary>
        ///  Internet connection or power supply or OS are not persistent reliable.
        ///  This method just update time on server for every updateTime.Interaval for any case.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerUpdateTaskTimeOnServer_Tick(object sender, EventArgs e) //LOOK: maybe it can run nonstop???
        {
            UpdateTaskTime();
        }

        private void TimeTrackerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerTaskTimer.Stop();
            timerUpdateTaskTimeOnServer.Stop();

            if (timer.HasStarted()) {
                DialogResult dialogResult = System.Windows.Forms.DialogResult.No;
                if (!isConnection()) {
                    dialogResult = MessageBox.Show("Connection is down. Your the latest update time for this task is " + timer.LastUploadedHours,
                                                    "Quit - TimeLab", MessageBoxButtons.YesNo);
                } else {
                    dialogResult = MessageBox.Show("Do you really want to quit?", "Quit - TimeLab Timer",
                                                    MessageBoxButtons.YesNo);
                }

                if (dialogResult == System.Windows.Forms.DialogResult.No) {
                    e.Cancel = true;
                    if (buttonTime.Enabled == true && buttonTime.Text == "Pause") {
                        timerTaskTimer.Start();
                        timerUpdateTaskTimeOnServer.Start();
                    }
                }

            }


        }

        private void textBoxNote_TextChanged(object sender, EventArgs e)
        {
            timer.Note = textBoxNote.Text;
        }

        private void linkLabelAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form aboutForm = new AboutForm();
            aboutForm.ShowDialog(this);
        }

        private void labelPortal_MouseEnter(object sender, EventArgs e)
        {
            string msg;
            if (labelPortal.Text != Portal) {
                msg = "Either your Internet or server connection are down. Program will be trying to reestablish connection every 20 seconds.";
            } else {
                msg = "Seeing your portal that means your connection works fine :-)!";
            }
            toolTip.Show(msg, labelPortal);
        }

        private void timerTasksUpdate_Tick(object sender, EventArgs e)
        {
            if (timer.State == TLTimer.STATE.Ready) updateTasksComboBox();
        }

        private void linkLabelClearTime_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            facade.deleteTime(timer);
            viewTime();
            stopTaskTimer();
            buttonTime.Text = "Start";
        }

        private void linkLabelClearTime_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Cleared time will be also deleted on the server", (LinkLabel)sender);
        }

        private void selectProjectById(int projectId)
        {
            foreach (Object o in comboBoxProjets.Items) {
                if (((Project)o).id == projectId) {
                    comboBoxProjets.SelectedIndex = comboBoxProjets.Items.IndexOf(o);
                    return;
                }
            }
        }

        private void selectTaskById(int taskId)
        {
            foreach (Object o in comboBoxTasks.Items) {
                if (((Task)o).id == taskId) {
                    comboBoxTasks.SelectedIndex = comboBoxTasks.Items.IndexOf(o);
                    return;
                }
            }
        }

        private void buttonMainTask_Click(object sender, EventArgs e)
        {
            int projectId = Properties.Settings.Default.MainTaskProjectId;
            int taskId = Properties.Settings.Default.MainTaskTaskId;
            selectProjectById(projectId);
            selectTaskById(taskId);
            labelMainTask.Text = ((Task)comboBoxTasks.SelectedItem).title;
            timer.Reset();
            startTaskTimer();
            buttonMainTask.Enabled = false;
            buttonTime.Enabled = true;
        }

        private bool isMainTaskSetted()
        {
            return Properties.Settings.Default.MainTaskTaskId != -1;
        }

        private void setMainTask()
        {
            Properties.Settings.Default.MainTaskProjectId = ((Project)comboBoxProjets.SelectedItem).id;
            Properties.Settings.Default.MainTaskTaskId = ((Task)comboBoxTasks.SelectedItem).id;
            Properties.Settings.Default.Save();
            labelMainTask.Text = ((Task)comboBoxTasks.SelectedItem).title;
        }

        private void linkLabelSetMainTask_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            setMainTask();
            buttonMainTask.Enabled = false;
        }

        private void clearMainTask()
        {
            Properties.Settings.Default.MainTaskProjectId = -1;
            Properties.Settings.Default.MainTaskTaskId = -1;
            Properties.Settings.Default.Save();
            labelMainTask.Text = "There is no main task setted";
            buttonMainTask.Enabled = false;

        }

        private void linkLabelClearMainTask_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clearMainTask();
        }

        private void buttonMainTask_MouseHover(object sender, EventArgs e)
        {
            if (buttonMainTask.Enabled == true) {
                toolTip.Show("Click to switch to your main task", buttonMainTask);
            }
        }

        private void labelMainTask_MouseEnter(object sender, EventArgs e)
        {
            if (labelMainTask.Text != "There is no main task setted") {
                toolTip.Show("This is your main task. You should work on it!", labelMainTask);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = !this.Visible;
        }

        private void TimeTrackerForm_Load(object sender, EventArgs e)
        {
            notifyIcon1.Icon = this.Icon;
        }

        private void Unauthorized()
        {
            LoginForm loginForm = new LoginForm("Unathorized access. Did you change credentials on server?");
            DialogResult dialogResult = loginForm.ShowDialog();
            while (dialogResult == DialogResult.Retry) {
                dialogResult = loginForm.ShowDialog();
            }
        }

        private void UpdateTaskTime()
        {
            try {
                facade.UpdateTaskTime(timer);
                labelPortal.Text = Portal;
                labelPortal.ForeColor = System.Drawing.SystemColors.ControlText;
                timer.LastUploadedHours = timer.GetHours();
            } catch (ObjectReferenceException) {
                MessageBox.Show("Timing task or its part has changed or deleted on the server. Please, check the task on the server and close this application.", "Machinations on server - TimeLab", MessageBoxButtons.OK);
            } catch (TaskNotFoundException) {
                MessageBox.Show("Timing task or its part has changed or deleted on the server. Please, check the task on the server and close this application.", "Machinations on server - TimeLab", MessageBoxButtons.OK);
            } catch (UnauthorizedException) {
                Unauthorized();
            } catch (ConnectionFailedException) {
                labelPortal.ForeColor = System.Drawing.Color.Red;
                labelPortal.Text = "Last uploaded time is " + timer.LastUploadedHours + " hours.";
                timerUpdateTaskTimeOnServer.Start();
            }

        }
    }
}