/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */

using System;
using System.Collections.Generic;
using TeamLab.API.Entities;


namespace TeamLab.API.Auth
{
    public class TaskAPI : AuthAPI, ITaskAPI
    {

        /// <summary>
        /// Returns the list with the detailed information about all tasks for the current user
        /// </summary>
        /// <returns></returns>
        public JTask[] MyTaskWithDetails()
        {           
            string resource = String.Format("{0}/task/@self", API_PROJECT);
            return execute<JTask[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the detailed information about the task with the ID specified in the request
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public JTask GetTask(int taskid)
        {         
            string resource = String.Format("{0}/task/{1}", API_PROJECT, taskid);
            return execute<JTask>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all the tasks within the project with the ID specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public JTask[] Tasks(int projectid)
        {         
            string resource = String.Format("{0}/{1}/task", API_PROJECT, projectid);
            return execute<JTask[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list with the detailed information about the tasks for the current user with the status specified in the request
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public JTask[] MyTaskByStatus(string status)
        {         
            string resource = String.Format("{0}/task@self/{1}", API_PROJECT, status);
            return execute<JTask[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all tasks for the current user with the selected status in the project with the ID specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public JTask[] MyTaskByProjectAndStatus(int projectid, Status status)
        {         
            string resource = String.Format("{0}/{1}/task/@self/{3}", API_PROJECT, projectid, status);
            return execute<JTask[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        ///  Returns the list of all tasks in the project with the ID specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public JTask[] AllTask(int projectid)
        {         
            string resource = String.Format("{0}/{1}/task/@all", API_PROJECT, projectid);
            return execute<JTask[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all tasks with the selected status in the project with the ID specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public JTask[] TasksWithStatus(int projectid, Status status)
        {         
            string resource = String.Format("{0}/{1}/task/{3}", API_PROJECT, projectid, status);
            return execute<JTask[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Adds the task to the selected project with the parameters (responsible user ID, task description, deadline time, etc) specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="body"></param>
        public void AddTAsk(int projectid, Dictionary<string, string> body)
        {
            string resource = String.Format("{0}/{1}/task", API_PROJECT, projectid);
            execute<JVoid>(resource, REST.METHOD.POST, body);

        }

        /// <summary>
        /// Updates the selected task with the parameters (responsible user ID, task description, deadline time, etc) specified in the request
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JTask UpdateTask(int taskid, Dictionary<string, string> body)
        {         
            string resource = String.Format("{0}/task/{1}", API_PROJECT, taskid);
            return execute<JTask>(resource, REST.METHOD.PUT, body);
        }

        /// <summary>
        /// Updates the status of the task with the ID specified in the request
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JTask UpdateTaskStatus(int taskid, Dictionary<string, string> body)
        {         
            string resource = String.Format("{0}/task/{1}/status", API_PROJECT, taskid);
            return execute<JTask>(resource, REST.METHOD.PUT, body);
        }

    }
}
