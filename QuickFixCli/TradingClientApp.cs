using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;

namespace QuickFixCli
{
    public class TradingClientApp : AuthClientApp
    {
        // Session ID for sending messages
        private SessionID? _sessionId;

        // Define custom message types that might not be included in QuickFix/n
        public const string MsgType_AccountInfoRequest = "AINF";
        public const string MsgType_AccountInfoResponse = "AINFR";
        public const string MsgType_RequestForPositionsAck = "AO";
        public const string MsgType_PositionReport = "AP";

        public TradingClientApp(string username, string password) : base(username, password)
        {
            Console.WriteLine("TradingClientApp initialized");
        }

        #region Override IApplication Members

        public override void OnCreate(SessionID sessionID)
        {
            base.OnCreate(sessionID);
            _sessionId = sessionID;
        }

        public override void FromApp(QuickFix.Message message, SessionID sessionID)
        {
            Console.WriteLine($"TradingClient received app message: {message}");

            // Let MessageCracker handle the message
            Crack(message, sessionID);
        }

        #endregion

        #region Message Handlers

        // Handle ExecutionReport responses (for orders)
        public void OnMessage(ExecutionReport execReport, SessionID sessionID)
        {
            string clOrdId = execReport.ClOrdID.Value;
            char ordStatus = execReport.OrdStatus.Value;
            char execType = execReport.ExecType.Value;

            Console.WriteLine($"Received ExecutionReport for order {clOrdId}");
            Console.WriteLine($"  Status: {ordStatus}, ExecType: {execType}");

            if (execReport.IsSetText())
                Console.WriteLine($"  Text: {execReport.Text.Value}");

            if (execReport.IsSetOrderID())
                Console.WriteLine($"  OrderID: {execReport.OrderID.Value}");

            if (execReport.IsSetSymbol())
                Console.WriteLine($"  Symbol: {execReport.Symbol.Value}");

            if (execReport.IsSetOrderQty())
                Console.WriteLine($"  Quantity: {execReport.OrderQty.Value}");

            if (execReport.IsSetPrice())
                Console.WriteLine($"  Price: {execReport.Price.Value}");

            if (execReport.IsSetLeavesQty())
                Console.WriteLine($"  LeavesQty: {execReport.LeavesQty.Value}");

            if (execReport.IsSetCumQty())
                Console.WriteLine($"  CumQty: {execReport.CumQty.Value}");

            if (execReport.IsSetAvgPx())
                Console.WriteLine($"  AvgPx: {execReport.AvgPx.Value}");

            // Handle based on mass status if needed
            if (execReport.IsSetMassStatusReqID())
            {
                string massStatusReqId = execReport.MassStatusReqID.Value;
                Console.WriteLine($"  Part of mass status request: {massStatusReqId}");
            }
        }

        // Handle OrderCancelReject responses
        public void OnMessage(OrderCancelReject cancelReject, SessionID sessionID)
        {
            string clOrdId = cancelReject.ClOrdID.Value;
            string origClOrdId = cancelReject.OrigClOrdID.Value;
            char ordStatus = cancelReject.OrdStatus.Value;

            Console.WriteLine($"Received OrderCancelReject: ClOrdID={clOrdId}, OrigClOrdID={origClOrdId}");
            Console.WriteLine($"  Status: {ordStatus}");

            if (cancelReject.IsSetText())
                Console.WriteLine($"  Reason: {cancelReject.Text.Value}");

            if (cancelReject.IsSetCxlRejReason())
                Console.WriteLine($"  CxlRejReason: {cancelReject.CxlRejReason.Value}");
        }

