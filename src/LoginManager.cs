using System;
using System.Collections.Generic;
using TeamLab;
using TeamLab.Auth;
using TeamLab.Entities;
using TeamLab.Unauth;


namespace TimeTracker
{
    class LoginManager : UnauthAPI, ILoginManager
    {        

        public void Authentificate(string portal, string userName, string password)
        {
            API.Portal = portal;
            var body = new Dictionary<string, string>()
            {
                {"userName", userName},
                {"password", password}
            };
            string resource = String.Format("{0}/authentication", API_VERSION);
            AuthAPI.Token = execute<AuthToken>(resource, REST.METHOD.POST, body).token;
        }
    }
}
