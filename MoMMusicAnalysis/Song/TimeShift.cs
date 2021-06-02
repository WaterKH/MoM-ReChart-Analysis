using System;
using System.Collections.Generic;
using System.IO;

namespace MoMMusicAnalysis
{
    public class TimeShift
    {
        public int ChangeTime { get; set; }
        public int Speed { get; set; }


        public TimeShift ProcessTimeShift(FileStream musicReader)
        {
            // Get Change Time
            this.ChangeTime = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Speed?
            this.Speed = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            return this;
        }


        public List<byte> RecompileTimeShift()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.ChangeTime));
            data.AddRange(BitConverter.GetBytes(this.Speed));

            return data;
        }
    }
}