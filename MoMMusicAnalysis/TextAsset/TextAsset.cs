using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MoMMusicAnalysis
{
    public class TextAsset
    {
        public int NameLength { get; set; }
        public string Name { get; set; }
        public List<byte> Unk1 { get; set; } = new List<byte>(); // 0x09 Length bytes?
        public int Unk2 { get; set; } // 0x0A

        public List<string> Texts = new List<string>
        {
            "Japanese", "English", "French", "Italian", "German",
            "Spanish", "Arabic", "ChineseTrad", "ChineseSimple", "Korean"
        };

        public Dictionary<string, string> ReadTexts = new Dictionary<string, string>();

        public int Unk3 { get; set; } // 0x0A
        public List<byte> Unk4 { get; set; } // 0x10 bytes?

        public TextAsset Process(FileStream musicReader)
        {
            // Get Name Length
            this.NameLength = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Name
            this.Name = Encoding.UTF8.GetString(musicReader.ReadBytesFromFileStream(this.NameLength).ToArray());

            // Get Unk1
            byte x = 0x00;
            while (x != 0x0A)
            {
                x = musicReader.ReadBytesFromFileStream(1)[0];
                this.Unk1.Add(x);
            }
            musicReader.Position -= 1;

            // Get Unk2
            this.Unk2 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            foreach (var text in this.Texts)
            {
                var length = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

                if (length > 2000)
                    return null;

                if (length % 4 != 0)
                {
                    length = length + (4 - (length % 4));
                }

                this.ReadTexts.Add(text, Encoding.UTF8.GetString(musicReader.ReadBytesFromFileStream(length).ToArray()));
            }
            
            // Get Unk3
            this.Unk3 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk4
            this.Unk4 = musicReader.ReadBytesFromFileStream(0x10);

            return this;
        }

        public void WriteToFile(string destination)
        {
            //        var unk1Str = "";
            //        this.Unk1.ForEach(x => unk1Str += x);

            //        var unk4Str = "";
            //        this.Unk4.ForEach(x => unk4Str += x);

            //        var textAsset = @$"
            //#region {this.Name}

            //Name: {this.Name}
            //Unk1: {unk1Str}
            //Unk2: {this.Unk2}

            //Japanese: {this.ReadTexts["Japanese"]}
            //English: {this.ReadTexts["English"]}
            //French: {this.ReadTexts["French"]}
            //Italian: {this.ReadTexts["Italian"]}
            //German: {this.ReadTexts["German"]}
            //Spanish: {this.ReadTexts["Spanish"]}
            //Arabic: {this.ReadTexts["Arabic"]}
            //Chinese (Traditional): {this.ReadTexts["ChineseTrad"]}
            //Chinese (Simplified): {this.ReadTexts["ChineseSimple"]}
            //Korean: {this.ReadTexts["Korean"]}

            //Unk3: {this.Unk3}
            //Unk4: {unk4Str}

            //#endregion {this.Name}


            //        ";

            var textAsset = this.Name + "\n";

            File.AppendAllText($"{destination}.cs", textAsset);
        }
    }
}