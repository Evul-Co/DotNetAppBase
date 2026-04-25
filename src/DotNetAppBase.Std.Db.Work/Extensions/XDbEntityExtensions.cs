using System.Collections.Generic;
using System.Data;
using System.Linq;
using DotNetAppBase.Std.Db.Work;

// ReSharper disable CheckNamespace
public static class XDataTableExtensions
// ReSharper restore CheckNamespace
{
    public static IEnumerable<TModel> Translate<TModel>(this DataTable table) where TModel : DbEntity, new()
    {
        foreach (var row in table.Rows.Cast<DataRow>())
        {
            var model = new TModel();
            model.Update(row);

            yield return model;
        }
    }
}