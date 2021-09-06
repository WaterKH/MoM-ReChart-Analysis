using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class PartyInfo
    {
        public int ObjectCount { get; set; }
        public List<Party> Parties { get; set; } = new List<Party>();
        public string Version { get; set; }

        public PartyInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Parties Name
            var partiesName = saveDataReader.GetStringFromFileStream(160);

            // Get Party Count
            var partyCount = saveDataReader.ReadByte() - 128;

            // Get Parties
            for (int i = 0; i < partyCount; ++i)
            {
                this.Parties.Add(new Party().Process(saveDataReader));
            }

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            var partyString = "";
            this.Parties.ForEach(x => partyString += $"\n{x.Display()}");

            return @$"
    #region PartyInfo

    Object Count: {this.ObjectCount}
    
    Parties: 
    #region Parties
    {partyString}
    #endregion Parties

    Version: {this.Version}
    
    #endregion PartyInfo
";
        }
    }

    public class Party
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public List<byte> EXP { get; set; }
        public byte Unlocked { get; set; }
        public byte Used { get; set; }
        public List<byte> PlayCount { get; set; }

        public Party Process(FileStream saveDataReader)
        {
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get EXP Name
            var expName = saveDataReader.GetStringFromFileStream(160);

            // Get EXP Value
            this.EXP = saveDataReader.FindDataFromFileStream();

            // Get Unlocked Name
            var unlockedName = saveDataReader.GetStringFromFileStream(160);

            // Get Unlocked Value
            this.Unlocked = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Used Name
            var usedName = saveDataReader.GetStringFromFileStream(160);

            // Get Used Value
            this.Used = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Play Count Name
            var playCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Play Count Value
            this.PlayCount = saveDataReader.FindDataFromFileStream(nextId: this.Id + 1);

            return this;
        }

        public string Display()
        {
            return @$"
    #region Party {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    EXP: {this.EXP.Display()}
    Unlocked: {this.Unlocked}
    Used: {this.Used}
    Play Count: {this.PlayCount.Display()}
    
    #endregion Party {this.Id}
";
        }
    }
}