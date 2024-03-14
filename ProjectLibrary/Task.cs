using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public string StatusId { get; set; }
        public Task() { }
        public Task(int id, string title,string description,int projectId, string statusId)
        {
            Id = id;
            Title = title;
            Description = description;
            ProjectId = projectId;
            StatusId = statusId;
        }
    }
}
