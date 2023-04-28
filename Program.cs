using Bonus;

Console.Write("Enter Customer Name: ");

string clientName = Console.ReadLine();

Client client = new Client(clientName);
int clientChoice;
bool clientExit = false;

while (!clientExit)
{
    clientChoice = ClientMenu.clientMenuChoice();
    switch (clientChoice)
    {
        case 1:
            ClientMenu.clientDeposit(client);
            break;
        case 2:
            ClientMenu.clientWithdraw(client);
            break;
        case 3:
            ClientMenu.clientTransfer(client);
            break;
        case 4:
            client.clientActivity();
            break;
        case 5:
            client.printBalance();
            break;
        case 6:
            Console.WriteLine("Thank you for using our service");
            clientExit = true;
            break;
    }
}