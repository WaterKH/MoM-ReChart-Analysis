using System;
using System.Collections.Generic;
using System.IO;

namespace MoMMusicAnalysis
{
    public class BossDarkZone : Note<BossLane>
    {
        //public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int EndAttackTime { get; set; }
        public int EmptyData { get; set; }


        public BossDarkZone ProcessDarkZone(FileStream musicReader)
        {
            // Get Hit (Start) Time (For Notes) - TODO Move back to StartTime?
            this.HitTime = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get End Time (For Notes - Start Time for Attack)
            this.EndTime = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get End Attack Time (End Time for Boss Animation)
            this.EndAttackTime = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Empty Data
            this.EmptyData = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            return this;
        }
        
        public List<byte> RecompileDarkZone()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.HitTime));
            data.AddRange(BitConverter.GetBytes(this.EndTime)); 
            data.AddRange(BitConverter.GetBytes(this.EndAttackTime));
            data.AddRange(BitConverter.GetBytes(this.EmptyData));

            return data;
        }
    }
}