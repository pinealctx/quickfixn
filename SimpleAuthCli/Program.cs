using QuickFix;
using QuickFix.Logger;
using QuickFix.Store;
using SimpleAuthCli;
using System;

namespace SimpleAuthCli
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
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
