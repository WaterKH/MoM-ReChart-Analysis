using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class ItemMixInfo
    {
        public int ObjectCount { get; set; }
        public List<byte> MoogleExp { get; set; }
        public List<byte> UseItemCount { get; set; }
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
        public string Version { get; set; }

        public ItemMixInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);
            
            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Moogle Exp Name
            var moggleExpName = saveDataReader.GetStringFromFileStream(160);

            // Get Moogle Exp Value
            this.MoogleExp = saveDataReader.FindDataFromFileStream();

            // Get Use Item Count Name
            var useItemCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Use Item Count Value
            this.UseItemCount = saveDataReader.FindDataFromFileStream();

            // Get Recipe Name
            var recipeName = saveDataReader.GetStringFromFileStream(160);

            // Header Identifier
            saveDataReader.ReadBytesFromFileStream(2);

            // Get Recipe Count
            var recipeCount = saveDataReader.ReadByte();

            // Get Recipes
            for (int i = 0; i < recipeCount; ++i)
            {
                this.Recipes.Add(new Recipe().Process(saveDataReader));
            }

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            var recipeString = "";
            this.Recipes.ForEach(x => recipeString += $"\n{x.Display()}");

            return @$"
    #region ItemMixInfo

    Object Count: {this.ObjectCount}
    Moogle Exp: {this.MoogleExp.Display()}
    Use Item Count: {this.UseItemCount.Display()}
    Recipes Count: {this.Recipes.Count}
    
    #region Recipes
    
    Recipes: {recipeString}

    #endregion Recipes

    Version: {this.Version}
    
    #endregion ItemMixInfo
";
        }
    }

    public class Recipe
    {
        public int Id { get; set; } // 0C 84 58 8x?
        public int ObjectCount { get; set; }
        public List<byte> TotalCreationNumber { get; set; }
        public byte HasConfirmed { get; set; }

        public Recipe Process(FileStream saveDataReader)
        {
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Total Creation Number Name
            var totalCreationNumberName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Creation Number Value
            this.TotalCreationNumber = saveDataReader.FindDataFromFileStream();

            // Get Has Confirmed Name
            var hasConfirmedName = saveDataReader.GetStringFromFileStream(160);

            // Get Has Confirmed Value
            this.HasConfirmed = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }

        public string Display()
        {
            return @$"
    #region Recipe {this.Id}

    ID: {this.Id}
    Object Count: {this.ObjectCount}
    Total Creation Number: {this.TotalCreationNumber.Display()}
    Has Confirmed: {this.HasConfirmed}
    
    #endregion Recipe {this.Id}
";
        }
    }
}