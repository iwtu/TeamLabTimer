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
    public interface IProfileAPI
    {
        JProfile[] AllProfiles();
        JProfile MyProfile();
        JProfile SpecificProfile(string username);
        JProfile[] SearchUsers(string query);
        JProfile AddNewUser(Dictionary<string, string> body);
        JProfile SetUserContacts(string userid, Dictionary<string, string> body);
        JProfile UpdateUser(string userid, Dictionary<string, string> body);
        JProfile DeleteUser(string userid);
        JProfile DeleteUserPhoto(string userid);
    }
}
