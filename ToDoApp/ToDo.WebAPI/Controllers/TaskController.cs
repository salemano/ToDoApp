using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ToDo.Core;
using ToDo.DAL;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.WebAPI.Controllers
{
    // Read requirements

    public class TaskController : ApiController
    {
        // http://localhost:1764/api/task/gettask

        private readonly ITaskService taskService;

        public TaskController()
        {
            this.taskService = new TaskService(new MongoTaskDataAccess());
        }

        [HttpPost]
        public string AddTask(string value)
        {
            var taskValue = new ToDoTask {Value = value};
            this.taskService.Insert(taskValue);
            return "Ok";
        }

        [HttpGet]
        public async Task<ToDoTask> GetTask(string value)
        {
            return await this.taskService.Get(value);
        }

        [HttpGet]
        public async Task<List<ToDoTask>> GetAll()
        {
            return await this.taskService.GetAll();
        }
        
        [HttpDelete]
        public void DeleteTask(string value)
        {
            this.taskService.Remove(value);
        }
    }
}