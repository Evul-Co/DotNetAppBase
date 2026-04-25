using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DotNetAppBase.Std.Library;

#if NETFRAMEWORK
using System.Data.OleDb;
#endif

// ReSharper disable CheckNamespace
public static class XDataTableExtensions
// ReSharper restore CheckNamespace
{
    public static DataTable AddColumns(this DataTable dt, IDictionary<string, Type> columns)
    {
        XHelper.Enumerable.ForEach(columns, pair => dt.Columns.Add(pair.Key, pair.Value));

        return dt;
    }

    public static DataTable AddRow(this DataTable dt, params object[] rowData)
    {
        dt.Rows.Add(rowData);

        return dt;
    }

    public static DataTable Clone(this DataTable origen, IEnumerable<DataRow> rows = null, params string[] columnsName)
    {
        var dt = origen.Clone();
        var items = (rows ?? origen.Rows.Cast<DataRow>()).ToArrayEfficient();

        items.ForEach(row => dt.Rows.Add(row.ItemArray));

        foreach (var cl in dt.Columns.Cast<DataColumn>().ToArrayEfficient())
        {
            if (columnsName.Any(s => s == cl.ColumnName))
            {
                continue;
            }

            dt.Columns.Remove(cl);
        }

        return dt;
    }

    public static DataTable Create(string name = "default") => new DataTable(name);

    public static bool IsEmpty(this DataTable dt) => dt.Rows.Count == 0;

    public static bool IsNullOrEmpty(this DataTable dt) => dt == null || dt.Rows.Count == 0;

#if NETFRAMEWORK
    public static DataSet LoadDataFromExcelFile(this DataSet emptyDataSet, string fileName)
    {
        var connectionString = $"provider=Microsoft.Jet.OLEDB.4.0; data source={fileName};Extended Properties=Excel 8.0;";

        var data = emptyDataSet;

        using var con = new OleDbConnection(connectionString);
        con.Open();

        foreach (var sheetName in GetExcelSheetNames(con))
        {
            var dataTable = new DataTable();
            var query = $"SELECT * FROM [{sheetName}]";

            var adapter = new OleDbDataAdapter(query, con);
            adapter.Fill(dataTable);
            data.Tables.Add(dataTable);
        }

        return data;
    }

    private static IEnumerable<string> GetExcelSheetNames(OleDbConnection con)
    {
        var dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        if (dt == null)
        {
            return null;
        }

        var excelSheetNames = new string[dt.Rows.Count];
        var i = 0;

        foreach (DataRow row in dt.Rows)
        {
            excelSheetNames[i] = row["TABLE_NAME"].ToString();
            i++;
        }

        return excelSheetNames;
    }
#endif
}