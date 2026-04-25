using System.Collections.Generic;
using System.Data;

namespace DotNetAppBase.Std.Library.Data.Udt;

public class UdtKey<TKey> : UdtBase
{
    public UdtKey()
        : base(GetTableName())
    {
        Columns.Add(new DataColumn("ID", typeof(TKey)));
    }

    public UdtKey(IEnumerable<TKey> data)
        : this()
    {
        XHelper.Enumerable.ForEach(data, Add);
    }

    public void Add(TKey id) => Rows.Add(XHelper.Sql.ToDbValue(id));

    private static string GetTableName() => $"Udt{typeof(TKey).Name}Key";
}