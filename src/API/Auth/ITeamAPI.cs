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
    interface ITeamAPI
    {
        public JPerson[] ProjectTeam(int projectid);
        public JPerson[] AddToTeam(int projectid, Dictionary<string, string> body);
        public JPerson[] SetTeamSecurity(int projectid, Dictionary<string, string> body);
        public JPerson[] RemoveFromTeam(int projectid, Dictionary<string, string> body);
    }
}
