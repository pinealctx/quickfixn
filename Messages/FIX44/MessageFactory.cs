// This is a generated file.  Don't edit it directly!

using System.Collections.Generic;
using QuickFix.FixValues;

namespace QuickFix.FIX44;

public class MessageFactory : IMessageFactory
{
    public ICollection<string> GetSupportedBeginStrings()
    {
       return new [] { BeginString.FIX44 };
    }

    public QuickFix.Message Create(string beginString, QuickFix.Fields.ApplVerID applVerId, string msgType)
    {
        return Create(beginString, msgType);
    }

    public QuickFix.Message Create(string beginString, string msgType)
    {
        return msgType switch
        {
            QuickFix.FIX44.Heartbeat.MsgType => new QuickFix.FIX44.Heartbeat(),
            QuickFix.FIX44.TestRequest.MsgType => new QuickFix.FIX44.TestRequest(),
            QuickFix.FIX44.ResendRequest.MsgType => new QuickFix.FIX44.ResendRequest(),
            QuickFix.FIX44.Reject.MsgType => new QuickFix.FIX44.Reject(),
            QuickFix.FIX44.SequenceReset.MsgType => new QuickFix.FIX44.SequenceReset(),
            QuickFix.FIX44.Logout.MsgType => new QuickFix.FIX44.Logout(),
            QuickFix.FIX44.IndicationOfInterest.MsgType => new QuickFix.FIX44.IndicationOfInterest(),
            QuickFix.FIX44.Advertisement.MsgType => new QuickFix.FIX44.Advertisement(),
            QuickFix.FIX44.ExecutionReport.MsgType => new QuickFix.FIX44.ExecutionReport(),
            QuickFix.FIX44.OrderCancelReject.MsgType => new QuickFix.FIX44.OrderCancelReject(),
            QuickFix.FIX44.Logon.MsgType => new QuickFix.FIX44.Logon(),
            QuickFix.FIX44.News.MsgType => new QuickFix.FIX44.News(),
            QuickFix.FIX44.Email.MsgType => new QuickFix.FIX44.Email(),
            QuickFix.FIX44.NewOrderSingle.MsgType => new QuickFix.FIX44.NewOrderSingle(),
            QuickFix.FIX44.NewOrderList.MsgType => new QuickFix.FIX44.NewOrderList(),
            QuickFix.FIX44.OrderCancelRequest.MsgType => new QuickFix.FIX44.OrderCancelRequest(),
            QuickFix.FIX44.OrderCancelReplaceRequest.MsgType => new QuickFix.FIX44.OrderCancelReplaceRequest(),
            QuickFix.FIX44.OrderStatusRequest.MsgType => new QuickFix.FIX44.OrderStatusRequest(),
            QuickFix.FIX44.AllocationInstruction.MsgType => new QuickFix.FIX44.AllocationInstruction(),
            QuickFix.FIX44.ListCancelRequest.MsgType => new QuickFix.FIX44.ListCancelRequest(),
            QuickFix.FIX44.ListExecute.MsgType => new QuickFix.FIX44.ListExecute(),
            QuickFix.FIX44.ListStatusRequest.MsgType => new QuickFix.FIX44.ListStatusRequest(),
            QuickFix.FIX44.ListStatus.MsgType => new QuickFix.FIX44.ListStatus(),
            QuickFix.FIX44.AllocationInstructionAck.MsgType => new QuickFix.FIX44.AllocationInstructionAck(),
            QuickFix.FIX44.DontKnowTrade.MsgType => new QuickFix.FIX44.DontKnowTrade(),
            QuickFix.FIX44.QuoteRequest.MsgType => new QuickFix.FIX44.QuoteRequest(),
            QuickFix.FIX44.Quote.MsgType => new QuickFix.FIX44.Quote(),
            QuickFix.FIX44.SettlementInstructions.MsgType => new QuickFix.FIX44.SettlementInstructions(),
            QuickFix.FIX44.MarketDataRequest.MsgType => new QuickFix.FIX44.MarketDataRequest(),
            QuickFix.FIX44.MarketDataSnapshotFullRefresh.MsgType => new QuickFix.FIX44.MarketDataSnapshotFullRefresh(),
            QuickFix.FIX44.MarketDataIncrementalRefresh.MsgType => new QuickFix.FIX44.MarketDataIncrementalRefresh(),
            QuickFix.FIX44.MarketDataRequestReject.MsgType => new QuickFix.FIX44.MarketDataRequestReject(),
            QuickFix.FIX44.QuoteCancel.MsgType => new QuickFix.FIX44.QuoteCancel(),
            QuickFix.FIX44.QuoteStatusRequest.MsgType => new QuickFix.FIX44.QuoteStatusRequest(),
            QuickFix.FIX44.MassQuoteAcknowledgement.MsgType => new QuickFix.FIX44.MassQuoteAcknowledgement(),
            QuickFix.FIX44.SecurityDefinitionRequest.MsgType => new QuickFix.FIX44.SecurityDefinitionRequest(),
            QuickFix.FIX44.SecurityDefinition.MsgType => new QuickFix.FIX44.SecurityDefinition(),
            QuickFix.FIX44.SecurityStatusRequest.MsgType => new QuickFix.FIX44.SecurityStatusRequest(),
            QuickFix.FIX44.SecurityStatus.MsgType => new QuickFix.FIX44.SecurityStatus(),
            QuickFix.FIX44.TradingSessionStatusRequest.MsgType => new QuickFix.FIX44.TradingSessionStatusRequest(),
            QuickFix.FIX44.TradingSessionStatus.MsgType => new QuickFix.FIX44.TradingSessionStatus(),
            QuickFix.FIX44.MassQuote.MsgType => new QuickFix.FIX44.MassQuote(),
            QuickFix.FIX44.BusinessMessageReject.MsgType => new QuickFix.FIX44.BusinessMessageReject(),
            QuickFix.FIX44.SecurityListRequest.MsgType => new QuickFix.FIX44.SecurityListRequest(),
            QuickFix.FIX44.SecurityList.MsgType => new QuickFix.FIX44.SecurityList(),
            QuickFix.FIX44.OrderMassStatusRequest.MsgType => new QuickFix.FIX44.OrderMassStatusRequest(),
            QuickFix.FIX44.RequestForPositions.MsgType => new QuickFix.FIX44.RequestForPositions(),
            QuickFix.FIX44.RequestForPositionsAck.MsgType => new QuickFix.FIX44.RequestForPositionsAck(),
            QuickFix.FIX44.PositionReport.MsgType => new QuickFix.FIX44.PositionReport(),
            QuickFix.FIX44.AccountInfoRequest.MsgType => new QuickFix.FIX44.AccountInfoRequest(),
            QuickFix.FIX44.AccountInfo.MsgType => new QuickFix.FIX44.AccountInfo(),
            _ => new QuickFix.Message()
        };
    }

