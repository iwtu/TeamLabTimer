/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */

using System;
using System.Globalization;

using Debug = System.Diagnostics.Debug;

namespace TimeTracker
{
    public class MainTimer
    {
        public enum STATE 
        {
            Running,
            Paused,
            Ready
        }

        //private int m_minutes;
        private int m_seconds;

        public int TimeId { get; set; }
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public string MyId { get; set; }
        public string Note { get; set; }
        public STATE State { get; set; }
        public string LastUploadedHours { get; set; }

        private int Seconds
        {
            get { return m_seconds; }
            set
            {
                Debug.Assert(value >= 0, "Possible value is only greater then 0");
                m_seconds = value;
            }
        }

        public MainTimer(string myId)
        {
            this.MyId = myId;
            Reset();
        }

        public void AddOneSecond()
        {
            m_seconds++;
        }

        public string GetTime(bool withSeconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(Seconds);
            return withSeconds ? String.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds)
                : string.Format("{0:D2}:{1:D2}", t.Hours, t.Minutes);
        }

        public void Reset()
        {
            Seconds = 0;
            TimeId = -1;
            State = STATE.Ready;
            LastUploadedHours = GetHours();
        }

        public bool HasTimeId()
        {
            return TimeId != -1;
        }

        public bool HasStarted()
        {
            return Seconds > 0;
        }

        /// <summary>
        /// TeamLab.com measerues time as real with two decimal digits
        /// </summary>
        /// <returns>Spent time in hours with two decimal digits </returns>
        public string GetHours()
        {
            double hours = (float)Seconds / 3600;
            return Math.Round(hours, 2).ToString("G", CultureInfo.InvariantCulture);
        }

    }
}
