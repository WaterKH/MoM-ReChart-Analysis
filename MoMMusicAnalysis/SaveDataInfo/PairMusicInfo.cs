using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class PairMusicInfo
    {
        public int ObjectCount { get; set; }
        public List<PairMusicRecord> MusicRecordDictionary = new List<PairMusicRecord>();
        public string Version { get; set; }

        public PairMusicInfo Process(FileStream saveDataReader)
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
                this.MusicRecordDictionary.Add(new PairMusicRecord().Process(saveDataReader));
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

    public class PairMusicRecord
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public List<PairDifficultyRecord> DifficultyRecordDictionary = new List<PairDifficultyRecord>();
        public byte LookedUnlockedWindowFlag { get; set; }
        public byte LookedPlaySettingFlag { get; set; }

        public PairMusicRecord Process(FileStream saveDataReader)
        {
            // Get Id
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Difficulty Record Dictionary Name
            var difficultyRecordDictionaryName = saveDataReader.GetStringFromFileStream(160);

            // Get Difficulty Record Dictionary Count
            var difficultyRecordDictionaryCount = saveDataReader.ReadByte() - 128;

            // Get Difficulty Record Dictionary
            for (int i = 0; i < difficultyRecordDictionaryCount; ++i)
            {
                this.DifficultyRecordDictionary.Add(new PairDifficultyRecord().Process(saveDataReader));
            }

            // Get Looked Unlocked Window Flag Name
            var lookedUnlockedWindowFlagName = saveDataReader.GetStringFromFileStream(160);

            // Get Looked Unlocked Window Flag
            this.LookedUnlockedWindowFlag = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Looked Play Setting Flag Name
            var lookedPlaySettingFlagName = saveDataReader.GetStringFromFileStream(160);

            // Get Looked Play Setting Flag
            this.LookedPlaySettingFlag = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

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
    Looked Unlocked Window Flag: {this.LookedUnlockedWindowFlag}
    Looked Play Setting Flag: {this.LookedPlaySettingFlag}
    
    Difficulty Records:
    #region DifficultyRecordDictionary
    {difficultyRecordsString}
    #endregion DifficultyRecordDictionary
    
    #endregion PairMusicRecord {this.Id}
";
        }
    }

    public class PairDifficultyRecord
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public List<byte> Score { get; set; }
        public List<byte> _UpdateDate { get; set; }
        public List<byte> TotalChain { get; set; }
        public List<byte> PlayRank { get; set; }
        public byte ClearMark { get; set; }
        public List<PairExcellentBarPortion> ExcellentBars { get; set; } = new List<PairExcellentBarPortion>();
        public List<byte> PlayCount { get; set; }
        public List<byte> FullChainCount { get; set; }

        public PairDifficultyRecord Process(FileStream saveDataReader)
        {
            // Get Id
            this.Id = saveDataReader.ReadByte();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte();

            // Get Score Name
            var scoreName = saveDataReader.GetStringFromFileStream(160);

            // Get Score
            this.Score = saveDataReader.FindDataFromFileStream();

            // Get Update Date Name
            var _updateDateName = saveDataReader.GetStringFromFileStream(160);

            // Get Update Date
            this._UpdateDate = saveDataReader.FindDataFromFileStream();

            // Get Total Chain Name
            var totalChainName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Chain
            this.TotalChain = saveDataReader.FindDataFromFileStream();

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
                this.ExcellentBars.Add(new PairExcellentBarPortion().Process(saveDataReader));
            }

            // Get Play Count Name
            var playCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Play Count
            this.PlayCount = saveDataReader.FindDataFromFileStream();

            // Get Full Chain Count Name
            var fullChainCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Full Chain Count
            this.FullChainCount = saveDataReader.FindDataFromFileStream();

            return this;
        }

        public string Display()
        {
            var excellentBarsString = "";
            this.ExcellentBars.ForEach(x => excellentBarsString += $"\n{x.Display()}");

            return @$"
    #region PairDifficultyRecord

    Object Count: {this.ObjectCount}
    Score: {this.Score.Display()}
    Update Date: {this._UpdateDate.Display()}
    Total Chain: {this.TotalChain.Display()}
    Play Rank: {this.PlayRank.Display()}
    Clear Mark: {this.ClearMark}
    
    Excellent Bars:
    #region ExcellentBarPortions
    {excellentBarsString}
    #endregion ExcellentBarPortions

    Play Count: {this.PlayCount.Display()}
    Full Chain Count: {this.FullChainCount.Display()}
    
    #endregion PairDifficultyRecord
";
        }
    }

    public class PairExcellentBarPortion
    {
        public int AmountFilled { get; set; }

        public PairExcellentBarPortion Process(FileStream saveDataReader)
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