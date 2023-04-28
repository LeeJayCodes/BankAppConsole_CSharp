using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonus
{
    public class Client
    {
        public string clientName;
        public Account checkingAccount;
        public Account savingAccount;

        public Client(string name)
        {
            this.clientName = name;
            checkingAccount = new CheckingAccount();
            savingAccount = new SavingAccount();
        }

        public void printBalance()
        {
            Console.WriteLine("Current balance:");
            Console.WriteLine("\t Account \t \t \t \t Balance");
            Console.WriteLine("\t ------- \t \t \t \t -------");
            Console.WriteLine("\t" + checkingAccount.typeOfAccount + "\t \t \t \t" + checkingAccount.balance);
            Console.WriteLine("\t" + savingAccount.typeOfAccount + "\t \t \t \t \t" + savingAccount.balance);
        }

        public void clientActivity()
        {
            checkingAccount.printActivity();
            savingAccount.printActivity();
        }
    }
}
