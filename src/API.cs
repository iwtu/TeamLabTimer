/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */


using System;
using System.Collections.Generic;
using Newtonsoft.Json; //for JsonConvert and deserialize string in JSON format
using TeamLab.Wrapper;

using JObject = Newtonsoft.Json.Linq.JObject;


namespace TeamLab.API
{
    public abstract class TeamLabAPI
    {
        private const string PROTOCOL = "http";
        private const string SERVER = "teamlab.com";
        public const string API_VERSION = "api/1.0";
        public const string API_PROJECT = API_VERSION + "/project";

        protected static string Host;
        private static string portal;
        public static string Portal
        {
            get
            {
                return portal;
            }
            set
            {
                portal = value;
                //Host = PROTOCOL + "://" + portal + "." + SERVER +"/";
                Host = String.Format("{0}://{1}.{2]/", PROTOCOL, portal, SERVER);
            }
        }

        protected REST.TeamLab.Request request = new REST.TeamLab.Request();
        
        protected TeamLabAPI() { }

        public TeamLabAPI(string portal)
        {
            Portal = portal;
            request = new REST.TeamLab.Request();
        }

        protected abstract T execute<T>(string url, REST.METHOD method, Dictionary<string, string> body);

        protected T transform<T>(string jstring)
        {
            string[] wholeResponse = jstring.Split('\n');
            string response = wholeResponse[wholeResponse.Length - 1];
            checkResponse(response); // JObject.Parse throws exception in case of error message in response
            JObject o = JObject.Parse(response);
            return JsonConvert.DeserializeObject<T>(o.SelectToken("response").ToString());
        }

        protected bool checkResponse(string response)
        {
            return true; // CIIN
        }
    }

    public class AuthAPI : TeamLabAPI
    {
        public static string Token;

        protected override T execute<T>(string resource, REST.METHOD method, Dictionary<string, string> body) // CIIN: not many lines but still  duplicate code. Think about it if necessary
        {
            string jstring = request.GetAuthorizedJSONResponse(Host + resource, method, body, Token);
            return transform<T>(jstring);
        }

    }

    public class UnauthAPI : TeamLabAPI
    {

        protected override T execute<T>(string resource, REST.METHOD method, Dictionary<string, string> body)
        {
            string jstring = request.GetJSONResponse(Host + resource, method, body);
            return transform<T>(jstring);
        }

    }

    public class AuthenticationAPI : UnauthAPI
    {
        public AuthToken GetToken(Dictionary<string, string> body)
        {
            //string resource = API_VERSION + "/authentication";
            string resource = String.Format("{0}/authentication", API_VERSION);
            AuthToken authToken = execute<AuthToken>(resource, REST.METHOD.POST, body);
            AuthAPI.Token = authToken.token;
            return authToken;
        }

    }

    public enum Status {
        open,
        closed, 
        noaccept,
        disable,
        unclassified,
        notinmilestone
    }
    
    public class TaskAPI : AuthAPI
    {

