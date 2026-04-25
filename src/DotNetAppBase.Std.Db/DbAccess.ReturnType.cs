namespace DotNetAppBase.Std.Db;

public partial class DbAccess
{
    public enum EReturnType
    {
        Unknown,

        DataSet,

        DataTable,

        DataRow,

        DataReader,

        Count,

        ProcReturn,

        ProcReturnAndValue
    }
}