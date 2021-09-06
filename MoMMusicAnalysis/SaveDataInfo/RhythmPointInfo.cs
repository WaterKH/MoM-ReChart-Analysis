using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class RhythmPointInfo
    {
        public int ObjectCount { get; set; }
        public EverydayPlayBonusInfo EverydayPlayBonusInfo { get; set; } = new EverydayPlayBonusInfo();
        public List<byte> TotalRhythpoValue { get; set; }
        public List<byte> AlreadyRewardRhythpoValue { get; set; }
        public int LastRewardID { get; set; }
        public string Version { get; set; }

        public RhythmPointInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Everyday Play Bonus Info
            this.EverydayPlayBonusInfo.Process(saveDataReader);

            // Get Total Rhythpo Value Name
            var totalRhythpoValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Rhythpo Value
            this.TotalRhythpoValue = saveDataReader.FindDataFromFileStream();

            // Get Already Reward Rhythpo Value Name
            var alreadyRewardRhythpoValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Already Reward Rhythpo Value
            this.AlreadyRewardRhythpoValue = saveDataReader.FindDataFromFileStream();

            // Get Last Reward ID Name
            var lastRewardIDName = saveDataReader.GetStringFromFileStream(160);

            // Get Last Reward ID
            this.LastRewardID = saveDataReader.GetIdFromFileStream();

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            return @$"
    #region RhythmPointInfo

    Object Count: {this.ObjectCount}

    Everyday Play Bonus Info: {this.EverydayPlayBonusInfo.Display()}

    Total Rhythm Point Value: {this.TotalRhythpoValue}
    Already Reward Rhythm Point Value: {this.AlreadyRewardRhythpoValue}
    Last Reward ID: {this.LastRewardID}
    Version: {this.Version}
    
    #endregion RhythmPointInfo
";
        }
    }

    public class EverydayPlayBonusInfo
    {
        public int ObjectCount { get; set; }
        public List<byte> Days { get; set; }
        public List<byte> LastDateTime { get; set; }

        public EverydayPlayBonusInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Days Name
            var daysName = saveDataReader.GetStringFromFileStream(160);

            // Get Days
            this.Days = saveDataReader.FindDataFromFileStream();

            // Get Last Date Time Name
            var lastDateTimeName = saveDataReader.GetStringFromFileStream(160);

            // Get Last Date Time
            this.LastDateTime = saveDataReader.FindDataFromFileStream();

            return this;
        }

        public string Display()
        {
            return @$"
    #region EverydayPlayBonusInfo

    Object Count: {this.ObjectCount}
    Days: {this.Days.Display()}
    Last Date Time: {this.LastDateTime}
    
    #endregion EverydayPlayBonusInfo
";
        }
    }
}