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
    public interface IProjectAPI
    {
        JProject[] Projects();
        JProject ProjectByID(int id);
        JProject[] ParticipatedProjects();
        JProject[] FollowedProjects();
        JProject[] ProjectByStatus(string status);
        JTime[] ProjectTimeSpent(int id);
        JMilestone[] GetMilestonesByProjectID(int id);
        JProject[] SearchAllProjects(string query);
        JFoundedProjets[] SearchProject(int id, string query);
        JMilestone[] GetMilestonesByProjectIDAndMilestoneStatus(int id, string status);
        JProject CreateProject(Dictionary<string, string> body);
        JProject RequestProject(Dictionary<string, string> body);
        JMilestone AddMilestone(int id, Dictionary<string, string> body);
        JProject UpdateProject(int id, Dictionary<string, string> body);
        JProject UpdateProjectTags(int id, Dictionary<string, string> body);
        JProject RequestProjectUpdate(int id, Dictionary<string, string> body);
        JProject DeleteProject(int id);
        JProject requestsProjectRemoval(int id);
        void DeleteTask(int taskid);
    }
}
