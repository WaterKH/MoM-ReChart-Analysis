using System;
using System.Collections.Generic;
using System.IO;

namespace MoMMusicAnalysis
{
    public static class Extensions
    {
        public static List<byte> ReadBytesFromFileStream(this FileStream musicReader, int length)
        {
            if (musicReader.Position + length > musicReader.Length)
                return null;

            var data = new List<byte>();

            for (int i = 0; i < length; ++i)
            {
                var t = musicReader.ReadByte();
                if (t == -1)
                    return null;
                data.Add((byte)t);
            }

            return data;
        }

        public static double ConvertHexToTime(this List<byte> data)
        {
            var t = BitConverter.ToInt32(data.ToArray(), 0);

            return t / 1000.0;
        }
    }
}