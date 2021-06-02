using System;
using System.Collections.Generic;
using System.IO;

namespace MoMMusicAnalysis
{
    public class PerformerNote<TPerformerLane> : Note<TPerformerLane> where TPerformerLane : Enum
    {
        public PerformerType PerformerType { get; set; }
        public int Unk1 { get; set; } // Always 0 for FieldBattle, 1 for MemoryDive (Aerial?)
        public int Unk2 { get; set; }
        public int Unk3 { get; set; }
        public int Unk4 { get; set; }
        public int Unk5 { get; set; }
        public PerformerType DuplicateType { get; set; } // Just a copy of PerformerType?
        public int Unk6 { get; set; }
        public int Unk7 { get; set; }
        public int Unk8 { get; set; }

        public PerformerNote<TPerformerLane> ProcessNote(FileStream musicReader)
        {
            // Get Performer Note Type
            this.PerformerType = (PerformerType)BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Hit Time
            this.HitTime = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Lane
            this.Lane = (TPerformerLane)(object)BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk1
            this.Unk1 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk2
            this.Unk2 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk3
            this.Unk3 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk4
            this.Unk4 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk5
            this.Unk5 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Duplicate Type
            this.DuplicateType = (PerformerType)(object)BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk6
            this.Unk6 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk7
            this.Unk7 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk8
            this.Unk8 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            return this;
        }

        public new List<byte> RecompileNote()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes((int)this.PerformerType));
            data.AddRange(BitConverter.GetBytes(this.HitTime));
            data.AddRange(BitConverter.GetBytes((int)(object)this.Lane));
            data.AddRange(BitConverter.GetBytes(this.Unk1));
            data.AddRange(BitConverter.GetBytes(this.Unk2));
            data.AddRange(BitConverter.GetBytes(this.Unk3));
            data.AddRange(BitConverter.GetBytes(this.Unk4));
            data.AddRange(BitConverter.GetBytes(this.Unk5));
            data.AddRange(BitConverter.GetBytes((int)this.DuplicateType));
            data.AddRange(BitConverter.GetBytes(this.Unk6));
            data.AddRange(BitConverter.GetBytes(this.Unk7));
            data.AddRange(BitConverter.GetBytes(this.Unk8));

            return data;
        }
    }
}