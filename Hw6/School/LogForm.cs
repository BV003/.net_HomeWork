using SchoolService;
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
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
            LoadLogs(); // 在窗体加载时加载日志数据
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadLogs()
        {
            DataTable logs = DatabaseHelper.GetAllLogs(); // 从数据库获取所有日志
            dataGridView1.DataSource = logs; // 将数据绑定到DataGridView
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadLogs(); // 刷新日志数据
        }
    }
}
