using System.Data;

namespace DotNetAppBase.Std.Library.Data.Udt;

public class UdtTupleIntString : UdtBase
{
    public UdtTupleIntString()
        : base(nameof(UdtTupleIntString))
    {
        Columns.Add(new DataColumn("Item1", typeof(int)));
        Columns.Add(new DataColumn("Item2", typeof(string)));
    }

    public void Add(int? item1, string item2)
    {
        Rows.Add(XHelper.Sql.ToDbValue(item1), XHelper.Sql.ToDbValue(item2));
    }
}