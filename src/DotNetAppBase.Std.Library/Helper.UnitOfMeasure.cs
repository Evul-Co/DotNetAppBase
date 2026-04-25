namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static class UnitOfMeasure
    {
        public enum EUnitType
        {
            Meter,
            Kilometer,
            Degree
        }

        public const double DegreeOnMeters = 111111.11;
        public const double DegreeOnKilometers = 111.11111;

        public const long Kilobyte = 1024;
        public const long Megabyte = 1024 * Kilobyte;
        public const long Gigabyte = 1024 * Megabyte;
        public const long Terabyte = 1024 * Gigabyte;
        public const long Petabyte = 1024 * Gigabyte;

        public static double ConvertDegreeTo(double rate, EUnitType futureUnit)
        {
            return futureUnit switch
                {
                    EUnitType.Kilometer => (rate * DegreeOnKilometers),
                    EUnitType.Meter => (rate * DegreeOnMeters),
                    _ => -1
                };
        }
    }
}