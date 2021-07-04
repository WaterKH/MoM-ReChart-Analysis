using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MoMMusicAnalysis
{
    public class FieldAsset : Note<FieldLane>
    {
        public bool JumpFlag { get; set; }
        public int Unk1 { get; set; } // Always 1 if the ModelType is 13? 
        public int AnimationReference { get; set; }
        public int FF1 { get; set; } = -1;
        public int FF2 { get; set; } = -1;
        public int FF3 { get; set; } = -1;
        public FieldAssetType ModelType { get; set; }
        public int Unk2 { get; set; }
        public int Unk3 { get; set; }
        public int Unk4 { get; set; }
        public int Unk5 { get; set; }
        public int Unk6 { get; set; }
        public int Unk7 { get; set; }
        public int Unk8 { get; set; }
        public int Unk9 { get; set; }


        public List<FieldAnimation> Animations { get; set; } = new List<FieldAnimation>();

        public FieldAsset ProcessNote(FileStream musicReader)
        {
            // Get Jump Flag
            this.JumpFlag = BitConverter.ToBoolean(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Hit Time
            this.HitTime = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Lane
            this.Lane = (FieldLane)BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk1
            this.Unk1 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Animation Reference
            this.AnimationReference = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get FF1
            this.FF1 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get FF2
            this.FF2 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get FF3
            this.FF3 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Model Type
            this.ModelType = (FieldAssetType)BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get the Rest
            this.Unk2 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk3 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk4 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk5 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk6 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk7 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk8 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk9 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            
            return this;
        }

        public new List<byte> RecompileNote()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.JumpFlag ? 1 : 0));
            data.AddRange(BitConverter.GetBytes(this.HitTime));
            data.AddRange(BitConverter.GetBytes((int)this.Lane));
            data.AddRange(BitConverter.GetBytes(this.Unk1));
            data.AddRange(BitConverter.GetBytes(this.AnimationReference));
            data.AddRange(BitConverter.GetBytes(this.FF1));
            data.AddRange(BitConverter.GetBytes(this.FF2));
            data.AddRange(BitConverter.GetBytes(this.FF3));
            data.AddRange(BitConverter.GetBytes((int)this.ModelType));
            data.AddRange(BitConverter.GetBytes(this.Unk2));
            data.AddRange(BitConverter.GetBytes(this.Unk3));
            data.AddRange(BitConverter.GetBytes(this.Unk4));
            data.AddRange(BitConverter.GetBytes(this.Unk5));
            data.AddRange(BitConverter.GetBytes(this.Unk6));
            data.AddRange(BitConverter.GetBytes(this.Unk7));
            data.AddRange(BitConverter.GetBytes(this.Unk8));
            data.AddRange(BitConverter.GetBytes(this.Unk9));

            return data;
        }
    }
}