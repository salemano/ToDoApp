using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace Interfaces
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