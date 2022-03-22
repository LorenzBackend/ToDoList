using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        public Main()
        {
            InitializeComponent();
        }

        private void AddTask()
        {
            string taskName = txtTaskName.Text;
            if (taskName.Length < 3)
            {
                MessageBox.Show("Invaild Task Name", "Add Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsTaskExist(taskName))
            {
                MessageBox.Show("Invaild Task Name", "Add Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TaskItem item = new TaskItem(taskName);
            item.OnRemoveTask += OnRemoveTask;
            TaskList.Controls.Add(item);
        }

        private void LoadTaskList(string path)
        {
            try
            {
                List<string> taskObjList = new List<string>();
                string json = File.ReadAllText(path);
                taskObjList = JsonConvert.DeserializeObject<List<string>>(json);

                TaskList.Controls.Clear();
                for (int x = 0; x < taskObjList.Count; x++)
                {
                    TaskList.Controls.Add(new TaskItem(taskObjList[x]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load TaskList", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveTaskList(string path)
        {
            try
            {
                List<string> taskObjList = new List<string>();

                foreach (TaskItem item in TaskList.Controls)
                {
                    taskObjList.Add(item.TaskName);
                }

                string json = JsonConvert.SerializeObject(taskObjList);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save TaskList", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool IsTaskExist(string taskName)
        {
            foreach (TaskItem item in TaskList.Controls)
            {
                if (item.TaskName == taskName)
                {
                    return true;
                }
            }

            return false;
        }
        private void OnRemoveTask(string taskName)
        {
            foreach(TaskItem item in TaskList.Controls)
            {
                if (item.TaskName == taskName)
                {
                    TaskList.Controls.Remove(item);
                    return;
                }
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddTask();
        }
        private void txtTaskName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddTask();
            }
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadTaskList(ofd.FileName);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SaveTaskList(sfd.FileName);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string SearchText = txtSearch.Text.ToLower();
            if (SearchText.Length < 1)
            {
                foreach (TaskItem item in TaskList.Controls)
                {
                    item.Show();
                }
            }
            else
            {
                foreach (TaskItem item in TaskList.Controls)
                {
                    if (item.TaskName.ToLower().Contains(SearchText))
                    {
                        item.Show();
                    }
                    else
                    {
                        item.Hide();
                    }
                }
            }
        }
    }
}
