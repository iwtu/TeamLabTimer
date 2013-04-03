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
    interface ITaskAPI
    {
        public JTask[] MyTaskWithDetails();
        public JTask GetTask(int taskid);
        public JTask[] Tasks(int projectid);
        public JTask[] MyTaskByStatus(string status);
        public JTask[] MyTaskByProjectAndStatus(int projectid, Status status);
        public JTask[] AllTask(int projectid);
        public JTask[] TasksWithStatus(int projectid, Status status);
        public void AddTAsk(int projectid, Dictionary<string, string> body);
        JTask UpdateTask(int taskid, Dictionary<string, string> body);
        public JTask UpdateTaskStatus(int taskid, Dictionary<string, string> body);
    }
}
