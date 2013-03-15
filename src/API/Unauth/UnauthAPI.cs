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
using TeamLab.API.Auth;


namespace TeamLab.API.Unauth
{
    public class UnauthAPI : TeamLabAPI
    {

        protected override T execute<T>(string resource, REST.METHOD method, Dictionary<string, string> body)
        {
            string jstring = request.GetJSONResponse(Host + resource, method, body);
            return transform<T>(jstring);
        }

        public void Authenticate(Dictionary<string, string> body)
        {
            string resource = String.Format("{0}/authentication", API_VERSION);
            AuthAPI.Token = execute<AuthToken>(resource, REST.METHOD.POST, body).token;
        }
    }
}
