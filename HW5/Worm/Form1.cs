using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace Worm
{
    public partial class Form1 : Form
    {
        private HashSet<string> phoneNumbers = new HashSet<string>();  // 存储去重后的电话号码
        private Dictionary<string, HashSet<string>> phoneNumberUrls = new Dictionary<string, HashSet<string>>();  // 电话号码及其关联的URLs
        private IWebDriver webDriver;

        public Form1()
        {
            InitializeComponent();
            // 初始化ChromeDriver
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless"); // 如果不想显示浏览器窗口，可以添加此行
            webDriver = new ChromeDriver(options);

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string keyword = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("请输入搜索关键字！");
                return;
            }

            listBox1.Items.Clear();
            phoneNumbers.Clear();
            phoneNumberUrls.Clear();
            lblPhoneCount.Text = "已爬取到电话号码: 0";  // 初始化显示

            // 进行异步搜索和网页爬取
            await SearchAndCrawlAsync(keyword);

            MessageBox.Show("爬取完成！");

        }
        private async Task SearchAndCrawlAsync(string keyword)
        {
            List<string> searchEngines = new List<string>
            {
                $"https://www.baidu.com/s?wd={keyword}"
            };

            List<Task> tasks = new List<Task>();

            foreach (var engine in searchEngines)
            {
                tasks.Add(Task.Run(() => CrawlSearchResults(engine)));
            }

            await Task.WhenAll(tasks);
        }

        // 爬取每个搜索引擎的搜索结果
        private void CrawlSearchResults(string url)
        {
            try
            {
                webDriver.Navigate().GoToUrl(url);  // 使用Selenium加载页面
                var pageSource = webDriver.PageSource;  // 获取页面源代码
                var urls = ExtractUrlsFromSearchPage(pageSource);

                foreach (var targetUrl in urls)
                {
                    CrawlPhoneNumbersFromUrl(targetUrl);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error crawling {url}: {ex.Message}");
            }
        }

        // 爬取特定URL并提取电话号码
        private void CrawlPhoneNumbersFromUrl(string url)
        {
            if (phoneNumbers.Count >= 100) return;

            try
            {
                webDriver.Navigate().GoToUrl(url);  // 使用Selenium加载页面
                var pageSource = webDriver.PageSource;  // 获取页面源代码

                var phoneNumbersInPage = ExtractPhoneNumbers(pageSource);

                lock (phoneNumbers)
                {
                    foreach (var phoneNumber in phoneNumbersInPage)
                    {
                        if (phoneNumbers.Count >= 100) break; // 如果达到100个号码，停止添加

                        if (phoneNumbers.Add(phoneNumber))
                        {
                            phoneNumberUrls[phoneNumber] = new HashSet<string> { url };  // 确保URL不重复
                            UpdatePhoneCount(phoneNumber);
                        }
                        else
                        {
                            phoneNumberUrls[phoneNumber].Add(url);  // 只添加新的URL
                        }
                    }
                }

                if (phoneNumbers.Count >= 100) return;

                Invoke(new Action(() => listBox1.Items.Add(url))); // 更新UI显示已爬取的URL
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error crawling {url}: {ex.Message}");
            }
        }

        // 更新UI中电话号码个数的显示
        private void UpdatePhoneCount(string phoneNumber)
        {
            Invoke(new Action(() =>
            {
                lblPhoneCount.Text = $"已爬取到电话号码: {phoneNumbers.Count}";
                textBox2.AppendText($"{phoneNumber}\r\n"); // 添加到 textBox2
            }));
        }

        // 提取页面中的电话号码
        private List<string> ExtractPhoneNumbers(string html)
        {
            var phoneNumbers = new List<string>();
            var regex = new Regex(@"\b\d{3,4}[-.\s]?\d{7,8}\b");  // 简单的电话号码匹配规则

            var matches = regex.Matches(html);
            foreach (Match match in matches)
            {
                phoneNumbers.Add(match.Value);
            }

            return phoneNumbers;
        }

        // 从搜索页面提取所有结果的URLs
        private List<string> ExtractUrlsFromSearchPage(string html)
        {
            var urls = new HashSet<string>(); // 使用 HashSet 来自动处理重复项
            var regex = new Regex(@"https?://[^\s""<>]+"); // 匹配以 http 或 https 开头的 URL

            // 使用正则表达式查找所有匹配的 URL
            var matches = regex.Matches(html);

            // 将匹配到的 URL 添加到 HashSet 中
            foreach (Match match in matches)
            {
                urls.Add(match.Value);
            }

            return new List<string>(urls); // 将 HashSet 转换回 List 返回
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            webDriver.Quit(); // 关闭浏览器
        }
    }

}

