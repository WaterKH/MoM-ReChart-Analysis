using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class AchievementInfo
    {
        public int ObjectCount { get; set; }
        public List<Achievement> AchieveDictionary = new List<Achievement>();
        public string Version { get; set; }

        public AchievementInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Achievements Name
            var achieveDictionaryName = saveDataReader.GetStringFromFileStream(160);

            // Get Achievements Count
            var achieveDictionaryCount = saveDataReader.GetCountFromFileStream();

            // Get Parties
            for (int i = 0; i < achieveDictionaryCount; ++i)
            {
                this.AchieveDictionary.Add(new Achievement().Process(saveDataReader));
            }

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            var achievementsString = "";
            this.AchieveDictionary.ForEach(x => achievementsString += $"\n{x.Display()}");

            return @$"
    #region AchievementInfo

    Object Count: {this.ObjectCount}

    Achievements:
    #region AchieveDictionary
    {achievementsString}
    #endregion AchieveDictionary
    
    Version: {this.Version}
    
    #endregion AchievementInfo
";
        }
    }

    public class Achievement
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public byte Padding { get; set; }
        public byte ViewHint { get; set; }
        public byte LookedAchieveWindow { get; set; }

        public Achievement Process(FileStream saveDataReader)
        {
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Padding Name
            var paddingName = saveDataReader.GetStringFromFileStream(160);

            // Get Padding
            this.Padding = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get View Hint Name
            var viewHintName = saveDataReader.GetStringFromFileStream(160);

            // Get View Hint
            this.ViewHint = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Looked Achieve Window Name
            var lookedAchieveWindowName = saveDataReader.GetStringFromFileStream(160);

            // Get Looked Achieve Window
            this.LookedAchieveWindow = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }

        public string Display()
        {
            return @$"
    #region Achievement {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    Padding: {this.Padding}
    View Hint: {this.ViewHint}
    Looked Achieve Window: {this.LookedAchieveWindow}
    
    #endregion Achievement {this.Id}
";
        }
    }
}