using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School
{
    public partial class Form1 : Form
    {
        private SchoolManagementForm schoolForm;
        private ClassForm classForm;
        private Student studentForm;
        private LogForm logsForm;


        public Form1()
        {
            InitializeComponent();
            InitializeChildForms();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void InitializeChildForms()
        {
            schoolForm = new SchoolManagementForm();
            classForm = new ClassForm();
            studentForm = new Student();
            logsForm = new LogForm();
        }

        private void 班级管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (classForm == null || classForm.IsDisposed)
            {
                // 如果窗体不存在或已被释放，则创建新的窗体实例
                classForm = new ClassForm();
            }
            // 显示窗体
            classForm.Show();
        }

        private void 学校管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (schoolForm == null || schoolForm.IsDisposed)
            {
                // 如果窗体不存在或已被释放，则创建新的窗体实例
                schoolForm = new SchoolManagementForm();
            }
            // 显示窗体
            schoolForm.Show();
        }

        private void 学生管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 检查studentForm是否已经打开
            if (studentForm == null || studentForm.IsDisposed)
            {
                // 如果窗体不存在或已被释放，则创建新的窗体实例
                studentForm = new Student();
            }
            // 显示窗体
            studentForm.Show();
        }

        private void 日志查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (logsForm == null || logsForm.IsDisposed)
            {
                // 如果窗体不存在或已被释放，则创建新的窗体实例
                logsForm = new LogForm();
            }
            // 显示窗体
            logsForm.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
