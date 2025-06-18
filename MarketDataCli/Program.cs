using QuickFix;
using QuickFix.Logger;
using QuickFix.Store;
using QuickFixCli;

namespace SimpleAuthCli
{
    class Program
    {
        static void Main(string[] args)
        {
            MarketMain(args);
        }

        static void MarketMain(string[] args)
        {
            Console.WriteLine("=============");
            Console.WriteLine("FIX Market Data Client");
            Console.WriteLine("=============");

            if (args.Length < 3 || args.Length > 5)
            {
                Console.WriteLine("Usage: SimpleAuthCli CONFIG_FILENAME USERNAME PASSWORD [SYMBOLS] [INCREMENTAL]");
                Console.WriteLine("Examples:");
                Console.WriteLine("  SimpleAuthCli simpleclient.cfg admin 123456");
                Console.WriteLine("  SimpleAuthCli simpleclient.cfg admin 123456 \"EURUSD,GBPUSD,USDJPY\"");
                Console.WriteLine("  SimpleAuthCli simpleclient.cfg admin 123456 \"EURUSD,GBPUSD\" true");
                Console.WriteLine("  SimpleAuthCli simpleclient.cfg admin 123456 \"\" true");
                Environment.Exit(2);
            }

            string configFile = args[0];
            string username = args[1];
            string password = args[2];

            // Parse symbols if provided
            List<string> desiredSymbols = null!;
            bool hasSymbols = false;
            if (args.Length >= 4 && !string.IsNullOrWhiteSpace(args[3]))
            {
                desiredSymbols = new List<string>(args[3].Split(','));
                Console.WriteLine($"Custom symbol list provided: {string.Join(", ", desiredSymbols)}");
                hasSymbols = true;
            }

            // Parse incremental updates flag (default to false)
            bool useIncrementalUpdates = false;
            if (args.Length == 5 || (args.Length == 4 && !hasSymbols))
            {
                string incrementalArg = args.Length == 5 ? args[4] : args[3];
                if (bool.TryParse(incrementalArg, out bool incremental))
                {
                    useIncrementalUpdates = incremental;
                }
            }

            Console.WriteLine($"Username: {username}");
            Console.WriteLine("Password has been set, but will not be displayed for security reasons.");
            Console.WriteLine($"Using incremental updates: {useIncrementalUpdates}");

            try
            {
                SessionSettings settings = new SessionSettings(configFile);
                IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
                ILogFactory logFactory = new ScreenLogFactory(settings);

                // Choose the appropriate application type
                AuthClientApp application;
                if (useIncrementalUpdates)
                {
                    application = new IncrementalMarketDataClientApp(username, password, desiredSymbols);
                    Console.WriteLine("Using incremental market data client");
                }
                else
                {
                    application = new MarketDataClientApp(username, password, desiredSymbols);
                    Console.WriteLine("Using standard market data client");
                }

                QuickFix.Transport.SocketInitiator initiator =
                    new QuickFix.Transport.SocketInitiator(application, storeFactory, settings, logFactory);

                initiator.Start();
                Console.WriteLine("Client started and connected to server, press <enter> to quit.");
                Console.Read();
                initiator.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        static void LegacyMain(string[] args)
        {
            Console.WriteLine("=============");
            Console.WriteLine("FIX Simple Authentication Client Example");
            Console.WriteLine("=============");

            if (args.Length != 3)
            {
                Console.WriteLine("Usage: SimpleAuthInitiator CONFIG_FILENAME USERNAME PASSWORD");
                System.Environment.Exit(2);
            }

            string configFile = args[0];
            string username = args[1];
            string password = args[2];

            Console.WriteLine($"Username: {username}");
            Console.WriteLine($"Password has been set, but will not be displayed for security reasons.");

            try
            {
                SessionSettings settings = new SessionSettings(configFile);
                AuthClientApp application = new AuthClientApp(username, password);
                IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
                ILogFactory logFactory = new ScreenLogFactory(settings);
                QuickFix.Transport.SocketInitiator initiator =
                    new QuickFix.Transport.SocketInitiator(application, storeFactory, settings, logFactory);

                initiator.Start();
                Console.WriteLine("Client started and connected to server, press <enter> to quit.");
                Console.Read();
                initiator.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
