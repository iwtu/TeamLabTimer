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
    public class JProject : JEntity
    {
        public bool canEdit;
        public JSecurity security = new JSecurity();
        public int projectFolder;
        public int status;
        public bool isPrivate;

        public class JSecurity
        {
            public bool canCreateMessage;
            public bool canCreateMilestone;
            public bool canCreateTask;
            public bool canEditTeam;
            public bool canReadFiles;
            public bool canReadMilestones;
            public bool canReadMessages;
            public bool canReadTasks;
        }
    }
}
