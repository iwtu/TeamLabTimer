/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */

using System;
using System.Collections.Generic;
using TeamLab.API.Entities;

namespace TeamLab.API.Auth
{
    interface IMilestoneAPI
    {
        public JMilestone[] UpcomingMilestones();
        public JMilestone[] OverdueMilestones();
        public JMilestone GetMilestone(int id);
        public JTask[] GetMilestoneTasks(int id);
        public JMilestone[] MilestonesByMonth(int year, int month);
        public JMilestone[] MilestonesByMonth(int year, int month, int day);
        public JMilestone UpdateMilestone(int id, Dictionary<string, string> body);
        public JMilestone DeleteMilestone(int id);
    }
}
