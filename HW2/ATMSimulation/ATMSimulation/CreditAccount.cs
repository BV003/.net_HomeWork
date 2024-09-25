using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMSimulation
{
    public class CreditAccount : Account
    {
        public int CreditLimit { get; private set; } // 改为 int

        public CreditAccount(string accountNumber, string password,int initialBalance, int creditLimit)
            : base(accountNumber, password,initialBalance)
        {
            CreditLimit = creditLimit;
        }

        public override bool Withdraw(int amount) // 改为 int
        {
            // 允许透支，如果取款金额加上当前余额不超过信用额度
            if (amount > Balance + CreditLimit)
            {
                return false; // 超过信用额度
            }
            Balance -= amount;
            return true; // 取款成功
        }

        public void SetCreditLimit(int newLimit) // 改为 int
        {
            if (newLimit >= 0)
            {
                CreditLimit = newLimit;
            }
        }

        public override string ToString()
        {
            return $"账号: {AccountNumber}, 余额: {Balance}, 信用额度: {CreditLimit}";
        }
    }
}
