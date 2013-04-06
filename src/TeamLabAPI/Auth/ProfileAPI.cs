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
    public class ProfileAPI : AuthAPI, IProfileAPI
    {
        /// <summary>
        /// Returns the list of profiles for all portal users
        /// This method returns a partial profile. Use more specific method to get full profile 
        /// </summary>
        /// <returns></returns>
        public JProfile[] AllProfiles()
        {           
            string resource = String.Format("{0}/people", API_VERSION);
            return execute<JProfile[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the detailed information about the current user profile
        /// </summary>
        /// <returns></returns>
        public JProfile MyProfile()
        {         
            string resource = String.Format("{0}/people/@self", API_VERSION);
            return execute<JProfile>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the detailed information about the profile of the user with the ID specified in the request
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public JProfile SpecificProfile(string username)
        {         
            string resource = String.Format("{0}/people/{1}", API_VERSION, username);
            return execute<JProfile>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of profiles for all portal users matching the search query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public JProfile[] SearchUsers(string query)
        {         
            string resource = String.Format("{0}/people/@search/{1}", API_VERSION, query);
            return execute<JProfile[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Adds a new portal user with the first and last name, email address and several optional parameters specified in the request
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public JProfile AddNewUser(Dictionary<string, string> body)
        {         
            string resource = String.Format("{0}/people", API_VERSION);
            return execute<JProfile>(resource, REST.METHOD.POST, body);
        }

        /// <summary>
        /// Updates the specified user contact information changing the data present on the portal for the sent data
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JProfile SetUserContacts(string userid, Dictionary<string, string> body)
        {
            string resource = String.Format("{0}/peoople/{1}/contacts", API_VERSION, userid);
            return execute<JProfile>(resource, REST.METHOD.POST, body);
        }

        /// <summary>
        /// Updates the data for the selected portal user with the first and last name, email address and/or optional parameters specified in the request
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JProfile UpdateUser(string userid, Dictionary<string, string> body)
        {
            string resource = String.Format("{0}/peoople/{1}", API_VERSION, userid);
            return execute<JProfile>(resource, REST.METHOD.PUT, body);
        }

        // CIID:
        //public JProfile UpdateUserPhoto(string userid)
        //{
        //    string resource = API_VERSION + "/people/" + username;
        //    return execute<JProfile>(resource, REST.METHOD.GET, null);
        //}

        // CIID:
        //public JProfile UpdateUserContacts()
        //{
        //    string resource = API_VERSION + "/people/" + username;
        //    return execute<JProfile>(resource, REST.METHOD.GET, null);
        //}

        /// <summary>
        /// Deletes the user with the ID specified in the request from the portal
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public JProfile DeleteUser(string userid)
        {         
            string resource = String.Format("{0}/people/{1}", API_VERSION, userid);
            return execute<JProfile>(resource, REST.METHOD.DELETE, null);
        }

        /// <summary>
        /// Deletes the photo of the user with the ID specified in the request
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public JProfile DeleteUserPhoto(string userid)
        {         
            string resource = String.Format("{0}/people/{1}/photo", API_VERSION, userid);
            return execute<JProfile>(resource, REST.METHOD.DELETE, null);
        }

        // CIID:
        //public JProfile DeleteUserContacts()
        //{
        //    string resource = API_VERSION + "/people/";
        //    return execute<JProfile>(resource, REST.METHOD.GET, null);
        //}

    }
}
