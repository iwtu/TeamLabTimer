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
using TeamLab.API.Entities;
using TeamLab.API.Auth;
using TeamLab.API.Unauth;
using TeamLab.Exceptions;
using System.Globalization;


namespace TeamLab.Control
{
    public enum STATE
    {
        Ready,
        Running,
        Paused
    }
    
    public class Facade
    {
        private ProjectAPI projectAPI = new ProjectAPI();
        private TaskAPI taskAPI = new TaskAPI();
        private TimeAPI timeAPI = new TimeAPI();                  
        private ProfileAPI profileAPI = new ProfileAPI();
        private UnauthAPI unauthAPI = new UnauthAPI();


        public void UpdateTaskTime(MainTimer timer)
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

        public void deleteTime(MainTimer timer)
        {
            if (timer.HasTimeId()) {
                timeAPI.DeleteTimeSpent(timer.TimeId);
            }
            timer.Reset();
        }

        private int StartTime(MainTimer timer)
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



        public void Authentificate(string portal, string userName, string password)
        {
            TeamLabAPI.Portal = portal;
            Dictionary<string, string> body = new Dictionary<string, string>();
            body.Add("userName", userName);
            body.Add("password", password);
            unauthAPI.Authenticate(body);
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