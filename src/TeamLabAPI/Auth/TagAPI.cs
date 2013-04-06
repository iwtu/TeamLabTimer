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
    public class TagAPI : AuthAPI, ITagAPI
    {
        /// <summary>
        /// Returns the detailed list of all projects with the specified tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public JProject[] ProjectByTag(string tag)
        {           
            string resource = String.Format("{0}/tag/{1}", API_PROJECT, tag);
            return execute<JProject[]>(resource, REST.METHOD.GET, null);
        }
    }    
}
