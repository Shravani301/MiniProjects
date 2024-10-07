using AccountLibrary.Exceptions;
using AccountLibrary.Models;
using AccountLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountLibrary.Controller
{
    public class AccountManager
    {
        static List<Account> accounts = new List<Account>();

        public double MINIMUM_BALANCE { get; private set; }

        public AccountManager()
        {
            accounts = Serialization.Deserialze();

        }
        public string Deposit(Account account, double amount)
        {
            if (amount > 50000)
            {

                throw new DepositLimitException("Deposit Limit exceeded, You can deposit at a time only 50000");
            }
            return account.DepositAmount(amount);
        }
        public static Account GetAccount(int accId)
        {
            var account= accounts.Where(accounts => accounts.AccountId == accId).FirstOrDefault();
            if (account == null)
                throw new AccountNotFoundException("No such Account exist");
            return account;
        }
        public void Withdraw(Account account, double amount)
        {
            
            if (account.AccountBalance - amount <= MINIMUM_BALANCE)
                    throw new MinimumBalanceException("Insufficient Balance");
            account.WithdrawAmount(amount);
            
        }

        public void Create(int accId, string name, string bankName, double openingBalance)
        {
            accounts.Add(new Account(accId, name, bankName, openingBalance));
        }

       public List<Account> DisplayAccounts()
        {
            if (accounts.Count == 0)
                {
                    throw new ZeroAccountsException("Zero Accounts exist");
                }

            return accounts;

        }
        public string SerializeAccount()
        {
            return Serialization.Serialize(accounts);
        }

    }
}
