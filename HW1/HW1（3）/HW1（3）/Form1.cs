namespace HW1_3_
{
    public partial class Form1 : Form
    {
        private int score = 0;
        public int idx = 0;//答题的道数
        private int maxQuestions = 10;
        public int curans = 0;
        public int userans = 0;
        public int time = 10;
        private int currentQuestion = 0;
        private int timePerQuestion = 60; // 秒
        private Random random = new Random();


        //计算出题目与相应的正确答案
        public void Makeup()
        {
            int num1 = random.Next(1, 10); // 生成1到9之间的随机数
            int num2 = random.Next(1, 10); // 生成1到9之间的随机数
            string operation;
            // 随机选择加法或减法
            if (random.Next(2) == 0)
            {
                operation = "+";
            }
            else
            {
                operation = "-";
            }
            int correctAnswer;
            if (operation == "+")
            {
                correctAnswer = num1 + num2;
            }
            else
            {
                // 确保减法的结果为正数
                if (num1 < num2)
                {
                    int temp = num1;
                    num1 = num2;
                    num2 = temp;
                }
                correctAnswer = num1 - num2;
            }
            // 构建题目字符串
            label2.Text = $"{num1} {operation} {num2}";
            curans = correctAnswer;
        }
        //判断正确与否，正确加分
        public void judge()
        {
            GetUserAnswer();
            if (userans == curans)
            {
                score = score + 10;
                label8.Text = "回答正确";
            }
            else
            {
                label8.Text = "回答错误";
            }
            label5.Text = Convert.ToString(score);

        }
        public void GetUserAnswer()
        {
            bool isEmpty = string.IsNullOrEmpty(textBox1.Text);
            if (isEmpty)
            {
                userans = -10;
            }
            else
            {
                userans = int.Parse(textBox1.Text);
            }

        }

        //time转字符串
        public void trans(int time)
        {
            string timeStr = time.ToString();
            // 现在你可以使用 timeStr 变量，它包含了 time 的字符串表示
            label7.Text = timeStr;
        }


        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 10000;//10s答题时间
            timer1.Start();
            timer2.Interval = 1000;//1s时间
            timer2.Start();
            Makeup();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            idx++;
            Makeup();
            time = 10;
            trans(time);
            label8.Text = "未回答";
            if (idx == 10)
            {
                Form2 form2 = new Form2(Convert.ToString(score));
                // 显示第二个窗体
                form2.Show();
            }
        }

        //提交
        private void button2_Click(object sender, EventArgs e)
        {
            judge();
            textBox1.Text = "";
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            judge();
            textBox1.Text = "";
            idx++;
            Makeup();
            time = 10;
            trans(time);
            label8.Text = "未回答";
            if (idx == 10)
            {
                Form2 form2 = new Form2(Convert.ToString(score));
                // 显示第二个窗体
                form2.Show();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            time--;
            trans(time);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
