/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */

using System;

namespace TeamLab.Entities
{
    public abstract class JEntity : JAncestor
    {
        public int id;
        public string title;
        public string description;
        public JPerson responsible = new JPerson();
        public JPerson updatedBy = new JPerson();
        public string created;
        public JPerson createdBy = new JPerson();
        public string updated;

        public override string ToString()
        {
            return title;
        }
    }
}
