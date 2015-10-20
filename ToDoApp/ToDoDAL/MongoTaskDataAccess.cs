using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ToDoDAL
{
    public class MongoTaskDataAccess : ITasksDataAccess
    {
        private MongoClient client;
        private const string CollectionName = "Tasks";
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

            var filter = Builders<BsonDocument>.Filter.Eq(FieldId, task);
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

        public async void RemoveTaskAsync(string task)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(FieldId, task);
            var tasks = this.GetTasks();
            await tasks.DeleteOneAsync(filter);
        }

        public async void UpdateTaskAsync(ToDoTask task, string newValue="")
        {
            var filter = Builders<BsonDocument>.Filter.Eq(FieldValue, task.Value);
            var update = Builders<BsonDocument>.Update.Set(FieldValue, newValue);
            var tasks = this.GetTasks();
            await tasks.UpdateOneAsync(filter, update);
        }

        public async void InsertTaskAsync(ToDoTask task)
        {
            var tasks = this.GetTasks();
            var document = new BsonDocument
            {
                { FieldValue, task.Value }
            };

            await tasks.InsertOneAsync(document);
        }

        private IMongoCollection<BsonDocument> GetTasks()
        {
            var database = this.client.GetDatabase(DatabaseName);
            var collection = database.GetCollection<BsonDocument>(CollectionName);
            return collection;
        }
    }
}