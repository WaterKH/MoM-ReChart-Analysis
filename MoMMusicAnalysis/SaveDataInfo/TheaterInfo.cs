using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class TheaterInfo
    {
        public int ObjectCount { get; set; }
        public List<MovieInfo> MovieInfos { get; set; } = new List<MovieInfo>();
        public string Version { get; set; }

        public TheaterInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Movie Infos Name
            var movieInfosName = saveDataReader.GetStringFromFileStream(160);

            // Get Movie Infos Count
            var movieInfosCount = saveDataReader.GetCountFromFileStream();

            // Get Movie Infos
            for (int i = 0; i < movieInfosCount; ++i)
            {
                this.MovieInfos.Add(new MovieInfo().Process(saveDataReader));
            }
            
            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }


        public string Display()
        {
            var movieInfosString = "";
            this.MovieInfos.ForEach(x => movieInfosString += $"\n{x.Display()}");

            return @$"
    #region TheaterInfo

    Object Count: {this.ObjectCount}

    Movies:
    #region MovieInfos 
    {movieInfosString}
    #endregion MovieInfos

    Version: {this.Version}
    
    #endregion TheaterInfo
";
        }
    }

    public class MovieInfo
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public byte SelectedFlag { get; set; }

        public MovieInfo Process(FileStream saveDataReader)
        {
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Selected Flag Name
            var selectedFlagName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Flag Value
            this.SelectedFlag = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }

        public string Display()
        {
            return @$"
    #region Movie {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    Selected Flag: {this.SelectedFlag}    

    #endregion Movie {this.Id}
";
        }
    }
}