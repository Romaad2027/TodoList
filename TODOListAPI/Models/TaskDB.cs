using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace TODOListAPI
{
    public partial class TaskDB
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public DateTime AddDate { get; set; }
        public int? IdUser { get; set; }

        [JsonIgnore]
        public virtual User IdUserNavigation { get; set; }
    }
}
