using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.Interfaces
{
    public interface ITasksDataAccess
    {
        Task<ToDoTask> GetTaskAsync(string task);

        Task<List<ToDoTask>> GetAllTasksAsync();

        string RemoveTask(string task);

        void UpdateTaskAsync(ToDoTask task, string newValue = "");

        string InsertTask(ToDoTask task);
    }
}