    public Group? Create(string beginString, string msgType, int correspondingFieldId)
    {
        if (QuickFix.FIX44.IndicationOfInterest.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.IndicationOfInterest.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.IndicationOfInterest.NoEventsGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.IndicationOfInterest.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.IndicationOfInterest.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.IndicationOfInterest.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoStipulations: return new QuickFix.FIX44.IndicationOfInterest.NoStipulationsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.IndicationOfInterest.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.IndicationOfInterest.NoLegsGroup.NoLegSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoLegStipulations: return new QuickFix.FIX44.IndicationOfInterest.NoLegsGroup.NoLegStipulationsGroup();
                case QuickFix.Fields.Tags.NoIOIQualifiers: return new QuickFix.FIX44.IndicationOfInterest.NoIOIQualifiersGroup();
                case QuickFix.Fields.Tags.NoRoutingIDs: return new QuickFix.FIX44.IndicationOfInterest.NoRoutingIDsGroup();
            }
        }

        if (QuickFix.FIX44.Advertisement.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.Advertisement.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.Advertisement.NoEventsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.Advertisement.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.Advertisement.NoLegsGroup.NoLegSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.Advertisement.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.Advertisement.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.Advertisement.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
            }
        }

        if (QuickFix.FIX44.ExecutionReport.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.ExecutionReport.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.ExecutionReport.NoPartyIDsGroup.NoPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoContraBrokers: return new QuickFix.FIX44.ExecutionReport.NoContraBrokersGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.ExecutionReport.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.ExecutionReport.NoEventsGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.ExecutionReport.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.ExecutionReport.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.ExecutionReport.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoStipulations: return new QuickFix.FIX44.ExecutionReport.NoStipulationsGroup();
                case QuickFix.Fields.Tags.NoContAmts: return new QuickFix.FIX44.ExecutionReport.NoContAmtsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.ExecutionReport.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.ExecutionReport.NoLegsGroup.NoLegSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoLegStipulations: return new QuickFix.FIX44.ExecutionReport.NoLegsGroup.NoLegStipulationsGroup();
                case QuickFix.Fields.Tags.NoNestedPartyIDs: return new QuickFix.FIX44.ExecutionReport.NoLegsGroup.NoNestedPartyIDsGroup();
                case QuickFix.Fields.Tags.NoNestedPartySubIDs: return new QuickFix.FIX44.ExecutionReport.NoLegsGroup.NoNestedPartyIDsGroup.NoNestedPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoMiscFees: return new QuickFix.FIX44.ExecutionReport.NoMiscFeesGroup();
            }
        }

        if (QuickFix.FIX44.Logon.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoMsgTypes: return new QuickFix.FIX44.Logon.NoMsgTypesGroup();
            }
        }

        if (QuickFix.FIX44.News.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoRoutingIDs: return new QuickFix.FIX44.News.NoRoutingIDsGroup();
                case QuickFix.Fields.Tags.NoRelatedSym: return new QuickFix.FIX44.News.NoRelatedSymGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.News.NoRelatedSymGroup.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.News.NoRelatedSymGroup.NoEventsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.News.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.News.NoLegsGroup.NoLegSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.News.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.News.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.News.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.LinesOfText: return new QuickFix.FIX44.News.LinesOfTextGroup();
            }
        }

        if (QuickFix.FIX44.Email.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoRoutingIDs: return new QuickFix.FIX44.Email.NoRoutingIDsGroup();
                case QuickFix.Fields.Tags.NoRelatedSym: return new QuickFix.FIX44.Email.NoRelatedSymGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.Email.NoRelatedSymGroup.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.Email.NoRelatedSymGroup.NoEventsGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.Email.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.Email.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.Email.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.Email.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.Email.NoLegsGroup.NoLegSecurityAltIDGroup();
                case QuickFix.Fields.Tags.LinesOfText: return new QuickFix.FIX44.Email.LinesOfTextGroup();
            }
        }

        if (QuickFix.FIX44.NewOrderSingle.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.NewOrderSingle.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.NewOrderSingle.NoPartyIDsGroup.NoPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoAllocs: return new QuickFix.FIX44.NewOrderSingle.NoAllocsGroup();
                case QuickFix.Fields.Tags.NoNestedPartyIDs: return new QuickFix.FIX44.NewOrderSingle.NoAllocsGroup.NoNestedPartyIDsGroup();
                case QuickFix.Fields.Tags.NoNestedPartySubIDs: return new QuickFix.FIX44.NewOrderSingle.NoAllocsGroup.NoNestedPartyIDsGroup.NoNestedPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoTradingSessions: return new QuickFix.FIX44.NewOrderSingle.NoTradingSessionsGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.NewOrderSingle.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.NewOrderSingle.NoEventsGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.NewOrderSingle.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.NewOrderSingle.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.NewOrderSingle.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoStipulations: return new QuickFix.FIX44.NewOrderSingle.NoStipulationsGroup();
            }
        }

        if (QuickFix.FIX44.NewOrderList.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoOrders: return new QuickFix.FIX44.NewOrderList.NoOrdersGroup();
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.NewOrderList.NoOrdersGroup.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.NewOrderList.NoOrdersGroup.NoPartyIDsGroup.NoPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoAllocs: return new QuickFix.FIX44.NewOrderList.NoOrdersGroup.NoAllocsGroup();
                case QuickFix.Fields.Tags.NoNestedPartyIDs: return new QuickFix.FIX44.NewOrderList.NoOrdersGroup.NoAllocsGroup.NoNestedPartyIDsGroup();
                case QuickFix.Fields.Tags.NoNestedPartySubIDs: return new QuickFix.FIX44.NewOrderList.NoOrdersGroup.NoAllocsGroup.NoNestedPartyIDsGroup.NoNestedPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoTradingSessions: return new QuickFix.FIX44.NewOrderList.NoOrdersGroup.NoTradingSessionsGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.NewOrderList.NoOrdersGroup.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.NewOrderList.NoOrdersGroup.NoEventsGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.NewOrderList.NoOrdersGroup.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.NewOrderList.NoOrdersGroup.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.NewOrderList.NoOrdersGroup.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoStipulations: return new QuickFix.FIX44.NewOrderList.NoOrdersGroup.NoStipulationsGroup();
            }
        }

        if (QuickFix.FIX44.OrderCancelRequest.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.OrderCancelRequest.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.OrderCancelRequest.NoPartyIDsGroup.NoPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.OrderCancelRequest.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.OrderCancelRequest.NoEventsGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.OrderCancelRequest.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.OrderCancelRequest.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.OrderCancelRequest.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
            }
        }

        if (QuickFix.FIX44.OrderCancelReplaceRequest.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.OrderCancelReplaceRequest.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.OrderCancelReplaceRequest.NoPartyIDsGroup.NoPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoAllocs: return new QuickFix.FIX44.OrderCancelReplaceRequest.NoAllocsGroup();
                case QuickFix.Fields.Tags.NoNestedPartyIDs: return new QuickFix.FIX44.OrderCancelReplaceRequest.NoAllocsGroup.NoNestedPartyIDsGroup();
                case QuickFix.Fields.Tags.NoNestedPartySubIDs: return new QuickFix.FIX44.OrderCancelReplaceRequest.NoAllocsGroup.NoNestedPartyIDsGroup.NoNestedPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoTradingSessions: return new QuickFix.FIX44.OrderCancelReplaceRequest.NoTradingSessionsGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.OrderCancelReplaceRequest.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.OrderCancelReplaceRequest.NoEventsGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.OrderCancelReplaceRequest.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.OrderCancelReplaceRequest.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.OrderCancelReplaceRequest.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
            }
        }

        if (QuickFix.FIX44.OrderStatusRequest.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.OrderStatusRequest.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.OrderStatusRequest.NoPartyIDsGroup.NoPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.OrderStatusRequest.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.OrderStatusRequest.NoEventsGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.OrderStatusRequest.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.OrderStatusRequest.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.OrderStatusRequest.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
            }
        }

        if (QuickFix.FIX44.AllocationInstruction.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoOrders: return new QuickFix.FIX44.AllocationInstruction.NoOrdersGroup();
                case QuickFix.Fields.Tags.NoNested2PartyIDs: return new QuickFix.FIX44.AllocationInstruction.NoOrdersGroup.NoNested2PartyIDsGroup();
                case QuickFix.Fields.Tags.NoNested2PartySubIDs: return new QuickFix.FIX44.AllocationInstruction.NoOrdersGroup.NoNested2PartyIDsGroup.NoNested2PartySubIDsGroup();
                case QuickFix.Fields.Tags.NoExecs: return new QuickFix.FIX44.AllocationInstruction.NoExecsGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.AllocationInstruction.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.AllocationInstruction.NoEventsGroup();
                case QuickFix.Fields.Tags.NoInstrAttrib: return new QuickFix.FIX44.AllocationInstruction.NoInstrAttribGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.AllocationInstruction.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.AllocationInstruction.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.AllocationInstruction.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.AllocationInstruction.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.AllocationInstruction.NoLegsGroup.NoLegSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.AllocationInstruction.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.AllocationInstruction.NoPartyIDsGroup.NoPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoStipulations: return new QuickFix.FIX44.AllocationInstruction.NoStipulationsGroup();
                case QuickFix.Fields.Tags.NoAllocs: return new QuickFix.FIX44.AllocationInstruction.NoAllocsGroup();
                case QuickFix.Fields.Tags.NoNestedPartyIDs: return new QuickFix.FIX44.AllocationInstruction.NoAllocsGroup.NoNestedPartyIDsGroup();
                case QuickFix.Fields.Tags.NoNestedPartySubIDs: return new QuickFix.FIX44.AllocationInstruction.NoAllocsGroup.NoNestedPartyIDsGroup.NoNestedPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoMiscFees: return new QuickFix.FIX44.AllocationInstruction.NoAllocsGroup.NoMiscFeesGroup();
                case QuickFix.Fields.Tags.NoDlvyInst: return new QuickFix.FIX44.AllocationInstruction.NoAllocsGroup.NoDlvyInstGroup();
                case QuickFix.Fields.Tags.NoSettlPartyIDs: return new QuickFix.FIX44.AllocationInstruction.NoAllocsGroup.NoDlvyInstGroup.NoSettlPartyIDsGroup();
                case QuickFix.Fields.Tags.NoSettlPartySubIDs: return new QuickFix.FIX44.AllocationInstruction.NoAllocsGroup.NoDlvyInstGroup.NoSettlPartyIDsGroup.NoSettlPartySubIDsGroup();
            }
        }

        if (QuickFix.FIX44.ListStatus.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoOrders: return new QuickFix.FIX44.ListStatus.NoOrdersGroup();
            }
        }

        if (QuickFix.FIX44.AllocationInstructionAck.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.AllocationInstructionAck.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.AllocationInstructionAck.NoPartyIDsGroup.NoPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoAllocs: return new QuickFix.FIX44.AllocationInstructionAck.NoAllocsGroup();
            }
        }

        if (QuickFix.FIX44.DontKnowTrade.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.DontKnowTrade.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.DontKnowTrade.NoEventsGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.DontKnowTrade.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.DontKnowTrade.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.DontKnowTrade.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.DontKnowTrade.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.DontKnowTrade.NoLegsGroup.NoLegSecurityAltIDGroup();
            }
        }

        if (QuickFix.FIX44.QuoteRequest.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoRelatedSym: return new QuickFix.FIX44.QuoteRequest.NoRelatedSymGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.QuoteRequest.NoRelatedSymGroup.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.QuoteRequest.NoRelatedSymGroup.NoEventsGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.QuoteRequest.NoRelatedSymGroup.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.QuoteRequest.NoRelatedSymGroup.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.QuoteRequest.NoRelatedSymGroup.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoStipulations: return new QuickFix.FIX44.QuoteRequest.NoRelatedSymGroup.NoStipulationsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.QuoteRequest.NoRelatedSymGroup.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.QuoteRequest.NoRelatedSymGroup.NoLegsGroup.NoLegSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoLegStipulations: return new QuickFix.FIX44.QuoteRequest.NoRelatedSymGroup.NoLegsGroup.NoLegStipulationsGroup();
                case QuickFix.Fields.Tags.NoNestedPartyIDs: return new QuickFix.FIX44.QuoteRequest.NoRelatedSymGroup.NoLegsGroup.NoNestedPartyIDsGroup();
                case QuickFix.Fields.Tags.NoNestedPartySubIDs: return new QuickFix.FIX44.QuoteRequest.NoRelatedSymGroup.NoLegsGroup.NoNestedPartyIDsGroup.NoNestedPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoQuoteQualifiers: return new QuickFix.FIX44.QuoteRequest.NoRelatedSymGroup.NoQuoteQualifiersGroup();
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.QuoteRequest.NoRelatedSymGroup.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.QuoteRequest.NoRelatedSymGroup.NoPartyIDsGroup.NoPartySubIDsGroup();
            }
        }

        if (QuickFix.FIX44.Quote.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoQuoteQualifiers: return new QuickFix.FIX44.Quote.NoQuoteQualifiersGroup();
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.Quote.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.Quote.NoPartyIDsGroup.NoPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.Quote.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.Quote.NoEventsGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.Quote.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.Quote.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.Quote.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoStipulations: return new QuickFix.FIX44.Quote.NoStipulationsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.Quote.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.Quote.NoLegsGroup.NoLegSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoLegStipulations: return new QuickFix.FIX44.Quote.NoLegsGroup.NoLegStipulationsGroup();
                case QuickFix.Fields.Tags.NoNestedPartyIDs: return new QuickFix.FIX44.Quote.NoLegsGroup.NoNestedPartyIDsGroup();
                case QuickFix.Fields.Tags.NoNestedPartySubIDs: return new QuickFix.FIX44.Quote.NoLegsGroup.NoNestedPartyIDsGroup.NoNestedPartySubIDsGroup();
            }
        }

        if (QuickFix.FIX44.SettlementInstructions.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoSettlInst: return new QuickFix.FIX44.SettlementInstructions.NoSettlInstGroup();
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.SettlementInstructions.NoSettlInstGroup.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.SettlementInstructions.NoSettlInstGroup.NoPartyIDsGroup.NoPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoDlvyInst: return new QuickFix.FIX44.SettlementInstructions.NoSettlInstGroup.NoDlvyInstGroup();
                case QuickFix.Fields.Tags.NoSettlPartyIDs: return new QuickFix.FIX44.SettlementInstructions.NoSettlInstGroup.NoDlvyInstGroup.NoSettlPartyIDsGroup();
                case QuickFix.Fields.Tags.NoSettlPartySubIDs: return new QuickFix.FIX44.SettlementInstructions.NoSettlInstGroup.NoDlvyInstGroup.NoSettlPartyIDsGroup.NoSettlPartySubIDsGroup();
            }
        }

        if (QuickFix.FIX44.MarketDataRequest.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoMDEntryTypes: return new QuickFix.FIX44.MarketDataRequest.NoMDEntryTypesGroup();
                case QuickFix.Fields.Tags.NoRelatedSym: return new QuickFix.FIX44.MarketDataRequest.NoRelatedSymGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.MarketDataRequest.NoRelatedSymGroup.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.MarketDataRequest.NoRelatedSymGroup.NoEventsGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.MarketDataRequest.NoRelatedSymGroup.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.MarketDataRequest.NoRelatedSymGroup.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.MarketDataRequest.NoRelatedSymGroup.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.MarketDataRequest.NoRelatedSymGroup.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.MarketDataRequest.NoRelatedSymGroup.NoLegsGroup.NoLegSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoTradingSessions: return new QuickFix.FIX44.MarketDataRequest.NoTradingSessionsGroup();
            }
        }

        if (QuickFix.FIX44.MarketDataSnapshotFullRefresh.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.MarketDataSnapshotFullRefresh.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.MarketDataSnapshotFullRefresh.NoEventsGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.MarketDataSnapshotFullRefresh.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.MarketDataSnapshotFullRefresh.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.MarketDataSnapshotFullRefresh.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.MarketDataSnapshotFullRefresh.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.MarketDataSnapshotFullRefresh.NoLegsGroup.NoLegSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoMDEntries: return new QuickFix.FIX44.MarketDataSnapshotFullRefresh.NoMDEntriesGroup();
                case QuickFix.Fields.Tags.NoTrdRegTimestamps: return new QuickFix.FIX44.MarketDataSnapshotFullRefresh.NoMDEntriesGroup.NoTrdRegTimestampsGroup();
            }
        }

        if (QuickFix.FIX44.MarketDataIncrementalRefresh.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoMDEntries: return new QuickFix.FIX44.MarketDataIncrementalRefresh.NoMDEntriesGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.MarketDataIncrementalRefresh.NoMDEntriesGroup.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.MarketDataIncrementalRefresh.NoMDEntriesGroup.NoEventsGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.MarketDataIncrementalRefresh.NoMDEntriesGroup.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.MarketDataIncrementalRefresh.NoMDEntriesGroup.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.MarketDataIncrementalRefresh.NoMDEntriesGroup.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.MarketDataIncrementalRefresh.NoMDEntriesGroup.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.MarketDataIncrementalRefresh.NoMDEntriesGroup.NoLegsGroup.NoLegSecurityAltIDGroup();
            }
        }

        if (QuickFix.FIX44.MarketDataRequestReject.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoAltMDSource: return new QuickFix.FIX44.MarketDataRequestReject.NoAltMDSourceGroup();
            }
        }

        if (QuickFix.FIX44.QuoteCancel.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.QuoteCancel.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.QuoteCancel.NoPartyIDsGroup.NoPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoQuoteEntries: return new QuickFix.FIX44.QuoteCancel.NoQuoteEntriesGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.QuoteCancel.NoQuoteEntriesGroup.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.QuoteCancel.NoQuoteEntriesGroup.NoEventsGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.QuoteCancel.NoQuoteEntriesGroup.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.QuoteCancel.NoQuoteEntriesGroup.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.QuoteCancel.NoQuoteEntriesGroup.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.QuoteCancel.NoQuoteEntriesGroup.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.QuoteCancel.NoQuoteEntriesGroup.NoLegsGroup.NoLegSecurityAltIDGroup();
            }
        }

        if (QuickFix.FIX44.QuoteStatusRequest.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.QuoteStatusRequest.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.QuoteStatusRequest.NoEventsGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.QuoteStatusRequest.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.QuoteStatusRequest.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.QuoteStatusRequest.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.QuoteStatusRequest.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.QuoteStatusRequest.NoLegsGroup.NoLegSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.QuoteStatusRequest.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.QuoteStatusRequest.NoPartyIDsGroup.NoPartySubIDsGroup();
            }
        }

        if (QuickFix.FIX44.MassQuoteAcknowledgement.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.MassQuoteAcknowledgement.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.MassQuoteAcknowledgement.NoPartyIDsGroup.NoPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoQuoteSets: return new QuickFix.FIX44.MassQuoteAcknowledgement.NoQuoteSetsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.MassQuoteAcknowledgement.NoQuoteSetsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.MassQuoteAcknowledgement.NoQuoteSetsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoQuoteEntries: return new QuickFix.FIX44.MassQuoteAcknowledgement.NoQuoteSetsGroup.NoQuoteEntriesGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.MassQuoteAcknowledgement.NoQuoteSetsGroup.NoQuoteEntriesGroup.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.MassQuoteAcknowledgement.NoQuoteSetsGroup.NoQuoteEntriesGroup.NoEventsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.MassQuoteAcknowledgement.NoQuoteSetsGroup.NoQuoteEntriesGroup.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.MassQuoteAcknowledgement.NoQuoteSetsGroup.NoQuoteEntriesGroup.NoLegsGroup.NoLegSecurityAltIDGroup();
            }
        }

        if (QuickFix.FIX44.SecurityDefinitionRequest.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.SecurityDefinitionRequest.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.SecurityDefinitionRequest.NoEventsGroup();
                case QuickFix.Fields.Tags.NoInstrAttrib: return new QuickFix.FIX44.SecurityDefinitionRequest.NoInstrAttribGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.SecurityDefinitionRequest.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.SecurityDefinitionRequest.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.SecurityDefinitionRequest.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.SecurityDefinitionRequest.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.SecurityDefinitionRequest.NoLegsGroup.NoLegSecurityAltIDGroup();
            }
        }

        if (QuickFix.FIX44.SecurityDefinition.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.SecurityDefinition.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.SecurityDefinition.NoEventsGroup();
                case QuickFix.Fields.Tags.NoInstrAttrib: return new QuickFix.FIX44.SecurityDefinition.NoInstrAttribGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.SecurityDefinition.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.SecurityDefinition.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.SecurityDefinition.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.SecurityDefinition.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.SecurityDefinition.NoLegsGroup.NoLegSecurityAltIDGroup();
            }
        }

        if (QuickFix.FIX44.SecurityStatusRequest.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.SecurityStatusRequest.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.SecurityStatusRequest.NoEventsGroup();
                case QuickFix.Fields.Tags.NoInstrAttrib: return new QuickFix.FIX44.SecurityStatusRequest.NoInstrAttribGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.SecurityStatusRequest.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.SecurityStatusRequest.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.SecurityStatusRequest.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.SecurityStatusRequest.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.SecurityStatusRequest.NoLegsGroup.NoLegSecurityAltIDGroup();
            }
        }

        if (QuickFix.FIX44.SecurityStatus.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.SecurityStatus.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.SecurityStatus.NoEventsGroup();
                case QuickFix.Fields.Tags.NoInstrAttrib: return new QuickFix.FIX44.SecurityStatus.NoInstrAttribGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.SecurityStatus.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.SecurityStatus.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.SecurityStatus.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.SecurityStatus.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.SecurityStatus.NoLegsGroup.NoLegSecurityAltIDGroup();
            }
        }

        if (QuickFix.FIX44.MassQuote.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.MassQuote.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.MassQuote.NoPartyIDsGroup.NoPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoQuoteSets: return new QuickFix.FIX44.MassQuote.NoQuoteSetsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.MassQuote.NoQuoteSetsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.MassQuote.NoQuoteSetsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoQuoteEntries: return new QuickFix.FIX44.MassQuote.NoQuoteSetsGroup.NoQuoteEntriesGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.MassQuote.NoQuoteSetsGroup.NoQuoteEntriesGroup.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.MassQuote.NoQuoteSetsGroup.NoQuoteEntriesGroup.NoEventsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.MassQuote.NoQuoteSetsGroup.NoQuoteEntriesGroup.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.MassQuote.NoQuoteSetsGroup.NoQuoteEntriesGroup.NoLegsGroup.NoLegSecurityAltIDGroup();
            }
        }

        if (QuickFix.FIX44.SecurityListRequest.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.SecurityListRequest.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.SecurityListRequest.NoEventsGroup();
                case QuickFix.Fields.Tags.NoInstrAttrib: return new QuickFix.FIX44.SecurityListRequest.NoInstrAttribGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.SecurityListRequest.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.SecurityListRequest.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.SecurityListRequest.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.SecurityListRequest.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.SecurityListRequest.NoLegsGroup.NoLegSecurityAltIDGroup();
            }
        }

        if (QuickFix.FIX44.SecurityList.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoRelatedSym: return new QuickFix.FIX44.SecurityList.NoRelatedSymGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.SecurityList.NoRelatedSymGroup.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.SecurityList.NoRelatedSymGroup.NoEventsGroup();
                case QuickFix.Fields.Tags.NoInstrAttrib: return new QuickFix.FIX44.SecurityList.NoRelatedSymGroup.NoInstrAttribGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.SecurityList.NoRelatedSymGroup.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.SecurityList.NoRelatedSymGroup.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.SecurityList.NoRelatedSymGroup.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoStipulations: return new QuickFix.FIX44.SecurityList.NoRelatedSymGroup.NoStipulationsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.SecurityList.NoRelatedSymGroup.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.SecurityList.NoRelatedSymGroup.NoLegsGroup.NoLegSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoLegStipulations: return new QuickFix.FIX44.SecurityList.NoRelatedSymGroup.NoLegsGroup.NoLegStipulationsGroup();
            }
        }

        if (QuickFix.FIX44.OrderMassStatusRequest.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.OrderMassStatusRequest.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.OrderMassStatusRequest.NoPartyIDsGroup.NoPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.OrderMassStatusRequest.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.OrderMassStatusRequest.NoEventsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.OrderMassStatusRequest.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.OrderMassStatusRequest.NoUnderlyingStipsGroup();
            }
        }

        if (QuickFix.FIX44.RequestForPositions.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.RequestForPositions.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.RequestForPositions.NoPartyIDsGroup.NoPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.RequestForPositions.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.RequestForPositions.NoEventsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.RequestForPositions.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.RequestForPositions.NoLegsGroup.NoLegSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.RequestForPositions.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.RequestForPositions.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.RequestForPositions.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoTradingSessions: return new QuickFix.FIX44.RequestForPositions.NoTradingSessionsGroup();
            }
        }

        if (QuickFix.FIX44.RequestForPositionsAck.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.RequestForPositionsAck.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.RequestForPositionsAck.NoPartyIDsGroup.NoPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.RequestForPositionsAck.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.RequestForPositionsAck.NoEventsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.RequestForPositionsAck.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.RequestForPositionsAck.NoLegsGroup.NoLegSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.RequestForPositionsAck.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.RequestForPositionsAck.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.RequestForPositionsAck.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
            }
        }

        if (QuickFix.FIX44.PositionReport.MsgType.Equals(msgType))
        {
            switch (correspondingFieldId)
            {
                case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX44.PositionReport.NoPartyIDsGroup();
                case QuickFix.Fields.Tags.NoPartySubIDs: return new QuickFix.FIX44.PositionReport.NoPartyIDsGroup.NoPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX44.PositionReport.NoSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX44.PositionReport.NoEventsGroup();
                case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX44.PositionReport.NoLegsGroup();
                case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX44.PositionReport.NoLegsGroup.NoLegSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyings: return new QuickFix.FIX44.PositionReport.NoUnderlyingsGroup();
                case QuickFix.Fields.Tags.NoUnderlyingSecurityAltID: return new QuickFix.FIX44.PositionReport.NoUnderlyingsGroup.NoUnderlyingSecurityAltIDGroup();
                case QuickFix.Fields.Tags.NoUnderlyingStips: return new QuickFix.FIX44.PositionReport.NoUnderlyingsGroup.NoUnderlyingStipsGroup();
                case QuickFix.Fields.Tags.NoPositions: return new QuickFix.FIX44.PositionReport.NoPositionsGroup();
                case QuickFix.Fields.Tags.NoNestedPartyIDs: return new QuickFix.FIX44.PositionReport.NoPositionsGroup.NoNestedPartyIDsGroup();
                case QuickFix.Fields.Tags.NoNestedPartySubIDs: return new QuickFix.FIX44.PositionReport.NoPositionsGroup.NoNestedPartyIDsGroup.NoNestedPartySubIDsGroup();
                case QuickFix.Fields.Tags.NoPosAmt: return new QuickFix.FIX44.PositionReport.NoPosAmtGroup();
            }
        }

        return null;
    }
}
