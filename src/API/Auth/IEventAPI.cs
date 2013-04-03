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
    interface IEventAPI
    {        
        public JEvents GetEvent(int eventid);
        public JEvents[] Events(int projectid);
    }
}
