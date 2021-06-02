using System;
using System.Collections.Generic;
using System.IO;

namespace MoMMusicAnalysis
{
    public class FieldAnimation : Note<FieldLane>
    {
        public int Id { get; set; }
        public int AnimationStartTime { get; set; }
        public int AnimationEndTime { get; set; } // If movement, the second "Note" will have a start time, and the first "note" will be the end time
        public bool AerialFlag { get; set; }
        public int Previous { get; set; } = -1; // If no movement occurs, it will be populated with FF
        public int Next { get; set; } = -1; // If no movement occurs, it will be populated with FF
        public int Unk1 { get; set; }
        public int Unk2 { get; set; }
        public int Unk3 { get; set; }
        public int Unk4 { get; set; }
        public int Unk5 { get; set; }
        public int Unk6 { get; set; }
        public int Unk7 { get; set; }
        public int Unk8 { get; set; }

        public FieldAnimation ProcessNote(FileStream musicReader)
        {
            // Get Note Type
            this.NoteType = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Animation End Time
            this.AnimationEndTime = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Lane
            this.Lane = (FieldLane)BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Animation Start Time
            this.AnimationStartTime = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Aerial Flag
            this.AerialFlag = BitConverter.ToBoolean(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Previous
            this.Previous = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Next
            this.Next = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get the Rest
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

            data.AddRange(BitConverter.GetBytes(this.NoteType));
            data.AddRange(BitConverter.GetBytes(this.AnimationEndTime));
            data.AddRange(BitConverter.GetBytes((int)this.Lane));
            data.AddRange(BitConverter.GetBytes(this.AnimationStartTime));
            data.AddRange(BitConverter.GetBytes(this.AerialFlag ? 1 : 0));
            data.AddRange(BitConverter.GetBytes(this.Previous));
            data.AddRange(BitConverter.GetBytes(this.Next));
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