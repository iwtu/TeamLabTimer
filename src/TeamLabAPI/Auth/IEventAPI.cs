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
    public interface IEventAPI
    {        
        JEvents GetEvent(int eventid);
        JEvents[] Events(int projectid);
    }
}
