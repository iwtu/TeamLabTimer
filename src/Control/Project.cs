/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */

using System;

namespace TeamLab.Control
{
    public class Project
    {
        public int id {get; set; }
        public string title {get; set; }
        public string description { get; set; }
        public int status { get; set; }

        public Project(int id, string title, string description, int status)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.status = status;
        }

        public override string ToString()
        {
            return title;
        }
    }
}
