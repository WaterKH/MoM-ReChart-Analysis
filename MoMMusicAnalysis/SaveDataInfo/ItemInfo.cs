using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class ItemInfo
    {
        public int ObjectCount { get; set; }
        public List<MsItem> MsItems { get; set; } = new List<MsItem>();
        public List<MusicItem> UnlockMusics { get; set; } = new List<MusicItem>();
        public List<MixItem> MixItems { get; set; } = new List<MixItem>();
        public List<IllustratedProfileCard> IllustratedProfileCards { get; set; } = new List<IllustratedProfileCard>();
        public string Version { get; set; }

        public ItemInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Ms Items Name
            var msItemsName = saveDataReader.GetStringFromFileStream(160);

            // Get Ms Items Count
            var msItemsCount = saveDataReader.ReadByte() - 128;

            // Get Ms Items
            for (int i = 0; i < msItemsCount; ++i)
            {
                this.MsItems.Add(new MsItem().Process(saveDataReader));
            }

            // Get Unlock Musics Name
            var unlockMusicsName = saveDataReader.GetStringFromFileStream(160);

            // Get Unlock Musics Count
            var unlockMusicsCount = saveDataReader.GetCountFromFileStream();

            // Get Unlock Musics
            for (int i = 0; i < unlockMusicsCount; ++i)
            {
                this.UnlockMusics.Add(new MusicItem().Process(saveDataReader));
            }

            // Get Mix Items Name
            var mixItemsName = saveDataReader.GetStringFromFileStream(160);

            // Get Mix Items Count
            var mixItemsCount = saveDataReader.GetCountFromFileStream();

            // Get Mix Items
            for (int i = 0; i < mixItemsCount; ++i)
            {
                this.MixItems.Add(new MixItem().Process(saveDataReader));
            }

            // Get Illustrated Profile Cards Name
            var illustratedProfileCardsName = saveDataReader.GetStringFromFileStream(160);

            // Get Illustrated Profile Cards Count
            var illustratedProfileCardsCount = saveDataReader.GetCountFromFileStream();

            // Get Mix Items
            for (int i = 0; i < illustratedProfileCardsCount; ++i)
            {
                this.IllustratedProfileCards.Add(new IllustratedProfileCard().Process(saveDataReader));
            }

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            var msItemsString = "";
            this.MsItems.ForEach(x => msItemsString += $"\n{x.Display()}");
            var unlockMusicsString = "";
            this.UnlockMusics.ForEach(x => unlockMusicsString += $"\n{x.Display()}");
            var mixItemsString = "";
            this.MixItems.ForEach(x => mixItemsString += $"\n{x.Display()}");
            var illustCardsString = "";
            this.IllustratedProfileCards.ForEach(x => illustCardsString += $"\n{x.Display()}");

            return @$"
    #region ItemInfo

    Object Count: {this.ObjectCount}
    
    Ms Items: 
    #region MsItems
    {msItemsString}
    #endregion MsItems

    Music Unlocks: 
    #region MusicUnlocks
    {unlockMusicsString}
    #endregion MusicUnlocks

    Mix Items: 
    #region MixItems
    {mixItemsString}
    #endregion MixItems

    Illustrated Profile Cards: 
    #region IllustratedProfileCards
    {illustCardsString}
    #endregion IllustratedProfileCards

    Version: {this.Version}
    
    #endregion ItemInfo
";
        }
    }

    public class MsItem
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public byte IsObtained { get; set; }
        public byte IsEquipped { get; set; }
        public List<byte> UseCount { get; set; } = new List<byte>();
        public List<byte> PossessionNumber { get; set; } = new List<byte>();
        public byte IsSelected { get; set; }

        public MsItem Process(FileStream saveDataReader)
        {
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Is Obtained Name
            var isObtainedName = saveDataReader.GetStringFromFileStream(160);

            // Get Is Obtained Value
            this.IsObtained = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Is Equipped Name
            var isEquippedName = saveDataReader.GetStringFromFileStream(160);

            // Get Is Equipped Value
            this.IsEquipped = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Use Count Name
            var useCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Use Count Value
            this.UseCount = saveDataReader.FindDataFromFileStream();

            // Get Possession Number Name
            var possessionNumberName = saveDataReader.GetStringFromFileStream(160);

            // Get Possession Number Value
            this.PossessionNumber = saveDataReader.FindDataFromFileStream();

            // Get Is Selected Name
            var isSelectedName = saveDataReader.GetStringFromFileStream(160);

            // Get Is Selected Value
            this.IsSelected = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }

        public string Display()
        {
            return @$"
    #region MsItem {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    Is Obtained: {this.IsObtained}
    Is Equipped: {this.IsEquipped}
    Use Count: {this.UseCount.Display()}
    Possession Number: {this.PossessionNumber.Display()}
    Is Selected: {this.IsSelected}
    
    #endregion MsItem {this.Id}
";
        }
    }

    public class MusicItem
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public List<byte> PossessionNumber { get; set; } = new List<byte>();
        public byte IsSelected { get; set; }

        public MusicItem Process(FileStream saveDataReader)
        {
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Possession Number Name
            var possessionNumberName = saveDataReader.GetStringFromFileStream(160);

            // Get Possession Number Value
            this.PossessionNumber = saveDataReader.FindDataFromFileStream();

            // Get Is Selected Name
            var isSelectedName = saveDataReader.GetStringFromFileStream(160);

            // Get Is Selected Value
            this.IsSelected = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }

        public string Display()
        {
            return @$"
    #region MusicItem {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    Possession Number: {this.PossessionNumber.Display()}
    Is Selected: {this.IsSelected}
    
    #endregion MusicItem {this.Id}
";
        }
    }

    public class MixItem
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public byte IsObtained { get; set; }
        public List<byte> PossessionNumber { get; set; }
        public byte IsSelected { get; set; }

        public MixItem Process(FileStream saveDataReader)
        {
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Is Obtained Name
            var isObtainedName = saveDataReader.GetStringFromFileStream(160);

            // Get Is Obtained Value
            this.IsObtained = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Possession Number Name
            var possessionNumberName = saveDataReader.GetStringFromFileStream(160);

            // Get Possession Number Value
            this.PossessionNumber = saveDataReader.FindDataFromFileStream();

            // Get Is Selected Name
            var isSelectedName = saveDataReader.GetStringFromFileStream(160);

            // Get Is Selected Value
            this.IsSelected = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }

        public string Display()
        {
            return @$"
    #region MixItem {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    Is Obtained: {this.IsObtained}
    Possession Number: {this.PossessionNumber.Display()}
    Is Selected: {this.IsSelected}
    
    #endregion MixItem {this.Id}
";
        }
    }

    public class IllustratedProfileCard
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public List<byte> PossessionNumber { get; set; } = new List<byte>();
        public byte IsSelected { get; set; }

        public IllustratedProfileCard Process(FileStream saveDataReader)
        {
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Possession Number Name
            var possessionNumberName = saveDataReader.GetStringFromFileStream(160);

            // Get Possession Number Value
            this.PossessionNumber = saveDataReader.FindDataFromFileStream();

            // Get Is Selected Name
            var isSelectedName = saveDataReader.GetStringFromFileStream(160);

            // Get Is Selected Value
            this.IsSelected = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }

        public string Display()
        {
            return @$"
    #region IllustratedProfileCard {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    Possession Number: {this.PossessionNumber.Display()}
    Is Selected: {this.IsSelected}
    
    #endregion IllustratedProfileCard {this.Id}
";
        }
    }
}