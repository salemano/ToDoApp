using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.DAL
{
    public class MongoTaskDataAccess : ITasksDataAccess
    {
        private MongoClient client;
        private const string CollectionName = "MyTasks";
        private const string DatabaseName = "ToDoApp";
        private const string FieldId = "Id";
        private const string FieldValue = "Value";

        public MongoTaskDataAccess(string mongoDbConnection = "mongodb://localhost:27017")
        {
            this.client = new MongoClient(mongoDbConnection);
        }

        public async Task<ToDoTask> GetTaskAsync(string task)
        {
            var tasks = this.GetTasks();

            var filter = Builders<BsonDocument>.Filter.Eq(FieldValue, task);
            var result = await tasks.Find(filter).FirstAsync();

            return new ToDoTask {Value = result[FieldValue].AsString };
        }

        public async Task<List<ToDoTask>> GetAllTasksAsync()
        {
            var collection = this.GetTasks();
            var result = await collection.Find(new BsonDocument()).ToListAsync();
            return
                result.Select(item => new ToDoTask
                {
                    Value = item[FieldValue].AsString
                }).ToList();
        }

        public string RemoveTask(string task)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(FieldValue, task);
            var tasks = this.GetTasks();
            return tasks.DeleteOneAsync(filter).Result.ToJson();
        }

        public async void UpdateTaskAsync(ToDoTask task, string newValue="")
        {
            var filter = Builders<BsonDocument>.Filter.Eq(FieldValue, task.Value);
            var update = Builders<BsonDocument>.Update.Set(FieldValue, newValue);
            var tasks = this.GetTasks();
            await tasks.UpdateOneAsync(filter, update);
        }

        public string InsertTask(ToDoTask task)
        {
            var tasks = this.GetTasks();
            var document = new BsonDocument
            {
                { FieldValue, task.Value }
            };

            return tasks.InsertOneAsync(document).ToJson();
        }

        private IMongoCollection<BsonDocument> GetTasks()
        {
            var database = this.client.GetDatabase(DatabaseName);
            var collection = database.GetCollection<BsonDocument>(CollectionName);
            return collection;
        }
    }
}