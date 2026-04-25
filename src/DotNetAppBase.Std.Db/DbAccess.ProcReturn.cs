namespace DotNetAppBase.Std.Db;

public partial class DbAccess
{
    public class Return
    {
        public Return(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static bool operator &(Return @return, int value) => @return.Value == value;

        public static implicit operator Return(int count) => new Return(count);

        public static implicit operator int(Return count) => count.Value;
    }

    public class ReturnAndData<TData> : Return
    {
        public ReturnAndData(int value, TData data)
            : base(value)
        {
            Data = data;
        }

        public TData Data { get; }
    }
}