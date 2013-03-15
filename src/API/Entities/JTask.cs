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
    public class JTask : JEntity
    {
        public string deadline;
        public int priority;
        public int milestoneId;
        public int status;
        public bool canEdit;
        public bool isExpired;
        public JProjectOwner projectOwner = new JProjectOwner();

    }
}
