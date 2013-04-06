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
    public interface ITeamAPI
    {
        JPerson[] ProjectTeam(int projectid);
        JPerson[] AddToTeam(int projectid, Dictionary<string, string> body);
        JPerson[] SetTeamSecurity(int projectid, Dictionary<string, string> body);
        JPerson[] RemoveFromTeam(int projectid, Dictionary<string, string> body);
    }
}
