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
    public class ProjectAPI : AuthAPI, IProjectAPI 
    {
        /// <summary>
        ///  Deletes the time from the task with the ID specified in the request
        /// </summary>
        /// <returns></returns>
        public JProject[] Projects()
        {
            string resource = API_PROJECT + "";
            return execute<JProject[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the detailed information about the project with ID specified in the request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JProject ProjectByID(int id)
        {         
            string resource = String.Format("{0}/{1}", API_PROJECT, id);
            return execute<JProject>(resource, REST.METHOD.GET, null);
        }


        /// <summary>
        ///  Returns the list of all projects in which the current user participates
        /// </summary>
        /// <returns></returns>
        public JProject[] ParticipatedProjects()
        {         
            string resource = String.Format("{0}/@self", API_PROJECT);
            return execute<JProject[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// projects
        /// </summary>
        /// <returns></returns>
        public JProject[] FollowedProjects()
        {         
            string resource = String.Format("{0}/@follow", API_PROJECT);
            return execute<JProject[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all projects with the status specified in the request
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public JProject[] ProjectByStatus(string status)
        {         
            string resource = String.Format("{0}/{1}", API_PROJECT, status);
            return execute<JProject[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        ///  Returns the detailed information about the time spent on the project with the ID specified in the request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JTime[] ProjectTimeSpent(int id)
        {         
            string resource = String.Format("{0}/{1}/time", API_PROJECT, id);
            return execute<JTime[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all the milestones within the project with the ID specified in the request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JMilestone[] GetMilestonesByProjectID(int id)
        {         
            string resource = String.Format("{0}/{1}/milestone", API_PROJECT, id);
            return execute<JMilestone[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        ///  Returns the list of all projects matching the query specified in the request
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public JProject[] SearchAllProjects(string query)
        {         
            string resource = String.Format("{0}/@search/{1}", API_PROJECT, query);
            return execute<JProject[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        ///  Returns the search results for the project containing the words/phrases matching the query specified in the request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        JFoundedProjets[] SearchProject(int id, string query)
        {         
            string resource = String.Format("{0}/{1}/@search/{2}", API_PROJECT, id, query);
            return execute<JFoundedProjets[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        ///  Returns the list of all the milestones with the selected status within the project with the ID specified in the request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public JMilestone[] GetMilestonesByProjectIDAndMilestoneStatus(int id, string status)
        {         
            string resource = String.Format("{0}/{1}/milestone/{2}", API_PROJECT, id, status);
            return execute<JMilestone[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Creates a new project using all the necessary (title, description, responsible ID, etc) and some optional parameters specified in the request
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public JProject CreateProject(Dictionary<string, string> body)
        {
            string resource = API_PROJECT;
            return execute<JProject>(resource, REST.METHOD.POST, body);
        }

        /// <summary>
        /// Returns the list of the portal projects matching the project title, description, responsible ID or tags specified in the request
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        JProject RequestProject(Dictionary<string, string> body)
        {         
            string resource = String.Format("{0}/request", API_PROJECT);
            return execute<JProject>(resource, REST.METHOD.POST, body);
        }

        /// <summary>
        /// Creates a new milestone using the parameters (project ID, milestone title, deadline, etc) specified in the request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JMilestone AddMilestone(int id, Dictionary<string, string> body)
        {
            string resource = String.Format("{0}/{1}/milestone", API_PROJECT, id);
            return execute<JMilestone>(resource, REST.METHOD.POST, body);
        }

        /// <summary>
        /// Updates the existing project information using all the parameters (project ID, title, description, responsible ID, etc) specified in the request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JProject UpdateProject(int id, Dictionary<string, string> body)
        {         
            string resource = String.Format("{0}/{1}", API_PROJECT, id);
            return execute<JProject>(resource, REST.METHOD.PUT, body);
        }

        /// <summary>
        ///  Updates the tags for the project with the selected project ID with the tags specified in the request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JProject UpdateProjectTags(int id, Dictionary<string, string> body)
        {         
            string resource = String.Format("{0}/{1}/tag", API_PROJECT);
            return execute<JProject>(resource, REST.METHOD.PUT, body);
        }

        /// <summary>
        /// Updates the existing project information using all the parameters (project ID, title, description, responsible ID, etc) specified in the request. Used for a project you are not responsible for or which you do not administer.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JProject RequestProjectUpdate(int id, Dictionary<string, string> body)
        {         
            string resource = String.Format("{0}/{1}/request", API_PROJECT, id);
            return execute<JProject>(resource, REST.METHOD.PUT, body);
        }

        /// <summary>
        /// Deletes the project with the ID specified in the request from the portal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JProject DeleteProject(int id)
        {         
            string resource = String.Format("{0}/{1}", API_PROJECT, id);
            return execute<JProject>(resource, REST.METHOD.DELETE, null);
        }

        /// <summary>
        /// Sends a request to delete the project with the ID specified in the request from the portal. Used for a project you are not responsible for or which you do not administer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JProject requestsProjectRemoval(int id)
        {         
            string resource = String.Format("{0}/{1}/request", API_PROJECT, id);
            return execute<JProject>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Deletes the task with the ID specified in the request from the project
        /// </summary>
        /// <param name="taskid"></param>
        public void DeleteTask(int taskid)
        {         
            string resource = String.Format("{0}/task/{1}", API_PROJECT, taskid);
            execute<JVoid>(resource, REST.METHOD.DELETE, null);
        }

    }
}
