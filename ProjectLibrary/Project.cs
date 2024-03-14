using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary
{
    public class Project_
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Project_() { }
        public Project_(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
    }
}
