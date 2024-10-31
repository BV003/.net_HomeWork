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
    public partial class ClassForm : Form
    {
        public ClassForm()
        {
            InitializeComponent();
            LoadSchoolsToComboBox(); // 加载所有学校名称到comboBox1
            LoadClasses(); // 加载所有班级数据到DataGridView
        }

        private void LoadSchoolsToComboBox()
        {
            DataTable schools = DatabaseHelper.GetAllSchools(); // 从数据库获取所有学校
            comboBox1.DataSource = schools;
            comboBox1.ValueMember = "SchoolId"; // 设置SchoolId作为实际值
            comboBox1.DisplayMember = "SchoolName"; // 设置SchoolName作为显示值
        }

        private void LoadClasses()
        {
            DataTable classes = DatabaseHelper.GetAllClasses(); // 从数据库获取所有班级
            dataGridView1.DataSource = classes; // 将数据绑定到DataGridView
        }

        private void ClassForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string className = textBox1.Text; // 假设 txtClassName 是班级名称的 TextBox
            if (string.IsNullOrWhiteSpace(className))
            {
                MessageBox.Show("班级名称不能为空。");
                return;
            }

            // 获取选中的学校ID
            var selectedSchoolId = comboBox1.SelectedValue;
            if (selectedSchoolId == null)
            {
                MessageBox.Show("请选择一个学校。");
                return;
            }

            int schoolId = Convert.ToInt32(selectedSchoolId);

            // 调用添加班级的方法
            int result = DatabaseHelper.AddClass(className, schoolId);
            if (result > 0)
            {
                MessageBox.Show("班级添加成功！");
                LoadClasses(); // 刷新班级列表
            }
            else
            {
                MessageBox.Show("班级添加失败。");
            }
            DatabaseHelper.InsertLog("Add", $"Added class with ID {result}.");
        }

        private void button2_Click(object sender, EventArgs e)
        {

                // 获取DataGridView中选中的行
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("请选择要删除的班级。");
                    return;
                }

                // 假设ClassId是DataGridView的第一列
                var selectedRow = dataGridView1.SelectedRows[0];
                int classId = Convert.ToInt32(selectedRow.Cells["ClassId"].Value);
                if (classId == 0)
                {
                    MessageBox.Show("无法识别班级ID。");
                    return;
                }

                // 调用DatabaseHelper删除班级
                int result = DatabaseHelper.DeleteClass(classId);
                if (result > 0)
                {
                    MessageBox.Show("班级删除成功！");
                    LoadClasses(); // 刷新DataGridView显示最新数据
                }
                else
                {
                    MessageBox.Show("班级删除失败或未找到该班级。");
                }
            // 在 DeleteClass 方法中
            DatabaseHelper.InsertLog("Delete", $"Deleted class with ID {classId}.");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 获取DataGridView中选中的行
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要重命名的班级。");
                return;
            }

            // 假设ClassId是DataGridView的第一列
            var selectedRow = dataGridView1.SelectedRows[0];
            int classId = Convert.ToInt32(selectedRow.Cells["ClassId"].Value);
            if (classId == 0)
            {
                MessageBox.Show("无法识别班级ID。");
                return;
            }

            // 获取新的班级名称
            string newClassName = textBox1.Text; // 假设 textBox1 是班级名称的 TextBox
            if (string.IsNullOrWhiteSpace(newClassName))
            {
                MessageBox.Show("班级名称不能为空。");
                return;
            }

            // 调用DatabaseHelper更新班级名称
            int result = DatabaseHelper.UpdateClassName(classId, newClassName);
            if (result > 0)
            {
                MessageBox.Show("班级重命名成功！");
                LoadClasses(); // 刷新DataGridView显示最新数据
            }
            else
            {
                MessageBox.Show("班级重命名失败。");
            }
            // 在 UpdateClassName 方法中
            DatabaseHelper.InsertLog("Update", $"Updated class with ID {classId} to name {newClassName}.");
        }
    }
}
