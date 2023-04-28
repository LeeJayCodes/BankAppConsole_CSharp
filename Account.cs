using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bonus
{
    public class Account
    {
        public double balance { get; set; }
        public List<Transaction> transactions = new List<Transaction>();
        public string typeOfAccount { get; set; }

        //Deposit
        public virtual void deposit(double amount, string activity = "DEPOSIT")
        {
            balance += amount;
            transactions.Add(new Transaction(amount, activity));
        }

        //Withdraw
        public virtual void withdraw(double amount, string activity = "WITHDRAW")
        {
            if (amount <= balance)
            {
                balance -= amount;
                transactions.Add(new Transaction(amount, activity));
            }
        }

        // Transfer
        public void transferIn(double amount, string activity = "TRANSFER: Transfer in")
        {
            balance += amount;
            transactions.Add(new Transaction(amount, activity));
        }

        public virtual void transferOut(double amount, string activity = "TRANSFER: Transfer out")
        {
            if (amount <= balance)
            {
                balance -= amount;
                transactions.Add(new Transaction(amount, activity));
            }

        }

        //Initially, simply used deposit method to transfer funds to account.
        //However, because deposit to saving account would result in increased balance by intereste rate * deposited amount,
        //I had to create separate class method to transfer funds in to the saving account

        public void transfer(double amount, Account toAccount)
        {
                transferOut(amount);
                toAccount.transferIn(amount);
        }

        public virtual bool transferBalanceCheck(double amount)
        {
            if (amount > balance)
            {
                return false;
            }
            return true;
        }

        //Printing account activities
        public void printActivity()
        {
            Console.WriteLine(typeOfAccount + " Account:");
            Console.WriteLine();
            Console.WriteLine("\t\t Amount \t\t\t Date \t\t\t Activity");
            Console.WriteLine("\t\t ------ \t\t\t ---- \t\t\t --------");

            foreach (Transaction activity in transactions)
            {
                Console.WriteLine($"\t\t {activity.amount} \t\t\t {activity.date} \t\t\t {activity.activity}");
            }
        }
    }

    public class CheckingAccount : Account
    {
        private static double dailyWithdrawCap = 300.0;
        
        public CheckingAccount()
        {
            typeOfAccount = "Checking";
        }

        public override void withdraw(double amount, string activity)
        {
            if (amount <= balance && amount <= dailyWithdrawCap && dailyWithdrawAvailability(amount) == true)
            {
                base.withdraw(amount);
                Console.WriteLine();
                Console.WriteLine("\t Withdraw Completed, current checking account balance: " + base.balance.ToString("0.00"));
            }
            else if (amount > balance)
            {
                Console.WriteLine("Insufficient funds. Your current balance is " + base.balance.ToString("0.00"));
            }
            else
            {
                Console.WriteLine("Exceeded daily max withdraw amount $" + dailyWithdrawCap.ToString());
            }
        }

        public bool dailyWithdrawAvailability(double amount) 
        {
            double totalAmount = amount;
            foreach ( Transaction transaction in transactions)
            {
                if (transaction.activity.ToUpper().Contains("WITHDRAW"))
                { 
                    totalAmount += transaction.amount;
                }
            }
            if (totalAmount <= dailyWithdrawCap)
            {
                return true;
            }
            else { return false; }
            
        }
       
    }
    public class SavingAccount : Account
    {
        private static double interestRate { get; } = 0.03;
        private static double penaltyAmount { get; } = 10;

        public SavingAccount()
        {
            typeOfAccount = "Saving";
        }

        public override void withdraw(double amount, string activity)
        {
            if (amount <= balance)
            {
                base.withdraw(amount, activity);
                base.withdraw(penaltyAmount, "Penalty");
                Console.WriteLine();
                Console.WriteLine("\t Withdraw Completed, current saving account balance: " + base.balance.ToString("0.00"));

            }
            else
            {
                Console.WriteLine("Insufficient funds. Your current balance is " + base.balance.ToString("0.00"));
            }
        }

        //Penalty applies when client transfer out funds from saving to other account
        public override void transferOut(double amount, string activity = "TRANSFER: Transfer out")
        {
            base.transferOut(amount, activity);
            base.withdraw(penaltyAmount, "Penalty");
        }
        public override bool transferBalanceCheck(double amount)
        {
            if (amount + penaltyAmount > balance)
            {
                Console.WriteLine("You have insufficient funds considering the penalty ($"+ penaltyAmount + ") for withdrawl (transfer)");
                return false;
            }
            return true;
        }
        //When user deposit funds to saving account, saving balance is also incrased by interest amount which is applicable interest rate * deposited amount
        public override void deposit(double amount, string activity)
        {
            double interest = amount * interestRate;
            base.deposit(amount, activity);
            base.deposit(interest, "DEPOSIT: Interest");
        }
    }


    public class Transaction
    {
        public DateOnly date;
        public double amount;
        public string activity;

        public Transaction(double amount, string activity)
        {
            this.date = DateOnly.FromDateTime(DateTime.Now);
            this.amount = amount;
            this.activity = activity;

        }
    }
}
