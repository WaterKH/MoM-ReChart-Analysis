using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class TutorialInfo
    {
        public int ObjectCount { get; set; }
        public List<Tutorial> TutorialDictionary = new List<Tutorial>();
        public string Version { get; set; }

        public TutorialInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Tutorials Name
            var tutorialDictionaryName = saveDataReader.GetStringFromFileStream(160);

            // Get Tutorials Count
            var tutorialDictionaryCount = saveDataReader.ReadByte() - 128;

            // Get Tutorials
            for (int i = 0; i < tutorialDictionaryCount; ++i)
            {
                this.TutorialDictionary.Add(new Tutorial().Process(saveDataReader));
            }

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            var tutorialsString = "";
            this.TutorialDictionary.ForEach(x => tutorialsString += $"\n{x.Display()}");

            return @$"
    #region TutorialInfo

    Object Count: {this.ObjectCount}

    Tutorials:
    #region TutorialDictionary
    {tutorialsString}
    #endregion TutorialDictionary
    
    Version: {this.Version}
    
    #endregion TutorialInfo
";
        }
    }

    public class Tutorial
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public byte Unlocked { get; set; }
        public byte Looked { get; set; }

        public Tutorial Process(FileStream saveDataReader)
        {
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Unlocked Name
            var unlockedName = saveDataReader.GetStringFromFileStream(160);

            // Get Unlocked
            this.Unlocked = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Looked Name
            var lookedName = saveDataReader.GetStringFromFileStream(160);

            // Get Looked
            this.Looked = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }

        public string Display()
        {
            return @$"
    #region Tutorial {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    Unlocked: {this.Unlocked}
    Looked: {this.Looked}
    
    #endregion Tutorial {this.Id}
";
        }
    }
}