using System.Timers;
using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;

namespace QuickFixSrv
{
    public class MarketDataAcceptorApp : AuthAcceptorApp
    {
        // Symbol data management
        private Dictionary<string, decimal> _symbolPrices = new();
        private Dictionary<SessionID, HashSet<string>> _clientSubscriptions = new();
        private Random _random = new Random();
        private System.Timers.Timer _priceUpdateTimer;

        // Market depth settings
        private const int BID_LEVELS = 3;
        private const int ASK_LEVELS = 3;
        private const decimal SPREAD_PERCENTAGE = 0.0002m; // 0.02% spread
        private const decimal LEVEL_STEP_PERCENTAGE = 0.0001m; // 0.01% between levels
        private const double RANDOM_PRICE_CHANGE_PERCENTAGE = 0.02; // ±2% random price change

        public MarketDataAcceptorApp(string username, string password, string symbolsWithPrices)
            : base(username, password)
        {
            // Parse input symbols and prices
            ParseSymbolsAndPrices(symbolsWithPrices);

            // Set up timer for price updates
            _priceUpdateTimer = new System.Timers.Timer(1000); // 1 second
            _priceUpdateTimer.Elapsed += OnPriceUpdateTimerElapsed;
            _priceUpdateTimer.AutoReset = true;
            _priceUpdateTimer.Start();

            Console.WriteLine($"Market data server initialized with {_symbolPrices.Count} symbols");
            foreach (var symbol in _symbolPrices)
            {
                Console.WriteLine($"  {symbol.Key}: {symbol.Value}");
            }
        }

        private void ParseSymbolsAndPrices(string symbolsWithPrices)
        {
            if (string.IsNullOrWhiteSpace(symbolsWithPrices))
                return;

            var symbolPairs = symbolsWithPrices.Split(',');
            foreach (var pair in symbolPairs)
            {
                var parts = pair.Trim().Split(':');
                if (parts.Length == 2 && decimal.TryParse(parts[1], out decimal price))
                {
                    _symbolPrices[parts[0]] = price;
                }
                else
                {
                    Console.WriteLine($"Invalid symbol:price pair: {pair}");
                }
            }
        }

        private void OnPriceUpdateTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            UpdatePrices();
            SendMarketDataToSubscribers();
        }

        private void UpdatePrices()
        {
            foreach (var symbol in _symbolPrices.Keys.ToList())
            {
                decimal currentPrice = _symbolPrices[symbol];

                // Generate random price change within ±RANDOM_PRICE_CHANGE_PERCENTAGE
                // Random between -2% and +2%
                decimal changePercentage = (decimal)(_random.NextDouble() * 2 - 1) * (decimal)RANDOM_PRICE_CHANGE_PERCENTAGE; 
                decimal newPrice = currentPrice * (1 + changePercentage);

                // Round to 5 decimal places which is standard for FX
                newPrice = Math.Round(newPrice, 5);

                _symbolPrices[symbol] = newPrice;
            }
        }

        private void SendMarketDataToSubscribers()
        {
            // For each client session
            foreach (var clientSubscription in _clientSubscriptions)
            {
                var sessionID = clientSubscription.Key;
                var symbols = clientSubscription.Value;

                // For each subscribed symbol
                foreach (var symbol in symbols)
                {
                    if (_symbolPrices.TryGetValue(symbol, out decimal midPrice))
                    {
                        SendMarketDataUpdate(sessionID, symbol, midPrice);
                    }
                }
            }
        }

