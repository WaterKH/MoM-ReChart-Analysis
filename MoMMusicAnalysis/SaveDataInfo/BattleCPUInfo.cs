using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class BattleCPUInfo
    {
        public int ObjectCount { get; set; }
        public int CurrentCPURankIDValue { get; set; }
        public List<byte> TotalWinsCount { get; set; }
        public int MaxCPURankIDValue { get; set; }
        public List<CPUPartyInfo> CPUPartyInfos { get; set; } = new List<CPUPartyInfo>();
        public string Version { get; set; }

        public BattleCPUInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Current CPU Rank ID Value Name
            var currentCPURankIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Current CPU Rank ID Value
            this.CurrentCPURankIDValue = saveDataReader.GetIdFromFileStream();

            // Get Total Wins Count Name
            var totalWinsCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Wins Count
            this.TotalWinsCount = saveDataReader.FindDataFromFileStream();

            // Get Max CPU Rank ID Value Name
            var maxCPURankIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Max CPU Rank ID Value
            this.MaxCPURankIDValue = saveDataReader.GetIdFromFileStream();

            // Get CPU Party Infos Name
            var cpuPartyInfosName = saveDataReader.GetStringFromFileStream(160);

            // Get CPU Party Infos Count
            var cpuPartyInfosCount = saveDataReader.ReadByte() - 144;

            // Get Mix Items
            for (int i = 0; i < cpuPartyInfosCount; ++i)
            {
                this.CPUPartyInfos.Add(new CPUPartyInfo().Process(saveDataReader));
            }

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            var cpuPartiesString = "";
            this.CPUPartyInfos.ForEach(x => cpuPartiesString += $"\n{x.Display()}");

            return @$"
    #region MusicSelectInfo

    Object Count: {this.ObjectCount}
    Current CPU Rank ID Value: {this.CurrentCPURankIDValue}
    Total Wins Count: {this.TotalWinsCount}
    Max CPU Rank ID Value: {this.MaxCPURankIDValue}
    
    CPU Party Infos:
    #region CPUPartyInfos
    {cpuPartiesString}
    #endregion CPUPartyInfos

    Version: {this.Version}
    
    #endregion MusicSelectInfo
";
        }
    }

    public class CPUPartyInfo
    {
        public int ObjectCount { get; set; }
        public int ComNameIDValue { get; set; }
        public int CurrentCPURankIDValue { get; set; }
        public int MusicIDValue { get; set; }
        public int CollecaIDValue { get; set; }
        public byte CollecaRarity { get; set; }
        public int ProficaIllustIDValue { get; set; }
        public int ProficaBaseDesignIDValue { get; set; }
        public int ProficaBaseColorIDValue { get; set; }
        public byte MsDifficulty { get; set; }
        public byte MatchResultState { get; set; }
        public byte MatchResultDisplayed { get; set; }
        public byte Level { get; set; }
        public List<UseItem> UseItems { get; set; } = new List<UseItem>();

        public CPUPartyInfo Process(FileStream saveDataReader)
        {
            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Com Name ID Value Name
            var comNameIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Com Name ID Value
            this.ComNameIDValue = saveDataReader.GetIdFromFileStream();

            // Get Current CPU Rank ID Value Name
            var currentCPURankIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Current CPU Rank ID Value
            this.CurrentCPURankIDValue = saveDataReader.GetIdFromFileStream();

            // Get Music ID Value Name
            var musicIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Music ID Value
            this.MusicIDValue = saveDataReader.GetIdFromFileStream();

            // Get Colleca ID Value Name
            var collecaIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Colleca ID Value
            this.CollecaIDValue = saveDataReader.GetIdFromFileStream();

            // Get Colleca Rarity Name
            var collecaRarityName = saveDataReader.GetStringFromFileStream(160);

            // Get Colleca Rarity
            this.CollecaRarity = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Profica Illust ID Value Name
            var proficaIllustIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Profica Illust ID Value
            this.ProficaIllustIDValue = saveDataReader.GetIdFromFileStream();

            // Get Profica Base Design ID Value Name
            var proficaBaseDesignIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Profica Base Design ID Value
            this.ProficaBaseDesignIDValue = saveDataReader.GetIdFromFileStream();

            // Get Profica Base Color ID Value Name
            var proficaBaseColorIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Profica Base Color ID Value
            this.ProficaBaseColorIDValue = saveDataReader.GetIdFromFileStream();

            // Get Ms Difficulty Name
            var msDifficultyName = saveDataReader.GetStringFromFileStream(160);

            // Get Ms Difficulty
            this.MsDifficulty = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Match Result State Name
            var matchResultStateName = saveDataReader.GetStringFromFileStream(160);

            // Get Match Result State
            this.MatchResultState = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Match Result Displayed Name
            var matchResultDisplayedName = saveDataReader.GetStringFromFileStream(160);

            // Get Match Result Displayed
            this.MatchResultDisplayed = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Level Name
            var levelName = saveDataReader.GetStringFromFileStream(160);

            // Get Level
            this.Level = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Use Item Name
            var useItemName = saveDataReader.GetStringFromFileStream(160);

            // Get Use Item Count
            var useItemsCount = saveDataReader.ReadByte() - 128;

            // Get Use Items
            for (int i = 0; i < useItemsCount; ++i)
            {
                this.UseItems.Add(new UseItem().Process(saveDataReader));
            }

            return this;
        }

        public string Display()
        {
            var useItemString = "";
            this.UseItems.ForEach(x => useItemString += $"\n{x.Display()}");

            return @$"
    #region MusicSelectInfo

    Object Count: {this.ObjectCount}
    Com Name ID Value: {this.ComNameIDValue}
    Current CPU Rank ID Value: {this.CurrentCPURankIDValue}
    Music ID Value: {this.MusicIDValue}
    Colleca ID Value: {this.CollecaIDValue}
    Colleca Rarity: {this.CollecaRarity}
    Profica Illust ID Value: {this.ProficaIllustIDValue}
    Profica Base Design ID Value: {this.ProficaBaseDesignIDValue}
    Profica Base Color ID Value: {this.ProficaBaseColorIDValue}
    Ms Difficulty: {this.MsDifficulty}
    Match Result State: {this.MatchResultState}
    Level: {this.Level}
    
    Use Items:
    #region UseItems
    {useItemString}
    #endregion UseItems
    
    #endregion MusicSelectInfo
";
        }
    }

    public class UseItem
    {
        public int Id { get; set; }
        public byte Used { get; set; }

        public UseItem Process(FileStream saveDataReader)
        {
            // Get Id
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Used
            this.Used = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }

        public string Display()
        {
            return @$"
    #region UseItem {this.Id}

    Id: {this.Id}
    Used: {this.Used}
    
    #endregion UseItem {this.Id}
";
        }
    }
}