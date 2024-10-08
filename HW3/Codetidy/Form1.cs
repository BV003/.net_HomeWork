using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Codetidy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // 计算行数的方法
        private int CountLines(string content)
        {
            return content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).Length;
        }

        // 计算单词数的方法
        private int CountWords(string content)
        {
            string[] words = Regex.Split(content, @"[\s\.,!?;:'""]+");
            int wordCount = 0;
            foreach (var word in words)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    wordCount++;
                }
            }
            return wordCount;
        }

        // 删除空行和注释的方法
        private string RemoveEmptyLinesAndComments(string content)
        {
            string[] lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            StringBuilder sb = new StringBuilder();
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && !line.TrimStart().StartsWith("//"))
                {
                    sb.AppendLine(line);
                }
            }
            return sb.ToString();
        }

        // 统计单词出现次数的方法
        private Dictionary<string, int> CountWordFrequencies(string content)
        {
            Dictionary<string, int> wordCounts = new Dictionary<string, int>();
            string[] words = Regex.Split(content, @"[\s\.,!/**/?;:'={}""]+");
            foreach (var word in words)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    if (wordCounts.ContainsKey(word))
                    {
                        wordCounts[word]++;
                    }
                    else
                    {
                        wordCounts[word] = 1;
                    }
                }
            }
            return wordCounts;
        }

        // 更新单词列表的方法
        private void UpdateWordList(Dictionary<string, int> wordCounts)
        {
            listBox1.Items.Clear();
            foreach (var pair in wordCounts)
            {
                listBox1.Items.Add($"{pair.Key}: {pair.Value}");
            }
        }
        private void ProcessFile(string filePath)
        {
            string fileContent = File.ReadAllText(filePath);
            int originalLineCount = CountLines(fileContent);
            int originalWordCount = CountWords(fileContent);

            // 显示原始统计数据
            textBox1.Text = originalLineCount.ToString();
            textBox2.Text = originalWordCount.ToString();

            // 删除空行和注释
            string formattedContent = RemoveEmptyLinesAndComments(fileContent);
            int formattedLineCount = CountLines(formattedContent);
            int formattedWordCount = CountWords(formattedContent);

            File.WriteAllText(filePath, formattedContent);
            // 显示格式化后的统计数据
            textBox3.Text = formattedLineCount.ToString();
            textBox4.Text = formattedWordCount.ToString();

            // 统计单词出现次数
            Dictionary<string, int> wordCounts = CountWordFrequencies(formattedContent);
            UpdateWordList(wordCounts);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "C# files (*.cs)|*.cs";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                ProcessFile(filePath);
            }
        }
    }
}
