using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Bonus
{
    public class ClientMenu
    {
        //User menu Interface
        public static int clientMenuChoice()
        {
            int clientChoice = 0;
            bool clientInputCheck = false;

            while (!clientInputCheck)
            {
                Console.WriteLine();
                Console.WriteLine("Select one of the following activities:");
                Console.WriteLine();
                Console.WriteLine("1. Deposit ...");
                Console.WriteLine("2. Withdraw ...");
                Console.WriteLine("3. Transfer ...");
                Console.WriteLine("4. Account Activity Enquiry ...");
                Console.WriteLine("5. Balance Enquiry ...");
                Console.WriteLine("6. Exit");
                Console.WriteLine();
                Console.Write("Enter your Selection (1 to 6): ");
                clientChoice = int.Parse(Console.ReadLine());

                if (clientChoice < 1 || clientChoice > 6)
                {
                    Console.WriteLine("Please select from number between 1 to 6");
                }
                else { clientInputCheck = true; }
            }

            return clientChoice;
        }

        public static void clientDeposit(Client client)
        {
            int accountType = 0;
            bool clientInputCheck = false;

            while (!clientInputCheck)
            {
                Console.Write("Select account (1 - Checking Account, 2 - Saving Account): ");


                accountType = int.Parse(Console.ReadLine());

                bool clienctSelection = checkingSavingValidaion(accountType);
                if (clienctSelection != true)
                {
                    clientInputCheck = false;
                }
                else
                {
                    clientInputCheck = true;
                }


            }

            Console.WriteLine();
            Console.Write("Enter Amount: ");

            double amount = double.Parse(Console.ReadLine());

            if (accountType == 1)
            {
                client.checkingAccount.deposit(amount);
                Console.WriteLine();
                Console.WriteLine("\t Deposit Completed, account current balance: " + client.checkingAccount.balance.ToString("0.00"));
                Console.WriteLine();

            }
            else
            {
                client.savingAccount.deposit(amount);
                Console.WriteLine();
                Console.WriteLine("\t Deposit Completed, account current balance: " + client.savingAccount.balance.ToString("0.00"));
                Console.WriteLine();
            }
        }

        public static void clientWithdraw(Client client)
        {
            int accountType = 0;
            bool clientInputCheck = false;

            while (!clientInputCheck)
            {
                Console.Write("Select account (1 - Checking Account, 2 - Saving Account): ");

                accountType = int.Parse(Console.ReadLine());

                bool clienctSelection = checkingSavingValidaion(accountType);
                if (clienctSelection != true)
                {
                    clientInputCheck = false;
                }
                else
                {
                    clientInputCheck = true;
                }


            }
            Console.WriteLine();
            Console.Write("Enter Amount: ");

            double amount = double.Parse(Console.ReadLine());

            if (accountType == 1)
            {
                client.checkingAccount.withdraw(amount);
            }
            else
            {
                client.savingAccount.withdraw(amount);
            }
        }

        public static void clientTransfer(Client client)
        {
            int accountFrom = 0;
            int accountTo = 0;
            bool clientInputCheck = false;
            bool transferCheck = false;
            while (!transferCheck)
            {
                //transfer from
                while (!clientInputCheck)
                {
                    Console.Write("Select account to transfer from (1 - Checking Account, 2 - Saving Account): ");

                    accountFrom = int.Parse(Console.ReadLine());

                    bool clienctSelection = checkingSavingValidaion(accountFrom);
                    if (clienctSelection != true)
                    {
                        clientInputCheck = false;
                    }
                    else
                    {
                        clientInputCheck = true;
                    }
                }
                //to re-use the same boolean variable for while loop for asking user which account to "trasnfer to"
                clientInputCheck = false;

                //transfer to
                while (!clientInputCheck)
                {
                    Console.Write("Select account to transfer to (1 - Checking Account, 2 - Saving Account): ");

                    accountTo = int.Parse(Console.ReadLine());

                    bool clienctSelection = checkingSavingValidaion(accountTo);
                    if (clienctSelection != true)
                    {
                        clientInputCheck = false;
                    }
                    else
                    {
                        clientInputCheck = true;
                    }
                }
                // Validate so From and To account are not the same account type
                if (accountFrom == accountTo)
                {
                    Console.WriteLine();
                    Console.WriteLine("You cannot transfer within the same account");
                    clientInputCheck = false;
                    break;
                }
                else
                {
                    clientInputCheck = true;
                }

                //
                Account fromAccount, toAccount;

                // 1 == checking account 2 == saving account
                if (accountFrom == 1)
                {
                    fromAccount = client.checkingAccount;
                }
                else { fromAccount = client.savingAccount; }

                if (accountTo == 1)
                {
                    toAccount = client.checkingAccount;
                }
                else { toAccount = client.savingAccount; }

                Console.WriteLine();
                Console.Write("Enter Amount: ");

                double amount = double.Parse(Console.ReadLine());

                if (accountFrom == 1)
                {
                    if (fromAccount.transferBalanceCheck(amount) == true)
                    {
                        fromAccount.transfer(amount, toAccount);
                        Console.WriteLine();
                        Console.WriteLine("\t Transfer Completed, current saving account balance: " + client.savingAccount.balance.ToString("0.00"));
                        transferCheck = true;
                    }
                    else
                    {
                        Console.WriteLine("You have insufficient balance to perform this transaction");
                        Console.WriteLine("Your current balance for Checking Account is $" + fromAccount.balance.ToString("0.00"));
                        transferCheck = true;
                    }

                }

                if (accountFrom == 2)
                {
                    if (fromAccount.transferBalanceCheck(amount) == true)
                    {
                        fromAccount.transfer(amount, toAccount);

                        Console.WriteLine();
                        Console.WriteLine("\t Transfer Completed, current checking account balance: " + client.checkingAccount.balance.ToString("0.00"));
                        transferCheck = true;
                    }
                    else
                    {
                        Console.WriteLine("You have insufficient balance to perform this transaction");
                        Console.WriteLine("Your current balance for Saving Account is $" + fromAccount.balance.ToString("0.00"));
                        transferCheck = true;
                    }

                }
            }




        }
        // simple validation check for user input
        public static bool checkingSavingValidaion(int clientChoice)
        {
            if (clientChoice < 1 || clientChoice > 2)
            {
                Console.WriteLine("Please select between 1. Checking account 2. Saving account");
                return false;
            }
            else { return true; }
        }
    }
}
