using System.Timers;
using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;

namespace QuickFixCli
{
    public class MarketDataClientApp : AuthClientApp
    {
        // Market data tracking
        private SessionID? _sessionId;
        private Dictionary<string, Dictionary<string, List<Tuple<double, double>>>> _prices = new();
        private List<string> _availableSymbols = new List<string>();
        private List<string> _desiredSymbols;
        private List<string> _subscribedSymbols = new List<string>();
        private bool _securityListRequested = false;
        private System.Timers.Timer _dataDisplayTimer;

        public MarketDataClientApp(string username, string password, List<string>? desiredSymbols = null) // Use nullable type for desiredSymbols
            : base(username, password)
        {
            // Initialize desired symbols with defaults if not provided
            _desiredSymbols = desiredSymbols ?? new List<string> {
                "EURUSD", "GBPUSD", "USDJPY", "USDCHF", "AUDUSD", "USDCAD"
            };

            // Setup timer for periodic display of market data
            _dataDisplayTimer = new System.Timers.Timer(5000); // 5 seconds
            _dataDisplayTimer.Elapsed += OnTimerElapsed;
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            DisplayMarketData();
        }

        private void DisplayMarketData()
        {
            Console.WriteLine("\n===== MARKET DATA SNAPSHOT =====");
            Console.WriteLine($"Timestamp: {DateTime.Now}");
            Console.WriteLine($"Subscribed Symbols: {_subscribedSymbols.Count}");

            foreach (var symbol in _subscribedSymbols)
            {
                if (_prices.TryGetValue(symbol, out var priceBook))
                {
                    Console.WriteLine($"\nSymbol: {symbol}");

                    if (priceBook.TryGetValue("bids", out var bids) && bids.Any())
                    {
                        Console.WriteLine("  Bids:");
                        foreach (var (price, size) in bids.Take(3)) // Show top 3 bids
                        {
                            Console.WriteLine($"    Price: {price}, Size: {size}");
                        }
                    }

                    if (priceBook.TryGetValue("asks", out var asks) && asks.Any())
                    {
                        Console.WriteLine("  Asks:");
                        foreach (var (price, size) in asks.Take(3)) // Show top 3 asks
                        {
                            Console.WriteLine($"    Price: {price}, Size: {size}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"\nSymbol: {symbol} - No data available");
                }
            }
            Console.WriteLine("================================\n");
        }

        #region Override IApplication Members

        // Override OnCreate to store the sessionID
        public override void OnCreate(SessionID sessionID)
        {
            base.OnCreate(sessionID);
            _sessionId = sessionID;
        }

        // Override OnLogon to request security list and start timer
        public override void OnLogon(SessionID sessionID)
        {
            base.OnLogon(sessionID);

            // Request security list after successful login
            if (!_securityListRequested)
            {
                SendSecurityListRequest();
                _securityListRequested = true;
            }

            // Start the timer to display market data
            _dataDisplayTimer.Start();
        }

        // Override OnLogout to stop timer
        public override void OnLogout(SessionID sessionID)
        {
            base.OnLogout(sessionID);
            _dataDisplayTimer.Stop();
        }

        // Override FromApp to handle application-level messages
        public override void FromApp(QuickFix.Message message, SessionID sessionID)
        {
            // Let MessageCracker handle specific message types
            Crack(message, sessionID);
        }

        #endregion

        #region Message Handlers

        // Handle SecurityList messages
        public void OnMessage(SecurityList securityList, SessionID sessionID)
        {
            Console.WriteLine("Received SecurityList message");

            _availableSymbols.Clear();

            // Extract symbols from the message
            int symbolCount = securityList.NoRelatedSym.Value;
            for (int i = 1; i <= symbolCount; i++)
            {
                SecurityList.NoRelatedSymGroup symbolGroup = new SecurityList.NoRelatedSymGroup();
                securityList.GetGroup(i, symbolGroup);
                _availableSymbols.Add(symbolGroup.Symbol.Value);
            }

            Console.WriteLine($"Received {_availableSymbols.Count} available symbols");

            // Find intersection with desired symbols
            _subscribedSymbols = _desiredSymbols
                .Where(s => _availableSymbols.Contains(s))
                .ToList();

            // Report symbols that were not available
            var unavailableSymbols = _desiredSymbols
                .Where(s => !_availableSymbols.Contains(s))
                .ToList();

            if (unavailableSymbols.Any())
            {
                Console.WriteLine("The following symbols are not available and will be skipped:");
                foreach (var symbol in unavailableSymbols)
                {
                    Console.WriteLine($"  - {symbol}");
                }
            }

            Console.WriteLine($"Subscribing to {_subscribedSymbols.Count} symbols");

            // Subscribe to market data for the intersection
            if (_subscribedSymbols.Any())
            {
                SendMarketDataRequest(_subscribedSymbols);
            }
        }

        // Handle MarketDataSnapshotFullRefresh messages
        public void OnMessage(MarketDataSnapshotFullRefresh mdSnapshot, SessionID sessionID)
        {
            string symbol = mdSnapshot.Symbol.Value;

            Dictionary<string, List<Tuple<double, double>>> priceBook = new Dictionary<string, List<Tuple<double, double>>>
            {
                { "bids", new List<Tuple<double, double>>() },
                { "asks", new List<Tuple<double, double>>() }
            };

            int entryCount = mdSnapshot.NoMDEntries.Value;
            for (int i = 1; i <= entryCount; i++)
            {
                MarketDataSnapshotFullRefresh.NoMDEntriesGroup group = new MarketDataSnapshotFullRefresh.NoMDEntriesGroup();
                mdSnapshot.GetGroup(i, group);

                char entryType = group.MDEntryType.Value;
                double price = (double)group.MDEntryPx.Value;
                double size = 0;

                if (group.IsSetMDEntrySize())
                {
                    size = (double)group.MDEntrySize.Value;
                }

                if (entryType == MDEntryType.BID)
                {
                    priceBook["bids"].Add(new Tuple<double, double>(price, size));
                }
                else if (entryType == MDEntryType.OFFER)
                {
                    priceBook["asks"].Add(new Tuple<double, double>(price, size));
                }
            }

            // Sort bids in descending order (highest bid first)
            priceBook["bids"] = priceBook["bids"].OrderByDescending(x => x.Item1).ToList();

            // Sort asks in ascending order (lowest ask first)
            priceBook["asks"] = priceBook["asks"].OrderBy(x => x.Item1).ToList();

            // Update the price book
            _prices[symbol] = priceBook;

            Console.WriteLine($"Updated market data for {symbol}: {priceBook["bids"].Count} bids, {priceBook["asks"].Count} asks");
        }

        // Handle MarketDataIncrementalRefresh messages
        public void OnMessage(MarketDataIncrementalRefresh mdRefresh, SessionID sessionID)
        {
            // Implementation of incremental market data updates
            // For simplicity, we're not handling incremental updates in this example
            Console.WriteLine("Received market data incremental refresh (not implemented in this example)");
        }

        #endregion

        #region Market Data Functions

        private void SendSecurityListRequest()
        {
            try
            {
                SecurityListRequest request = new SecurityListRequest();
                request.SecurityReqID = new SecurityReqID(Guid.NewGuid().ToString());
                request.SecurityListRequestType = new SecurityListRequestType(SecurityListRequestType.ALL_SECURITIES);
                request.SubscriptionRequestType = new SubscriptionRequestType(SubscriptionRequestType.SNAPSHOT);

                SendMessage(request);
                Console.WriteLine("Sent SecurityListRequest");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error sending SecurityListRequest: {e.Message}");
            }
        }

        private void SendMarketDataRequest(List<string> symbols)
        {
            if (symbols == null || !symbols.Any())
            {
                Console.WriteLine("No symbols to subscribe to");
                return;
            }

            try
            {
                MarketDataRequest request = new MarketDataRequest();
                request.MDReqID = new MDReqID(Guid.NewGuid().ToString());
                request.SubscriptionRequestType = new SubscriptionRequestType(SubscriptionRequestType.SNAPSHOT_PLUS_UPDATES);
                request.MarketDepth = new MarketDepth(0);
                request.MDUpdateType = new MDUpdateType(0);

                // Add entry types (bid and ask)
                MarketDataRequest.NoMDEntryTypesGroup entryTypesGroup = new MarketDataRequest.NoMDEntryTypesGroup();

                entryTypesGroup.MDEntryType = new MDEntryType(MDEntryType.BID);
                request.AddGroup(entryTypesGroup);

                entryTypesGroup.MDEntryType = new MDEntryType(MDEntryType.OFFER);
                request.AddGroup(entryTypesGroup);

                // Add symbols
                MarketDataRequest.NoRelatedSymGroup symbolGroup = new MarketDataRequest.NoRelatedSymGroup();
                foreach (string symbol in symbols)
                {
                    symbolGroup.Symbol = new Symbol(symbol);
                    request.AddGroup(symbolGroup);
                }

                SendMessage(request);
                Console.WriteLine($"Sent MarketDataRequest for {symbols.Count} symbols");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error sending MarketDataRequest: {e.Message}");
            }
        }

        // Get price for a specific symbol, side, and quantity
        public double GetPrice(string symbol, string side, int quantity)
        {
            try
            {
                if (!_prices.TryGetValue(symbol, out var priceBook) || priceBook == null)
                {
                    Console.WriteLine($"Price book not found for symbol: {symbol}");
                    return 0;
                }

                if (side.ToLower() == "buy")
                {
                    List<Tuple<double, double>> asks = priceBook["asks"];
                    if (!asks.Any())
                    {
                        Console.WriteLine($"No ask prices available for symbol: {symbol}");
                        return 0;
                    }

                    double totalQuantity = 0;
                    foreach (var (price, size) in asks)
                    {
                        totalQuantity += size;
                        if (totalQuantity >= quantity)
                        {
                            return price;
                        }
                    }
                }
                else // sell
                {
                    List<Tuple<double, double>> bids = priceBook["bids"];
                    if (!bids.Any())
                    {
                        Console.WriteLine($"No bid prices available for symbol: {symbol}");
                        return 0;
                    }

                    double totalQuantity = 0;
                    foreach (var (price, size) in bids)
                    {
                        totalQuantity += size;
                        if (totalQuantity >= quantity)
                        {
                            return price;
                        }
                    }
                }

                Console.WriteLine($"Not enough liquidity for {symbol} {side} {quantity}");
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting price: {e.Message}");
                return 0;
            }
        }

        public virtual void SendMessage(QuickFix.Message message)
        {
            if (_sessionId == null)
            {
                Console.WriteLine($"Warning: Session ID is null, cannot send message");
                return;
            }
            Session.SendToTarget(message, _sessionId);
        }
        #endregion
    }
}
