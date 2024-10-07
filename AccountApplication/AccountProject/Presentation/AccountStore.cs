using AccountLibrary.Controller;
using AccountLibrary.Exceptions;
using AccountLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountProject.Presentation
{
    public class AccountStore
    {
        static AccountManager manager = new AccountManager();
        public static void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("Enter your Choice");
                Console.WriteLine("1. Create account\n" +
                    "2. Deposit\n" +
                    "3. Withdraw\n" +
                    "4. Display Account Details\n" +
                    "5. Display All Accounts\n" +
                    "6. Exit");
                int choice = Convert.ToInt32(Console.ReadLine());
                DoTask(choice);
            }
        }
        public static void DoTask(int choice)
        {
            switch (choice)
            {
                case 1:
                    CreateAccount();
                    break;
                case 2:
                    DepositMoney();
                    break;
                case 3:
                    WithdrawMoney();
                    break;
                case 4:
                    ShowDetails();
                    break;
                case 5:
                    DisplayAllAccounts();
                    break;
                case 6:
                    manager.SerializeAccount();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Enter correct choice");
                    break;
            }
        }
        public static void CreateAccount()
        {
            Console.WriteLine("Enter your Account Number");
            int accId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Account Holder Name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Bank Name");
            string bankName = Console.ReadLine();
            Console.WriteLine("Enter Openeing Balance");
            double amount = Convert.ToDouble(Console.ReadLine());
            manager.Create(accId, name, bankName, amount);
        }

        public static void DepositMoney()
        {
            try
            {
                Console.WriteLine("Enter account number");
                int id = Convert.ToInt32(Console.ReadLine());
                Account account = AccountManager.GetAccount(id);//exception
                Console.WriteLine("Enter amount");
                double amount = Convert.ToDouble(Console.ReadLine());
                manager.Deposit(account, amount);//exception
            }
            catch (AccountNotFoundException ex)
            { 
                Console.WriteLine(ex.Message);
            }
            catch (DepositLimitException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.ToString());
            }


        }
        public static void WithdrawMoney()
        {
            try
            {
                Console.WriteLine("Enter account number");
                int id = Convert.ToInt32(Console.ReadLine());
                Account account = AccountManager.GetAccount(id);//exception
                Console.WriteLine("Enter amount");
                double amount = Convert.ToDouble(Console.ReadLine());
                manager.Withdraw(account, amount);//exception
            }
            catch (AccountNotFoundException ex)
            { 
                Console.WriteLine(ex.Message);
            }
            catch (MinimumBalanceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        public static void ShowDetails()
        {
            try
            {
                Console.WriteLine("Enter Account Number");
                int id = Convert.ToInt32(Console.ReadLine());
                Account account = AccountManager.GetAccount(id);//exception
                Console.WriteLine(account);//exception
            }
            catch (AccountNotFoundException ex)
            { 
                Console.WriteLine(ex.Message);
            }

        }

        public static void DisplayAllAccounts()
        {
            Console.WriteLine("Avaialble accounts are:");
            try
            {
                List<Account> accounts = manager.DisplayAccounts();
                foreach (Account account in accounts)
                {
                    Console.WriteLine(account);
                }
            }
            catch (ZeroAccountsException ex)
            { 
                Console.WriteLine(ex.Message);
            }
        }
    }
}
