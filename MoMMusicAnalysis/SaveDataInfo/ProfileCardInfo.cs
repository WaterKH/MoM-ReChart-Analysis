using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class ProfileCardInfo
    {
        public int ObjectCount { get; set; }
        public List<byte> TotalProficaCount { get; set; }
        public List<byte> OwnProfica { get; set; }
        public Guid OwnGuid { get; set; }
        public int ProficaIllustIDValue { get; set; }
        public int ProficaBaseDesignIDValue { get; set; }
        public int ProficaBaseColorIDValue { get; set; }
        public int MarkScore { get; set; }
        public List<RivalProfileCard> RivalProficaList { get; set; } = new List<RivalProfileCard>();
        public string Version { get; set; }

        public ProfileCardInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Total Profica Count Name
            var totalProficaCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Profica Count
            this.TotalProficaCount = saveDataReader.FindDataFromFileStream();

            // Get Own Profica Name
            var ownProficaName = saveDataReader.GetStringFromFileStream(160);

            // Get Own Profica
            this.OwnProfica = saveDataReader.FindDataFromFileStream();

            // Get Own GUID Name
            var ownGuidName = saveDataReader.GetStringFromFileStream(160);

            // Get GUID Header?
            saveDataReader.ReadByte();

            // Get GUID
            this.OwnGuid = Guid.Parse(saveDataReader.GetStringFromFileStream());

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

            // Get Mark Score Name
            var markScoreName = saveDataReader.GetStringFromFileStream(160);

            // Get Mark Score
            this.MarkScore = saveDataReader.GetIdFromFileStream();

            // Get Rival Profica List Name
            var rivalProficaListName = saveDataReader.GetStringFromFileStream(160);

            // Get Rival Profica List Count
            var rivalProficaListCount = saveDataReader.GetCountFromFileStream();

            // Get Rival Profica List
            for (int i = 0; i < rivalProficaListCount; ++i)
            {
                this.RivalProficaList.Add(new RivalProfileCard().Process(saveDataReader));
            }

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            var rivalsString = "";
            this.RivalProficaList.ForEach(x => rivalsString += $"\n{x.Display()}");

            return @$"
    #region ProfileCardInfo

    Object Count: {this.ObjectCount}
    Total Profica Count: {this.TotalProficaCount}
    Own Profica: {this.OwnProfica}
    Own Guid: {this.OwnGuid.ToString()}
    Profica Illust ID Value: {this.ProficaIllustIDValue}
    Profica Base Color ID Value: {this.ProficaBaseColorIDValue}
    Mark Score: {this.MarkScore}

    Rival Profile Cards: 
    #region RivalProfileCards
    {rivalsString}
    #endregion RivalProfileCards

    Version: {this.Version}
    
    #endregion ProfileCardInfo
";
        }
    }

    public class RivalProfileCard
    {
        public int ObjectCount { get; set; }
        public byte IsNewProfica { get; set; }
        public byte IsLocked { get; set; }
        public List<byte> DateTime { get; set; }
        public LastPlayMusic LastPlayMusic { get; set; } = new LastPlayMusic();
        public Guid RivalGuid { get; set; }
        public int ProficaIllustIDValue { get; set; }
        public int ProficaBaseDesignIDValue { get; set; }
        public int ProficaBaseColorIDValue { get; set; }
        public List<byte> AccountName { get; set; }
        public List<byte> AccountID { get; set; }
        public List<byte> TotalRhythpoValue { get; set; }
        public List<byte> TotalPlayTime { get; set; }
        public List<byte> TotalAchievement { get; set; }
        public int GameCompletion { get; set; }
        public PlayMusicRanking PlayMusicRanking { get; set; } = new PlayMusicRanking();
        public PlayMusicRanking MatchSelectRanking { get; set; } = new PlayMusicRanking();
        public RivalParty UseParty { get; set; } = new RivalParty();
        public RivalPartyRanking UsePartyCount { get; set; } = new RivalPartyRanking();
        public CollectionCardCategory CollectionCardCategory { get; set; } = new CollectionCardCategory();
        public ClearMusicNumber ClearMusicNumber { get; set; } = new ClearMusicNumber();
        public List<byte> TotalMusicPlayCount { get; set; }
        public List<byte> TotalWinCount { get; set; }
        public List<byte> CurrentRate { get; set; }
        public int CpuBestRankIDValue { get; set; }
        public List<byte> TotalMissionClear { get; set; }
        public List<byte> LastObtainedAccountName { get; set; }

        public RivalProfileCard Process(FileStream saveDataReader)
        {
            // Get Object Count
            this.ObjectCount = saveDataReader.GetCountFromFileStream();

            // Get Is New Profica Name
            var isNewProficaName = saveDataReader.GetStringFromFileStream(160);

            // Get Is New Profica Count
            this.IsNewProfica = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Is Locked Name
            var isLockedName = saveDataReader.GetStringFromFileStream(160);

            // Get Is Locked
            this.IsLocked = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Date Time Name
            var dateTimeName = saveDataReader.GetStringFromFileStream(160);

            // Get Date Time
            this.DateTime = saveDataReader.FindDataFromFileStream();

            // Get Last Play Music
            this.LastPlayMusic.Process(saveDataReader);

            // Get Rival GUID Name
            var rivalGuidName = saveDataReader.GetStringFromFileStream(160);

            // Get GUID Header?
            saveDataReader.ReadByte();

            // Get Rival GUID
            this.RivalGuid = Guid.Parse(saveDataReader.GetStringFromFileStream());

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

            // Get Account Name Name
            var accountNameName = saveDataReader.GetStringFromFileStream(160);

            // Get Account Name
            this.AccountName = saveDataReader.FindDataFromFileStream();

            // Get Account Id Name
            var accountIdName = saveDataReader.GetStringFromFileStream(160);

            // Get Account Id
            this.AccountID = saveDataReader.FindDataFromFileStream();

            // Get Total Rhythpo Value Name
            var totalRhythpoValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Rhythpo Value
            this.TotalRhythpoValue = saveDataReader.FindDataFromFileStream();

            // Get Total Play Time Name
            var totalPlayTimeName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Play Time
            this.TotalPlayTime = saveDataReader.FindDataFromFileStream();

            // Get Total Achievement Name
            var totalAchievementName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Achievement
            this.TotalAchievement = saveDataReader.FindDataFromFileStream();

            // Get Game Completion Name
            var gameCompletionName = saveDataReader.GetStringFromFileStream(160);

            // Get Game Completion
            this.GameCompletion = saveDataReader.GetIdFromFileStream();

            // Get Play Music Ranking
            this.PlayMusicRanking = this.PlayMusicRanking.Process(saveDataReader);

            // Get Match Select Ranking
            this.MatchSelectRanking = this.PlayMusicRanking.Process(saveDataReader);

            // Get Use Party
            this.UseParty.Process(saveDataReader);

            // Get Use Party Count
            this.UsePartyCount.Process(saveDataReader);

            // Get Collection Card Category
            this.CollectionCardCategory.Process(saveDataReader);

            // Get Clear Music Number
            this.ClearMusicNumber.Process(saveDataReader);

            // Get Total Music Play Count Name
            var totalMusicPlayCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Music Play Count
            this.TotalMusicPlayCount = saveDataReader.FindDataFromFileStream();

            // Get Total Win Count Name
            var totalWinCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Win Count
            this.TotalWinCount = saveDataReader.FindDataFromFileStream();

            // Get Current Rate Name
            var currentRateName = saveDataReader.GetStringFromFileStream(160);

            // Get Current Rate
            this.CurrentRate = saveDataReader.FindDataFromFileStream();

            // Get Cpu Best Rank ID Value Name
            var cpuBestRankIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Cpu Best Rank ID Value
            this.CpuBestRankIDValue = saveDataReader.GetIdFromFileStream();

            // Get Total Mission Clear Name
            var totalMissionClearName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Mission Clear
            this.TotalMissionClear = saveDataReader.FindDataFromFileStream();

            // Get Last Obtained Account Name Name
            var LastObtainedAccountNameName = saveDataReader.GetStringFromFileStream(160);

            // 0xC7 Identifier?
            saveDataReader.ReadByte();

            // Get LastObtainedAccountName
            this.LastObtainedAccountName = saveDataReader.ReadBytesFromFileStream(14);

            return this;
        }

        public string Display()
        {
            return @$"
    #region RivalProfileCard

    Object Count: {this.ObjectCount}
    Is New Profica: {this.IsNewProfica}
    Is Locked: {this.IsLocked}
    Date Time: {this.DateTime.Display()}
    
    LastPlayMusic:
    #region LastPlayMusic
    {this.LastPlayMusic.Display()}
    #endregion LastPlayMusic

    Rival GUID: {this.RivalGuid.ToString()}
    Profica Illust ID Value: {this.ProficaIllustIDValue}
    Profica Base Design ID Value: {this.ProficaBaseDesignIDValue}
    Profica Base Color ID Value: {this.ProficaBaseColorIDValue}
    Account Name: {this.AccountName.Display()}
    Account ID: {this.AccountID.Display()}
    Total Rhythpo Value: {this.TotalRhythpoValue.Display()}
    Total Play Time: {this.TotalPlayTime.Display()}
    Total Achievement: {this.TotalAchievement.Display()}
    Game Completion: {this.GameCompletion}

    Play Music Ranking:
    #region PlayMusicRanking
    {this.PlayMusicRanking.Display()}
    #endregion PlayMusicRanking

    Match Select Ranking:
    #region MatchSelectRanking
    {this.MatchSelectRanking.Display()}
    #endregion MatchSelectRanking

    Use Party:
    #region UseParty
    {this.UseParty.Display()}
    #endregion UseParty

    Use Party Count:
    #region UsePartyCount
    {this.UsePartyCount.Display()}
    #endregion UsePartyCount

    Collection Card Category:
    #region CollectionCardCategory
    {this.CollectionCardCategory.Display()}
    #endregion CollectionCardCategory

    Clear Music Number:
    #region ClearMusicNumber
    {this.ClearMusicNumber.Display()}
    #endregion ClearMusicNumber

    Total Music Play Count: {this.TotalMusicPlayCount.Display()}
    Total Win Count: {this.TotalWinCount.Display()}
    Current Rate: {this.CurrentRate.Display()}
    Cpu Best Rank ID Value: {this.CpuBestRankIDValue}
    Total Mission Clear: {this.TotalMissionClear.Display()}
    Last Obtained Account Name: {this.LastObtainedAccountName.Display()}

    #endregion RivalProfileCard
";
        }
    }

    public class LastPlayMusic
    {
        public int ObjectCount { get; set; }
        public int MusicIDValue { get; set; }
        public byte MsDifficulty { get; set; }

        public LastPlayMusic Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Music ID Value Name
            var musicIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Music ID Value
            this.MusicIDValue = saveDataReader.GetIdFromFileStream();

            // Get Ms Difficulty Name
            var msDifficultyName = saveDataReader.GetStringFromFileStream(160);

            // Get Ms Difficulty
            this.MsDifficulty = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }

        public string Display()
        {
            return @$"
    #region LastPlayMusic

    Object Count: {this.ObjectCount}
    Music ID Value: {this.MusicIDValue}
    Ms Difficulty: {this.MsDifficulty}
    
    #endregion LastPlayMusic
";
        }
    }

    public class PlayMusicRanking
    {
        public int ObjectCount { get; set; }
        public List<PlayMusicRankingRecord> PlayRankingRecords { get; set; } = new List<PlayMusicRankingRecord>();

        public PlayMusicRanking Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Play Music Ranking Records Name
            var playMusicRankingRecordsName = saveDataReader.GetStringFromFileStream(160);

            // Get Play Music Ranking Records Count
            var playMusicRankingRecordsCount = saveDataReader.ReadByte() - 144;

            // Get Play Music Ranking Records
            for (int i = 0; i < playMusicRankingRecordsCount; ++i)
            {
                this.PlayRankingRecords.Add(new PlayMusicRankingRecord().Process(saveDataReader));
            }

            return this;
        }

        public string Display()
        {
            var playMusicRecordsString = "";
            this.PlayRankingRecords.ForEach(x => playMusicRecordsString += $"\n{x.Display()}");

            return @$"
    #region PlayMusicRanking

    Object Count: {this.ObjectCount}

    Play Music Ranking Records:
    #region PlayMusicRanking
    {playMusicRecordsString}
    #endregion PlayMusicRanking
";
        }
    }

    public class PlayMusicRankingRecord
    {
        public int ObjectCount { get; set; }
        public int MusicIDValue { get; set; }
        public List<byte> PlayCount { get; set; }

        public PlayMusicRankingRecord Process(FileStream saveDataReader)
        {
            // Get Name
            //var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Music ID Value Name
            var musicIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Music ID Value
            this.MusicIDValue = saveDataReader.GetIdFromFileStream();

            // Get Play Count Name
            var playCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Play Count
            this.PlayCount = saveDataReader.FindDataFromFileStream(nextCount: 0x82);

            return this;
        }

        public string Display()
        {
            return @$"
    #region LastPlayMusic

    Object Count: {this.ObjectCount}
    Music ID Value: {this.MusicIDValue}
    Play Count: {this.PlayCount.Display()}
    
    #endregion LastPlayMusic
";
        }
    }

    public class RivalParty
    {
        public int ObjectCount { get; set; }
        public int CurrentPartyIDValue { get; set; }
        public List<byte> Level { get; set; }
        public RivalPartyRanking UsePartyRanking = new RivalPartyRanking();

        public RivalParty Process(FileStream saveDataReader)
        {
            // Get Use Party Name
            var usePartyName = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Current Party ID Value Name
            var currentPartyIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Current Party ID Value
            this.CurrentPartyIDValue = saveDataReader.GetIdFromFileStream();

            // Get Level Name
            var levelName = saveDataReader.GetStringFromFileStream(160);

            // Get Level
            this.Level = saveDataReader.FindDataFromFileStream();

            // Get Use Part Ranking
            this.UsePartyRanking.Process(saveDataReader);

            return this;
        }

        public string Display()
        {
            return @$"
    #region RivalParty

    Object Count: {this.ObjectCount}
    Current Party ID Value: {this.CurrentPartyIDValue}
    Level: {this.Level.Display()}
    
    #endregion RivalParty
";
        }
    }

    public class RivalPartyRanking
    {
        public int ObjectCount { get; set; }
        public List<PartyRankingRecord> PartyRankingRecords { get; set; } = new List<PartyRankingRecord>();

        public RivalPartyRanking Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Party Ranking Records Name
            var partyRankingRecordsName = saveDataReader.GetStringFromFileStream(160);

            // Get Party Ranking Records Count
            var partyRankingRecordsCount = saveDataReader.ReadByte() - 144;

            // Get Party Ranking Records
            for (int i = 0; i < partyRankingRecordsCount; ++i)
            {
                this.PartyRankingRecords.Add(new PartyRankingRecord().Process(saveDataReader));
            }

            return this;
        }

        public string Display()
        {
            var partyRankingRecordsString = "";
            this.PartyRankingRecords.ForEach(x => partyRankingRecordsString += $"\n{x.Display()}");

            return @$"
    #region RivalPartyRanking

    Object Count: {this.ObjectCount}

    Party Ranking Records:
    #region PartyRankingRecords
    {partyRankingRecordsString}
    #endregion PartyRankingRecords

    #endregion RivalPartyRanking
";
        }
    }
    
    public class PartyRankingRecord
    {
        public int ObjectCount { get; set; }
        public int CurrentPartyIDValue { get; set; }
        public List<byte> PlayCount { get; set; }

        public PartyRankingRecord Process(FileStream saveDataReader)
        {
            // Get Name
            //var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Current Party ID Value Name
            var currentPartyIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Current Party ID Value
            this.CurrentPartyIDValue = saveDataReader.GetIdFromFileStream();

            // Get Play Count Name
            var playCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Play Count Value
            this.PlayCount = saveDataReader.FindDataFromFileStream(nextCount: 0x82);

            return this;
        }

        public string Display()
        {
            return @$"
    #region PartyRankingRecord

    Object Count: {this.ObjectCount}
    Current Party ID Value: {this.CurrentPartyIDValue}
    Play Count: {this.PlayCount.Display()}

    #endregion PartyRankingRecord
";
        }
    }

    public class CollectionCardCategory
    {
        public int ObjectCount { get; set; }
        public List<CardCategory> Categories { get; set; } = new List<CardCategory>();

        public CollectionCardCategory Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Categories Name
            var categoriesName = saveDataReader.GetStringFromFileStream(160);

            // Get Categories Count
            var categoriesCount = saveDataReader.ReadByte() - 128;

            // Get Categories
            for (int i = 0; i < categoriesCount; ++i)
            {
                this.Categories.Add(new CardCategory().Process(saveDataReader));
            }

            return this;
        }

        public string Display()
        {
            var categoriesString = "";
            this.Categories.ForEach(x => categoriesString += $"\n{x.Display()}");

            return @$"
    #region CollectionCardCategory

    Object Count: {this.ObjectCount}

    Categories:
    #region Categories
    {categoriesString}
    #endregion Categories

    #endregion CollectionCardCategory
";
        }
    }

    public class CardCategory
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public List<SubCardCategory> SubCategories = new List<SubCardCategory>();

        public CardCategory Process(FileStream saveDataReader)
        {
            // Get Id
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 144;

            for (int i = 0; i < this.ObjectCount; ++i)
            {
                this.SubCategories.Add(new SubCardCategory().Process(saveDataReader));
            }

            return this;
        }

        public string Display()
        {
            var categoriesString = "";
            this.SubCategories.ForEach(x => categoriesString += $"\n{x.Display()}");

            return @$"
    #region CardCategory {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    
    SubCategories:
    #region SubCategories
    {categoriesString}
    #endregion SubCategories

    #endregion CardCategory {this.Id}
";
        }
    }

    public class SubCardCategory
    {
        public int ObjectCount { get; set; }
        public byte CategoryNumber { get; set; }

        public SubCardCategory Process(FileStream saveDataReader)
        {
            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Category Number Name
            var categoryNumberName = saveDataReader.GetStringFromFileStream(160);

            // Get Category Number
            this.CategoryNumber = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }

        public string Display()
        {
            return @$"
    #region SubCardCategory

    Object Count: {this.ObjectCount}
    Category Number: {this.CategoryNumber}

    #endregion PartyRankingRecord
";
        }
    }

    public class ClearMusicNumber
    {
        public int ObjectCount { get; set; }
        public List<byte> TotalMusicNumber { get; set; }
        public List<RivalDifficultyRecord> DifficultyRecordDictionary = new List<RivalDifficultyRecord>();

        public ClearMusicNumber Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Total Music Number Name
            var totalMusicNumberName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Music Number
            this.TotalMusicNumber = saveDataReader.FindDataFromFileStream();

            // Get Difficulty Record Dictionary Name
            var difficultyRecordDictionaryName = saveDataReader.GetStringFromFileStream(160);

            // Get Difficulty Record Dictionary Count
            var difficultyRecordDictionaryCount = saveDataReader.ReadByte() - 128;

            // Get Categories
            for (int i = 0; i < difficultyRecordDictionaryCount; ++i)
            {
                this.DifficultyRecordDictionary.Add(new RivalDifficultyRecord().Process(saveDataReader));
            }

            return this;
        }

        public string Display()
        {
            var difficultyRecordsString = "";
            this.DifficultyRecordDictionary.ForEach(x => difficultyRecordsString += $"\n{x.Display()}");

            return @$"
    #region ClearMusicNumber

    Object Count: {this.ObjectCount}
    Total Music Number: {this.TotalMusicNumber}

    Difficulty Records:
    #region DifficultyRecordDictionary
    {difficultyRecordsString}
    #endregion DifficultyRecordDictionary

    #endregion ClearMusicNumber
";
        }
    }

    public class RivalDifficultyRecord
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public List<RivalPlayModeRecord> PlayModeRecordDictionary = new List<RivalPlayModeRecord>();
        //public List<RivalPlayModeRecord> StandardPlayModeRecordDictionary = new List<RivalPlayModeRecord>();
        //public List<RivalPlayModeRecord> ProudPlayModeRecordDictionary = new List<RivalPlayModeRecord>();

        public RivalDifficultyRecord Process(FileStream saveDataReader)
        {
            // Get Id
            this.Id = saveDataReader.ReadByte();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Play Mode Record Dictionary Name
            var playModeRecordDictionaryName = saveDataReader.GetStringFromFileStream(160);

            // Get Play Mode Record Dictionary Count
            var playModeRecordDictionaryCount = saveDataReader.ReadByte() - 128;

            // Get Play Mode Record Dictionary
            for (int i = 0; i < playModeRecordDictionaryCount; ++i)
            {
                this.PlayModeRecordDictionary.Add(new RivalPlayModeRecord().Process(saveDataReader, i));
            }

            return this;
        }

        public string Display()
        {
            var playModeRecordsString = "";
            this.PlayModeRecordDictionary.ForEach(x => playModeRecordsString += $"\n{x.Display()}");

            return @$"
    #region RivalDifficultyRecord

    Object Count: {this.ObjectCount}

    Play Mode Records:
    #region PlayModeRecordDictionary
    {playModeRecordsString}
    #endregion PlayModeRecordDictionary

    #endregion RivalDifficultyRecord
";
        }
    }

    public class RivalPlayModeRecord
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public byte ClearMusicNumber { get; set; }
        public byte FullExcelentMusicNumber { get; set; } // Mispelled for data in savedata
        public byte ExcellentBarCompleteMusicNumber { get; set; }
        public byte FullChainMusicNumber { get; set; }

        public RivalPlayModeRecord Process(FileStream saveDataReader, int currId)
        {
            // Get Id
            this.Id = saveDataReader.ReadByte();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Clear Music Number Name
            var clearMusicNumberName = saveDataReader.GetStringFromFileStream(160);

            // Get Clear Music Number
            this.ClearMusicNumber = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Full Excelent Music Number Name
            var fullExcelentMusicNumberName = saveDataReader.GetStringFromFileStream(160);

            // Get Full Excelent Music Number
            this.FullExcelentMusicNumber = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Excellent Bar Complete Music Number Name
            var excellentBarCompleteMusicNumberName = saveDataReader.GetStringFromFileStream(160);

            // Get Excellent Bar Complete Music Number
            this.ExcellentBarCompleteMusicNumber = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Full Chain Music Number Name
            var fullChainMusicNumberName = saveDataReader.GetStringFromFileStream(160);

            // Get Full Chain Music Number
            this.FullChainMusicNumber = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }

        public string Display()
        {
            return @$"
    #region RivalPlayModeRecord

    Object Count: {this.ObjectCount}
    Clear Music Number: {this.ClearMusicNumber}
    Full Excellent Music Number: {this.FullExcelentMusicNumber}
    Full Chain Music Number: {this.FullChainMusicNumber}

    #endregion RivalPlayModeRecord
";
        }
    }
}