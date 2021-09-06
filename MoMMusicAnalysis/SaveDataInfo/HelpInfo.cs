using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class HelpInfo
    {
        public int ObjectCount { get; set; }
        public List<Help> HelpDictionary = new List<Help>();
        public string Version { get; set; }

        public HelpInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Helps Name
            var helpDictionaryName = saveDataReader.GetStringFromFileStream(160);

            // Get Helps Count
            var helpDictionaryCount = saveDataReader.GetCountFromFileStream();

            // Get Helps
            for (int i = 0; i < helpDictionaryCount; ++i)
            {
                this.HelpDictionary.Add(new Help().Process(saveDataReader));
            }

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            var helpsString = "";
            this.HelpDictionary.ForEach(x => helpsString += $"\n{x.Display()}");

            return @$"
    #region HelpInfo

    Object Count: {this.ObjectCount}

    Helps:
    #region HelpDictionary
    {helpsString}
    #endregion HelpDictionary
    
    Version: {this.Version}
    
    #endregion HelpInfo
";
        }
    }

    public class Help
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public byte Unlocked { get; set; }
        public byte Looked { get; set; }

        public Help Process(FileStream saveDataReader)
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
    #region Help {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    Unlocked: {this.Unlocked}
    Looked: {this.Looked}
    
    #endregion Help {this.Id}
";
        }
    }
}