// This is a generated file.  Don't edit it directly!

using System;
using QuickFix.Fields;

namespace QuickFix.FIX44;

public class AccountInfo : Message
{
    public const string MsgType = "AAB";

    public AccountInfo() : base()
    {
        Header.SetField(new MsgType("AAB"));
    }

    public AccountInfo(
            Currency aCurrency,
            MarginRatio aMarginRatio,
            Balance aBalance,
            AvailableForMarginTrading aAvailableForMarginTrading,
            SecurityDeposit aSecurityDeposit,
            ClosedPL aClosedPL,
            OpenPL aOpenPL,
            MarginRequirement aMarginRequirement,
            NetOpenPosition aNetOpenPosition
        ) : this()
    {
        Currency = aCurrency;
        MarginRatio = aMarginRatio;
        Balance = aBalance;
        AvailableForMarginTrading = aAvailableForMarginTrading;
        SecurityDeposit = aSecurityDeposit;
        ClosedPL = aClosedPL;
        OpenPL = aOpenPL;
        MarginRequirement = aMarginRequirement;
        NetOpenPosition = aNetOpenPosition;
    }

    public Account Account
    {
        get
        {
            Account val = new();
            GetField(val);
            return val;
        }
        set  => SetField(value);
    }

    public void Set(Account val) { Account = val; }
    public Account Get(Account val) { GetField(val); return val; }
    public bool IsSet(Account val) { return IsSetAccount(); }
    public bool IsSetAccount() { return IsSetField(Tags.Account); }

    public CounterpartID CounterpartID
    {
        get
        {
            CounterpartID val = new();
            GetField(val);
            return val;
        }
        set  => SetField(value);
    }

    public void Set(CounterpartID val) { CounterpartID = val; }
    public CounterpartID Get(CounterpartID val) { GetField(val); return val; }
    public bool IsSet(CounterpartID val) { return IsSetCounterpartID(); }
    public bool IsSetCounterpartID() { return IsSetField(Tags.CounterpartID); }

    public Currency Currency
    {
        get
        {
            Currency val = new();
            GetField(val);
            return val;
        }
        set  => SetField(value);
    }

    public void Set(Currency val) { Currency = val; }
    public Currency Get(Currency val) { GetField(val); return val; }
    public bool IsSet(Currency val) { return IsSetCurrency(); }
    public bool IsSetCurrency() { return IsSetField(Tags.Currency); }

    public MarginRatio MarginRatio
    {
        get
        {
            MarginRatio val = new();
            GetField(val);
            return val;
        }
        set  => SetField(value);
    }

    public void Set(MarginRatio val) { MarginRatio = val; }
    public MarginRatio Get(MarginRatio val) { GetField(val); return val; }
    public bool IsSet(MarginRatio val) { return IsSetMarginRatio(); }
    public bool IsSetMarginRatio() { return IsSetField(Tags.MarginRatio); }

    public Balance Balance
    {
        get
        {
            Balance val = new();
            GetField(val);
            return val;
        }
        set  => SetField(value);
    }

    public void Set(Balance val) { Balance = val; }
    public Balance Get(Balance val) { GetField(val); return val; }
    public bool IsSet(Balance val) { return IsSetBalance(); }
    public bool IsSetBalance() { return IsSetField(Tags.Balance); }

    public AvailableForMarginTrading AvailableForMarginTrading
    {
        get
        {
            AvailableForMarginTrading val = new();
            GetField(val);
            return val;
        }
        set  => SetField(value);
    }

    public void Set(AvailableForMarginTrading val) { AvailableForMarginTrading = val; }
    public AvailableForMarginTrading Get(AvailableForMarginTrading val) { GetField(val); return val; }
    public bool IsSet(AvailableForMarginTrading val) { return IsSetAvailableForMarginTrading(); }
    public bool IsSetAvailableForMarginTrading() { return IsSetField(Tags.AvailableForMarginTrading); }

    public CreditLimit CreditLimit
    {
        get
        {
            CreditLimit val = new();
            GetField(val);
            return val;
        }
        set  => SetField(value);
    }

    public void Set(CreditLimit val) { CreditLimit = val; }
    public CreditLimit Get(CreditLimit val) { GetField(val); return val; }
    public bool IsSet(CreditLimit val) { return IsSetCreditLimit(); }
    public bool IsSetCreditLimit() { return IsSetField(Tags.CreditLimit); }

    public SecurityDeposit SecurityDeposit
    {
        get
        {
            SecurityDeposit val = new();
            GetField(val);
            return val;
        }
        set  => SetField(value);
    }

    public void Set(SecurityDeposit val) { SecurityDeposit = val; }
    public SecurityDeposit Get(SecurityDeposit val) { GetField(val); return val; }
    public bool IsSet(SecurityDeposit val) { return IsSetSecurityDeposit(); }
    public bool IsSetSecurityDeposit() { return IsSetField(Tags.SecurityDeposit); }

    public ClosedPL ClosedPL
    {
        get
        {
            ClosedPL val = new();
            GetField(val);
            return val;
        }
        set  => SetField(value);
    }

    public void Set(ClosedPL val) { ClosedPL = val; }
    public ClosedPL Get(ClosedPL val) { GetField(val); return val; }
    public bool IsSet(ClosedPL val) { return IsSetClosedPL(); }
    public bool IsSetClosedPL() { return IsSetField(Tags.ClosedPL); }

    public OpenPL OpenPL
    {
        get
        {
            OpenPL val = new();
            GetField(val);
            return val;
        }
        set  => SetField(value);
    }

    public void Set(OpenPL val) { OpenPL = val; }
    public OpenPL Get(OpenPL val) { GetField(val); return val; }
    public bool IsSet(OpenPL val) { return IsSetOpenPL(); }
    public bool IsSetOpenPL() { return IsSetField(Tags.OpenPL); }

    public MarginRequirement MarginRequirement
    {
        get
        {
            MarginRequirement val = new();
            GetField(val);
            return val;
        }
        set  => SetField(value);
    }

    public void Set(MarginRequirement val) { MarginRequirement = val; }
    public MarginRequirement Get(MarginRequirement val) { GetField(val); return val; }
    public bool IsSet(MarginRequirement val) { return IsSetMarginRequirement(); }
    public bool IsSetMarginRequirement() { return IsSetField(Tags.MarginRequirement); }

    public NetOpenPosition NetOpenPosition
    {
        get
        {
            NetOpenPosition val = new();
            GetField(val);
            return val;
        }
        set  => SetField(value);
    }

    public void Set(NetOpenPosition val) { NetOpenPosition = val; }
    public NetOpenPosition Get(NetOpenPosition val) { GetField(val); return val; }
    public bool IsSet(NetOpenPosition val) { return IsSetNetOpenPosition(); }
    public bool IsSetNetOpenPosition() { return IsSetField(Tags.NetOpenPosition); }

    public CreditLimitNOP CreditLimitNOP
    {
        get
        {
            CreditLimitNOP val = new();
            GetField(val);
            return val;
        }
        set  => SetField(value);
    }

    public void Set(CreditLimitNOP val) { CreditLimitNOP = val; }
    public CreditLimitNOP Get(CreditLimitNOP val) { GetField(val); return val; }
    public bool IsSet(CreditLimitNOP val) { return IsSetCreditLimitNOP(); }
    public bool IsSetCreditLimitNOP() { return IsSetField(Tags.CreditLimitNOP); }
}
