using System;
using System.Collections.Generic;
using System.IO;

namespace MoMMusicAnalysis
{
    public class TimeShift<TLane> : Note<TLane>
    {
        //public int StartTime { get; set; }
        public int Speed { get; set; }


        public TimeShift<TLane> ProcessTimeShift(FileStream musicReader)
        {
            // Get Hit Time - Using HitTime to be consistent
            this.HitTime = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Speed?
            this.Speed = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            return this;
        }


        public List<byte> RecompileTimeShift()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.HitTime));
            data.AddRange(BitConverter.GetBytes(this.Speed));

            return data;
        }
    }
}