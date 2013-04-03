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
    public class AuthAPI : TeamLabAPI
    {
        public static string Token;

        protected override T execute<T>(string resource, REST.METHOD method, Dictionary<string, string> body) // CIIN: not many lines but still  duplicate code. Think about it if necessary
        {
            string jstring = request.GetAuthorizedJSONResponse(Host + resource, method, body, Token);
            return transform<T>(jstring);
        }       
    }    
}
