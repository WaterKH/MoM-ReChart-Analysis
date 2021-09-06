using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class MusicSelectInfo
    {
        public int ObjectCount { get; set; }
        public int SelectedMusicIDValue { get; set; }
        public byte SelectedSortCategory { get; set; }
        public byte SelectedSortType { get; set; }
        public byte SelectedDifficulty { get; set; }
        public byte PlayMode { get; set; }
        public List<byte> _lottedPickupDate { get; set; }
        public List<byte> PickupFullChainCount { get; set; }
        public List<byte> TotalClearCount { get; set; }
        public string Version { get; set; }

        public MusicSelectInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Selected Music ID Value Name
            var selectedMusicIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Music ID Value
            this.SelectedMusicIDValue = saveDataReader.GetIdFromFileStream();

            // Get Selected Sort Category Name
            var selectedSortCategoryName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Sort Category State
            this.SelectedSortCategory = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Selected Sort Type Name
            var selectedSortTypeName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Sort Type State
            this.SelectedSortType = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Selected Difficulty Name
            var selectedDifficultyName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected Difficulty State
            this.SelectedDifficulty = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Play Mode Name
            var playModeName = saveDataReader.GetStringFromFileStream(160);

            // Get Play Mode Value
            this.PlayMode = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get _lottedPickupDate Name
            var _lottedPickupDateName = saveDataReader.GetStringFromFileStream(160);

            // Get _lottedPickupDate Value
            this._lottedPickupDate = saveDataReader.FindDataFromFileStream();

            // Get Pickup Full Chain Count Name
            var pickupFullChainCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Pickup Full Chain Count
            this.PickupFullChainCount = saveDataReader.FindDataFromFileStream();

            // Get Total Clear Count Name
            var totalClearCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Clear Count
            this.TotalClearCount = saveDataReader.FindDataFromFileStream();

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            return @$"
    #region MusicSelectInfo

    Object Count: {this.ObjectCount}
    Selected Music ID Value: {this.SelectedMusicIDValue}
    Selected Sort Category: {this.SelectedSortCategory}
    Selected Sort Type: {this.SelectedSortType}
    Selected Difficulty: {this.SelectedDifficulty}
    Play Mode: {this.PlayMode}
    _lotted Pickup Date: {this._lottedPickupDate.Display()}
    Pickup Full Chain Count: {this.PickupFullChainCount.Display()}
    Total Clear Count: {this.TotalClearCount}
    Version: {this.Version}
    
    #endregion MusicSelectInfo
";
        }
    }
}