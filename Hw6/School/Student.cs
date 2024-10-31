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
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
            LoadClassesToComboBox(); // 加载所有班级名称到comboBoxClasses
            LoadStudents(); // 加载所有学生数据到DataGridView
        }
        private void LoadClassesToComboBox()
        {
            DataTable classes = DatabaseHelper.GetAllClasses(); // 从数据库获取所有班级
            comboBox1.DataSource = classes;
            comboBox1.ValueMember = "ClassId"; // 设置ClassId作为实际值
            comboBox1.DisplayMember = "ClassName"; // 设置ClassName作为显示值
        }

        private void LoadStudents()
        {
            DataTable students = DatabaseHelper.GetAllStudents(); // 从数据库获取所有学生
            dataGridView1.DataSource = students; // 将数据绑定到DataGridView
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Student_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string studentName = textBox1.Text; // 获取输入的学生名称
            if (string.IsNullOrWhiteSpace(studentName))
            {
                MessageBox.Show("学生名称不能为空。");
                return;
            }
            int classId = Convert.ToInt32(comboBox1.SelectedValue);
            if (classId == 0)
            {
                MessageBox.Show("请选择一个班级。");
                return;
            }

            int result = DatabaseHelper.AddStudent(studentName, classId); // 添加学生
            if (result > 0)
            {
                MessageBox.Show("学生添加成功！");
                LoadStudents(); // 刷新DataGridView显示最新数据
            }
            else
            {
                MessageBox.Show("学生添加失败。");
            }

            // 在 AddStudent 方法中
            DatabaseHelper.InsertLog("Add", $"Added student with ID {result}.");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedRows = dataGridView1.SelectedRows;
            if (selectedRows.Count == 0)
            {
                MessageBox.Show("请选择要删除的学生。");
                return;
            }

            int studentId = Convert.ToInt32(selectedRows[0].Cells["StudentId"].Value);
            if (studentId == 0)
            {
                MessageBox.Show("无法识别学生ID。");
                return;
            }

            int result = DatabaseHelper.DeleteStudent(studentId); // 删除学生
            if (result > 0)
            {
                MessageBox.Show("学生删除成功！");
                LoadStudents(); // 刷新DataGridView显示最新数据
            }
            else
            {
                MessageBox.Show("学生删除失败或未找到该学生。");
            }
            // 在 DeleteStudent 方法中
            DatabaseHelper.InsertLog("Delete", $"Deleted student with ID {studentId}.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var selectedRows = dataGridView1.SelectedRows;
            if (selectedRows.Count == 0)
            {
                MessageBox.Show("请选择要重命名的学生。");
                return;
            }

            int studentId = Convert.ToInt32(selectedRows[0].Cells["StudentId"].Value);
            if (studentId == 0)
            {
                MessageBox.Show("无法识别学生ID。");
                return;
            }

            string newStudentName = textBox1.Text; // 假设 textBoxName 是学生名称的 TextBox
            if (string.IsNullOrWhiteSpace(newStudentName))
            {
                MessageBox.Show("学生名称不能为空。");
                return;
            }

            int result = DatabaseHelper.UpdateStudentName(studentId, newStudentName); // 更新学生名称
            if (result > 0)
            {
                MessageBox.Show("学生重命名成功！");
                LoadStudents(); // 刷新DataGridView显示最新数据
            }
            else
            {
                MessageBox.Show("学生重命名失败。");
            }

            // 在 UpdateStudentName 方法中
            DatabaseHelper.InsertLog("Update", $"Updated student with ID {studentId} to name {newStudentName}.");
        }
    }
}
