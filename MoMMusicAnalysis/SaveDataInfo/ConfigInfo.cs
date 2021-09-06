using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class ConfigInfo
    {
        public int ObjectCount { get; set; }
        public byte AttackIsl { get; set; }
        public byte UsedGuestMember { get; set; }
        public byte DisplayMemoryMovieSubtitles { get; set; }
        public byte StaffLaneTransparency { get; set; }
        public byte MemoryDiveMaskTransparency { get; set; }
        public List<byte> KeyboardButtonAssignments { get; set; } = new List<byte>();
        public string Version { get; set; }
        public byte FieldBattleControlType { get; set; }
        public byte BossBattleControlType { get; set; }
        public byte MemoryDiveControlType { get; set; }

        public ConfigInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Attack Isl Name
            var attackIslName = saveDataReader.GetStringFromFileStream(160);

            // Get Attack Isl Value
            this.AttackIsl = saveDataReader.FindDataFromFileStream().FirstOrDefault();

            // Get Used Guest Member Name
            var usedGuestMemberName = saveDataReader.GetStringFromFileStream(160);

            // Get Used Guest Member Value
            this.UsedGuestMember = saveDataReader.FindDataFromFileStream().FirstOrDefault();

            // Get Display Memory Movie Subtitles Name
            var displayMemoryMovieSubtitlesName = saveDataReader.GetStringFromFileStream(160);

            // Get Display Memory Movie Subtitles Value
            this.DisplayMemoryMovieSubtitles = saveDataReader.FindDataFromFileStream().FirstOrDefault();

            // Get Staff Lane Transparency Name
            var staffLaneTransparencyName = saveDataReader.GetStringFromFileStream(160);

            // Get Used Guest Member Value
            this.StaffLaneTransparency = saveDataReader.FindDataFromFileStream().FirstOrDefault();

            // Get Display Memory Movie Subtitles Name
            var memoryDiveMaskTransparencyName = saveDataReader.GetStringFromFileStream(160);

            // Get Display Memory Movie Subtitles Value
            this.MemoryDiveMaskTransparency = saveDataReader.FindDataFromFileStream().FirstOrDefault();

            // Get Keyboard Button Assignments Name
            var keyboardButtonAssignmentsName = saveDataReader.GetStringFromFileStream(160);

            // Get Keyboard Button Assignments Count
            var unk = saveDataReader.ReadBytesFromFileStream(4);

            // Get Keyboard Button Asignments Count
            var keyboardButtonAssignmentsCount = saveDataReader.GetCountFromFileStream();

            // Get Keyboard Button Assignments
            for (int i = 0; i < keyboardButtonAssignmentsCount; ++i)
            {
                this.KeyboardButtonAssignments.Add(saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault());
            }

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            // Get Field Battle Control Type Name
            var fieldBattleControlTypeName = saveDataReader.GetStringFromFileStream(160);

            // Get Field Battle Control Type Value
            this.FieldBattleControlType = saveDataReader.FindDataFromFileStream().FirstOrDefault();

            // Get Boss Battle Control Type Name
            var bossBattleControlTypeName = saveDataReader.GetStringFromFileStream(160);

            // Get Boss Battle Control Type Value
            this.BossBattleControlType = saveDataReader.FindDataFromFileStream().FirstOrDefault();

            // Get Memory Dive Control Type Name
            var memoryDiveControlTypeName = saveDataReader.GetStringFromFileStream(160);

            // Get Memory Dive Control Type Value
            this.MemoryDiveControlType = saveDataReader.FindDataFromFileStream().FirstOrDefault();

            return this;
        }


        public string Display()
        {
            var buttonsString = "";
            this.KeyboardButtonAssignments.ForEach(x => buttonsString += $"\n{x}");

            return @$"
    #region ConfigInfo

    Object Count: {this.ObjectCount}
    Attack Isl: {this.AttackIsl}
    Used Guest Member: {this.UsedGuestMember}
    Display Memory Movie Subtitles: {this.DisplayMemoryMovieSubtitles}
    Staff Lane Transparency: {this.StaffLaneTransparency}
    Memory Dive Mask Transparency: {this.MemoryDiveMaskTransparency}
    
    KeyboardButtonAssignments:
    #region KeyboardButtons
    {buttonsString}
    #endregion KeyboardButtons

    Version: {this.Version}
    Field Battle Control Type: {this.FieldBattleControlType}
    Boss Battle Control Type: {this.BossBattleControlType}
    Memory Dive Control Type: {this.MemoryDiveControlType}
    
    #endregion ConfigInfo
";
        }
    }
}