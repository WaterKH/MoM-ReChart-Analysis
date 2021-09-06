using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class BattleVsInfo
    {
        public int ObjectCount { get; set; }
        public InternetBattleInfo InternetBattleInfo { get; set; } = new InternetBattleInfo();
        public LocalBattleInfo LocalBattleInfo { get; set; } = new LocalBattleInfo();
        public BattleRoyalInfo BattleRoyalInfo { get; set; } = new BattleRoyalInfo();
        public int SelectedMusicValue { get; set; }
        public byte SelectedSortType { get; set; }
        public byte SelectedSortCategory { get; set; }
        public List<byte> InternetBattleReleased { get; set; }
        public List<byte> ProficaReleased { get; set; }
        public List<byte> TotalPlayTime { get; set; }
        public List<byte> PlayTimeDouble { get; set; }
        public List<byte> TotalPlayCount { get; set; }
        public List<byte> TotalBurst { get; set; }
        public string Version { get; set; }

        public BattleVsInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Internet Battle Info
            this.InternetBattleInfo.Process(saveDataReader);

            // Get Local Battle Info
            this.LocalBattleInfo.Process(saveDataReader);

            // Get Battle Royal Info
            this.BattleRoyalInfo.Process(saveDataReader);

            // Get Selected Music Value Name
            var selectedMusicValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Music Value
            this.SelectedMusicValue = saveDataReader.GetIdFromFileStream();

            // Get Selected Sort Type Name
            var selectedSortTypeName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Sort Type
            this.SelectedSortType = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Selected Sort Category Name
            var selectedSortCategoryName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Sort Category
            this.SelectedSortCategory = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Internet Battle Released Name
            var internetBattleReleasedName = saveDataReader.GetStringFromFileStream(160);

            // Get Internet Battle Released
            this.InternetBattleReleased = saveDataReader.FindDataFromFileStream();

            // Get Profica Released Name
            var proficaReleasedName = saveDataReader.GetStringFromFileStream(160);

            // Get Profica Released
            this.ProficaReleased = saveDataReader.FindDataFromFileStream();

            // Get Total Play Time Name
            var totalPlayTimeName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Play Time
            this.TotalPlayTime = saveDataReader.FindDataFromFileStream();

            // Get Play Time Double Name
            var playTimeDoubleName = saveDataReader.GetStringFromFileStream(160);

            // Get Play Time Double
            this.PlayTimeDouble = saveDataReader.FindDataFromFileStream();

            // Get Total Play Count Name
            var totalPlayCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Play Count
            this.TotalPlayCount = saveDataReader.FindDataFromFileStream();

            // Get Total Burst Name
            var totalBurstName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Burst
            this.TotalBurst = saveDataReader.FindDataFromFileStream();

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            return @$"
    #region BattleVsInfo

    Object Count: {this.ObjectCount}
    Internet Battle Info: {this.InternetBattleInfo.Display()}
    Local Battle Info: {this.LocalBattleInfo.Display()}
    Battle Royal Info: {this.BattleRoyalInfo.Display()}
    Selected Music Value: {this.SelectedMusicValue}
    Selected Sort Type: {this.SelectedSortType}
    Selected Sort Category: {this.SelectedSortCategory}
    Internet Battle Released: {this.InternetBattleReleased.Display()}
    Profica Released: {this.ProficaReleased.Display()}
    Total Play Time: {this.TotalPlayTime.Display()}
    Play Time Double: {this.PlayTimeDouble.Display()}
    Total Play Count: {this.TotalPlayCount.Display()}
    Total Burst: {this.TotalBurst.Display()}
    Version: {this.Version}
    
    #endregion BattleVsInfo
";
        }
    }

    public class InternetBattleInfo
    {
        public int ObjectCount { get; set; }
        public List<byte> CurrentRate { get; set; }
        public byte SelectedDifficulty { get; set; }
        public byte SelectedBurstUseType { get; set; }
        public List<byte> TotalVictory { get; set; }
        public List<byte> CurrentContinuousVictory { get; set; }
        public List<byte> BestContinuousVictory { get; set; }
        public List<byte> BestRate { get; set; }
        public List<int> DisconnectionPoint { get; set; } = new List<int>();
        public byte IsDisconnection { get; set; }
        public byte LastState { get; set; }
        public int BattleBanTime { get; set; }

        public InternetBattleInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Current Rate Name
            var currentRateName = saveDataReader.GetStringFromFileStream(160);

            // Get Current Rate
            this.CurrentRate = saveDataReader.FindDataFromFileStream();

            // Get Selected Difficulty Name
            var selectedDifficultyName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Difficulty
            this.SelectedDifficulty = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Selected Burst Use Type Name
            var selectedBurstUseTypeName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Burst Use Type
            this.SelectedBurstUseType = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Total Victory Name
            var totalVictoryName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Victory Number
            this.TotalVictory = saveDataReader.FindDataFromFileStream();

            // Get Current Continuous Victory Name
            var currentContinuousVictoryName = saveDataReader.GetStringFromFileStream(160);

            // Get Current Continuous Victory
            this.CurrentContinuousVictory = saveDataReader.FindDataFromFileStream();

            // Get Best Continuous Victory Name
            var bestContinuousVictoryName = saveDataReader.GetStringFromFileStream(160);

            // Get Best Continuous Victory
            this.BestContinuousVictory = saveDataReader.FindDataFromFileStream();

            // Get Best Rate Name
            var bestRateName = saveDataReader.GetStringFromFileStream(160);

            // Get Best Rate
            this.BestRate = saveDataReader.FindDataFromFileStream();

            // Get Disconnection Point Name
            var disconnectionPointName = saveDataReader.GetStringFromFileStream(160);

            // Get Disconnection Point Count
            var disconnectionPointCount = saveDataReader.GetCountFromFileStream();

            // Get Disconnection Points
            for (int i = 0; i < disconnectionPointCount; ++i)
            {
                this.DisconnectionPoint.Add(saveDataReader.GetIdFromFileStream());
            }

            // Get Is Disconnection Name
            var isDisconnectionName = saveDataReader.GetStringFromFileStream(160);

            // Get Is Disconnection
            this.IsDisconnection = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Last State Name
            var lastStateName = saveDataReader.GetStringFromFileStream(160);

            // Get Last State
            this.LastState = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Battle Ban Time Name
            var battleBanTimeName = saveDataReader.GetStringFromFileStream(160);

            // Get Battle Ban Time
            this.BattleBanTime = saveDataReader.GetIdFromFileStream();

            return this;
        }

        public string Display()
        {
            var disconnectionPointString = string.Join("\n", this.DisconnectionPoint);

            return @$"
    #region InternetBattleInfo

    Object Count: {this.ObjectCount}
    Current Rate: {this.CurrentRate.Display()}
    Selected Difficulty: {this.SelectedDifficulty}
    Selected Burst Use Type: {this.SelectedBurstUseType}
    Total Victory: {this.TotalVictory}
    Current Continuous Victory: {this.CurrentContinuousVictory}
    Best Continuous Victory: {this.BestContinuousVictory}
    Best Rate: {this.BestRate}
    DisconnectionPoint: {disconnectionPointString}
    Is Disconnection: {this.IsDisconnection}
    Last State: {this.LastState}
    Battle Ban Time: {this.BattleBanTime}
    
    #endregion InternetBattleInfo
";
        }
    }

    public class LocalBattleInfo
    {
        public int ObjectCount { get; set; }
        public byte SelectedDifficulty { get; set; }
        public byte SelectedBurstUseType { get; set; }
        public List<byte> TotalVictory { get; set; }

        public LocalBattleInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Selected Difficulty Name
            var selectedDifficultyName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Difficulty
            this.SelectedDifficulty = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Selected Burst Use Type Name
            var selectedBurstUseTypeName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Burst Use Type
            this.SelectedBurstUseType = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Total Victory Name
            var totalVictoryName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Victory Number
            this.TotalVictory = saveDataReader.FindDataFromFileStream();

            return this;
        }

        public string Display()
        {
            return @$"
    #region LocalBattleInfo

    Object Count: {this.ObjectCount}
    Selected Difficulty: {this.SelectedDifficulty}
    Selected Burst Use Type: {this.SelectedBurstUseType}
    Total Victory: {this.TotalVictory}
    
    #endregion LocalBattleInfo
";
        }
    }

    public class BattleRoyalInfo
    {
        public int ObjectCount { get; set; }
        public byte SelectedDifficulty { get; set; }
        public byte MusicSelectStyle { get; set; }
        public int SelectedMusicValue { get; set; }
        public List<byte> MissCount { get; set; }
        public List<byte> TotalTopVictory { get; set; }

        public BattleRoyalInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Selected Difficulty Name
            var selectedDifficultyName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Difficulty
            this.SelectedDifficulty = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Music Select Style Name
            var musicSelectStyleName = saveDataReader.GetStringFromFileStream(160);

            // Get Music Select Style
            this.MusicSelectStyle = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Selected Music Value Name
            var SelectedMusicValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Music Value
            this.SelectedMusicValue = saveDataReader.GetIdFromFileStream();

            // Get Miss Count Name
            var missCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Miss Count
            this.MissCount = saveDataReader.FindDataFromFileStream();

            // Get Total Top Victory Name
            var totalTopVictoryName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Top Victory
            this.TotalTopVictory = saveDataReader.FindDataFromFileStream();

            return this;
        }

        public string Display()
        {
            return @$"
    #region BattleRoyalInfo

    Object Count: {this.ObjectCount}
    Selected Difficulty: {this.SelectedDifficulty}
    Music Select Style: {this.MusicSelectStyle}
    Selected Music Value: {this.SelectedMusicValue}
    Miss Count: {this.MissCount.Display()}
    Total Top Victory: {this.TotalTopVictory.Display()}
    
    #endregion BattleRoyalInfo
";
        }
    }
}