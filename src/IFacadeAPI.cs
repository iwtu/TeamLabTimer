using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeTracker
{
    interface IFacadeAPI
    {
        void UpdateTaskTime(MainTimer timer);
        void DeleteTime(MainTimer timer);
        Project[] GetParticiapedProjects();
        Task[] GetAllTasks(int projectid);
        Task[] GetMyOpenTaskByProject(int projectid);
        string GetMyId();
        Task GetTask(int taskid);
        void changeProjectTitle(int id, string newTitle);
    }
}
