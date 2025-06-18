using QuickFix;
using QuickFix.FIX44;

namespace SimpleAuthSrv
{
    public class AuthAcceptorApp : MessageCracker, IApplication
    {
        private readonly string _validUsername;
        private readonly string _validPassword;

        public AuthAcceptorApp(string username, string password)
        {
            _validUsername = username;
            _validPassword = password;
        }

        #region IApplication Members

        public void OnCreate(SessionID sessionID)
        {
            Console.WriteLine($"Creating session: {sessionID}");
        }

        public void OnLogon(SessionID sessionID)
        {
            Console.WriteLine($"User logged in: {sessionID}");
        }

        public void OnLogout(SessionID sessionID)
        {
            Console.WriteLine($"User logged out: {sessionID}");
        }

        public void FromAdmin(QuickFix.Message message, SessionID sessionID)
        {
            Console.WriteLine($"Received admin message: {message} from session: {sessionID}");

            // Using Message Cracker to handle specific message types
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
        }

        public void FromApp(QuickFix.Message message, SessionID sessionID)
        {
            Console.WriteLine($"Received application message: {message} from session: {sessionID}");

            // App message handling using Message Cracker
            Crack(message, sessionID);
        }

        public void ToApp(QuickFix.Message message, SessionID sessionID)
        {
            Console.WriteLine($"Sending application message: {message} to session: {sessionID}");
        }

        #endregion

        #region Message Handlers

        // Handle Logon messages specifically
        public void OnMessage(Logon logon, SessionID sessionID)
        {
            Console.WriteLine($"Received Logon message: {logon} from session: {sessionID}");

            string username = "";
            string password = "";

            if (logon.IsSetUsername())
                username = logon.Username.Value;

            if (logon.IsSetPassword())
                password = logon.Password.Value;

            Console.WriteLine($"Username: {username} login attempt");

            // Verify credentials
            if (username != _validUsername || password != _validPassword)
            {
                Console.WriteLine("Username or password is incorrect");
                throw new RejectLogon("Username or password is incorrect");
            }

            Console.WriteLine("Valid credentials, allowing logon");
        }

        // Handle Heartbeat messages specifically
        public void OnMessage(Heartbeat heartbeat, SessionID sessionID)
        {
            Console.WriteLine($"Received Heartbeat message: {heartbeat} from session: {sessionID}");
        }

        // Handle TestRequest messages specifically
        public void OnMessage(TestRequest testRequest, SessionID sessionID)
        {
            Console.WriteLine($"Received TestRequest message: {testRequest} from session: {sessionID}");
        }

        #endregion
    }
}
