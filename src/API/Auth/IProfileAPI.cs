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
    interface IProfileAPI
    {
        public JProfile[] AllProfiles();
        public JProfile MyProfile();
        public JProfile SpecificProfile(string username);
        public JProfile[] SearchUsers(string query);
        public JProfile AddNewUser(Dictionary<string, string> body);
        public JProfile SetUserContacts(string userid, Dictionary<string, string> body);
        public JProfile UpdateUser(string userid, Dictionary<string, string> body);
        public JProfile DeleteUser(string userid);
        public JProfile DeleteUserPhoto(string userid);
    }
}
