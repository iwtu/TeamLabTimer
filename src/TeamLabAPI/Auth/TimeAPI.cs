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
    public class TimeAPI : AuthAPI, ITimeAPI
    {

        /// <summary>
        /// Returns the time spent on the task with the ID specified in the request
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public JTime[] GetTimeSpent(int taskid)
        {            
            string resource = String.Format("{0}/task/{1}/time", API_PROJECT, taskid);
            return execute<JTime[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Adds the time to the selected task with the time parameters specified in the request
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JTime AddTaskTime(int taskid, Dictionary<string, string> body)
        {         
            string resource = String.Format("{0}/task/{1}/time", API_PROJECT, taskid);
            return execute<JTime>(resource, REST.METHOD.POST, body);
        }

        /// <summary>
        ///  Updates the time for the selected task with the time parameters specified in the request
        /// </summary>
        /// <param name="timeid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JTime UpdateTaskTime(int timeid, Dictionary<string, string> body)
        {         
            string resource = String.Format("{0}/time/{1}", API_PROJECT, timeid);
            return execute<JTime>(resource, REST.METHOD.PUT, body);
        }

        /// <summary>
        /// Deletes the time from the task with the ID specified in the request
        /// </summary>
        /// <param name="timeid"></param>
        /// <returns></returns>
        public JTime DeleteTimeSpent(int timeid)
        {         
            string resource = String.Format("{0}/time/{1}", API_PROJECT, timeid);
            return execute<JTime>(resource, REST.METHOD.DELETE, null);
        }

    }
}