        private void SendMarketDataUpdate(SessionID sessionID, string symbol, decimal midPrice)
        {
            try
            {
                MarketDataSnapshotFullRefresh marketData = new MarketDataSnapshotFullRefresh();
                marketData.Symbol = new Symbol(symbol);

                // Calculate bid and ask prices based on mid price and spread
                decimal halfSpread = midPrice * SPREAD_PERCENTAGE / 2;
                decimal bidPrice = midPrice - halfSpread;
                decimal askPrice = midPrice + halfSpread;

                // Add bids
                for (int i = 0; i < BID_LEVELS; i++)
                {
                    decimal levelPrice = bidPrice - (i * midPrice * LEVEL_STEP_PERCENTAGE);
                    decimal levelSize = 1000000m * (BID_LEVELS - i); // Size decreases at deeper levels

                    MarketDataSnapshotFullRefresh.NoMDEntriesGroup bidGroup = new MarketDataSnapshotFullRefresh.NoMDEntriesGroup();
                    bidGroup.MDEntryType = new MDEntryType(MDEntryType.BID);
                    bidGroup.MDEntryPx = new MDEntryPx(levelPrice);
                    bidGroup.MDEntrySize = new MDEntrySize(levelSize);
                    marketData.AddGroup(bidGroup);
                }

                // Add asks
                for (int i = 0; i < ASK_LEVELS; i++)
                {
                    decimal levelPrice = askPrice + (i * midPrice * LEVEL_STEP_PERCENTAGE);
                    decimal levelSize = 1000000m * (ASK_LEVELS - i); // Size decreases at deeper levels

                    MarketDataSnapshotFullRefresh.NoMDEntriesGroup askGroup = new MarketDataSnapshotFullRefresh.NoMDEntriesGroup();
                    askGroup.MDEntryType = new MDEntryType(MDEntryType.OFFER);
                    askGroup.MDEntryPx = new MDEntryPx(levelPrice);
                    askGroup.MDEntrySize = new MDEntrySize(levelSize);
                    marketData.AddGroup(askGroup);
                }

                Session.SendToTarget(marketData, sessionID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending market data update: {ex.Message}");
            }
        }

        #region Override IApplication Members

        public override void OnLogout(SessionID sessionID)
        {
            base.OnLogout(sessionID);

            // Clean up subscriptions when client disconnects
            if (_clientSubscriptions.ContainsKey(sessionID))
            {
                _clientSubscriptions.Remove(sessionID);
                Console.WriteLine($"Removed subscriptions for session {sessionID}");
            }
        }

        public override void FromApp(QuickFix.Message message, SessionID sessionID)
        {
            Console.WriteLine($"Received application message: {message}");

            // Let the MessageCracker handle the message
            Crack(message, sessionID);
        }

        #endregion

        #region Message Handlers

        // Handle SecurityListRequest messages
        public void OnMessage(SecurityListRequest request, SessionID sessionID)
        {
            Console.WriteLine($"Received SecurityListRequest from {sessionID}");

            try
            {
                SecurityList response = new SecurityList();
                response.SecurityReqID = new SecurityReqID(request.SecurityReqID.Value);
                response.SecurityResponseID = new SecurityResponseID(Guid.NewGuid().ToString());
                response.SecurityRequestResult = new SecurityRequestResult(SecurityRequestResult.VALID_REQUEST);

                // Add available symbols
                response.NoRelatedSym = new NoRelatedSym(_symbolPrices.Count);

                foreach (var symbol in _symbolPrices.Keys)
                {
                    SecurityList.NoRelatedSymGroup group = new SecurityList.NoRelatedSymGroup();
                    group.Symbol = new Symbol(symbol);
                    response.AddGroup(group);
                }

                Session.SendToTarget(response, sessionID);
                Console.WriteLine($"Sent SecurityList with {_symbolPrices.Count} symbols to {sessionID}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error handling SecurityListRequest: {e.Message}");
            }
        }

        // Handle MarketDataRequest messages
        public void OnMessage(MarketDataRequest request, SessionID sessionID)
        {
            string mdReqID = request.MDReqID.Value;
            char subscriptionType = request.SubscriptionRequestType.Value;

            Console.WriteLine($"Received MarketDataRequest from {sessionID}, type: {subscriptionType}");

            try
            {
                // Handle subscription request
                if (subscriptionType == SubscriptionRequestType.SNAPSHOT_PLUS_UPDATES ||
                    subscriptionType == SubscriptionRequestType.SNAPSHOT)
                {
                    // Create a new entry for this client if it doesn't exist
                    if (_clientSubscriptions.TryGetValue(sessionID, out HashSet<string>? subscriptions))
                    {
                        subscriptions = new HashSet<string>();
                        _clientSubscriptions[sessionID] = subscriptions;
                    }

                    // Get the number of symbols in the request
                    int symbolCount = request.NoRelatedSym.Value;

                    // Process each symbol
                    for (int i = 1; i <= symbolCount; i++)
                    {
                        MarketDataRequest.NoRelatedSymGroup group = new MarketDataRequest.NoRelatedSymGroup();
                        request.GetGroup(i, group);

                        string symbol = group.Symbol.Value;

                        // Check if symbol is supported
                        if (_symbolPrices.ContainsKey(symbol))
                        {
                            subscriptions?.Add(symbol);
                            Console.WriteLine($"Added subscription for {symbol} to session {sessionID}");

                            // If snapshot was requested, send it immediately
                            if (_symbolPrices.TryGetValue(symbol, out decimal price))
                            {
                                SendMarketDataUpdate(sessionID, symbol, price);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Requested symbol not supported: {symbol}");

                            // Send rejection for unsupported symbol
                            MarketDataRequestReject reject = new MarketDataRequestReject();
                            reject.MDReqID = new MDReqID(mdReqID);
                            reject.MDReqRejReason = new MDReqRejReason(MDReqRejReason.UNKNOWN_SYMBOL);
                            reject.Text = new Text($"Symbol not supported: {symbol}");
                            Session.SendToTarget(reject, sessionID);
                        }
                    }
                }
                else if (subscriptionType == SubscriptionRequestType.DISABLE_PREVIOUS_SNAPSHOT_PLUS_UPDATE_REQUEST)
                {
                    // Handle unsubscribe request
                    if (_clientSubscriptions.TryGetValue(sessionID, out HashSet<string>? subscriptions))
                    {
                        int symbolCount = request.NoRelatedSym.Value;

                        if (symbolCount > 0)
                        {
                            // Unsubscribe from specific symbols
                            for (int i = 1; i <= symbolCount; i++)
                            {
                                MarketDataRequest.NoRelatedSymGroup group = new MarketDataRequest.NoRelatedSymGroup();
                                request.GetGroup(i, group);

                                string symbol = group.Symbol.Value;
                                if (subscriptions.Remove(symbol))
                                {
                                    Console.WriteLine($"Removed subscription for {symbol} from session {sessionID}");
                                }
                            }
                        }
                        else
                        {
                            // Unsubscribe from all
                            subscriptions.Clear();
                            Console.WriteLine($"Removed all subscriptions for session {sessionID}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error handling MarketDataRequest: {e.Message}");
            }
        }

        #endregion

        public void Dispose()
        {
            _priceUpdateTimer.Stop();
            _priceUpdateTimer.Dispose();
        }
    }
}
