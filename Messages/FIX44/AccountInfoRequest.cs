// This is a generated file.  Don't edit it directly!

using System;
using QuickFix.Fields;

namespace QuickFix.FIX44;

public class AccountInfoRequest : Message
{
    public const string MsgType = "AAA";

    public AccountInfoRequest() : base()
    {
        Header.SetField(new MsgType("AAA"));
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
}
