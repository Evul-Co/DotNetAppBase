using System.Data;

// ReSharper disable UnusedMember.Global

namespace DotNetAppBase.Std.Library.Data.Udt;

public class UdtTupleStringString : UdtBase
{
    public UdtTupleStringString()
        : base(nameof(UdtTupleIntString))
    {
        Columns.Add(new DataColumn("Item1", typeof(string)));
        Columns.Add(new DataColumn("Item2", typeof(string)));
    }

    public void Add(string item1, string item2)
    {
        Rows.Add(XHelper.Sql.ToDbValue(item1), XHelper.Sql.ToDbValue(item2));
    }
}