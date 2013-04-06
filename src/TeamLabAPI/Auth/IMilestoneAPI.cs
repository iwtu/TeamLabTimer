/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */

using System;
using System.Collections.Generic;
using TeamLab.Entities;

namespace TeamLab.Auth
{
    public interface IMilestoneAPI
    {
        JMilestone[] UpcomingMilestones();
        JMilestone[] OverdueMilestones();
        JMilestone GetMilestone(int id);
        JTask[] GetMilestoneTasks(int id);
        JMilestone[] MilestonesByMonth(int year, int month);
        JMilestone[] MilestonesByMonth(int year, int month, int day);
        JMilestone UpdateMilestone(int id, Dictionary<string, string> body);
        JMilestone DeleteMilestone(int id);
    }
}
