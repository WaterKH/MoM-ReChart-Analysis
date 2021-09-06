using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class CollectionCardInfo
    {
        public int ObjectCount { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<CollectionCard> CollectionCards { get; set; } = new List<CollectionCard>();
        public string Version { get; set; }

        public CollectionCardInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Categories Name
            var categoriesName = saveDataReader.GetStringFromFileStream(160);

            // Get Category Count
            var categoryCount = saveDataReader.ReadByte() - 128;

            // Get Categories
            for (int i = 0; i < categoryCount; ++i)
            {
                this.Categories.Add(new Category().Process(saveDataReader));
            }

            // Get Collection Cards Name
            var collectionCardsName = saveDataReader.GetStringFromFileStream(160);

            // Header Identifier
            saveDataReader.ReadBytesFromFileStream(1);

            // Get Collection Card Count
            var count = saveDataReader.ReadBytesFromFileStream(2);
            count.Reverse();
            var collectionCardCount = BitConverter.ToInt16(count.ToArray());

            // Get Collection Cards
            for (int i = 0; i < collectionCardCount; ++i)
            {
                this.CollectionCards.Add(new CollectionCard().Process(saveDataReader));
            }

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }


        public string Display()
        {
            var categoriesString = "";
            this.Categories.ForEach(x => categoriesString += $"\n{x.Display()}");
            
            var collectionCardsString = "";
            this.CollectionCards.ForEach(x => collectionCardsString += $"\n{x.Display()}");

            return @$"
    #region CollectionCardInfo

    Object Count: {this.ObjectCount}

    Categories:
    #region Categories 
    {categoriesString}
    #endregion Categories

    Collection Cards:
    #region CollectionCards 
    {collectionCardsString}
    #endregion CollectionCards

    Version: {this.Version}
    
    #endregion CollectionCardInfo
";
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public byte PossessionNumber { get; set; }
        public byte Level { get; set; }

        public Category Process(FileStream saveDataReader)
        {
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Possession Number Name
            var possessionNumberName = saveDataReader.GetStringFromFileStream(160);

            // Get Possession Number Value
            this.PossessionNumber = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Level Name
            var levelName = saveDataReader.GetStringFromFileStream(160);

            // Get Level Value
            this.Level = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }


        public string Display()
        {
            return @$"
    #region Category {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    Possession Number: {this.PossessionNumber}
    Level: {this.Level}
    
    #endregion Category {this.Id}
";
        }
    }

    public class CollectionCard
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public Card GoldCard { get; set; }
        public Card PlatCard { get; set; }

        public CollectionCard Process(FileStream saveDataReader)
        {
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 144;

            // Get Gold Card
            this.GoldCard = new Card().Process(saveDataReader);

            // Get Plat Card
            this.PlatCard = new Card().Process(saveDataReader);
            
            return this;
        }


        public string Display()
        {
            return @$"
    #region CollectionCard {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    Gold Card: {this.GoldCard.Display()}
    Platinum Card: {this.PlatCard.Display()}
    
    #endregion CollectionCard {this.Id}
";
        }
    }

    public class Card
    {
        public int ObjectCount { get; set; }
        public byte Obtained { get; set; }
        public byte Selected { get; set; }

        public Card Process(FileStream saveDataReader)
        {
            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Obtained Name
            var obtainedName = saveDataReader.GetStringFromFileStream(160);

            // Get Obtained
            this.Obtained = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Selected Name
            var selectedName = saveDataReader.GetStringFromFileStream(160);

            // Get Selected
            this.Selected = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }


        public string Display()
        {
            return @$"
        Obtained: {this.Obtained}
        Selected: {this.Selected}
";
        }
    }
}