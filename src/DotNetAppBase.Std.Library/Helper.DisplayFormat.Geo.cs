using System;
using System.ComponentModel;
using DotNetAppBase.Std.Library.Properties;

#pragma warning disable 1591

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static partial class DisplayFormat
    {
        public static class Geo
        {
            [Localizable(false)]
            public static string FormatDistance(double distance, UnitOfMeasure.EUnitType currentUnit, bool showCentimeter)
            {
                if (distance <= 0)
                {
                    return DbMessages.Geo_FormatDistance_ZeroMeter;
                }

                double distanceMetric;

                if (currentUnit == UnitOfMeasure.EUnitType.Kilometer)
                {
                    distanceMetric = distance * 1000;
                }
                else if (currentUnit == UnitOfMeasure.EUnitType.Degree)
                {
                    distanceMetric = UnitOfMeasure.ConvertDegreeTo(distance, UnitOfMeasure.EUnitType.Meter);
                }
                else
                {
                    distanceMetric = distance;
                }

                var km = (int) Math.Truncate(distanceMetric / 1000);
                var mt = distanceMetric - 1000 * km;

                bool hasMeters;
                if (showCentimeter)
                {
                    hasMeters = mt > 0;
                }
                else
                {
                    hasMeters = Convert.ToInt32(mt) > 0;
                }

                if (hasMeters)
                {
                    var formatMetter = showCentimeter ? "F2" : "F0";

                    if (km > 0)
                    {
                        return string.Format("{0} Km {1:" + formatMetter + "} metros", km, mt);
                    }

                    return string.Format("{0:" + formatMetter + "} metros", mt);
                }

                return $"{km} Km";
            }

            // Rua Tinguís, 647 - Vila Goes, Apartamento 1 - 86026200 , Londrina - PR
            [Localizable(false)]
            public static string FullAddress(string logradouro, string numero, string bairro, string complemento, string cep, string municipio) => $"{logradouro}, {numero} - {bairro}, {complemento} - {cep}, {municipio}";

            // Rua Tinguís, 647 - Vila Goes, Londrina - PR
            [Localizable(false)]
            public static string SmallAddress(string logradouro, string numero, string bairro, string municipio) => $"{logradouro}, {numero} - {bairro}, {municipio}";

            public static string FormatCoordinate(double lat, double lng) => $"{EncodeLat(lat)}, {EncodeLng(lng)}";

            public static string EncodeLat(double lat)
            {
                ConvertDecimalToHoursMinutesSeconds(lat, out var d, out var h, out var m);

                return $"{d}º {h}'' {m}' {(d < 0 ? "S" : "N")}";
            }

            public static string EncodeLng(double lat)
            {
                ConvertDecimalToHoursMinutesSeconds(lat, out var d, out var h, out var m);

                return $"{d}º {h}'' {m}' {(d < 0 ? "W" : "E")}";
            }

            /// <summary>
            /// Convert decimal to hours, minutes and seconds
            /// </summary>
            /// <param name="graus">Decimal of graus</param>
            /// <param name="g">Extracted graus</param>
            /// <param name="m">Extracted minutes</param>
            /// <param name="s">Extracted seconds</param>
            public static void ConvertDecimalToHoursMinutesSeconds(double graus, out int g, out int m, out int s)
            {
                var hours = Math.Floor(graus);
                var minutes = (graus - hours) * 60.0D;
                g = (int)Math.Floor(graus / 24);
                m = (int)Math.Floor(hours - (g * 24));
                s = (int)Math.Floor(minutes);
            }
        }
    }
}