using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;

namespace QuickFixSrv
{
    public class IncrementalMarketDataAcceptorApp : MarketDataAcceptorApp
    {
        // Track previous prices for incremental updates
        private Dictionary<string, decimal> _previousPrices = new();

        // Track client connection state
        private Dictionary<SessionID, bool> _clientReceivedFullRefresh = new();

        // Settings for incremental updates
        private int _updateCounter = 0;
        private int _fullRefreshInterval = 10; // Send full refresh every 10 updates
        private bool _useIncrementalUpdates = true;

        public IncrementalMarketDataAcceptorApp(string username, string password, string symbolsWithPrices,
                                              bool useIncrementalUpdates = true, int fullRefreshInterval = 10)
            : base(username, password, symbolsWithPrices)
        {
            _useIncrementalUpdates = useIncrementalUpdates;
            _fullRefreshInterval = fullRefreshInterval;
            Console.WriteLine($"Incremental Market Data server initialized (Incremental updates: {_useIncrementalUpdates})");
        }

        #region Override IApplication Members

        public override void OnLogon(SessionID sessionID)
        {
            base.OnLogon(sessionID);

            // Mark that this client needs a full refresh
            _clientReceivedFullRefresh[sessionID] = false;
            Console.WriteLine($"Client {sessionID} connected - will send full refresh first");
        }

        public override void OnLogout(SessionID sessionID)
        {
            base.OnLogout(sessionID);

            // Clean up client state
            if (_clientReceivedFullRefresh.ContainsKey(sessionID))
            {
                _clientReceivedFullRefresh.Remove(sessionID);
            }
        }

        #endregion

        #region Message Handlers

        // Override MarketDataRequest handler to track client state
        public new void OnMessage(MarketDataRequest request, SessionID sessionID)
        {
            // Let the base class handle the subscription first
            base.OnMessage(request, sessionID);

            // Mark that client needs full refresh on new subscription
            if (request.SubscriptionRequestType.Value == SubscriptionRequestType.SNAPSHOT_PLUS_UPDATES)
            {
                _clientReceivedFullRefresh[sessionID] = false;
            }
        }

        // Add handler for MassQuoteAcknowledgement
        public void OnMessage(MassQuoteAcknowledgement acknowledgement, SessionID sessionID)
        {
            Console.WriteLine($"Received MassQuoteAcknowledgement from {sessionID}");
        }

        #endregion

        // This method intercepts outgoing messages to substitute MassQuote when appropriate
        public override void ToApp(QuickFix.Message message, SessionID sessionID)
        {
            // If this is a full refresh message and client already received initial refresh
            if (_useIncrementalUpdates &&
                message.Header.GetString(Tags.MsgType) == MarketDataSnapshotFullRefresh.MsgType &&
                _clientReceivedFullRefresh.TryGetValue(sessionID, out bool receivedFullRefresh) &&
                receivedFullRefresh)
            {
                // Increment counter and check if we should periodically send full refresh anyway
                _updateCounter++;
                if (_updateCounter % _fullRefreshInterval == 0)
                {
                    // Let the full refresh go through periodically
                    base.ToApp(message, sessionID);
                    Console.WriteLine($"Sending periodic full refresh to {sessionID}");
                    return;
                }

                // Extract symbol from the message
                MarketDataSnapshotFullRefresh fullRefresh = (MarketDataSnapshotFullRefresh)message;
                string symbol = fullRefresh.Symbol.Value;

                // Get price information from the message
                decimal bidPrice = 0, askPrice = 0;
                int entryCount = fullRefresh.NoMDEntries.Value;
                for (int i = 1; i <= entryCount; i++)
                {
                    MarketDataSnapshotFullRefresh.NoMDEntriesGroup group = new MarketDataSnapshotFullRefresh.NoMDEntriesGroup();
                    fullRefresh.GetGroup(i, group);

                    if (group.MDEntryType.Value == MDEntryType.BID && bidPrice == 0)
                    {
                        bidPrice = group.MDEntryPx.Value;
                    }
                    else if (group.MDEntryType.Value == MDEntryType.OFFER && askPrice == 0)
                    {
                        askPrice = group.MDEntryPx.Value;
                    }

                    // We only need the top bid/ask for MassQuote
                    if (bidPrice != 0 && askPrice != 0) break;
                }

                // Create and send MassQuote instead of full refresh
                MassQuote massQuote = new MassQuote();
                massQuote.QuoteID = new QuoteID(Guid.NewGuid().ToString());

                // Add quote set for the symbol
                MassQuote.NoQuoteSetsGroup quoteSetGroup = new MassQuote.NoQuoteSetsGroup();
                quoteSetGroup.QuoteSetID = new QuoteSetID(symbol);
                
                // Add entry for the symbol
                MassQuote.NoQuoteSetsGroup.NoQuoteEntriesGroup quoteEntryGroup = new MassQuote.NoQuoteSetsGroup.NoQuoteEntriesGroup();
                quoteEntryGroup.QuoteEntryID = new QuoteEntryID("1");
                quoteEntryGroup.BidPx = new BidPx(bidPrice);
                quoteEntryGroup.OfferPx = new OfferPx(askPrice);
                quoteEntryGroup.BidSize = new BidSize(1000000);
                quoteEntryGroup.OfferSize = new OfferSize(1000000);
                quoteEntryGroup.Symbol = new Symbol(symbol);

                quoteSetGroup.AddGroup(quoteEntryGroup);
                massQuote.AddGroup(quoteSetGroup);

                // Send the MassQuote instead
                Session.SendToTarget(massQuote, sessionID);
                Console.WriteLine($"Sent incremental MassQuote for {symbol} to {sessionID}");

                // Don't send the original message
                throw new DoNotSend();
            }
            else if (message.Header.GetString(Tags.MsgType) == MarketDataSnapshotFullRefresh.MsgType)
            {
                // If this is a full refresh, mark that client received it
                _clientReceivedFullRefresh[sessionID] = true;
                Console.WriteLine($"Sending initial full refresh to {sessionID}");
                base.ToApp(message, sessionID);
            }
            else
            {
                // For other messages, use the base implementation
                base.ToApp(message, sessionID);
            }
        }
    }
}
