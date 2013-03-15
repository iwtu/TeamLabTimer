using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using TeamLab.Exceptions;

using JObject = Newtonsoft.Json.Linq.JObject;

namespace TeamLab.REST
{
    public class TeamLabRequest : SynchronousRequest
    {

        private enum EXTENSION
        {
            json,
            xml
        };

        protected override HttpWebRequest SetHeader(string url, METHOD method, Dictionary<string, string> headers)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "application/json";
            request.Method = method.ToString();
            request.Timeout = 5000;
            System.Net.ServicePointManager.Expect100Continue = false; //to prevent from adding ExpectContinue: 100 to Header

            if (headers != null && headers.Count > 0)
            {
                foreach (KeyValuePair<string, string> pair in headers)
                    request.Headers.Add(pair.Key, pair.Value);
            }

            return request;
        }

        private string GetTeamLabResponse(string url, EXTENSION ext, METHOD method, Dictionary<string, string> headers, Dictionary<string, string> bodyParams)
        {

            try
            {
                return GetResponse(url + "." + ext.ToString(), method, headers, bodyParams);
            }
            catch (WebException ex)
            {

                if (ex.Message == "The remote server returned an error: (401) Unauthorized.")
                {
                    throw new UnauthorizedException();
                }

                string msgFromServer = null;
                try
                {
                    var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(resp);
                    msgFromServer = obj.error.message;
                }
                catch (NullReferenceException)
                {
                    throw new ConnectionFailedException();
                }

                switch (msgFromServer)
                {
                    case "Invalid username or password.":
                        throw new WrongCredentialsException();
                    case "Could not resolve current tenant :-(.":
                        throw new WrongPortalException();
                    case "Not found": //Task was not found on the server
                        throw new TaskNotFoundException();
                    case "Object reference not set to an instance of an object.":
                        throw new ObjectReferenceException();
                    default:
                        throw ex;

                }

            }

        }

        private Dictionary<string, string> GetAuthorizationHeaders(string authorizationToken)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", authorizationToken);
            return headers;
        }

        public string GetAuthorizedJSONResponse(string url, METHOD method, Dictionary<string, string> bodyParams, string authorizationToken)
        {
            return GetTeamLabResponse(url, EXTENSION.json, method, GetAuthorizationHeaders(authorizationToken), bodyParams);
        }

        public string GetAuthorizedXMLResponse(string url, METHOD method, Dictionary<string, string> bodyParams, string authorizationToken)
        {
            return GetTeamLabResponse(url, EXTENSION.xml, method, GetAuthorizationHeaders(authorizationToken), bodyParams);
        }

        public string GetJSONResponse(string url, METHOD method, Dictionary<string, string> bodyParams)
        {
            return GetTeamLabResponse(url, EXTENSION.json, method, null, bodyParams);
        }

        public string GetXMLResponse(string url, METHOD method, Dictionary<string, string> bodyParams)
        {
            return GetTeamLabResponse(url, EXTENSION.xml, method, null, bodyParams);
        }
    }
}
