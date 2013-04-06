using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using TimeTracker.Exceptions;

using JObject = Newtonsoft.Json.Linq.JObject;

namespace REST
{
    public abstract class SynchronousRequest
    {

        /// <summary>
        /// This method sets header of HTTP request and returns HttpWebRequest
        /// </summary>
        /// <param name="url">Full (host + resource) URL address</param>
        /// <param name="method">REST method</param>
        /// <param name="headers">Additional HTTP headers</param>
        /// <returns>HttpWebRequest</returns>
        abstract protected HttpWebRequest SetHeader(string url, METHOD method, Dictionary<string, string> headers);

        protected string ConvertResponseToString(HttpWebResponse response)
        {
            string result = "Status code: " + (int)response.StatusCode + " " + response.StatusCode + "\r\n";

            foreach (string key in response.Headers.Keys)
            {
                result += string.Format("{0}: {1} \r\n", key, response.Headers[key]);
            }

            result += "\r\n";
            result += new StreamReader(response.GetResponseStream()).ReadToEnd();

            return result;
        }

        protected void SendBody(HttpWebRequest request, string requestBody)
        {
            if (requestBody.Length > 0)
            {
                using (Stream requestStream = request.GetRequestStream())
                using (StreamWriter writer = new StreamWriter(requestStream))
                {
                    writer.Write(requestBody);
                }
            }
        }
        protected string MakeRequestBody(Dictionary<string, string> bodyParams)
        {
            string body = "";
            foreach (KeyValuePair<string, string> kvp in bodyParams)
            {
                body += kvp.Key + "=" + kvp.Value + "&";
            }

            return body.Substring(0, body.Length - 1);
        }

        protected string GetResponse(string url, METHOD method, Dictionary<string, string> headers, Dictionary<string, string> bodyParams = null)
        {
            string responseAsString = "";
            HttpWebRequest request = SetHeader(url, method, headers);
            if (bodyParams != null)
            {
                SendBody(request, MakeRequestBody(bodyParams)); // CIIN: asynchronous response. 
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse(); //CIIN: may cause timeout (perhaps)
            responseAsString = ConvertResponseToString(response);

            return responseAsString;
        }
    }
}
