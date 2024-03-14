using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace ProjectLibrary
{
    public class UIEditor
    {
        private static string conn = "Host=localhost;Port=5432;Username=postgres;Password=159632;Database=Project";
        private ProjectRepository projectRepository = new ProjectRepository(conn);
        private TaskRepository taskRepository = new TaskRepository(conn);

        public DataGridView ProjectView { get; set; }
        public DataGridView TaskView { get; set; }

        public void RefreshProjectList()
        {
            ProjectView.Rows.Clear();
            List<Project_> projects = projectRepository.GetAllProjects();
            foreach (Project_ project in projects)
            {
                ProjectView.Rows.Add(project.Id, project.Title, project.Description);
            }
        }

        public void CreateProject(Project_ project)
        {
            projectRepository.CreateProject(project);
        }

        public void EditProject(Project_ project)
        {
            projectRepository.UpdateProject(project);
        }

        public void DeleteProject(int projectId)
        {
            projectRepository.DeleteProject(projectId);
        }

        public void RefreshTaskList(int projectId)
        {
            TaskView.Rows.Clear();
            List<Task> tasks = taskRepository.GetAllTasks(projectId);
            foreach (Task task in tasks)
            {
                TaskView.Rows.Add(task.Id, task.Title, task.Description, task.StatusId, task.ProjectId);
            }
        }

        public void CreateTask(Task task)
        {
            taskRepository.CreateTask(task);
        }

        public void EditTask(Task task)
        {
            taskRepository.UpdateTask(task);
        }

        public void DeleteTask(int taskId)
        {
            taskRepository.DeleteTask(taskId);
        }

    }
}
