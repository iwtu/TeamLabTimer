/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */

using System;

namespace TeamLab.API.Entities
{
    public class JTime : JAncestor
    {
        public int id;
        public string date;
        public double hours;
        public string note;
        public int relatedProject;
        public int relatedTask;
        public JPerson createdBy = new JPerson();
    }
}
