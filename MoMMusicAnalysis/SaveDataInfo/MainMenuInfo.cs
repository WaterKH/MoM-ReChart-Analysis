using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class MainMenuInfo
    {
        public int ObjectCount { get; set; }
        public byte MusicSelectUnlockState { get; set; }
        public byte VersusBattleUnlockState { get; set; }
        public byte MuseumUnlockState { get; set; }
        public string Version { get; set; }

        public MainMenuInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var mainMenuInfoName = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Music Select Unlock State Name
            var musicSelectUnlockStateName = saveDataReader.GetStringFromFileStream(160);
            // Get Music Select Unlock State
            this.MusicSelectUnlockState = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Versus Battle Unlock State Name
            var versusBattleUnlockStateName = saveDataReader.GetStringFromFileStream(160);

            // Get Versus Battle Unlock State
            this.VersusBattleUnlockState = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Museum Unlock State Name
            var museumUnlockStateName = saveDataReader.GetStringFromFileStream(160);

            // Get Museum Unlock State
            this.MuseumUnlockState = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            return @$"
    #region MainMenuInfo

    Object Count: {this.ObjectCount}
    Music Select Unlock State: {this.MusicSelectUnlockState}
    Versus Battle Unlock State: {this.VersusBattleUnlockState}
    Museum Unlock State: {this.MuseumUnlockState}
    Version: {this.Version}
    
    #endregion MainMenuInfo
";
        }
    }
}