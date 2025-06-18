using System;
using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;

namespace SimpleAuthCli
{
    public class AuthClientApp : MessageCracker, IApplication
    {
        private readonly string _username;
        private readonly string _password;

        public AuthClientApp(string username, string password)
        {
            _username = username;
            _password = password;
        }

        #region IApplication Members

        public void OnCreate(SessionID sessionID)
        {
            Console.WriteLine($"Session created: {sessionID}");
        }

        public void OnLogon(SessionID sessionID)
        {
            Console.WriteLine($"Logged in: {sessionID}");
        }

        public void OnLogout(SessionID sessionID)
        {
            Console.WriteLine($"Logged out: {sessionID}");
        }

        public void FromAdmin(QuickFix.Message message, SessionID sessionID)
        {
            Console.WriteLine($"Received admin message: {message} from session: {sessionID}");

            // Using Message Cracker to handle specific admin message types
            try
            {
                Crack(message, sessionID);
            }
            catch (UnsupportedMessageType)
            {
                Console.WriteLine($"Received unsupported admin message type: {message.GetType()}");
            }
        }

        public void ToAdmin(QuickFix.Message message, SessionID sessionID)
        {
            Console.WriteLine($"Sending admin message: {message} to session: {sessionID}");
            // Add login credentials to the Logon message
            if (message.Header.GetString(Tags.MsgType) == Logon.MsgType)
            {
                // Convert message to Logon type
                Logon logon = (Logon)message;
                logon.SetField(new Username(_username));
                logon.SetField(new Password(_password));
                Console.WriteLine($"Added credentials to Logon message: Username={_username}");
            }
        }

        public void FromApp(QuickFix.Message message, SessionID sessionID)
        {
            Console.WriteLine("Received application message: " + message + " from session: " + sessionID);

            // Using Message Cracker to handle specific app message types
            Crack(message, sessionID);
        }

        public void ToApp(QuickFix.Message message, SessionID sessionID)
        {
            Console.WriteLine($"Sending application message: {message} to session: {sessionID}");
        }

        #endregion

        #region Message Handlers

        // Handle Logon messages
        public void OnMessage(Logon logon, SessionID sessionID)
        {
            Console.WriteLine("Received Logon message from session: " + sessionID);
        }

        // Handle Heartbeat messages
        public void OnMessage(Heartbeat heartbeat, SessionID sessionID)
        {
            Console.WriteLine($"Received Heartbeat message from session: {sessionID}");
        }

        // Handle Reject messages
        public void OnMessage(Reject reject, SessionID sessionID)
        {
            Console.WriteLine($"Received Reject message: {reject.Text.Value} for session: {sessionID}");
        }

        #endregion
    }
}
