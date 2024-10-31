using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SchoolService;

namespace School
{
    public partial class SchoolManagementForm : Form
    {
        public SchoolManagementForm()
        {
            InitializeComponent();
            LoadSchools();
        }
        private void LoadSchools()
        {
            DataTable schools = DatabaseHelper.GetAllSchools(); // 从数据库获取学校数据
            dataGridView1.DataSource = schools; // 将数据绑定到DataGridView
        }

        private void SchoolManagementForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string schoolName = textBox2.Text;
            int schoolId = DatabaseHelper.AddSchool(schoolName);
            if (schoolId > 0)
            {
                MessageBox.Show("学校添加成功！");
                // 清空TextBox或执行其他操作
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("学校添加失败。");
            }
            DatabaseHelper.InsertLog("Add", $"Added school with ID {schoolId}.");
            LoadSchools();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedRows = dataGridView1.SelectedRows;
            if (selectedRows.Count == 0)
            {
                MessageBox.Show("请选择要删除的学校。");
                return;
            }

            // 假设SchoolId是第一列
            var schoolId = Convert.ToInt32(selectedRows[0].Cells["SchoolId"].Value);
            if (schoolId == 0)
            {
                MessageBox.Show("无法识别学校ID。");
                return;
            }

            // 调用DatabaseHelper删除学校
            var result = DatabaseHelper.DeleteSchool(schoolId);
            if (result > 0)
            {
                MessageBox.Show("学校删除成功！");
                LoadSchools(); // 刷新DataGridView显示最新数据
            }
            else
            {
                MessageBox.Show("学校删除失败或未找到该学校。");
            }
            DatabaseHelper.InsertLog("Delete", $"Deleted school with ID {schoolId}.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var selectedRows = dataGridView1.SelectedRows;
            if (selectedRows.Count == 0)
            {
                return; // 如果没有选中的行，则不执行任何操作
            }

            // 假设SchoolId是第一列
            var schoolId = Convert.ToInt32(selectedRows[0].Cells["SchoolId"].Value);
            if (schoolId == 0)
            {
                return; // 如果无法识别学校ID，则不执行任何操作
            }

            // 从TextBox2获取新学校名称
            string newSchoolName = textBox2.Text; // 假设您的TextBox名为txtSchoolName
            if (string.IsNullOrWhiteSpace(newSchoolName))
            {
                return; // 如果新名称为空，则不执行任何操作
            }

            // 调用DatabaseHelper更新学校名称
            var result = DatabaseHelper.UpdateSchoolName(schoolId, newSchoolName);
            if (result > 0)
            {
                LoadSchools(); // 刷新DataGridView显示最新数据
            }
            DatabaseHelper.InsertLog("Update", $"Updated school with ID {schoolId} to name {newSchoolName}.");
        }
    }
}
