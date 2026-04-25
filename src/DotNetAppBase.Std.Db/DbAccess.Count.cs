namespace DotNetAppBase.Std.Db;

public partial class DbAccess
{
    public struct Count
    {
        public Count(int value)
        {
            Value = value;
        }

        public int Value { get; set; }

        public static implicit operator Count(int count) => new Count(count);

        public static implicit operator int(Count count) => count.Value;

        public static bool operator &(Count count, int value) => count.Value == value;
    }
}