using System;
using System.Collections.Generic;
using TeamLab.API.Entities;

namespace TeamLab.API.Auth
{
    public class MilestoneAPI : AuthAPI
    {
        /// <summary>
        /// Returns the list of all upcoming milestones within all portal projects
        /// </summary>
        /// <returns></returns>
        public JMilestone[] UpcomingMilestones()
        {            
            string resource = String.Format("{0}/milestone", API_PROJECT);
            return execute<JMilestone[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all overdue milestones in the portal projects
        /// </summary>
        /// <returns></returns>
        public JMilestone[] OverdueMilestones()
        {
            string resource = String.Format("{0}/milestone/late", API_PROJECT);
            return execute<JMilestone[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        ///  Returns the list with the detailed information about the milestone with the ID specified in the request 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JMilestone GetMilestone(int id)
        {
            string resource = String.Format("{0}/milestone/{1}", API_PROJECT, id);
            return execute<JMilestone>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all tasks within the milestone with the ID specified in the request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JTask[] GetMilestoneTasks(int id)
        {            
            string resource = String.Format("{0}/milestone/{1}/task", API_PROJECT, id);
            return execute<JTask[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all milestones due in the month specified in the request
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public JMilestone[] MilestonesByMonth(int year, int month)
        {         
            string resource = String.Format("{0}/milestone/{1}/{2}", API_PROJECT, year, month);
            return execute<JMilestone[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all milestones due on the date specified in the request
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public JMilestone[] MilestonesByMonth(int year, int month, int day)
        {         
            string resource = String.Format("{0}/milestone/{1}/{2}/{3}", API_PROJECT, year, month, day);
            return execute<JMilestone[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Updates the selected milestone changing the milestone parameters (title, deadline, status, etc.) specified in the request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JMilestone UpdateMilestone(int id, Dictionary<string, string> body)
        {         
            string resource = String.Format("{0}/milestone/{1}", API_PROJECT, id);
            return execute<JMilestone>(resource, REST.METHOD.PUT, body);
        }

        /// <summary>
        /// Deletes the milestone with the ID specified in the request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JMilestone DeleteMilestone(int id)
        {         
            string resource = String.Format("{0}/milestone/{1}", API_PROJECT, id);
            return execute<JMilestone>(resource, REST.METHOD.DELETE, null);
        }

    }
}
