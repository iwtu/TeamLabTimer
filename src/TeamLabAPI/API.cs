/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */


using System;
using System.Collections.Generic;
using Newtonsoft.Json; //for JsonConvert and deserialize string in JSON format
using TeamLab.Auth;
using TeamLab.Entities;

using JObject = Newtonsoft.Json.Linq.JObject;


namespace TeamLab
{
    public abstract class API
    {
        private const string PROTOCOL = "http";
        private const string SERVER = "teamlab.com";
        public const string API_VERSION = "api/1.0";
        public const string API_PROJECT = API_VERSION + "/project";

        protected static string Host;
        private static string portal;
        public static string Portal
        {
            get
            {
                return portal;
            }
            set
            {
                portal = value;                
                Host = String.Format("{0}://{1}.{2}/", PROTOCOL, portal, SERVER);
            }
        }

        protected REST.TeamLabRequest request = new REST.TeamLabRequest();
        
        protected API() { }

        public API(string portal)
        {
            Portal = portal;            
        }        

        protected T transform<T>(string jstring)
        {
            string[] wholeResponse = jstring.Split('\n');
            string response = wholeResponse[wholeResponse.Length - 1];
            checkResponse(response); // JObject.Parse throws exception in case of error message in response
            JObject o = JObject.Parse(response);
            return JsonConvert.DeserializeObject<T>(o.SelectToken("response").ToString());
        }

        protected bool checkResponse(string response)
        {
            return true; // CIIN
        }        
                
    }

}






