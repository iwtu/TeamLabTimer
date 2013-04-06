/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */

using System;
using System.Collections.Generic;
using TeamLab.Entities;


namespace TeamLab.Auth
{
    public interface ITaskAPI
    {
        JTask[] MyTaskWithDetails();
        JTask GetTask(int taskid);
        JTask[] Tasks(int projectid);
        JTask[] MyTaskByStatus(string status);
        JTask[] MyTaskByProjectAndStatus(int projectid, Status status);
        JTask[] AllTask(int projectid);
        JTask[] TasksWithStatus(int projectid, Status status);
        void AddTAsk(int projectid, Dictionary<string, string> body);
        JTask UpdateTask(int taskid, Dictionary<string, string> body);
        JTask UpdateTaskStatus(int taskid, Dictionary<string, string> body);
    }
}
