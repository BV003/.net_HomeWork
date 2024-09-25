using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMSimulation
{
    public class Account
    {
        public string AccountNumber { get; private set; }
        public string AccountPassword { get; private set; }
        public int Balance { get; set; }

        public Account(string accountNumber, string accountPassword, int initialBalance)
        {
            AccountNumber = accountNumber;
            AccountPassword = accountPassword;
            Balance = initialBalance;
        }

        public void Deposit(int amount)
        {
            if (amount > 0)
            {
                Balance += amount;
            }
        }

        public virtual bool Withdraw(int amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }

        public int GetBalance()
        {
            return Balance;
        }
    }
}
