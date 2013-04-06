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
    public class JProfile : JAncestor
    {
        public class Contact
        {
            public string type;
            public string value;
        }

        public class Group
        {
            public string id;
            public string manager;
        }

        public string id;
        public string userName;
        public string firstName;
        public string lastName;
        public string email;
        public string birthday;
        public string sex;
        public int status;
        public string terminated;
        public string department;
        public string workFrom;
        public string location;
        public string notes;
        public string title;
        Contact[] contacts;
        Group[] groups;
        public string avatarMedium;
        public string avatar;
        public string avatarSmall;
    }
}
