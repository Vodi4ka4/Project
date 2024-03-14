using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary
{
    public class Project_class
    {
        private NpgsqlConnection connection;

        public Project_class(string connection)
        {
            this.connection = new NpgsqlConnection(connection);
        }

        public void runQueryWithoutResult(NpgsqlCommand query)
        {
            connection.Open();
            var command = query;
            command.Connection = connection;
            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<Project_> GetAllProjects()
        {
            var result = new List<Project_>();
            connection.Open();
            var command = new NpgsqlCommand("SELECT * FROM project", connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var project = new Project_();
                project.Id = (int)reader[0];
                project.Title = (string)reader[1];
                project.Description = (string)reader[2];
                result.Add(project);
            }
            reader.Close();
            connection.Close();
            return result;
        }

        public void CreateProject(Project_ project)
        {
            var command = new NpgsqlCommand($"INSERT INTO project(title, description) VALUES ('{project.Title}', '{project.Description}')");
            runQueryWithoutResult(command);
        }

        public void UpdateProject(Project_ project)
        {
            try
            {
                var command = new NpgsqlCommand($"UPDATE project SET title = '{project.Title}', description = '{project.Description}' WHERE id = {project.Id}");
                runQueryWithoutResult(command);
            }
            catch
            {
                throw new Exception($"Проекта с id = {project.Id} не существует");
            }
        }

        public void DeleteProject(int projectId)
        {
            var command = new NpgsqlCommand($"DELETE FROM project WHERE id = {projectId}");
            runQueryWithoutResult(command);
        }

        public List<Task> GetAllTasks(int projectId)
        {
            List<Task> tasks = new List<Task>();
            connection.Open();
            var command = new NpgsqlCommand($"SELECT * FROM task WHERE project = {projectId}", connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Task task = new Task();
                task.Id = (int)reader[0];
                task.Title = (string)reader[2];
                task.Description = (string)reader[3];
                task.ProjectId = (int)reader[1];
                task.StatusId = (string)reader[4];
                tasks.Add(task);
            }
            reader.Close();
            connection.Close();

            return tasks;
        }

        public void CreateTask(Task task)
        {
            var command = new NpgsqlCommand($"INSERT INTO task(title,description,project,status) VALUES (" +
                $" '{task.Title}', '{task.Description}', {task.ProjectId}, '{task.StatusId}')");
            runQueryWithoutResult(command);
        }

        public void UpdateTask(Task task)
        {
            try
            {
                var command = new NpgsqlCommand($"UPDATE task SET title = '{task.Title}', description = '{task.Description}', " +
                    $" project = {task.ProjectId}, status = '{task.StatusId}' WHERE id = {task.Id}");
                runQueryWithoutResult(command);
            }
            catch
            {
                throw new Exception($"Проекта с id = {task.ProjectId} не существует, невозможно добавить задачу");
            }
        }

        public void DeleteTask(int taskId)
        {
            var command = new NpgsqlCommand($"DELETE FROM task WHERE id = {taskId}");
            runQueryWithoutResult(command);
        }

    }
}

