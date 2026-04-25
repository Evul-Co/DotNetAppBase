using System.Data;
using System.Linq;

// ReSharper disable CheckNamespace
public static class XDataSetExtensions
// ReSharper restore CheckNamespace
{
    public static bool IsEmpty(this DataSet ds) => ds.Tables.Count == 0 || ds.Tables.Cast<DataTable>().All(table => table.Rows.Count == 0);
}