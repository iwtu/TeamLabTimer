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
    interface IProjectAPI
    {
        public JProject[] Projects();
        public JProject ProjectByID(int id);
        public JProject[] ParticipatedProjects();
        public JProject[] FollowedProjects();
        public JProject[] ProjectByStatus(string status);
        public JTime[] ProjectTimeSpent(int id);
        public JMilestone[] GetMilestonesByProjectID(int id);
        public JProject[] SearchAllProjects(string query);
        JFoundedProjets[] SearchProject(int id, string query);
        public JMilestone[] GetMilestonesByProjectIDAndMilestoneStatus(int id, string status);
        public JProject CreateProject(Dictionary<string, string> body);
        JProject RequestProject(Dictionary<string, string> body);
        public JMilestone AddMilestone(int id, Dictionary<string, string> body);
        public JProject UpdateProject(int id, Dictionary<string, string> body);
        public JProject UpdateProjectTags(int id, Dictionary<string, string> body);
        public JProject RequestProjectUpdate(int id, Dictionary<string, string> body);
        public JProject DeleteProject(int id);
        public JProject requestsProjectRemoval(int id);
        public void DeleteTask(int taskid);
    }
}
