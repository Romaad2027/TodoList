using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace TODOListAPI
{
    public partial class User
    {
        public User()
        {
            Tasks = new HashSet<TaskDB>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public virtual ICollection<TaskDB> Tasks { get; set; }
    }
}
