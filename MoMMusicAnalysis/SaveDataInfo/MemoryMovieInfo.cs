using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class MemoryMovieInfo
    {
        public int ObjectCount { get; set; }
        public List<MemoryMovie> MemoryMovieDictionary = new List<MemoryMovie>();
        public string Version { get; set; }

        public MemoryMovieInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Memory Movies Name
            var memoryMovieDictionaryName = saveDataReader.GetStringFromFileStream(160);

            // Get Memory Movies Count
            var memoryMovieDictionaryCount = saveDataReader.GetCountFromFileStream();

            // Get Memory Movies
            for (int i = 0; i < memoryMovieDictionaryCount; ++i)
            {
                this.MemoryMovieDictionary.Add(new MemoryMovie().Process(saveDataReader));
            }

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            var memoryMoviesString = "";
            this.MemoryMovieDictionary.ForEach(x => memoryMoviesString += $"\n{x.Display()}");

            return @$"
    #region MemoryMovieInfo

    Object Count: {this.ObjectCount}

    Memory Movies:
    #region MemoryMovieDictionary
    {memoryMoviesString}
    #endregion MemoryMovieDictionary
    
    Version: {this.Version}
    
    #endregion MemoryMovieInfo
";
        }
    }

    public class MemoryMovie
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public byte Played { get; set; }
        public byte Selected { get; set; }

        public MemoryMovie Process(FileStream saveDataReader)
        {
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Played Name
            var playedName = saveDataReader.GetStringFromFileStream(160);

            // Get Played
            this.Played = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Selected Name
            var selectedName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected
            this.Selected = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }

        public string Display()
        {
            return @$"
    #region MemoryMovie {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    Played: {this.Played}
    Selected: {this.Selected}
    
    #endregion MemoryMovie {this.Id}
";
        }
    }
}