        /// <summary>
        /// Returns the list with the detailed information about all tasks for the current user
        /// </summary>
        /// <returns></returns>
        public JTask[] MyTaskWithDetails()
        {
            //string resource = API_PROJECT + "/task/@self";
            string resource = String.Format("{0}/task/@self", API_PROJECT);
            return execute<JTask[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the detailed information about the task with the ID specified in the request
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public JTask GetTask(int taskid)
        {
            //string resource = API_PROJECT + "/task/" + taskid;
            string resource = String.Format("{0}/task/{1}", API_PROJECT, taskid);
            return execute<JTask>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all the tasks within the project with the ID specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public JTask[] Tasks(int projectid)
        {
            //string resource = API_PROJECT + "/" + projectid + "/task";
            string resource = String.Format("{0}/{1}/task", API_PROJECT, projectid);
            return execute<JTask[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list with the detailed information about the tasks for the current user with the status specified in the request
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public JTask[] MyTaskByStatus(string status)
        {
            //string resource = API_PROJECT + "/task/@self/" + status;
            string resource = String.Format("{0}/task@self/{1}", API_PROJECT, status);
            return execute<JTask[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all tasks for the current user with the selected status in the project with the ID specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public JTask[] MyTaskByProjectAndStatus(int projectid, Status status)
        {
            //string resource = API_PROJECT + "/" + projectid + "/task/@self/" + status;
            string resource = String.Format("{0}/{1}/task/@self/{3}", API_PROJECT, projectid, status);
            return execute<JTask[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        ///  Returns the list of all tasks in the project with the ID specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public JTask[] AllTask(int projectid)
        {
            //string resource = API_PROJECT + "/" + projectid + "/task/@all";
            string resource = String.Format("{0}/{1}/task/@all", API_PROJECT, projectid);
            return execute<JTask[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all tasks with the selected status in the project with the ID specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public JTask[] TasksWithStatus(int projectid, Status status)
        {
            //string resource = API_PROJECT + "/" + projectid + "/task/" + status;
            string resource = String.Format("{0}/{1}/task/{3}", API_PROJECT, projectid, status);
            return execute<JTask[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Adds the task to the selected project with the parameters (responsible user ID, task description, deadline time, etc) specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="body"></param>
        public void AddTAsk(int projectid, Dictionary<string, string> body)
        {
            //string resource = API_PROJECT + "/" + projectid + "/task";
            string resource = String.Format("{0}/{1}/task", API_PROJECT, projectid);
            execute<JVoid>(resource, REST.METHOD.POST, body);

        }

        /// <summary>
        /// Updates the selected task with the parameters (responsible user ID, task description, deadline time, etc) specified in the request
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JTask UpdateTask(int taskid, Dictionary<string, string> body)
        {
            //string resource = API_PROJECT + "/task/" + taskid;
            string resource = String.Format("{0}/task/{1}", API_PROJECT, taskid);
            return execute<JTask>(resource, REST.METHOD.PUT, body);
        }

        /// <summary>
        /// Updates the status of the task with the ID specified in the request
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JTask UpdateTaskStatus(int taskid, Dictionary<string, string> body)
        {
            //string resource = API_PROJECT + "/task/" + taskid + "/status";
            string resource = String.Format("{0}/task/{1}/status", API_PROJECT, taskid);
            return execute<JTask>(resource, REST.METHOD.PUT, body);
        }

    }

    public class TimeAPI : AuthAPI
    {

        /// <summary>
        /// Returns the time spent on the task with the ID specified in the request
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public JTime[] GetTimeSpent(int taskid)
        {
            //string resource = API_PROJECT + "/task/" + taskid + "/time";
            string resource = String.Format("{0}/task/{1}/time", API_PROJECT, taskid);
            return execute<JTime[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Adds the time to the selected task with the time parameters specified in the request
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JTime AddTaskTime(int taskid, Dictionary<string, string> body)
        {
            //string resource = API_PROJECT + "/task/" + taskid + "/time";
            string resource = String.Format("{0}/task/{1}/time", API_PROJECT, taskid);
            return execute<JTime>(resource, REST.METHOD.POST, body);
        }
        
        /// <summary>
        ///  Updates the time for the selected task with the time parameters specified in the request
        /// </summary>
        /// <param name="timeid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JTime UpdateTaskTime(int timeid, Dictionary<string, string> body)
        {
            //string resource = API_PROJECT + "/time/" + timeid;
            string resource = String.Format("{0}/time/{1}", API_PROJECT, timeid);
            return execute<JTime>(resource, REST.METHOD.PUT, body);
        }

        /// <summary>
        /// Deletes the time from the task with the ID specified in the request
        /// </summary>
        /// <param name="timeid"></param>
        /// <returns></returns>
        public JTime DeleteTimeSpent(int timeid)
        {
            //string resource = API_PROJECT + "/time/" + timeid;
            string resource = String.Format("{0}/time/{1}", API_PROJECT, timeid);
            return execute<JTime>(resource, REST.METHOD.DELETE, null);
        }

    }


    public class ProjectAPI : AuthAPI
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
            //string resource = API_PROJECT + "/" + id;
            string resource = String.Format("{0}/{1}", API_PROJECT, id);
            return execute<JProject>(resource, REST.METHOD.GET, null);
        }


        /// <summary>
        ///  Returns the list of all projects in which the current user participates
        /// </summary>
        /// <returns></returns>
        public JProject[] ParticipatedProjects()
        {
            //string resource = API_PROJECT + "/@self";
            string resource = String.Format("{0}/@self", API_PROJECT);
            return execute<JProject[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// projects
        /// </summary>
        /// <returns></returns>
        public JProject[] FollowedProjects()
        {
            //string resource = API_PROJECT + "/@follow";
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
            //string resource = API_PROJECT + "/" + status;
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
            //string resource = API_PROJECT + "/" + id + "/time";
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
            //string resource = API_PROJECT + "/" + id + "/milestone";
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
            //string resource = API_PROJECT + "/@search/" + query;
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
            //string resource = API_PROJECT + "/" + id + "/@search/" + query;
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
            //string resource = API_PROJECT + "/" + id + "/milestone/" + status;
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
            //string resource = API_PROJECT + "/request";
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
            //string resource = API_PROJECT + "/" + id + "/milestone";
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
            //string resource = API_PROJECT + "/" + id;
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
            //string resource = API_PROJECT + "/" + id + "/tag";
            string resource = String.Format("{0}/{1}/tag", API_PROJECT);
            return execute<JProject>(resource, REST.METHOD.PUT, body);
        }

        /// <summary>
        /// Updates the existing project information using all the parameters (project ID, title, description, responsible ID, etc) specified in the request. Used for a project you are not responsible for or which you do not administer.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        JProject RequestProjectUpdate(int id, Dictionary<string, string> body)
        {
            //string resource = API_PROJECT + "/" + id + "/request";
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
            //string resource = API_PROJECT + "/" + id;
            string resource = String.Format("{0}/{1}", API_PROJECT, id);
            return execute<JProject>(resource, REST.METHOD.DELETE, null);
        }

        /// <summary>
        /// Sends a request to delete the project with the ID specified in the request from the portal. Used for a project you are not responsible for or which you do not administer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        JProject requestsProjectRemoval(int id)
        {
            //string resource = API_PROJECT + "/" + id + "/request";
            string resource = String.Format("{0}/{1}/request", API_PROJECT, id);
            return execute<JProject>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Deletes the task with the ID specified in the request from the project
        /// </summary>
        /// <param name="taskid"></param>
        public void DeleteTask(int taskid)
        {
            //string resource = API_PROJECT + "/task/" + taskid;
            string resource = String.Format("{0}/task/{1}", API_PROJECT, taskid);
            execute<JVoid>(resource, REST.METHOD.DELETE, null);
        }

    }

    public class EventsAPI : AuthAPI
    {

        /// <summary>
        ///  Returns the detailed information about the event with the ID specified in the request within the project
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public JEvents GetEvent(int eventid)
        {
            //string resource = API_PROJECT + "/event/" + eventid;
            string resource = String.Format("{0}/event/{1}", API_PROJECT, eventid);
            return execute<JEvents>(resource, REST.METHOD.GET, null);

        }

        /// <summary>
        /// Returns the list of events within the project with the ID specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public JEvents[] Events(int projectid)
        {
            //string resource = API_PROJECT + "/" + projectid + "/event";
            string resource = String.Format("{0}/{1}/event", API_PROJECT, projectid);
            return execute<JEvents[]>(resource, REST.METHOD.GET, null);


        }

    }

    public class MilestoneAPI : AuthAPI
    {
        /// <summary>
        /// Returns the list of all upcoming milestones within all portal projects
        /// </summary>
        /// <returns></returns>
        public JMilestone[] UpcomingMilestones()
        {
            //string resource = API_PROJECT + "/milestone";
            string resource = String.Format("{0}/milestone", API_PROJECT);
            return execute<JMilestone[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all overdue milestones in the portal projects
        /// </summary>
        /// <returns></returns>
        public JMilestone[] OverdueMilestones()
        {
            //string resource = API_PROJECT + "/milestone/late";
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
            //string resource = API_PROJECT + "/milestone/" + id;
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
            //string resource = API_PROJECT + "/milestone/" + id + "/task";
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
            //string resource = API_PROJECT + "/milestone/" + year + "/" + month;
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
            //string resource = API_PROJECT + "/milestone/" + year + "/" + month + "/" + day;
            string resource = String.Format("{0}/milestone/{1}/{2}/{3}", API_PROJECT, year, month,day);
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
            //string resource = API_PROJECT + "/milestone/" + id;
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
            //string resource = API_PROJECT + "/milestone/" + id;
            string resource = String.Format("{0}/milestone/{1}", API_PROJECT, id);
            return execute<JMilestone>(resource, REST.METHOD.DELETE, null);
        }

    }

    public class TeamAPI : AuthAPI
    {
        /// <summary>
        /// Returns the list of all users participating in the project with the ID specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public JPerson[] ProjectTeam(int projectid)
        {
            //string resource = API_PROJECT + "/" + projectid + "/team";
            string resource = String.Format("{0}/{1}/team", API_PROJECT, projectid);
            return execute<JPerson[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Adds the user with the ID specified in the request to the selected project team
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JPerson[] AddToTeam(int projectid, Dictionary<string, string> body)
        {
            //string resource = API_PROJECT + "/" + projectid + "/team";
            string resource = String.Format("{0}/{1}/team", API_PROJECT, projectid);
            return execute<JPerson[]>(resource, REST.METHOD.POST, body);
        }

        /// <summary>
        /// Sets the security rights for the user or users with the IDs specified in the request within the selected project
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JPerson[] SetTeamSecurity(int projectid, Dictionary<string, string> body)
        {
            //string resource = API_PROJECT + "/" + projectid + "/team/security";
            string resource = String.Format("{0}/{1}/team/security", API_PROJECT, projectid);
            return execute<JPerson[]>(resource, REST.METHOD.POST, body);
        }

        /// <summary>
        ///  Removes the user with the ID specified in the request from the selected project team
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JPerson[] RemoveFromTeam(int projectid, Dictionary<string, string> body)
        {
            //string resource = API_PROJECT + "/" + projectid + "/team";
            string resource = String.Format("{0}/{1}/team", API_PROJECT, projectid);
            return execute<JPerson[]>(resource, REST.METHOD.DELETE, body);
        }
    }

    public class TagAPI : AuthAPI
    {
        /// <summary>
        /// Returns the detailed list of all projects with the specified tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        JProject[] ProjectByTag(string tag)
        {
            //string resource = API_PROJECT + "/tag/" + tag;
            string resource = String.Format("{0}/tag/{1}", API_PROJECT, tag);
            return execute<JProject[]>(resource, REST.METHOD.GET, null);
        }
    }    
       
    public class ProfileAPI : AuthAPI
    {
        /// <summary>
        /// Returns the list of profiles for all portal users
        /// This method returns a partial profile. Use more specific method to get full profile 
        /// </summary>
        /// <returns></returns>
        public JProfile[] AllProfiles()
        {
            //string resource = API_VERSION + "/people";
            string resource = String.Format("{0}/people", API_VERSION);
            return execute<JProfile[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the detailed information about the current user profile
        /// </summary>
        /// <returns></returns>
        public JProfile MyProfile()
        {
            //string resource = API_VERSION + "/people/@self";
            string resource = String.Format("{0}/people/@self", API_VERSION);
            return execute<JProfile>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the detailed information about the profile of the user with the ID specified in the request
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public JProfile SpecificProfile(string username)
        {
            //string resource = API_VERSION + "/people/" + username; 
            string resource = String.Format("{0}/people/{1}", API_VERSION, username);
            return execute<JProfile>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of profiles for all portal users matching the search query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public JProfile[] SearchUsers(string query)
        {
            //string resource = API_VERSION + "/people/@search/" + query;
            string resource = String.Format("{0}/people/@search/{1}", API_VERSION, query);
            return execute<JProfile[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Adds a new portal user with the first and last name, email address and several optional parameters specified in the request
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public JProfile AddNewUser(Dictionary<string,string> body)
        {
            //string resource = API_VERSION + "/people";
            string resource = String.Format("{0}/people", API_VERSION);
            return execute<JProfile>(resource, REST.METHOD.POST, body);
        }

        /// <summary>
        /// Updates the specified user contact information changing the data present on the portal for the sent data
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JProfile SetUserContacts(string userid, Dictionary<string, string> body)
        {            
            string resource = String.Format("{0}/peoople/{1}/contacts", API_VERSION, userid);
            return execute<JProfile>(resource, REST.METHOD.POST, body);
        }

        /// <summary>
        /// Updates the data for the selected portal user with the first and last name, email address and/or optional parameters specified in the request
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public JProfile UpdateUser(string userid, Dictionary<string, string> body)
        {            
            string resource = String.Format("{0}/peoople/{1}", API_VERSION, userid);
            return execute<JProfile>(resource, REST.METHOD.PUT, body);
        }

        // CIID:
        //public JProfile UpdateUserPhoto(string userid)
        //{
        //    string resource = API_VERSION + "/people/" + username;
        //    return execute<JProfile>(resource, REST.METHOD.GET, null);
        //}

        // CIID:
        //public JProfile UpdateUserContacts()
        //{
        //    string resource = API_VERSION + "/people/" + username;
        //    return execute<JProfile>(resource, REST.METHOD.GET, null);
        //}

        /// <summary>
        /// Deletes the user with the ID specified in the request from the portal
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public JProfile DeleteUser(string userid)
        {
            //string resource = API_VERSION + "/people/" + userid;
            string resource = String.Format("{0}/people/{1}", API_VERSION, userid);
            return execute<JProfile>(resource, REST.METHOD.DELETE, null);
        }

        /// <summary>
        /// Deletes the photo of the user with the ID specified in the request
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public JProfile DeleteUserPhoto(string userid)
        {
            //string resource = API_VERSION + "/people/" + userid + "/photo";
            string resource = String.Format("{0}/people/{1}/photo", API_VERSION, userid);
            return execute<JProfile>(resource, REST.METHOD.DELETE, null);
        }

        // CIID:
        //public JProfile DeleteUserContacts()
        //{
        //    string resource = API_VERSION + "/people/";
        //    return execute<JProfile>(resource, REST.METHOD.GET, null);
        //}

    }

       public abstract class JAncestor { }

    public abstract class JEntity : JAncestor
    {
        public int id;
        public string title;
        public string description;
        public JPerson responsible = new JPerson();
        public JPerson updatedBy = new JPerson();
        public string created;
        public JPerson createdBy = new JPerson();
        public string updated;

        public override string ToString()
        {
            return title;
        }
    }

    public class JTask : JEntity
    {
        public string deadline;
        public int priority;
        public int milestoneId;
        public int status;
        public bool canEdit;
        public bool isExpired;
        public JProjectOwner projectOwner = new JProjectOwner();

    }

    public class JTime : JAncestor
    {
        public int id;
        public string date;
        public double hours;
        public string note;
        public int relatedProject;
        public int relatedTask;
        public JPerson createdBy = new JPerson();
    }

    public class JProject : JEntity
    {
        public bool canEdit;
        public JSecurity security = new JSecurity();
        public int projectFolder;
        public int status;
        public bool isPrivate;

        public class JSecurity
        {
            public bool canCreateMessage;
            public bool canCreateMilestone;
            public bool canCreateTask;
            public bool canEditTeam;
            public bool canReadFiles;
            public bool canReadMilestones;
            public bool canReadMessages;
            public bool canReadTasks;
        }
    }

    public class JEvents : JEntity
    {
        int projectId;
        string from;
        string to;
    }

    public class JMilestone : JEntity
    {
        public bool isNotify;
        public bool isKey;
        public string deadline;
    }

    public class AuthToken : JAncestor
    {
        public string token;
        public string expires;
    }

    public class JPerson : JAncestor
    {
        public string id;
        public string displayName;
        public string title;
        public string avatarSmall;
    }

    public class JProjectOwner : JAncestor
    {
        public int id;
        public string title;
        public int status;
    }

    public class JFoundedProjets : JAncestor
    {
        public class Item
        {
            public int id;
            public int entityType;
            public string title;
            public string created;
        }

        public Item[] items;
        public JProjectOwner projectOwner;
    }


    public class JVoid : JAncestor { }

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






