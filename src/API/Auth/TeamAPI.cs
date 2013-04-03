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
    public class TeamAPI : AuthAPI, ITeamAPI
    {
        /// <summary>
        /// Returns the list of all users participating in the project with the ID specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public JPerson[] ProjectTeam(int projectid)
        {            
            string resource = String.Format("{0}/{1}/team", API_PROJECT, projectid);
            return execute<JPerson[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Adds the user with the ID specified in the request to the selected project team
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JPerson[] AddToTeam(int projectid, Dictionary<string, string> body)
        {         
            string resource = String.Format("{0}/{1}/team", API_PROJECT, projectid);
            return execute<JPerson[]>(resource, REST.METHOD.POST, body);
        }

        /// <summary>
        /// Sets the security rights for the user or users with the IDs specified in the request within the selected project
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JPerson[] SetTeamSecurity(int projectid, Dictionary<string, string> body)
        {         
            string resource = String.Format("{0}/{1}/team/security", API_PROJECT, projectid);
            return execute<JPerson[]>(resource, REST.METHOD.POST, body);
        }

        /// <summary>
        ///  Removes the user with the ID specified in the request from the selected project team
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JPerson[] RemoveFromTeam(int projectid, Dictionary<string, string> body)
        {         
            string resource = String.Format("{0}/{1}/team", API_PROJECT, projectid);
            return execute<JPerson[]>(resource, REST.METHOD.DELETE, body);
        }
    }
}
