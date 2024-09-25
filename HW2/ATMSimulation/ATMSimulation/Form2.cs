using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMSimulation
{
    public partial class Form2 : Form
    {
        private void Form2_BigMoneyFetched(object sender, BigMoneyArgs e)
        {
            MessageBox.Show($"警告: 账号 {e.Account.AccountNumber} 取款了大笔金额: {e.Amount} 元", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public class BadCashException : Exception
        {
            public BadCashException(string message) : base(message) { }
        }
        public event EventHandler<BigMoneyArgs> BigMoneyFetched;
        private Random random = new Random();
        public void Withdraw(Account account, int amount)
        {
            try
            {
                // 模拟坏钞的概率
                if (random.Next(100) < 30) // 30% 概率
                {
                    throw new BadCashException("取款失败，出现坏钞。");
                }
                if (account.Withdraw(amount))
                {
                    MessageBox.Show("取款成功: " + amount + " 元", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (amount > 10000)
                    {
                        OnBigMoneyFetched(new BigMoneyArgs(account, amount));
                    }
                }
                else
                {
                    MessageBox.Show("余额不足，取款失败。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (BadCashException ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected virtual void OnBigMoneyFetched(BigMoneyArgs e)
        {
            BigMoneyFetched?.Invoke(this, e);
        }


        private List<Bank> banks;
        private string selectedBankName;
        private string accountNumber;
        private string password;
        private Account account;

        public Form2(List<Bank> banks, string selectedBankName, string accountNumber, string password)
        {
            InitializeComponent();
            this.banks = banks;
            this.selectedBankName = selectedBankName;
            this.accountNumber = accountNumber;
            this.password = password;
            Bank selectedBank = banks.Find(b => b.GetBankname() == selectedBankName);
            Account taccount = selectedBank.GetAccount(accountNumber, password);
            this.account = taccount;
            // 注册事件
            this.BigMoneyFetched += Form2_BigMoneyFetched;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int balance = account.GetBalance();
            MessageBox.Show("您还剩下 " + balance + " 元", "余额查询", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 尝试将输入的金额转换为整数
            if (int.TryParse(textBox1.Text, out int amount) && amount > 0)
            {
                // 调用 Deposit 方法进行存款
                account.Deposit(amount);

                // 显示存款成功的消息
                MessageBox.Show("存款成功: " + amount + " 元", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = null;
            }
            else
            {
                // 显示输入错误的消息
                MessageBox.Show("请输入一个有效的存款金额（大于0）。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Withdraw(this.account, int.Parse(textBox1.Text));
            textBox1.Text =null;
        }
    }
}
