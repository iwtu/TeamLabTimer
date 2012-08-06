/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */

using System;

namespace TeamLab.Exceptions
{
    public abstract class TeamLabExpception : Exception
    {
        public TeamLabExpception() : base() { }
        public TeamLabExpception(string msg) : base(msg) { }
        public TeamLabExpception(string msg, Exception ex) : base(msg, ex) { }

    }


    public class WrongCredentialsException : TeamLabExpception
    {
        public override string Message
        {
            get
            {
                return "Invalid login or password";
            }
        }
    }

    public class WrongPortalException : TeamLabExpception
    {
        public override string Message
        {
            get
            {
                return "This portal probably do not exists.";
            }
        }
    }

    public class ConnectionFailedException : TeamLabExpception
    {
        public override string Message
        {
            get
            {
                return "Connection has failed.";
            }
        }
    }

    public class TaskNotFoundException : TeamLabExpception
    {
       
    }

    public class UnathorizedException : TeamLabExpception
    {
        public override string Message
        {
            get
            {
                return "Your password, login or portal has changed.";
            }
        }
    }

    public class ObjectReferenceException : TeamLabExpception
    {
        
    }
}
