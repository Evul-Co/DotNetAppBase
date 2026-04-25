using System;
using System.IO;
using System.Text;

// ReSharper disable UnusedMember.Global

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static class Streams
    {
        public static void CreateMemory(Action<MemoryStream> read)
        {
            using var stream = new MemoryStream();

            read(stream);
        }

        public static void CreateMemory(byte[] buffer, Action<MemoryStream> read)
        {
            CreateMemory(
                stream =>
                    {
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Position = 0;

                        read(stream);
                    });
        }

        public static void CreateMemory(Action<MemoryStream> write, Action<MemoryStream> read)
        {
            CreateMemory(
                stream =>
                    {
                        write(stream);
                        stream.Position = 0;
                        read(stream);
                    });
        }

        public static Stream GetStream(string value) => new MemoryStream(Encoding.UTF8.GetBytes(value ?? string.Empty));

        public static byte[] StreamToByteArray(Action<MemoryStream> write)
        {
            byte[] buffer = null;

            CreateMemory(
                stream =>
                    {
                        write(stream);

                        stream.Position = 0;
                        buffer = stream.ToArray();
                    });

            return buffer;
        }
    }
}