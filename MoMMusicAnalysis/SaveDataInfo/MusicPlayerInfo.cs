using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class MusicPlayerInfo
    {
        public int ObjectCount { get; set; }
        public int SelectedMusicIDValue { get; set; }
        public byte SelectedSortCategory { get; set; }
        public byte SelectedSortType { get; set; }
        public byte SelectedRepeatStatus { get; set; }
        public string Version { get; set; }

        public MusicPlayerInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Selected Music ID Value Name
            var selectedMusicIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Music ID Value Length
            var selectedMusicIDValueLength = saveDataReader.ReadBytesFromFileStream(1);

            // Get Play Time Value
            this.SelectedMusicIDValue = BitConverter.ToInt32(saveDataReader.ReadBytesFromFileStream(4).ToArray());

            // Get Selected Sort Category Name
            var selectedSortCategoryName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Sort Category Value
            this.SelectedSortCategory = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Selected Sort Type Name
            var selectedSortTypeName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Sort Type Value
            this.SelectedSortType = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Selected Repeat Status Name
            var selectedRepeatStatusName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Repeat Status Value
            this.SelectedRepeatStatus = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            return @$"
    #region MusicPlayerInfo

    Selected Music ID Value: {this.SelectedMusicIDValue}
    Selected Sort Category: {this.SelectedSortCategory}
    Selected Sort Type: {this.SelectedSortType}
    Selected Repeat Status: {this.SelectedRepeatStatus}
    Version: {this.Version}
    
    #endregion MusicPlayerInfo
";
        }
    }
}