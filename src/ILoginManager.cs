﻿/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */



namespace TimeTracker
{
    interface ILoginManager
    {
        void Authentificate(string portal, string userName, string password);
    }
}
