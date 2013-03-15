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
    public class Task
    {
        public int id;
        public string title;
        public string description;
        public int status;

        public Task(int id, string title, string description, int status)
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
