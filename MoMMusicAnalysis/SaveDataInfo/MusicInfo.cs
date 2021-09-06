using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class MusicInfo
    {
        public int ObjectCount { get; set; }
        public List<MusicRecord> MusicRecordDictionary = new List<MusicRecord>();
        public string Version { get; set; }

        public MusicInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Music Record Dictionary Name
            var musicRecordDictionaryName = saveDataReader.GetStringFromFileStream(160);

            // Get Music Record Dictionary Count
            var musicRecordDictionaryCount = saveDataReader.GetCountFromFileStream();

            // Get Music Record Dictionary
            for (int i = 0; i < musicRecordDictionaryCount; ++i)
            {
                this.MusicRecordDictionary.Add(new MusicRecord().Process(saveDataReader));
            }

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            var pairMusicRecordsString = "";
            this.MusicRecordDictionary.ForEach(x => pairMusicRecordsString += $"\n{x.Display()}");

            return @$"
    #region PairMusicInfo

    Object Count: {this.ObjectCount}
    
    Pair Music Records:
    #region MusicRecordDictionary
    {pairMusicRecordsString}
    #endregion MusicRecordDictionary

    Version: {this.Version}
    
    #endregion PairMusicInfo
";
        }
    }

    public class MusicRecord
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public List<DifficultyRecord> DifficultyRecordDictionary = new List<DifficultyRecord>();
        public byte LookedUnlockedWindowFlag { get; set; }
        public byte LookedPlaySettingFlag { get; set; }
        public byte LookedMusicPlayerCursorMatchFlag { get; set; }
        public byte MusicPlayerFavoriteFlag { get; set; }
        public List<byte> Pickup { get; set; }
        public byte Favorite { get; set; }
        public List<byte> FriendCount { get; set; }
        public List<byte> VersusBattleSelectCount { get; set; }

        public MusicRecord Process(FileStream saveDataReader)
        {
            // Get Id
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Difficulty Record Dictionary Name
            var beginnerDifficultyRecordDictionaryName = saveDataReader.GetStringFromFileStream(160);

            // Get Difficulty Record Dictionary Count
            var beginnerDifficultyRecordDictionaryCount = saveDataReader.ReadByte() - 128;

            // Get Difficulty Record Dictionary
            for (int i = 0; i < beginnerDifficultyRecordDictionaryCount; ++i)
            {
                this.DifficultyRecordDictionary.Add(new DifficultyRecord().Process(saveDataReader));
            }

            // Get Looked Unlocked Window Flag Name
            var lookedUnlockedWindowFlagName = saveDataReader.GetStringFromFileStream(160);

            // Get Looked Unlocked Window Flag
            this.LookedUnlockedWindowFlag = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Looked Play Setting Flag Name
            var lookedPlaySettingFlagName = saveDataReader.GetStringFromFileStream(160);

            // Get Looked Play Setting Flag
            this.LookedPlaySettingFlag = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Unk
            saveDataReader.ReadByte();

            // Get Looked Music Player Cursor Match Flag Name
            var lookedMusicPlayerCursorMatchFlagName = saveDataReader.GetStringFromFileStream();

            // Get Looked Music Player Cursor Match Flag
            this.LookedMusicPlayerCursorMatchFlag = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Music Player Favorite Flag Name
            var musicPlayerFavoriteFlagName = saveDataReader.GetStringFromFileStream(160);

            // Get Music Player Favorite Flag
            this.MusicPlayerFavoriteFlag = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Pickup Name
            var pickupName = saveDataReader.GetStringFromFileStream(160);

            // Get Pickup
            this.Pickup = saveDataReader.FindDataFromFileStream();

            // Get Favorite Name
            var favoriteName = saveDataReader.GetStringFromFileStream(160);

            // Get Favorite
            this.Favorite = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Friend Count Name
            var friendCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Friend Count
            this.FriendCount = saveDataReader.FindDataFromFileStream();

            // Get Versus Battle Select Count Name
            var versusBattleSelectCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Versus Battle Select Count
            this.VersusBattleSelectCount = saveDataReader.FindDataFromFileStream(nextId: this.Id + 1);

            return this;
        }

        public string Display()
        {
            var difficultyRecordsString = "";
            this.DifficultyRecordDictionary.ForEach(x => difficultyRecordsString += $"\n{x.Display()}");

            return @$"
    #region PairMusicRecord {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    
    Difficulty Records:
    #region DifficultyRecordDictionary
    {difficultyRecordsString}
    #endregion DifficultyRecordDictionary
    
    Looked Music Player Cursor Match Flag: {this.LookedMusicPlayerCursorMatchFlag}
    Music Player Favorite Flag: {this.MusicPlayerFavoriteFlag}
    Pickup: {this.Pickup.Display()}
    Favorite: {this.Favorite}
    Friend Count: {this.FriendCount.Display()}
    Versus Battle Select Count: {this.VersusBattleSelectCount}

    #endregion PairMusicRecord {this.Id}
";
        }
    }

    public class DifficultyRecord
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public List<PlayModeRecord> PlayModeRecordDictionary = new List<PlayModeRecord>();
        //public List<PlayModeRecord> StandardPlayModeRecordDictionary = new List<PlayModeRecord>();
        //public List<PlayModeRecord> ProudPlayModeRecordDictionary = new List<PlayModeRecord>();
        public byte PlayedPerformerFlag { get; set; }
        public List<byte> CoolCount { get; set; }
        public List<byte> ComboCount { get; set; }
        public byte AchievedFullComboFlag { get; set; }

        public DifficultyRecord Process(FileStream saveDataReader)
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
                this.PlayModeRecordDictionary.Add(new PlayModeRecord().Process(saveDataReader));
            }

            // Get Played Performer Flag Name
            var playedPerformerFlagName = saveDataReader.GetStringFromFileStream(160);

            // Get Played Performer Flag
            this.PlayedPerformerFlag = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Cool Count Name
            var coolCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Cool Count
            this.CoolCount = saveDataReader.FindDataFromFileStream();

            // Get Combo Count Name
            var comboCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Combo Count
            this.ComboCount = saveDataReader.FindDataFromFileStream();

            // Get Achieved Full Combo Flag Name
            var achievedFullComboFlagName = saveDataReader.GetStringFromFileStream(160);

            // Get Achieved Full Combo Flag
            this.AchievedFullComboFlag = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }

        public string Display()
        {
            var playModeRecordsString = "";
            this.PlayModeRecordDictionary.ForEach(x => playModeRecordsString += $"\n{x.Display()}");
            //var standardPlayModeRecordsString = "";
            //this.StandardPlayModeRecordDictionary.ForEach(x => standardPlayModeRecordsString += $"\n{x.Display()}");
            //var proudPlayModeRecordsString = "";
            //this.ProudPlayModeRecordDictionary.ForEach(x => proudPlayModeRecordsString += $"\n{x.Display()}");

            return @$"
    #region PairMusicRecord {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    
    Play Mode Records:
    #region BeginnerPlayModeRecordDictionary
    {playModeRecordsString}
    #endregion BeginnerPlayModeRecordDictionary
    
    Played Performer Flag: {this.PlayedPerformerFlag}
    Cool Count: {this.CoolCount}
    Combo Count: {this.ComboCount}
    Achieved Full Combo Flag: {this.AchievedFullComboFlag}

    #endregion PairMusicRecord {this.Id}
";
        }
    }

    public class PlayModeRecord
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public List<byte> Score { get; set; }
        public List<byte> _UpdateDate { get; set; }
        public List<byte> MaxChain { get; set; }
        public List<byte> PlayRank { get; set; }
        public byte ClearMark { get; set; }
        public List<ExcellentBarPortion> ExcellentBarZones { get; set; } = new List<ExcellentBarPortion>();
        public List<byte> PlayCount { get; set; }
        public byte FullChainClear { get; set; }

        public PlayModeRecord Process(FileStream saveDataReader)
        {
            // Get Id
            this.Id = saveDataReader.ReadByte();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Score Name
            var scoreName = saveDataReader.GetStringFromFileStream(160);

            // Get Score
            this.Score = saveDataReader.FindDataFromFileStream();

            // Get Update Date Name
            var _updateDateName = saveDataReader.GetStringFromFileStream(160);

            // Get Update Date
            this._UpdateDate = saveDataReader.FindDataFromFileStream();

            // Get Max Chain Name
            var maxChainName = saveDataReader.GetStringFromFileStream(160);

            // Get Max Chain
            this.MaxChain = saveDataReader.FindDataFromFileStream();

            // Get Play Rank Name
            var playRankName = saveDataReader.GetStringFromFileStream(160);

            // Get Play Rank
            this.PlayRank = saveDataReader.FindDataFromFileStream();

            // Get Clear Mark Name
            var clearMarkName = saveDataReader.GetStringFromFileStream(160);

            // Get Clear Mark
            this.ClearMark = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Excellent Bars Name
            var excellentBarsName = saveDataReader.GetStringFromFileStream(160);

            // Get Excellent Bars Count
            var excellentBarsCount = saveDataReader.GetCountFromFileStream();

            // Get Excellent Bars
            for (int i = 0; i < excellentBarsCount; ++i)
            {
                this.ExcellentBarZones.Add(new ExcellentBarPortion().Process(saveDataReader));
            }

            // Get Play Count Name
            var playCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Play Count
            this.PlayCount = saveDataReader.FindDataFromFileStream();

            // Get Full Chain Count Name
            var fullChainCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Full Chain Count
            this.FullChainClear = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }

        public string Display()
        {
            var excellentBarsString = "";
            this.ExcellentBarZones.ForEach(x => excellentBarsString += $"\n{x.Display()}");

            return @$"
    #region SongRecord {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    Score: {this.Score.Display()}
    Update Date: {this._UpdateDate.Display()}
    Total Chain: {this.MaxChain.Display()}
    Play Rank: {this.PlayRank.Display()}
    Clear Mark: {this.ClearMark}
    
    Excellent Bars:
    #region ExcellentBarPortions
    {excellentBarsString}
    #endregion ExcellentBarPortions

    Play Count: {this.PlayCount.Display()}
    Full Chain Clear: {this.FullChainClear}
    
    #endregion SongRecord {this.Id}
";
        }
    }

    public class ExcellentBarPortion
    {
        public int AmountFilled { get; set; }

        public ExcellentBarPortion Process(FileStream saveDataReader)
        {
            // Get Amount
            this.AmountFilled = saveDataReader.GetIdFromFileStream();

            return this;
        }

        public string Display()
        {
            return @$"
    Amount Filled: {this.AmountFilled}
";
        }
    }
}