        // Handle PositionReport messages
        public void OnMessage(PositionReport posReport, SessionID sessionID)
        {
            string symbol = posReport.Symbol.Value;
            string posReqId = posReport.PosReqID.Value;

            Console.WriteLine($"Received PositionReport for {symbol}, reqID: {posReqId}");

            // Loop through position quantities
            int posCount = posReport.NoPositions.Value;
            for (int i = 1; i <= posCount; i++)
            {
                PositionReport.NoPositionsGroup posGroup = new PositionReport.NoPositionsGroup();
                posReport.GetGroup(i, posGroup);

                string posType = posGroup.PosType.Value;

                if (posGroup.IsSetLongQty())
                    Console.WriteLine($"  Long position: {posGroup.LongQty.Value} {posType}");

                if (posGroup.IsSetShortQty())
                    Console.WriteLine($"  Short position: {posGroup.ShortQty.Value} {posType}");
            }

            if (posReport.IsSetSettlPrice())
                Console.WriteLine($"  Settle Price: {posReport.SettlPrice.Value}");
        }

        // Handle RequestForPositionsAck messages
        public void OnMessage(QuickFix.Message message, SessionID sessionID)
        {
            string msgType = message.Header.GetString(Tags.MsgType);

            // Handle custom message types that might not have specific classes
            if (msgType == MsgType_RequestForPositionsAck)
            {
                string posReqId = message.GetString(Tags.PosReqID);
                int totalPositions = message.GetInt(Tags.TotalNumPosReports);

                Console.WriteLine($"Received RequestForPositionsAck: {posReqId}");
                Console.WriteLine($"  Total positions: {totalPositions}");

                if (message.IsSetField(Tags.Text))
                    Console.WriteLine($"  Text: {message.GetString(Tags.Text)}");
            }
            else if (msgType == MsgType_AccountInfoResponse)
            {
                string account = message.GetString(Tags.Account);
                Console.WriteLine($"Received AccountInfoResponse for account: {account}");

                // Log any account fields present in the message
                if (message.IsSetField(1001)) // Example custom field for balance
                    Console.WriteLine($"  Balance: {message.GetDecimal(1001)}");

                if (message.IsSetField(1002)) // Example custom field for margin
                    Console.WriteLine($"  Margin: {message.GetDecimal(1002)}");

                if (message.IsSetField(Tags.Text))
                    Console.WriteLine($"  Text: {message.GetString(Tags.Text)}");
            }
            else
            {
                // Other message types we're not specifically handling
                base.FromApp(message, sessionID);
            }
        }

        #endregion

        #region Trading Methods

        // Send a new order
        public void PlaceOrder(
            string symbol,
            char side,
            double quantity,
            char orderType,
            double? price = null,
            char timeInForce = TimeInForce.DAY)
        {
            if (_sessionId == null)
            {
                Console.WriteLine("Cannot place order - not logged in");
                return;
            }

            // Generate a unique order ID
            string clOrdId = Guid.NewGuid().ToString().Replace("-", "");

            try
            {
                // Create the order message
                NewOrderSingle order = new NewOrderSingle(
                    new ClOrdID(clOrdId),
                    new Symbol(symbol),
                    new Side(side),
                    new TransactTime(DateTime.UtcNow),
                    new OrdType(orderType)
                );

                order.OrderQty = new OrderQty(quantity);
                order.TimeInForce = new TimeInForce(timeInForce);

                if (price.HasValue && (orderType == OrdType.LIMIT || orderType == OrdType.STOP_LIMIT))
                {
                    order.Price = new Price(price.Value);
                }

                // Send the order
                Session.SendToTarget(order, _sessionId);
                Console.WriteLine($"Placed {side} order for {quantity} {symbol} with ID {clOrdId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error placing order: {ex.Message}");
            }
        }

        // Cancel an existing order
        public void CancelOrder(string origClOrdId, string symbol, char side, double quantity)
        {
            if (_sessionId == null)
            {
                Console.WriteLine("Cannot cancel order - not logged in");
                return;
            }

            string clOrdId = Guid.NewGuid().ToString().Replace("-", "");

            try
            {
                OrderCancelRequest cancelRequest = new OrderCancelRequest(
                    new OrigClOrdID(origClOrdId),
                    new ClOrdID(clOrdId),
                    new Symbol(symbol),
                    new Side(side),
                    new TransactTime(DateTime.UtcNow)
                );

                cancelRequest.OrderQty = new OrderQty(quantity);

                Session.SendToTarget(cancelRequest, _sessionId);
                Console.WriteLine($"Requested to cancel order {origClOrdId} with new ID {clOrdId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cancelling order: {ex.Message}");
            }
        }

