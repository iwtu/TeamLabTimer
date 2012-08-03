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
        public enum TYPE : int
        {
            CredentialsOrPortal,
            RequestTimeRanOut,
            Connection,
            DeleteTimerOnServer
        }

        public TYPE Type { get; protected set; }
        
        public TeamLabExpception() : base() { }
        public TeamLabExpception(string msg) : base(msg) { }
        public TeamLabExpception(string msg, Exception ex) : base(msg, ex) { }
           
    }

    public class CredentialsOrPortalException : TeamLabExpception
    {        
        
        public CredentialsOrPortalException()
        {
            Type = TYPE.CredentialsOrPortal;
        }

        public override string Message
        {
            get
            {
                return "Entered credentials or portal are inncorrect.";
            }
        }
    }

    // FIXME: This exception is never catched
    // FIX: Catch TeamLabException instead
    public class RequestTimeRanOutException : TeamLabExpception 
    { 
        public RequestTimeRanOutException()
        {
            Type = TYPE.RequestTimeRanOut;
        }
        
        public override string Message
        {
            get { return "Timeout for server response ran out."; }
        }
    }

    public class ConnectionException : TeamLabExpception
    {
        public ConnectionException()
        {
            Type = TYPE.Connection;
        }
        
        public override string Message
        {
            get
            {
                return "Connection failed.";
            }
        }
    }

    /// <summary>
    /// This exception is called whenever someone delete processing timer on the server via web interface.
    /// </summary>
    public class DeleteTimerOnServerException : TeamLabExpception
    {
        
        public DeleteTimerOnServerException()
        {
            Type = TYPE.DeleteTimerOnServer;
        }
        
        public override string Message
        {
            get
            {
                return "Timer being in the progress was deleted on the server.";
            }
        }
    }
    
}
