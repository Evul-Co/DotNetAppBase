using System.ComponentModel;
using System.Data;
using System.Linq;

namespace DotNetAppBase.Std.Db.Work;

public class DbCollection<T> : BindingList<T> where T : DbEntity, new()
{
    public DbCollection() { }

    public DbCollection(DataTable table)
    {
        Load(table);
    }

    public void Load(DataTable table, bool clearAll = true)
    {
        RaiseListChangedEvents = false;
        try
        {
            if (clearAll)
            {
                Clear();
            }

            if (table == null)
            {
                return;
            }

            foreach (var row in table.Rows.Cast<DataRow>())
            {
                var entity = new T();
                entity.Update(row);

                Add(entity);
            }
        }
        finally
        {
            RaiseListChangedEvents = true;

            ResetBindings();
        }
    }
}