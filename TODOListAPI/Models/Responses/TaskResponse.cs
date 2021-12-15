using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TODOListAPI.Models.Responses
{
    public class TaskResponse
    {
        public User user { get; set; }

        public ICollection<TaskDB> ToDoItems { get; set; }

        public TaskDB EditableItem { get; set; }

        public TaskResponse(string userName)
        {
            using (var db = new TODOListEFContext())
            {
                EditableItem = new TaskDB();
                user = db.Users.FirstOrDefault(u => u.Name == userName);
                ToDoItems = db.Tasks.FromSqlRaw($"SELECT * FROM Task WHERE idUser = {user.Id}").ToList();
            }
        }

        public void SetUser(string userName)
        {
            using (var db = new TODOListEFContext())
            {
                user = db.Users.FirstOrDefault(u => u.Name == userName);
            }
        }
    }
}
