using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class TaskItem : UserControl
    {
        public delegate void OnRemoveTaskEvent(string taskName);
        public event OnRemoveTaskEvent OnRemoveTask;
        public string TaskName { get; set; }

        public TaskItem(string taskName)
        {
            InitializeComponent();
            TaskName = taskName;
            lblTaskName.Text = taskName;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (OnRemoveTask != null) OnRemoveTask(TaskName);
        }
    }
}
