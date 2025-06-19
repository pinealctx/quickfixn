using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;

namespace QuickFixCli
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

        // Property to get _username
        public string Username
        {
            get { return _username; }
        }

        #region IApplication Members

        public virtual void OnCreate(SessionID sessionID)
        {
            Console.WriteLine($"Session created: {sessionID}");
        }

        public virtual void OnLogon(SessionID sessionID)
        {
            Console.WriteLine($"Logged in: {sessionID}");
        }

        public virtual void OnLogout(SessionID sessionID)
        {
            Console.WriteLine($"Logged out: {sessionID}");
        }

        public virtual void FromAdmin(QuickFix.Message message, SessionID sessionID)
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

        public virtual void ToAdmin(QuickFix.Message message, SessionID sessionID)
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

        public virtual void FromApp(QuickFix.Message message, SessionID sessionID)
        {
            Console.WriteLine("Received application message: " + message + " from session: " + sessionID);

            // Using Message Cracker to handle specific app message types
            Crack(message, sessionID);
        }

        public virtual void ToApp(QuickFix.Message message, SessionID sessionID)
        {
            Console.WriteLine($"Sending application message: {message} to session: {sessionID}");
        }

        #endregion

        #region Message Handlers

        // Handle Logon messages
        public virtual void OnMessage(Logon logon, SessionID sessionID)
        {
            Console.WriteLine("Received Logon message from session: " + sessionID);
        }

        // Handle Heartbeat messages
        public virtual void OnMessage(Heartbeat heartbeat, SessionID sessionID)
        {
            Console.WriteLine($"Received Heartbeat message from session: {sessionID}");
        }

        // Handle Reject messages
        public virtual void OnMessage(Reject reject, SessionID sessionID)
        {
            Console.WriteLine($"Received Reject message: {reject.Text.Value} for session: {sessionID}");
        }

        #endregion
    }
}
