using System;
using System.Collections.Generic;
using System.IO;

namespace MoMMusicAnalysis
{
    public class FieldNote : Note<FieldLane>
    {
        public FieldModelType ModelType { get; set; }
        public int AnimationReference { get; set; }
        public bool AerialFlag { get; set; } = false;
        public int ProjectileOriginNote { get; set; } = -1;
        public int PreviousEnemyNote { get; set; } = -1;
        public int NextEnemyNote { get; set; } = -1;
        public int AerialAndCrystalCounter { get; set; } = -1;
        public bool StarFlag { get; set; } = false;
        public bool PartyFlag { get; set; } = false;
        public int Unk1 { get; set; }
        public int Unk2 { get; set; }
        public int Unk3 { get; set; }
        public int Unk4 { get; set; }
        public int Unk5 { get; set; }
        public int Unk6 { get; set; }


        public List<FieldAnimation> Animations { get; set; } = new List<FieldAnimation>();

        public FieldNote ProcessNote(FileStream musicReader)
        {
            // Get Note Type
            this.NoteType = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Hit Time
            this.HitTime = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Lane
            this.Lane = (FieldLane)BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Aerial Flag
            this.AerialFlag = BitConverter.ToBoolean(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Animation Reference
            this.AnimationReference = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Projectile's Origin Note Number
            this.ProjectileOriginNote = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Previous Enemy Note (In Chain)
            this.PreviousEnemyNote = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Next Enemy Note (In Chain)
            this.NextEnemyNote = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Aerial & Crystal Special Counter
            this.AerialAndCrystalCounter = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Model Type
            this.ModelType = (FieldModelType)BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Star Flag
            this.StarFlag = BitConverter.ToBoolean(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Party Flag
            this.PartyFlag = BitConverter.ToBoolean(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Rest
            this.Unk1 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk2 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk3 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk4 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk5 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            this.Unk6 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            return this;
        }

        public new List<byte> RecompileNote()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.NoteType));
            data.AddRange(BitConverter.GetBytes(this.HitTime));
            data.AddRange(BitConverter.GetBytes((int)this.Lane));
            data.AddRange(BitConverter.GetBytes(this.AerialFlag ? 1 : 0));
            data.AddRange(BitConverter.GetBytes(this.AnimationReference));
            data.AddRange(BitConverter.GetBytes(this.ProjectileOriginNote));
            data.AddRange(BitConverter.GetBytes(this.PreviousEnemyNote));
            data.AddRange(BitConverter.GetBytes(this.NextEnemyNote));
            data.AddRange(BitConverter.GetBytes(this.AerialAndCrystalCounter));
            data.AddRange(BitConverter.GetBytes((int)this.ModelType));
            data.AddRange(BitConverter.GetBytes(this.StarFlag ? 1 : 0));
            data.AddRange(BitConverter.GetBytes(this.PartyFlag ? 1 : 0));
            data.AddRange(BitConverter.GetBytes(this.Unk1));
            data.AddRange(BitConverter.GetBytes(this.Unk2));
            data.AddRange(BitConverter.GetBytes(this.Unk3));
            data.AddRange(BitConverter.GetBytes(this.Unk4));
            data.AddRange(BitConverter.GetBytes(this.Unk5));
            data.AddRange(BitConverter.GetBytes(this.Unk6));

            return data;
        }
    }
}