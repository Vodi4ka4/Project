using ProjectLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Task_ : Form
    {
        private UIEditor uiEditor = new UIEditor();
        Project_class project_Class = new Project_class("Host=localhost;Port=5432;Username=postgres;Password=159632;Database=Project");
        int project = 0;
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public string StatusId { get; set; }
        public Task_(int projectId)
        {
            InitializeComponent();
            project = projectId;
        }
        private void UpdateTask()
        {
            uiEditor.RefreshTaskList(project);
        }

        private void Task__Load(object sender, EventArgs e)
        {
            uiEditor.TaskView = dataGridView_Task;
            UpdateTask();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                ProjectLibrary.Task task = new ProjectLibrary.Task(Id, textBox_title.Text, textBox_description.Text, Convert.ToInt32(textBox_id_projetc.Text), textBox_status.Text);
                project_Class.CreateTask(task);
                uiEditor.TaskView = dataGridView_Task;
                UpdateTask();
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                uiEditor.DeleteTask(Convert.ToInt32(textBox_id_deleted.Text));
                uiEditor.ProjectView = dataGridView_Task;
                UpdateTask();
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            // Создаем новый проект с обновленными данными из текстовых полей
            ProjectLibrary.Task task = new ProjectLibrary.Task(Convert.ToInt32(textBox_id.Text), textBox_title_update.Text, textBox_description_update.Text, Convert.ToInt32(textBox_id_project_update.Text), textBox_status_update.Text);

            // Вызываем метод EditProject экземпляра uiEditor для обновления проекта
            project_Class.UpdateTask(task);
            uiEditor.ProjectView = dataGridView_Task;
            UpdateTask();
        }
    }
}
