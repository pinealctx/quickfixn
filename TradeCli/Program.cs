using QuickFix;
using QuickFix.Logger;
using QuickFix.Store;
using QuickFixCli;

namespace TradeCli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=============");
            Console.WriteLine("FIX Trading Client");
            Console.WriteLine("=============");

            if (args.Length < 4)
            {
                ShowUsage();
                return;
            }

            string configFile = args[0];
            string username = args[1];
            string password = args[2];
            string command = args[3].ToLower();

            try
            {
                SessionSettings settings = new SessionSettings(configFile);
                IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
                ILogFactory logFactory = new ScreenLogFactory(settings);

                // Create the trading application
                TradingClientApp application = new TradingClientApp(username, password);

                // Create and start the initiator
                QuickFix.Transport.SocketInitiator initiator =
                    new QuickFix.Transport.SocketInitiator(application, storeFactory, settings, logFactory);

                initiator.Start();
                Console.WriteLine("Client started and connected to server");

                // Wait a short time for the connection to establish
                System.Threading.Thread.Sleep(8000);

                // Process the command
                switch (command)
                {
                    case "account":
                        // Get account info
                        if (args.Length < 5)
                        {
                            Console.WriteLine("Account command requires an account ID");
                            break;
                        }
                        string accountId = args[4];
                        Console.WriteLine($"Requesting account info for: {accountId}");
                        application.RequestAccountInfo(accountId);
                        break;

                    case "positions":
                        // Get positions
                        if (args.Length < 5)
                        {
                            Console.WriteLine("Positions command requires an account ID");
                            break;
                        }
                        accountId = args[4];
                        Console.WriteLine($"Requesting positions for account: {accountId}");
                        application.RequestPositions(accountId);
                        break;

                    case "order":
                        // Place an order
                        if (args.Length < 10)
                        {
                            Console.WriteLine("Order command requires: symbol, orderType, timeInForce, side, quantity, [price], [stopPrice]");
                            break;
                        }
                        string symbol = args[4];
                        char orderType = args[5][0]; // '1' for Market, '2' for Limit, '3' for Stop, '4' for Stop-Limit
                        char timeInForce = args[6][0]; // '1' for Day, '2' for IOC, etc.
                        char side = args[7][0]; // '1' for Buy, '2' for Sell
                        decimal quantity = decimal.Parse(args[8]);
                        decimal? price = args.Length > 9 ? decimal.Parse(args[9]) : null;
                        decimal? stopPrice = args.Length > 10 ? decimal.Parse(args[10]) : null;

                        Console.WriteLine($"Placing order: {symbol} {(side == '1' ? "BUY" : "SELL")} {quantity} @ {(price.HasValue ? price.ToString() : "Market")}");
                        application.PlaceOrder(symbol, orderType, timeInForce, side, quantity, price, stopPrice);
                        break;

                    default:
                        Console.WriteLine($"Unknown command: {command}");
                        ShowUsage();
                        break;
                }

                Console.WriteLine("Waiting for responses, press Enter to exit.");
                Console.ReadLine();
                initiator.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine(e.StackTrace);
            }
        }

        static void ShowUsage()
        {
            Console.WriteLine("Usage: TradeCli CONFIG_FILENAME USERNAME PASSWORD COMMAND [ARGUMENTS]");
            Console.WriteLine("Commands:");
            Console.WriteLine("  account ACCOUNT_ID");
            Console.WriteLine("  positions ACCOUNT_ID");
            Console.WriteLine("  order SYMBOL ORDER_TYPE TIME_IN_FORCE SIDE QUANTITY [PRICE] [STOP_PRICE]");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  TradeCli client.cfg trader1 password account ACC123");
            Console.WriteLine("  TradeCli client.cfg trader1 password positions ACC123");
            Console.WriteLine("  TradeCli client.cfg trader1 password order EURUSD 2 1 1 10000 1.1234");
            Console.WriteLine();
            Console.WriteLine("Order Types: 1=Market, 2=Limit, 3=Stop, 4=Stop-Limit");
            Console.WriteLine("Time In Force: 1=Day, 2=IOC, 3=OPG, 4=GTC, 5=GTX");
            Console.WriteLine("Side: 1=Buy, 2=Sell");
        }
    }
}
