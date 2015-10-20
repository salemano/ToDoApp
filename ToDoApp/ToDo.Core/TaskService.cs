using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.Core
{
    public class TaskService : ITaskService
    {
        private readonly ITasksDataAccess dataAccess;

        public TaskService(ITasksDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public async Task<ToDoTask> Get(string value)
        {
            var result = this.dataAccess.GetTaskAsync(value);
            return await result;
        }

        public void Insert(ToDoTask task)
        {
            this.dataAccess.InsertTaskAsync(task);
        }

        public Task<List<ToDoTask>> GetAll()
        {
            return this.dataAccess.GetAllTasksAsync();
        }

        public void Remove(string value)
        {
            this.dataAccess.RemoveTaskAsync(value);
        }

        public void Update(ToDoTask task)
        {
            this.dataAccess.UpdateTaskAsync(task);
        }
    }
}