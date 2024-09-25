using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Principal;

namespace ATMSimulation
{
    public class Bank
    {
        private List<Account> accounts;
        private string bankname;

        public Bank(string name)
        {
            accounts = new List<Account>();
            bankname = name;
        }

        public void AddAccount(Account account)
        {
            accounts.Add(account);
        }

        public List<Account> GetAccounts()
        {
            return accounts;
        }
        public string GetBankname()
        {
            return bankname;
        }

        public Account GetAccount(string accountNumber, string password)
        {
            // 查找匹配的账号和密码
            return accounts.FirstOrDefault(a => a.AccountNumber == accountNumber && a.AccountPassword == password);
        }
    }
}
