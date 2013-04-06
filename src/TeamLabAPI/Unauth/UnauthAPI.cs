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
using TeamLab.Auth;


namespace TeamLab.Unauth
{
    public class UnauthAPI : API, IAPI
    {        
        public T execute<T>(string resource, REST.METHOD method, Dictionary<string, string> body)
        {
            string jstring = request.GetJSONResponse(Host + resource, method, body);
            return transform<T>(jstring);
        }
        
    }
}
