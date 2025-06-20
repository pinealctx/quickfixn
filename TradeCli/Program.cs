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
                    case "idle":
                        break; // No action needed, just keep the client idle
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
                        if (args.Length < 9)
                        {
                            Console.WriteLine("Order command requires: symbol, orderType, timeInForce, side, quantity");
                            break;
                        }
                        string symbol = args[4];
                        char orderType = args[5][0]; // '1' for Market, '2' for Limit, '3' for Stop, '4' for Stop-Limit
                        char timeInForce = args[6][0]; // '1' for Day, '2' for IOC, etc.
                        char side = args[7][0]; // '1' for Buy, '2' for Sell
                        decimal quantity = decimal.Parse(args[8]);

                        // Optional parameters - parse in the same order as the method signature
                        decimal? price = null;
                        string? account = null;
                        decimal? stopPrice = null;

                        // Parse price if provided (arg 9)
                        if (args.Length > 9 && !string.IsNullOrWhiteSpace(args[9]))
                        {
                            price = decimal.Parse(args[9]);
                        }

                        // Parse account if provided (arg 10)
                        if (args.Length > 10 && !string.IsNullOrWhiteSpace(args[10]))
                        {
                            account = args[10];
                        }

                        // Parse stopPrice if provided (arg 11)
                        if (args.Length > 11 && !string.IsNullOrWhiteSpace(args[11]))
                        {
                            stopPrice = decimal.Parse(args[11]);
                        }

                        Console.WriteLine($"Placing order: {symbol} {(side == '1' ? "BUY" : "SELL")} {quantity}" +
                            $"{(price.HasValue ? $" @ {price}" : " @ Market")}" +
                            $"{(account != null ? $" for account {account}" : "")}" +
                            $"{(stopPrice.HasValue ? $" with stop price {stopPrice}" : "")}");

                        // Call PlaceOrder with parameters in the correct order according to the method signature
                        application.PlaceOrder(
                            symbol,
                            orderType,
                            timeInForce,
                            side,
                            quantity,
                            price,       // Optional price 
                            account,     // Optional account
                            stopPrice);  // Optional stop price
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
            Console.WriteLine("  order SYMBOL ORDER_TYPE TIME_IN_FORCE SIDE QUANTITY [PRICE] [ACCOUNT] [STOP_PRICE]");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  TradeCli client.cfg trader1 password account ACC123");
            Console.WriteLine("  TradeCli client.cfg trader1 password positions ACC123");
            Console.WriteLine("  TradeCli client.cfg trader1 password order EURUSD 2 1 1 10000");
            Console.WriteLine("  TradeCli client.cfg trader1 password order EURUSD 2 1 1 10000 1.1234");
            Console.WriteLine("  TradeCli client.cfg trader1 password order EURUSD 2 1 1 10000 1.1234 ACC123");
            Console.WriteLine("  TradeCli client.cfg trader1 password order EURUSD 2 1 1 10000 1.1234 ACC123 1.1200");
            Console.WriteLine();
            Console.WriteLine("Order Types: 1=Market, 2=Limit, 3=Stop, 4=Stop-Limit");
            Console.WriteLine("Time In Force:");
            Console.WriteLine("  1=Day (DAY) - Valid for the day only");
            Console.WriteLine("  2=IOC (IMMEDIATE_OR_CANCEL) - Execute immediately, cancel unfilled");
            Console.WriteLine("  3=OPG (AT_THE_OPENING) - Execute at the opening of market");
            Console.WriteLine("  4=GTC (GOOD_TILL_CANCEL) - Valid until explicitly cancelled");
            Console.WriteLine("  5=GTX (GOOD_TILL_CROSSING) - Valid until the next trading session");
            Console.WriteLine("Side: 1=Buy, 2=Sell");
        }
    }
}
