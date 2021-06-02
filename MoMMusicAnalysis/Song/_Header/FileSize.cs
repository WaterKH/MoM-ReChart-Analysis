using System;
using System.Collections.Generic;

namespace MoMMusicAnalysis
{
    public class FileSize
    {
        public int MainFileSize1 { get; set; } // This doesn't count the above Unity information (Reversed?)
        public int MainFileSize2 { get; set; } //  This is the same as the previous value for some reason? (Reversed?)
        public short EmptyData { get; set; } // 2 Extra bytes of nothing?


        public List<byte> RecompileFileSize()
        {
            var data = new List<byte>();

            var reversedData = BitConverter.GetBytes(this.MainFileSize1);
            Array.Reverse(reversedData);
            data.AddRange(reversedData);

            reversedData = BitConverter.GetBytes(this.MainFileSize2);
            Array.Reverse(reversedData);
            data.AddRange(reversedData);
            
            data.AddRange(BitConverter.GetBytes(this.EmptyData));

            return data;
        }
    }
}