using System;
using System.Collections.Generic;
using System.Text;

namespace MoMMusicAnalysis
{
    // Total size 0x14
    public class Section
    {
        public List<byte> Id { get; set; } // 8 bytes
        public int Offset {get; set; }
        public int Size { get; set; }
        public int EmptyData { get; set; } // 0 if song, 1 if assets?

        public List<byte> RecompileSection()
        {
            var data = new List<byte>();

            data.AddRange(Id);
            data.AddRange(BitConverter.GetBytes(this.Offset));
            data.AddRange(BitConverter.GetBytes(this.Size));
            data.AddRange(BitConverter.GetBytes(this.EmptyData));

            return data;
        }
    }
}