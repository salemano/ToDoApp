using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.Interfaces
{
    public interface ITaskService
    {
        Task<ToDoTask> Get(string value);

        void Insert(ToDoTask task);

        Task<List<ToDoTask>> GetAll();

        void Update(ToDoTask task);

        void Remove(string value);
    }
}