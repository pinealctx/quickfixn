using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;

namespace QuickFixCli
{
    public class IncrementalMarketDataClientApp : MarketDataClientApp
    {
        // Track whether we support incremental updates
        private bool _supportIncrementalUpdates = true;

        public IncrementalMarketDataClientApp(string username, string password, List<string>? desiredSymbols = null, bool supportIncrementalUpdates = true)
            : base(username, password, desiredSymbols)
        {
            _supportIncrementalUpdates = supportIncrementalUpdates;
            Console.WriteLine($"Incremental Market Data client initialized (Incremental updates: {_supportIncrementalUpdates})");
        }

        // Handle MassQuote messages that contain incremental updates
        public void OnMessage(MassQuote massQuote, SessionID sessionID)
        {
            Console.WriteLine("Received MassQuote message (incremental update)");

            if (!_supportIncrementalUpdates)
            {
                Console.WriteLine("Ignoring incremental update as incremental updates are not enabled");
                return;
            }

            try
            {
                int quoteSetCount = massQuote.NoQuoteSets.Value;
                for (int i = 1; i <= quoteSetCount; i++)
                {
                    MassQuote.NoQuoteSetsGroup quoteSetGroup = new MassQuote.NoQuoteSetsGroup();
                    massQuote.GetGroup(i, quoteSetGroup);

                    // Get quote entries for this set
                    int entryCount = quoteSetGroup.NoQuoteEntries.Value;
                    for (int j = 1; j <= entryCount; j++)
                    {
                        MassQuote.NoQuoteSetsGroup.NoQuoteEntriesGroup quoteEntry = new MassQuote.NoQuoteSetsGroup.NoQuoteEntriesGroup();
                        quoteSetGroup.GetGroup(j, quoteEntry);

                        // Extract symbol and price information
                        string symbol = quoteEntry.Symbol.Value;

                        Dictionary<string, List<Tuple<double, double>>> priceBook = new()
                        {
                            { "bids", new List<Tuple<double, double>>() },
                            { "asks", new List<Tuple<double, double>>() }
                        };

                        // Check if the symbol exists in our price book
                        if (!_prices.TryGetValue(symbol, out var existingPriceBook))
                        {
                            _prices[symbol] = priceBook;
                        }
                        else
                        {
                            priceBook = existingPriceBook;
                            // Clear existing quotes as this is a replacement
                            priceBook["bids"].Clear();
                            priceBook["asks"].Clear();
                        }

                        // Add bid price if available
                        if (quoteEntry.IsSetBidPx())
                        {
                            double bidPrice = (double)quoteEntry.BidPx.Value;
                            double bidSize = quoteEntry.IsSetBidSize() ? (double)quoteEntry.BidSize.Value : 0;
                            priceBook["bids"].Add(new Tuple<double, double>(bidPrice, bidSize));
                        }

                        // Add offer price if available
                        if (quoteEntry.IsSetOfferPx())
                        {
                            double offerPrice = (double)quoteEntry.OfferPx.Value;
                            double offerSize = quoteEntry.IsSetOfferSize() ? (double)quoteEntry.OfferSize.Value : 0;
                            priceBook["asks"].Add(new Tuple<double, double>(offerPrice, offerSize));
                        }

                        // Sort bids and asks
                        priceBook["bids"] = priceBook["bids"].OrderByDescending(x => x.Item1).ToList();
                        priceBook["asks"] = priceBook["asks"].OrderBy(x => x.Item1).ToList();

                        Console.WriteLine($"Updated incremental market data for {symbol}: {priceBook["bids"].Count} bids, {priceBook["asks"].Count} asks");
                    }

                    // Send acknowledgement for the MassQuote
                    if (massQuote.IsSetQuoteID())
                    {
                        SendMassQuoteAcknowledgement(sessionID, massQuote.QuoteID.Value);
                    }
                    else
                    {
                        Console.WriteLine("MassQuote does not contain QuoteID, cannot send acknowledgement");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing MassQuote message: {ex.Message}");
            }
        }

        // Send MassQuoteAcknowledgement after receiving MassQuote
        private void SendMassQuoteAcknowledgement(SessionID sessionID, string quoteID)
        {
            try
            {
                MassQuoteAcknowledgement acknowledgement = new MassQuoteAcknowledgement();
                acknowledgement.QuoteID = new QuoteID(quoteID);
                acknowledgement.QuoteStatus = new QuoteStatus(QuoteStatus.ACCEPTED);

                SendMessage(acknowledgement);
                Console.WriteLine("Sent MassQuoteAcknowledgement");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error sending MassQuoteAcknowledgement: {e.Message}");
            }
        }

        // Override SendMarketDataRequest to specify we support incremental updates
        protected override void SendMarketDataRequest(List<string> symbols)
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

                // Set MDUpdateType to INCREMENTAL_REFRESH (1) if we support incremental updates
                if (_supportIncrementalUpdates)
                {
                    request.MDUpdateType = new MDUpdateType(1); // 1 = INCREMENTAL_REFRESH
                }
                else
                {
                    request.MDUpdateType = new MDUpdateType(0); // 0 = FULL_REFRESH
                }

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
                Console.WriteLine($"Sent MarketDataRequest for {symbols.Count} symbols (Incremental updates: {_supportIncrementalUpdates})");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error sending MarketDataRequest: {e.Message}");
            }
        }
    }
}
