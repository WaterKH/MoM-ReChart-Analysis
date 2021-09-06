using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MoMMusicAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            //var dataPath = @"D:\EpicGames\KH_MoM\KINGDOM HEARTS Melody of Memory_Data\StreamingAssets\Data\SaveDatabase";
            //var passwordPath = @"D:\EpicGames\KH_MoM\KINGDOM HEARTS Melody of Memory_Data\StreamingAssets\Data\";

            //new SaveProcessor().ProcessSaveData(dataPath, true);
            //new SaveProcessor().Test();

            using var musicReader = File.OpenRead("commonlanguages");
            
            var header = new TextAssetHeader().ProcessHeader(musicReader);

            musicReader.ReadBytesFromFileStream(0x138);

            //foreach(var section in header.Sections.OrderBy(x => x.EmptyData))
            //{  
            //}
            
            List<TextAsset> textAssets = new List<TextAsset>();

            try
            {
                while (true)
                {
                    textAssets.Add(new TextAsset().Process(musicReader));
                }
            }
            catch (Exception e)
            {

            }

            if (File.Exists("commonlanguage.cs"))
                File.Delete("commonlanguage.cs");

            foreach (var asset in textAssets)
            {
                asset.WriteToFile("commonlanguage");
            }
        }

        public static void FileAnalysis()
        {
            var files = Directory.GetFiles("decompressed_music");
            var songProcessor = new SongProcessor();

            foreach (var file in files)
            {
                var str = "";
                //Console.WriteLine(file);
                var temp = songProcessor.ProcessSong(file);

                if (temp == null) continue;

                foreach (var (pos, song) in temp.SongPositions)
                {
                    try
                    {
                        var fbSong = ((FieldBattleSong)song).TimeShifts;
                        fbSong.ForEach(x => str += x.Speed + " ");
                        continue;
                    }
                    catch (Exception)
                    {

                    }

                    try
                    {
                        var mdSong = ((MemoryDiveSong)song).TimeShifts;
                        mdSong.ForEach(x => str += x.Speed + " ");
                        continue;
                    }
                    catch (Exception)
                    {

                    }

                    try
                    {
                        var bbSong = ((BossBattleSong)song).TimeShifts;
                        bbSong.ForEach(x => str += x.Speed + " ");

                        continue;
                    }
                    catch (Exception)
                    {

                    }
                }

                if (!string.IsNullOrEmpty(str))
                {
                    str = $"{file}\n{str}\n";
                    File.AppendAllText("test.txt", str);
                    str = "";
                }
            }


            //var musicSource = "new_music";
            //var musicDestination = "decompressed_music";

            var fileToAnalyze = "094-The Encounter.decomp";

            var musicFile = songProcessor.ProcessSong(fileToAnalyze, true);

            var test = musicFile.RecompileMusicFile();
            File.WriteAllBytes($"{fileToAnalyze}_test", test.ToArray());
        }


        public static void NameFiles(string songListSource, string musicSource, string musicDestination)
        {
            using var reader = new StreamReader(songListSource);

            var line = "";
            while ((line = reader.ReadLine()) != null)
            {
                var musicInfo = line.Split(" - ")[^1].Split(" (MUSIC/");

                var musicName = musicInfo[0..^1];
                var musicSheet = musicInfo[1].ToLower()[0..^1];

                using var musicReader = File.OpenRead($"{musicSource}/{musicSheet}");

                var data = new List<byte>();
                int b;
                while ((b = musicReader.ReadByte()) != -1)
                    data.Add((byte)b);

                File.WriteAllBytes($"{musicDestination}/{line.Split(" - ")[0]}-{string.Join(" - ", musicName)}", data.ToArray());
            }
        }
    }
}