using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;
using System.Text;

namespace QuickFixCli
{
    public class AuthClientApp : MessageCracker, IApplication
    {
        private readonly string _username;
        private readonly string _password;

        // Session ID for sending messages
        protected SessionID? _sessionId;
        protected bool _isLoggedIn = false;

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
            _sessionId = sessionID;
            _isLoggedIn = true;
            Console.WriteLine($"Logged in: {sessionID}");
        }

        public void OnLogout(SessionID sessionID)
        {
            _sessionId = null;
            _isLoggedIn = false;
            Console.WriteLine($"Logged out: {sessionID}");
        }

        public void FromAdmin(QuickFix.Message message, SessionID sessionID)
        {
            // Print full message with checksum (raw format and readable format)
            PrintFullMessage("RECV ADMIN", message);

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
            // Add login credentials to the Logon message
            if (message.Header.GetString(Tags.MsgType) == Logon.MsgType)
            {
                // Convert message to Logon type
                Logon logon = (Logon)message;
                logon.SetField(new Username(_username));
                logon.SetField(new Password(_password));
                Console.WriteLine($"Added credentials to Logon message: Username={_username}");
            }

            // Print full message with checksum (raw format and readable format)
            PrintFullMessage("SEND ADMIN", message);
        }

        public void FromApp(QuickFix.Message message, SessionID sessionID)
        {
            // Print full message with checksum (raw format and readable format)
            PrintFullMessage("RECV APP", message);

            // Using Message Cracker to handle specific app message types
            try
            {
                Crack(message, sessionID);
            }
            catch (UnsupportedMessageType)
            {
                Console.WriteLine($"Unsupported message type: {message.Header.GetString(Tags.MsgType)}");
            }
        }

        public void ToApp(QuickFix.Message message, SessionID sessionID)
        {
            // Print full message with checksum (raw format and readable format)
            PrintFullMessage("SEND APP", message);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Prints a message in both human-readable and raw format with full checksum details
        /// </summary>
        protected void PrintFullMessage(string direction, QuickFix.Message message)
        {
            // Get the complete message with proper body length and checksum
            string rawMessage = message.ConstructString();

            // Create human-readable version with SOH replaced by pipe
            string readableMessage = rawMessage.Replace(QuickFix.Message.SOH, '|');

            // Create hex representation to see all bytes including SOH
            StringBuilder hexBuilder = new StringBuilder();
            foreach (byte b in Encoding.ASCII.GetBytes(rawMessage))
            {
                hexBuilder.Append($"{b:X2} ");
            }

            Console.WriteLine();
            Console.WriteLine($"===== {direction} MESSAGE =====");
            Console.WriteLine($"Readable: {readableMessage}");
            // Console.WriteLine($"Raw Hex: {hexBuilder}");
            Console.WriteLine("=======================");
        }

        public void SendMessage(QuickFix.Message message)
        {
            if (_sessionId == null)
            {
                Console.WriteLine($"Warning: Session ID is null, cannot send message");
                return;
            }
            Session.SendToTarget(message, _sessionId);
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
            Console.WriteLine($"Received Reject message for session: {sessionID}");
            if (reject.IsSetText())
            {
                Console.WriteLine($"  Reject reason: {reject.Text.Value}");
            }
        }

        #endregion
    }
}
