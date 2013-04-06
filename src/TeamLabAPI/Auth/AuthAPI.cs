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
    public class AuthAPI : API, IAPI
    {
        public static string Token;
        
        public T execute<T>(string resource, REST.METHOD method, Dictionary<string, string> body)
        {
            string jstring = request.GetAuthorizedJSONResponse(Host + resource, method, body, Token);
            return transform<T>(jstring);
        }       
    }    
}
