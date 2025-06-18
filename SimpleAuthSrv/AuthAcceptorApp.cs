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

        public virtual void OnCreate(SessionID sessionID)
        {
            Console.WriteLine($"Creating session: {sessionID}");
        }

        public virtual void OnLogon(SessionID sessionID)
        {
            Console.WriteLine($"User logged in: {sessionID}");
        }

        public virtual void OnLogout(SessionID sessionID)
        {
            Console.WriteLine($"User logged out: {sessionID}");
        }

        public virtual void FromAdmin(QuickFix.Message message, SessionID sessionID)
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

        public virtual void ToAdmin(QuickFix.Message message, SessionID sessionID)
        {
            Console.WriteLine($"Sending admin message: {message} to session: {sessionID}");
        }

        public virtual void FromApp(QuickFix.Message message, SessionID sessionID)
        {
            Console.WriteLine($"Received application message: {message} from session: {sessionID}");

            // App message handling using Message Cracker
            Crack(message, sessionID);
        }

        public virtual void ToApp(QuickFix.Message message, SessionID sessionID)
        {
            Console.WriteLine($"Sending application message: {message} to session: {sessionID}");
        }

        #endregion

        #region Message Handlers

        // Handle Logon messages specifically
        public virtual void OnMessage(Logon logon, SessionID sessionID)
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
        public virtual void OnMessage(Heartbeat heartbeat, SessionID sessionID)
        {
            Console.WriteLine($"Received Heartbeat message: {heartbeat} from session: {sessionID}");
        }

        // Handle TestRequest messages specifically
        public virtual void OnMessage(TestRequest testRequest, SessionID sessionID)
        {
            Console.WriteLine($"Received TestRequest message: {testRequest} from session: {sessionID}");
        }

        #endregion
    }
}
