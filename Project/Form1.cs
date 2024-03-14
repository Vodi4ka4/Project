using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectLibrary;

namespace Project
{
    public partial class Form_Project : Form
    {
        private UIEditor uiEditor = new UIEditor();
        Project_class project_Class = new Project_class("Host=localhost;Port=5432;Username=postgres;Password=159632;Database=Project");
        public bool IsEdit { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public Form_Project()
        {
            InitializeComponent();
        }

        private void Form_Project_Load(object sender, EventArgs e)
        {
            uiEditor.ProjectView = dataGridView_Project;
            UpdateProject();
        }
        private void UpdateProject()
        {
            uiEditor.RefreshProjectList();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox_title.Text) || string.IsNullOrEmpty(textBox_description.Text))
                {
                    MessageBox.Show("Заполните данные корректно");
                    return;
                }

                Project_ project = new Project_(Id, textBox_title.Text, textBox_description.Text);
                if (IsEdit)
                {
                    uiEditor.EditProject(project); // Вот здесь нужно использовать экземпляр uiEditor
                }
                else
                {
                    uiEditor.CreateProject(project); // Также здесь
                }
                uiEditor.ProjectView = dataGridView_Project;
                UpdateProject();
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
                uiEditor.DeleteProject(Convert.ToInt32(textBox_id_deleted.Text));
                uiEditor.ProjectView = dataGridView_Project;
                UpdateProject();
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            
                // Создаем новый проект с обновленными данными из текстовых полей
                Project_ project = new Project_(Convert.ToInt32(textBox_id.Text), textBox_title_update.Text, textBox_description_update.Text);

                // Вызываем метод EditProject экземпляра uiEditor для обновления проекта
                project_Class.UpdateProject(project);
                uiEditor.ProjectView = dataGridView_Project;
                UpdateProject();
            
        }



        private void dataGridView_Project_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int projectId = 0;
            // Проверяем, что строка выбрана (index не равен -1)
            if (rowIndex != -1)
            {
                // Получаем ID проекта из выбранной строки
                projectId = Convert.ToInt32(dataGridView_Project.Rows[rowIndex].Cells["Column1"].Value);
            }
            Task_ task_ = new Task_(projectId);
            task_.FormClosed += (s, args) => Close();
            task_.Show();
        }
    }
}
