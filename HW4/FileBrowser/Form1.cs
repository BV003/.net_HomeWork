using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileBrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FillRootTreeView(treeView1, @"C:\test");

        }
        private void FillRootTreeView(TreeView treeView, string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo rootdir = new DirectoryInfo(@"C:\root");
            TreeNode node = new TreeNode(rootdir.Name);
            node.Tag = dir.FullName;
            treeView.Nodes.Add(node);
            FillSubTreeView(treeView, dir.FullName, node);
        }
        private void FillSubTreeView(TreeView treeView, string path,TreeNode parent)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                TreeNode node = new TreeNode(dir.Name);
                node.Tag = dir.FullName;
                parent.Nodes.Add(node); // 将根节点添加到TreeView中

                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    TreeNode subNode = new TreeNode(subDir.Name);
                    subNode.Tag = subDir.FullName;
                    FillSubTreeView(treeView, subDir.FullName,node); // 递归填充子节点
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Error accessing directory: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }




        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listView1.Items.Clear();
            DirectoryInfo dirInfo = new DirectoryInfo(e.Node.Tag.ToString());
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                ListViewItem item = new ListViewItem(file.Name);
                item.SubItems.Add(file.Length.ToString());
                item.SubItems.Add(file.FullName); // 添加完整路径到第三列
                item.Tag = file.FullName;
                listView1.Items.Add(item);
            }
            foreach (DirectoryInfo subDir in dirInfo.GetDirectories())
            {
                ListViewItem item = new ListViewItem(subDir.Name);
                item.SubItems.Add("Folder");
                item.Tag = subDir.FullName;
                listView1.Items.Add(item);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 获取当前选中的项
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];

                // 获取文件路径
                string filePath = selectedItem.Tag.ToString();

                // 检查文件扩展名是否为.txt或.exe
                if (filePath.EndsWith(".txt", StringComparison.OrdinalIgnoreCase) || filePath.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        // 如果是.txt文件，使用记事本打开
                        if (filePath.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                        {
                            Process.Start("notepad.exe", filePath);
                        }
                        // 如果是.exe文件，直接运行
                        else if (filePath.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                        {
                            Process.Start(filePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error opening file: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("This operation is only allowed for .txt and .exe files.");
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 清除现有的节点
            treeView1.Nodes.Clear();

            // 重新加载根节点
            FillRootTreeView(treeView1, @"C:\test");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 对ListView进行排序，默认按照第一列（Filename）进行升序排序
            if (listView1.View == View.Details)
            {
                listView1.Sorting = SortOrder.Ascending;
                listView1.Sort();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (listView1.View)
            {
                case View.Details:
                    listView1.View = View.LargeIcon;
                    break;
                case View.LargeIcon:
                    listView1.View = View.SmallIcon;
                    break;
                case View.SmallIcon:
                    listView1.View = View.List;
                    break;
                case View.List:
                    listView1.View = View.Details;
                    break;
                default:
                    listView1.View = View.Details;
                    break;
            }
        }
    }
}
