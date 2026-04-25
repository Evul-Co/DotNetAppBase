using System;
using System.Linq;

namespace DotNetAppBase.Std.Library.Protocol;

public class Crc16Ccitt
{
    private const ushort Poly = 4129;
    private readonly ushort _initialValue;
    private readonly ushort[] _table = new ushort[256];

    public Crc16Ccitt(ECrcType type)
    {
        _initialValue = (ushort) type;
        for (var i = 0; i < _table.Length; ++i)
        {
            ushort temp = 0;
            var a = (ushort) (i << 8);
            for (var j = 0; j < 8; ++j)
            {
                if (((temp ^ a) & 0x8000) != 0)
                {
                    temp = (ushort) ((temp << 1) ^ Poly);
                }
                else
                {
                    temp <<= 1;
                }

                a <<= 1;
            }

            _table[i] = temp;
        }
    }

    public ushort ComputeChecksum(byte[] bytes)
    {
        return bytes.Aggregate(_initialValue, (current, t) => (ushort) ((current << 8) ^ _table[(current >> 8) ^ (0xff & t)]));
    }

    public byte[] ComputeChecksumBytes(byte[] bytes)
    {
        var crc = ComputeChecksum(bytes);

        return BitConverter.GetBytes(crc);
    }
}