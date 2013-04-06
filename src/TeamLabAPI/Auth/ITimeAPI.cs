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
    public interface ITimeAPI
    {
        JTime[] GetTimeSpent(int taskid);
        JTime AddTaskTime(int taskid, Dictionary<string, string> body);
        JTime UpdateTaskTime(int timeid, Dictionary<string, string> body);
        JTime DeleteTimeSpent(int timeid);
    }
}
