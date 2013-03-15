using System;
using System.Collections.Generic;
using TeamLab.API.Entities;

namespace TeamLab.API.Auth
{
    public class TagAPI : AuthAPI
    {
        /// <summary>
        /// Returns the detailed list of all projects with the specified tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        JProject[] ProjectByTag(string tag)
        {           
            string resource = String.Format("{0}/tag/{1}", API_PROJECT, tag);
            return execute<JProject[]>(resource, REST.METHOD.GET, null);
        }
    }    
}
