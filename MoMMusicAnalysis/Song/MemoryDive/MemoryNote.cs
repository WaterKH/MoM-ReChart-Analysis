using System;
using System.Collections.Generic;
using System.IO;

namespace MoMMusicAnalysis
{
    public class MemoryNote : Note<MemoryLane>
    {
        public MemoryNoteType MemoryNoteType { get; set; }
        public bool AerialFlag { get; set; } // Always true
        public SwipeType SwipeDirection { get; set; } // Can this be used to also modify Red Note positions?
        public int StartHoldNote { get; set; }
        public int EndHoldNote { get; set; }
        public int UnkFF { get; set; }
        public int Unk1 { get; set; }
        public int Unk2 { get; set; }
        public int Unk3 { get; set; }
        public int Unk4 { get; set; }
        public int Unk5 { get; set; }
        public int Unk6 { get; set; }
        public int Unk7 { get; set; }
        public int Unk8 { get; set; }


        public MemoryNote ProcessNote(FileStream musicReader)
        {
            // Get Memory Note Type
            this.MemoryNoteType = (MemoryNoteType)BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Hit Time
            this.HitTime = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Lane
            this.Lane = (MemoryLane)BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Aerial Flag?
            this.AerialFlag = BitConverter.ToBoolean(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Swipe Direction (For Yellow Notes) - TODO Find out why this is sometimes set for normal notes
            this.SwipeDirection = (SwipeType)BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Start Hold Note
            this.StartHoldNote = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get End Hold Note
            this.EndHoldNote = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get UnkFF
            this.UnkFF = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Rest
            this.Unk1 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk2 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk3 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk4 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk5 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk6 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk7 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk8 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            return this;
        }
        
        public new List<byte> RecompileNote()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes((int)this.MemoryNoteType));
            data.AddRange(BitConverter.GetBytes(this.HitTime));
            data.AddRange(BitConverter.GetBytes((int)this.Lane));
            data.AddRange(BitConverter.GetBytes(this.AerialFlag ? 1 : 0));
            data.AddRange(BitConverter.GetBytes((int)this.SwipeDirection));
            data.AddRange(BitConverter.GetBytes(this.StartHoldNote));
            data.AddRange(BitConverter.GetBytes(this.EndHoldNote));
            data.AddRange(BitConverter.GetBytes(this.UnkFF));
            data.AddRange(BitConverter.GetBytes(this.Unk1));
            data.AddRange(BitConverter.GetBytes(this.Unk2));
            data.AddRange(BitConverter.GetBytes(this.Unk3));
            data.AddRange(BitConverter.GetBytes(this.Unk4));
            data.AddRange(BitConverter.GetBytes(this.Unk5));
            data.AddRange(BitConverter.GetBytes(this.Unk6));
            data.AddRange(BitConverter.GetBytes(this.Unk7));
            data.AddRange(BitConverter.GetBytes(this.Unk8));

            return data;
        }
    }
}