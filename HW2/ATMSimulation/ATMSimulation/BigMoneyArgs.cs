using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMSimulation
{
    public class BigMoneyArgs : EventArgs
    {
        public Account Account { get; private set; }
        public int Amount { get; private set; }

        public BigMoneyArgs(Account account, int amount)
        {
            Account = account;
            Amount = amount;
        }
    }
}