        // Modify an existing order
        public void ModifyOrder(
            string origClOrdId,
            string symbol,
            char side,
            double quantity,
            char orderType,
            double price,
            char timeInForce = TimeInForce.DAY)
        {
            if (_sessionId == null)
            {
                Console.WriteLine("Cannot modify order - not logged in");
                return;
            }

            string clOrdId = Guid.NewGuid().ToString().Replace("-", "");

            try
            {
                OrderCancelReplaceRequest replaceRequest = new OrderCancelReplaceRequest(
                    new OrigClOrdID(origClOrdId),
                    new ClOrdID(clOrdId),
                    new Symbol(symbol),
                    new Side(side),
                    new TransactTime(DateTime.UtcNow),
                    new OrdType(orderType)
                );

                replaceRequest.OrderQty = new OrderQty(quantity);
                replaceRequest.TimeInForce = new TimeInForce(timeInForce);

                if (orderType == OrdType.LIMIT || orderType == OrdType.STOP_LIMIT)
                {
                    replaceRequest.Price = new Price(price);
                }

                Session.SendToTarget(replaceRequest, _sessionId);
                Console.WriteLine($"Requested to modify order {origClOrdId} with new ID {clOrdId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error modifying order: {ex.Message}");
            }
        }

        // Get the status of a specific order
        public void GetOrderStatus(string clOrdId)
        {
            if (_sessionId == null)
            {
                Console.WriteLine("Cannot get order status - not logged in");
                return;
            }

            try
            {
                OrderStatusRequest statusRequest = new OrderStatusRequest(
                    new ClOrdID(clOrdId)
                );

                Session.SendToTarget(statusRequest, _sessionId);
                Console.WriteLine($"Requested status for order {clOrdId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting order status: {ex.Message}");
            }
        }

        // Get all open orders
        public void GetAllOrders()
        {
            if (_sessionId == null)
            {
                Console.WriteLine("Cannot get all orders - not logged in");
                return;
            }

            string massStatusReqId = Guid.NewGuid().ToString().Replace("-", "");

            try
            {
                OrderMassStatusRequest massStatusRequest = new OrderMassStatusRequest(
                    new MassStatusReqID(massStatusReqId),
                    new MassStatusReqType(MassStatusReqType.STATUS_FOR_ALL_ORDERS)
                );

                Session.SendToTarget(massStatusRequest, _sessionId);
                Console.WriteLine($"Requested status for all orders with ID {massStatusReqId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all orders: {ex.Message}");
            }
        }

        #endregion

        #region Position and Account Methods

        // Request positions
        public void RequestPositions()
        {
            if (_sessionId == null)
            {
                Console.WriteLine("Cannot request positions - not logged in");
                return;
            }

            string posReqId = Guid.NewGuid().ToString().Replace("-", "");

            try
            {
                RequestForPositions request = new RequestForPositions(
                    new PosReqID(posReqId),
                    new PosReqType(PosReqType.POSITIONS),
                    new TransactTime(DateTime.UtcNow),
                    new AccountType(AccountType.ACCOUNT_IS_CARRIED_ON_CUSTOMER_SIDE_OF_BOOKS)
                );

                Session.SendToTarget(request, _sessionId);
                Console.WriteLine($"Requested positions with ID {posReqId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error requesting positions: {ex.Message}");
            }
        }

        // Request account information
        public void RequestAccountInfo(string account)
        {
            if (_sessionId == null)
            {
                Console.WriteLine("Cannot request account info - not logged in");
                return;
            }

            try
            {
                // Create a custom AccountInfoRequest message
                QuickFix.Message request = new QuickFix.Message();
                request.Header.SetField(new MsgType(MsgType_AccountInfoRequest));
                request.SetField(new Account(account));
                request.SetField(new TransactTime(DateTime.UtcNow));

                Session.SendToTarget(request, _sessionId);
                Console.WriteLine($"Requested account information for {account}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error requesting account info: {ex.Message}");
            }
        }

        #endregion
    }
}
