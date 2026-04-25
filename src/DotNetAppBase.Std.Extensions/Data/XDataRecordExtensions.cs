using System.Data;

// ReSharper disable CheckNamespace
public static class XDataRecordExtensions
// ReSharper restore CheckNamespace
{
    public static object GetValue(this IDataRecord record, string name)
    {
        var ordinal = record.GetOrdinal(name);
        if (record.IsDBNull(ordinal))
        {
            return null;
        }

        return record.GetValue(ordinal);
    }
}