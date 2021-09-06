using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class UserInfo
    {
        public int ObjectCount { get; set; }
        public int PlayTime { get; set; }
        public double PlayTimeDouble { get; set; }
        public byte UnlokedMenuSceneButton { get; set; } // Mispelled like data in savedata
        public byte LookedOpeningMemoryDive { get; set; }
        public string Version { get; set; }

        public UserInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var userInfoName = saveDataReader.GetStringFromFileStream(160);
            
            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Play Time Name
            var playTimeName = saveDataReader.GetStringFromFileStream(160);

            // Get Play Time Value Length
            var playTimeValueLength = saveDataReader.ReadBytesFromFileStream(1); // CA == 4 bytes?

            // Get Play Time Value
            this.PlayTime = BitConverter.ToInt32(saveDataReader.ReadBytesFromFileStream(4).ToArray());

            // Get Play Time Double Name
            var playTimeDoubleName = saveDataReader.GetStringFromFileStream(160);

            // Get Play Time Value Double Length
            var playTimeDoubleValueLength = saveDataReader.ReadBytesFromFileStream(1); // CB == 8 bytes?

            // Get Play Time Double Value
            this.PlayTimeDouble = BitConverter.ToInt64(saveDataReader.ReadBytesFromFileStream(8).ToArray());

            // Get Unloked Menu Scene Button Name
            var unlokedMenuSceneButtonName = saveDataReader.GetStringFromFileStream(160);

            // Get Unloked Menu Scene Button Value
            this.UnlokedMenuSceneButton = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // GetLooked Opening Memory Dive Name
            var lookedOpeningMemoryDiveName = saveDataReader.GetStringFromFileStream(160);

            // Get Looked Opening Memory Dive Value
            this.LookedOpeningMemoryDive = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            return @$"
    #region UserInfo

    Object Count: {this.ObjectCount}
    Play Time: {this.PlayTime}
    Play Time Double: {this.PlayTimeDouble}
    Unlocked Menu Scene Button: {this.UnlokedMenuSceneButton}
    Looked Opening Memory Dive: {this.LookedOpeningMemoryDive}
    Version: {this.Version}
    
    #endregion UserInfo
";
        }
    }
}