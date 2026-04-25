using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static partial class Comm
    {
        public static class Stream
        {
            private static readonly uint[] Lookup32Unsafe = CreateLookup32Unsafe();

            private static readonly unsafe uint* Lookup32UnsafeP = (uint*) GCHandle.Alloc(Lookup32Unsafe, GCHandleType.Pinned).AddrOfPinnedObject();

            public static unsafe string ByteArrayToHexString(byte[] bytes, int start=0, int? end=null)
            {
                end ??= bytes.Length;

                var size = end.Value - start;

                var lookupP = Lookup32UnsafeP;
                var result = new char[size * 2];

                fixed (byte* bytesP = bytes)
                fixed (char* resultP = result)
                {
                    var resultP2 = (uint*) resultP;
                    for (var i = start; i < end; i++)
                    {
                        resultP2[i] = lookupP[bytesP[i]];
                    }
                }

                return new string(result);
            }

            public static string ComputeMd5Hash(string s, Encoding encoding = null)
            {
                encoding ??= Encoding.UTF8;

                var bytes = encoding.GetBytes(s);

                var sec = new MD5CryptoServiceProvider();
                var hashBytes = sec.ComputeHash(bytes);

                return ByteArrayToHexString(hashBytes);
            }

            public static string ComputeSha1Hash(string s, Encoding encoding = null)
            {
                encoding ??= Encoding.UTF8;

                var bytes = encoding.GetBytes(s);

                var sha1 = SHA1.Create();
                var hashBytes = sha1.ComputeHash(bytes);

                return ByteArrayToHexString(hashBytes);
            }

            public static byte[] HexStringToByteArray(string hex)
            {
                var numberChars = hex.Length;
                var bytes = new byte[numberChars / 2];
                for (var i = 0; i < numberChars; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
                }

                return bytes;
            }

            public static string StringToHexString(string s, Encoding encoding)
            {
                var data = encoding.GetBytes(s);

                return ByteArrayToHexString(data);
            }

            private static uint[] CreateLookup32Unsafe()
            {
                var result = new uint[256];
                for (var i = 0; i < 256; i++)
                {
                    // ReSharper disable LocalizableElement
                    var s = i.ToString("X2");
                    // ReSharper restore LocalizableElement

                    if (BitConverter.IsLittleEndian)
                    {
                        result[i] = s[0] + ((uint) s[1] << 16);
                    }
                    else
                    {
                        result[i] = s[1] + ((uint) s[0] << 16);
                    }
                }

                return result;
            }
        }
    }
}