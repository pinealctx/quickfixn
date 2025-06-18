using QuickFix;
using QuickFix.Logger;
using QuickFix.Store;
using QuickFixSrv;

namespace SimpleAuthSrv
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
            Console.WriteLine("Market Data Server Example");
            Console.WriteLine("=============");

            if (args.Length < 4 || args.Length > 5)
            {
                Console.WriteLine("Usage: SimpleAuthSrv CONFIG_FILENAME USERNAME PASSWORD SYMBOLS_WITH_PRICES [INCREMENTAL]");
                Console.WriteLine("Example: SimpleAuthSrv simpleacc.cfg admin 123456 \"EURUSD:1.1,GBPUSD:1.3,USDJPY:150.5\"");
                Console.WriteLine("Example with incremental updates: SimpleAuthSrv simpleacc.cfg admin 123456 \"EURUSD:1.1,GBPUSD:1.3,USDJPY:150.5\" true");
                Environment.Exit(2);
            }

            string configFile = args[0];
            string username = args[1];
            string password = args[2];
            string symbolsWithPrices = args[3];

            // Parse incremental updates flag (default to false)
            bool useIncrementalUpdates = false;
            if (args.Length == 5 && bool.TryParse(args[4], out bool incremental))
            {
                useIncrementalUpdates = incremental;
            }

            Console.WriteLine($"Setting username: {username}");
            Console.WriteLine($"Supported symbols and prices: {symbolsWithPrices}");
            Console.WriteLine($"Using incremental updates: {useIncrementalUpdates}");

            // Use either incremental or standard app based on the flag
            IDisposable app = null!;
            try
            {
                SessionSettings settings = new SessionSettings(configFile);
                IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
                ILogFactory logFactory = new FileLogFactory(settings);
                IAcceptor acceptor;

                if (useIncrementalUpdates)
                {
                    var incrementalApp = new IncrementalMarketDataAcceptorApp(username, password, symbolsWithPrices);
                    app = incrementalApp;
                    acceptor = new ThreadedSocketAcceptor(incrementalApp, storeFactory, settings, logFactory);
                }
                else
                {
                    var standardApp = new MarketDataAcceptorApp(username, password, symbolsWithPrices);
                    app = standardApp;
                    acceptor = new ThreadedSocketAcceptor(standardApp, storeFactory, settings, logFactory);
                }

                acceptor.Start();
                Console.WriteLine("Server started and accepting connections, press <enter> to quit.");
                Console.Read();
                acceptor.Stop();
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"A fatal error occurred: {e}");
            }
            finally
            {
                app?.Dispose();
            }
        }

        static void LegacyMain(string[] args)
        {
            Console.WriteLine("=============");
            Console.WriteLine("FIX Simple Authentication Server Example");
            Console.WriteLine("=============");

            if (args.Length != 3)
            {
                Console.WriteLine("Usage: SimpleAuthSrv CONFIG_FILENAME USERNAME PASSWORD");
                System.Environment.Exit(2);
            }

            string configFile = args[0];
            string username = args[1];
            string password = args[2];

            Console.WriteLine($"Setting username: {username}");
            Console.WriteLine($"Password has been set, but will not be displayed for security reasons.");

            try
            {
                SessionSettings settings = new SessionSettings(configFile);
                AuthAcceptorApp app = new AuthAcceptorApp(username, password);
                IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
                ILogFactory logFactory = new FileLogFactory(settings);
                IAcceptor acceptor = new ThreadedSocketAcceptor(app, storeFactory, settings, logFactory);

                acceptor.Start();
                Console.WriteLine("Server started and accepting connections, press <enter> to quit.");
                Console.Read();
                acceptor.Stop();
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"A fatal error occurred: {e}");
            }
        }
    }
}
