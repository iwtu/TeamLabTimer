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
using TeamLab;
using TeamLab.Entities;
using TeamLab.Auth;
using TeamLab.Unauth;
using TimeTracker.Exceptions;
using System.Globalization;


namespace TimeTracker
{
    public class TLFacadeAPI : IFacadeAPI
    {
        private readonly IProjectAPI projectAPI; // = new ProjectAPI();
        private readonly ITaskAPI taskAPI;// = new TaskAPI();
        private readonly ITimeAPI timeAPI; // = new TimeAPI();
        private readonly IProfileAPI profileAPI;// = new ProfileAPI();        

        public TLFacadeAPI(IProjectAPI projectAPI, ITaskAPI taskAPI, ITimeAPI timeAPI, IProfileAPI profileAPI)
        {
            this.projectAPI = projectAPI;
            this.taskAPI = taskAPI;
            this.timeAPI = timeAPI;
            this.profileAPI = profileAPI;
        }


        public void UpdateTaskTime(MainTimer timer)
        {            
            if (timer.LastUploadedHours == timer.GetHours()) return;
            // if it is something to update
            var body = new Dictionary<string, string>
            {
                {"note", timer.Note},
                {"date", DateTime.Now.ToString(CultureInfo.InvariantCulture)},
                {"personId", timer.MyId},
                {"hours", timer.GetHours()}
            };


            if (timer.TimeId == -1)
            {
                body.Add("projectId", Convert.ToString(timer.ProjectId));
                timer.TimeId = (timeAPI.AddTaskTime(timer.TaskId, body)).id;
            }
            else
            {
                timeAPI.UpdateTaskTime(timer.TimeId, body);
            }
        }

        public void DeleteTime(MainTimer timer)
        {
            if (timer.HasTimeId())
            {
                timeAPI.DeleteTimeSpent(timer.TimeId);
            }
            timer.Reset();
        }

        private int StartTime(MainTimer timer)
        {
            var body = new Dictionary<string, string>()
            {
                {"note", timer.Note},
                {"date", DateTime.UtcNow.ToString()},
                {"personId", timer.MyId},
                {"hours", timer.GetHours().ToString()},
                {"projectId", Convert.ToString(timer.ProjectId)}
            };
            JTime time = timeAPI.AddTaskTime(timer.TaskId, body);
            return time.id;
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
            for (int i = 0; i < jprojects.Length; i++)
            {
                JProject jp = jprojects[i];
                projects[i] = new Project(jp.id, jp.title, jp.description, jp.status);
            }
            return projects;
        }

        private Task[] transformJtasksToTasks(JTask[] jtasks)
        {
            Task[] tasks = new Task[jtasks.Length]; //CIIN: Make function 
            for (int i = 0; i < jtasks.Length; i++)
            {
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
            var body = new Dictionary<string, string>()
            {
                {"title", newTitle},
                {"description", project.description},
                {"responsibleId", project.responsible.id.ToString()},
                {"tags", ""},
                {"private", project.isPrivate.ToString()},
                {"status", project.status.ToString()}
            };
            projectAPI.UpdateProject(id, body);
        }

    }

}