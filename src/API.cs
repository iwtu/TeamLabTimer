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
                Host = PROTOCOL + "://" + portal + "." + SERVER +"/";
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
            return true; // CIIN: make code if necessary 
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
            string resource = "api/1.0/authentication";
            AuthToken authToken = execute<AuthToken>(resource, REST.METHOD.POST, body);
            AuthAPI.Token = authToken.token;
            return authToken;
        }

    }

    public enum Status : int {
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
            string resource = "api/1.0/project/task/@self";
            return execute<JTask[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the detailed information about the task with the ID specified in the request
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public JTask GetTask(int taskid)
        {
            string resource = "api/1.0/project/task/" + taskid;
            return execute<JTask>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all the tasks within the project with the ID specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public JTask[] Tasks(int projectid)
        {
            string resource = "api/1.0/project/" + projectid + "/task";
            return execute<JTask[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list with the detailed information about the tasks for the current user with the status specified in the request
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public JTask[] MyTaskByStatus(string status)
        {
            string resource = "api/1.0/project/task/@self/" + status;
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
            string resource = "api/1.0/project/" + projectid + "/task/@self/" + status;
            return execute<JTask[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        ///  Returns the list of all tasks in the project with the ID specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public JTask[] AllTask(int projectid)
        {
            string resource = "api/1.0/project/" + projectid + "/task/@all";
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
            string resource = "api/1.0/project/" + projectid + "/task/" + status;
            return execute<JTask[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Adds the task to the selected project with the parameters (responsible user ID, task description, deadline time, etc) specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="body"></param>
        public void AddTAsk(int projectid, Dictionary<string, string> body)
        {
            string resource = "api/1.0/project/" + projectid + "/task";
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
            string resource = "api/1.0/project/task/" + taskid;
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
            string resource = "api/1.0/project/task/" + taskid + "/status";
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
            string resource = "api/1.0/project/task/" + taskid + "/time";
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
            string resource = "api/1.0/project/task/" + taskid + "/time";
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
            string resource = "api/1.0/project/time/" + timeid;
            return execute<JTime>(resource, REST.METHOD.PUT, body);
        }

        /// <summary>
        /// Deletes the time from the task with the ID specified in the request
        /// </summary>
        /// <param name="timeid"></param>
        /// <returns></returns>
        public JTime DeleteTimeSpent(int timeid)
        {
            string resource = "api/1.0/project/time/" + timeid;
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
            string resource = "api/1.0/project";
            return execute<JProject[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the detailed information about the project with ID specified in the request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JProject ProjectByID(int id)
        {
            string resource = "api/1.0/project/" + id;
            return execute<JProject>(resource, REST.METHOD.GET, null);
        }


        /// <summary>
        ///  Returns the list of all projects in which the current user participates
        /// </summary>
        /// <returns></returns>
        public JProject[] ParticipatedProjects()
        {
            string resource = "api/1.0/project/@self";
            return execute<JProject[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// projects
        /// </summary>
        /// <returns></returns>
        public JProject[] FollowedProjects()
        {
            string resource = "api/1.0/project/@follow";
            return execute<JProject[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all projects with the status specified in the request
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public JProject[] ProjectByStatus(string status)
        {
            string resource = "api/1.0/project/" + status;
            return execute<JProject[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        ///  Returns the detailed information about the time spent on the project with the ID specified in the request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JTime[] ProjectTimeSpent(int id)
        {
            string resource = "api/1.0/project/" + id + "/time";
            return execute<JTime[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all the milestones within the project with the ID specified in the request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JMilestone[] GetMilestonesByProjectID(int id)
        {
            string resource = "api/1.0/project/" + id + "/milestone";
            return execute<JMilestone[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        ///  Returns the list of all projects matching the query specified in the request
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public JProject[] SearchAllProjects(string query)
        {
            string resource = "api/1.0/project/@search/" + query;
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
            string resource = "api/1.0/project/" + id + "/@search/" + query;
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
            string resource = "api/1.0/project/" + id + "/milestone/" + status;
            return execute<JMilestone[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Creates a new project using all the necessary (title, description, responsible ID, etc) and some optional parameters specified in the request
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public JProject CreateProject(Dictionary<string, string> body)
        {
            string resource = "api/1.0/project";
            return execute<JProject>(resource, REST.METHOD.POST, body);
        }

        /// <summary>
        /// Returns the list of the portal projects matching the project title, description, responsible ID or tags specified in the request
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        JProject RequestProject(Dictionary<string, string> body)
        {
            string resource = "api/1.0/project/request";
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
            string resource = "api/1.0/project/" + id + "/milestone";
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
            string resource = "api/1.0/project/" + id;
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
            string resource = "api/1.0/project/" + id + "/tag";
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
            string resource = "api/1.0/project/" + id + "/request";
            return execute<JProject>(resource, REST.METHOD.PUT, body);
        }

        /// <summary>
        /// Deletes the project with the ID specified in the request from the portal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JProject DeleteProject(int id)
        {
            string resource = "api/1.0/project/" + id;
            return execute<JProject>(resource, REST.METHOD.DELETE, null);
        }

        /// <summary>
        /// Sends a request to delete the project with the ID specified in the request from the portal. Used for a project you are not responsible for or which you do not administer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        JProject requestsProjectRemoval(int id)
        {
            string resource = "api/1.0/project/" + id + "/request";
            return execute<JProject>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Deletes the task with the ID specified in the request from the project
        /// </summary>
        /// <param name="taskid"></param>
        public void DeleteTask(int taskid)
        {
            string resource = "api/1.0/project/task/" + taskid;
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
            string resource = "api/1.0/project/event/" + eventid;
            return execute<JEvents>(resource, REST.METHOD.GET, null);

        }

        /// <summary>
        /// Returns the list of events within the project with the ID specified in the request
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public JEvents[] Events(int projectid)
        {
            string resource = "api/1.0/project/" + projectid + "/event";
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
            string resource = "api/1.0/project/milestone";
            return execute<JMilestone[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all overdue milestones in the portal projects
        /// </summary>
        /// <returns></returns>
        public JMilestone[] OverdueMilestones()
        {
            string resource = "api/1.0/project/milestone/late";
            return execute<JMilestone[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        ///  Returns the list with the detailed information about the milestone with the ID specified in the request 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JMilestone GetMilestone(int id)
        {
            string resource = "api/1.0/project/milestone/" + id;
            return execute<JMilestone>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of all tasks within the milestone with the ID specified in the request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JTask[] GetMilestoneTasks(int id)
        {
            string resource = "api/1.0/project/milestone/" + id + "/task";
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
            string resource = "api/1.0/project/milestone/" + year + "/" + month;
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
            string resource = "api/1.0/project/milestone/" + year + "/" + month + "/" + day;
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
            string resource = "api/1.0/project/milestone/" + id;
            return execute<JMilestone>(resource, REST.METHOD.PUT, body);
        }

        /// <summary>
        /// Deletes the milestone with the ID specified in the request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JMilestone DeleteMilestone(int id)
        {
            string resource = "api/1.0/project/milestone/" + id;
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
            string resource = "api/1.0/project/" + projectid + "/team";
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
            string resource = "api/1.0/project/" + projectid + "/team";
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
            string resource = "api/1.0/project/" + projectid + "/team/security";
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
            string resource = "api/1.0/project/" + projectid + "/team";
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
            string resource = "api/1.0/project/tag/" + tag;
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
            string resource = "api/1.0/people";
            return execute<JProfile[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the detailed information about the current user profile
        /// </summary>
        /// <returns></returns>
        public JProfile MyProfile()
        {
            string resource = "api/1.0/people/@self";
            return execute<JProfile>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the detailed information about the profile of the user with the ID specified in the request
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public JProfile SpecificProfile(string username)
        {
            string resource = "api/1.0/people/" + username; 
            return execute<JProfile>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Returns the list of profiles for all portal users matching the search query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public JProfile[] SearchUsers(string query)
        {
            string resource = "api/1.0/people/@search/" + query;
            return execute<JProfile[]>(resource, REST.METHOD.GET, null);
        }

        /// <summary>
        /// Adds a new portal user with the first and last name, email address and several optional parameters specified in the request
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public JProfile AddNewUser(Dictionary<string,string> body)
        {
            string resource = "api/1.0/people";
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
            string resource = "api/1.0/people/" + userid + "/contacts";
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
            string resource = "api/1.0/people/" + userid;
            return execute<JProfile>(resource, REST.METHOD.PUT, body);
        }

        // CIID:
        //public JProfile UpdateUserPhoto(string userid)
        //{
        //    string resource = "api/1.0/people/" + username;
        //    return execute<JProfile>(resource, REST.METHOD.GET, null);
        //}

        // CIID:
        //public JProfile UpdateUserContacts()
        //{
        //    string resource = "api/1.0/people/" + username;
        //    return execute<JProfile>(resource, REST.METHOD.GET, null);
        //}

        /// <summary>
        /// Deletes the user with the ID specified in the request from the portal
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public JProfile DeleteUser(string userid)
        {
            string resource = "api/1.0/people/" + userid;
            return execute<JProfile>(resource, REST.METHOD.DELETE, null);
        }

        /// <summary>
        /// Deletes the photo of the user with the ID specified in the request
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public JProfile DeleteUserPhoto(string userid)
        {
            string resource = "api/1.0/people/" + userid + "/photo";
            return execute<JProfile>(resource, REST.METHOD.DELETE, null);
        }

        // CIID:
        //public JProfile DeleteUserContacts()
        //{
        //    string resource = "api/1.0/people/";
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






