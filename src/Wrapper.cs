/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */

using System;
using System.Text;
using System.Collections.Generic;
using TeamLab.API;
using TeamLab.Exceptions;
using System.Globalization;

namespace TeamLab.Wrapper
{

    public class Project
    {
        public int id;
        public string title;
        public string description;
        public int status;

        public Project(int id, string title, string description, int status)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.status = status;
        }

        public override string ToString()
        {
            return title;
        }
    }

    public class Task
    {
        public int id;
        public string title;
        public string description;
        public int status;

        public Task(int id, string title, string description, int status)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.status = status;
        }

        public override string ToString()
        {
            return title;
        }
    }

    public class TLTimer
    {
        public enum STATE : int
        {
            Running,
            Paused,
            Ready
        }

        private int m_minutes;
        private int m_seconds;

        public int TimeId { get; set; }
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public string MyId { get; set; }
        public string Note { get; set; }
        public STATE State { get; set; }
        public string LastUploadedHours { get; set; }

        private int Hours { get; set; }
        private int Minutes
        {
            get { return m_minutes; }
            set
            {
                if (value < 0 || value > 59)
                    throw new ArgumentOutOfRangeException("Possible values are between 0 and 59 integers");
                else
                    m_minutes = value;
            }
        }
        private int Seconds
        {
            get { return m_seconds; }
            set
            {
                if (value < 0 || value > 59)
                    throw new ArgumentOutOfRangeException("Possible values are between 0 and 59 integers");
                else
                    m_seconds = value;
            }
        }

        public TLTimer(string myId)
        {
            this.MyId = myId;
            Reset();
        }

        public void AddOneSecond()
        {
            if (m_seconds == 59) {
                m_seconds = 0;
                if (m_minutes == 59) {
                    m_minutes = 0;
                    Hours++;
                } else {
                    m_minutes++;
                }
            } else {
                m_seconds++;
            }
        }

        public string GetTime(bool withSeconds)
        {
            StringBuilder hours = new StringBuilder(Hours.ToString());
            StringBuilder minutes = new StringBuilder(Minutes.ToString());
            StringBuilder seconds = new StringBuilder(Seconds.ToString());

            if (Minutes < 10) minutes.Insert(0, '0');
            if (Seconds < 10) seconds.Insert(0, '0');

            StringBuilder result = hours.Append(':').Append(minutes);
            if (withSeconds) {
                result = result.Append(':').Append(seconds);
            }

            return result.ToString();
        }

        public void Reset()
        {
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
            TimeId = -1;
            State = STATE.Ready;
            LastUploadedHours = GetHours();
        }

        public bool HasTimeId()
        {
            return TimeId != -1;
        }

        public bool HasStarted()
        {
            return Hours != 0 || Minutes != 0 || Seconds != 0;
        }

        /// <summary>
        /// TeamLab.com measerues time as real with two decimal digits
        /// </summary>
        /// <returns>Spent time in hours with two decimal digits </returns>
        public string GetHours()
        {
            double hours = Hours + ((float)Minutes / 60) + ((float)Seconds / 3600);
            return Math.Round(hours, 2).ToString("G", CultureInfo.InvariantCulture);
        }

    }

    public class Facade
    {
        private ProjectAPI projectAPI = new ProjectAPI();
        private TaskAPI taskAPI = new TaskAPI();
        private TimeAPI timeAPI = new TimeAPI();
        private MilestoneAPI milestoneAPI = new MilestoneAPI();
        private EventsAPI eventsAPI = new EventsAPI();
        private TeamAPI teamAPI = new TeamAPI();
        private TagAPI tagAPI = new TagAPI();
        private ProfileAPI profileAPI = new ProfileAPI();


        public void UpdateTaskTime(TLTimer timer)
        {
            // if it is something to update
            if (timer.LastUploadedHours != timer.GetHours()) {
                Dictionary<string, string> body = new Dictionary<string, string>();
                body.Add("note", timer.Note);
                body.Add("date", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                body.Add("personId", timer.MyId);
                body.Add("hours", timer.GetHours().ToString());


                if (timer.TimeId == -1) {
                    body.Add("projectId", Convert.ToString(timer.ProjectId));
                    timer.TimeId = (timeAPI.AddTaskTime(timer.TaskId, body)).id;
                } else {
                    timeAPI.UpdateTaskTime(timer.TimeId, body);
                }

            }
        }

        public void deleteTime(TLTimer timer)
        {
            if (timer.HasTimeId()) {
                timeAPI.DeleteTimeSpent(timer.TimeId);
            }
            timer.Reset();
        }

        private int StartTime(TLTimer timer)
        {
            Dictionary<string, string> body = new Dictionary<string, string>();
            body.Add("note", timer.Note);
            body.Add("date", DateTime.UtcNow.ToString());
            body.Add("personId", timer.MyId);
            body.Add("hours", timer.GetHours().ToString());
            body.Add("projectId", Convert.ToString(timer.ProjectId));
            JTime time = timeAPI.AddTaskTime(timer.TaskId, body);
            return time.id;
        }



        public static void Authentificate(string portal, string userName, string password)
        {
            TeamLabAPI.Portal = portal;
            Dictionary<string, string> body = new Dictionary<string, string>();
            body.Add("userName", userName);
            body.Add("password", password);
            AuthToken token = new AuthenticationAPI().GetToken(body);
        }

        public Project[] GetParticiapedProjects()
        {
            JProject[] jprojects = projectAPI.ParticipatedProjects();
            return tranformJProjectsToProjects(jprojects);
        }

        public Task[] GetAllTasks(int projectid)
        {
            JTask[] jtasks = taskAPI.AllTask(projectid);
            return transformJtasksToTasks(jtasks);
        }

        public Task[] GetMyOpenTaskByProject(int projectid)
        {
            JTask[] jtasks = taskAPI.MyTaskByProjectAndStatus(projectid, Status.open);
            return transformJtasksToTasks(jtasks);
        }

        private Project[] tranformJProjectsToProjects(JProject[] jprojects)
        {
            Project[] projects = new Project[jprojects.Length];
            for (int i = 0; i < jprojects.Length; i++) {
                JProject jp = jprojects[i];
                projects[i] = new Project(jp.id, jp.title, jp.description, jp.status);
            }
            return projects;
        }

        private Task[] transformJtasksToTasks(JTask[] jtasks)
        {
            Task[] tasks = new Task[jtasks.Length]; //CIIN: Make function 
            for (int i = 0; i < jtasks.Length; i++) {
                JTask jt = jtasks[i];
                tasks[i] = new Task(jt.id, jt.title, jt.description, jt.status);
            }
            return tasks;
        }

        public string GetMyId()
        {
            return profileAPI.MyProfile().id;
        }

        public Task GetTask(int taskid)
        {
            JTask jt = taskAPI.GetTask(taskid);
            return new Task(jt.id, jt.title, jt.description, jt.status);
        }

        public void changeProjectTitle(int id, string newTitle)
        {
            JProject project = projectAPI.ProjectByID(id);
            Dictionary<string, string> body = new Dictionary<string, string>();
            body.Add("title", newTitle);
            body.Add("description", project.description);
            body.Add("responsibleId", project.responsible.id.ToString());
            body.Add("tags", "");
            body.Add("private", project.isPrivate.ToString());
            body.Add("status", project.status.ToString());

            projectAPI.UpdateProject(id, body);
        }

    }

}