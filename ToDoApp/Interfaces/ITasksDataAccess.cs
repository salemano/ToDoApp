using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace Interfaces
{
    public interface ITasksDataAccess
    {
        Task<ToDoTask> GetTaskAsync(string task);

        Task<List<ToDoTask>> GetAllTasksAsync();

        void RemoveTaskAsync(string task);

        void UpdateTaskAsync(ToDoTask task, string newValue = "");

        void InsertTaskAsync(ToDoTask task);
    }
}