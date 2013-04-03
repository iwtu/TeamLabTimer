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
    public class EventsAPI : AuthAPI, IEventAPI
    {

        /// <summary>
        ///  Returns the detailed information about the event with the ID specified in the request within the project
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public JEvents GetEvent(int eventid)
        {
            string resource = String.Format("{0}/event/{1}", API_PROJECT, eventid);
            return execute<JEvents>(resource, REST.METHOD.GET, null);

        }

        /// <summary>
        /// Returns the list of events within the project with the ID specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public JEvents[] Events(int projectid)
        {            
            string resource = String.Format("{0}/{1}/event", API_PROJECT, projectid);
            return execute<JEvents[]>(resource, REST.METHOD.GET, null);


        }

    }
}
