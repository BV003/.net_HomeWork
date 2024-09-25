namespace ATMSimulation
{
    public partial class Form1 : Form
    {
        private List<Bank> banks;
        public Form1()
        {
            InitializeComponent();
            InitializeBankData();
        }

        private void InitializeBankData()
        {
            banks = new List<Bank>();

            // 创建第一个银行
            Bank bank1 = new Bank("花旗银行");
            bank1.AddAccount(new CreditAccount("1", "1", 20000,10000000));
            bank1.AddAccount(new Account("654321", "password2", 15000));

            // 创建第二个银行
            Bank bank2 = new Bank("JP摩根");
            bank2.AddAccount(new Account("111222", "password3", 30000));
            bank2.AddAccount(new Account("333444", "password4", 10000));

            // 将银行添加到列表中
            banks.Add(bank1);
            banks.Add(bank2);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedBankName = comboBox1.SelectedItem?.ToString();
            string accountNumber = textBox1.Text; // 假设有一个文本框 txtAccountNumber
            string password = textBox2.Text; // 假设有一个文本框 txtPassword

            // 查找对应的银行
            Bank selectedBank = banks.Find(b => b.GetBankname() == selectedBankName);

            if (selectedBank != null)
            {
                // 验证账户和密码
                Account account = selectedBank.GetAccount(accountNumber, password); // 假设你有 GetAccount 方法

                if (account != null)
                {
                    MessageBox.Show("登录成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form2 bankForm = new Form2(banks, selectedBankName, accountNumber, password);
                    bankForm.Show();
                }
                else
                {
                    MessageBox.Show("登录失败：账号或密码错误。", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("请选择有效的银行。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
