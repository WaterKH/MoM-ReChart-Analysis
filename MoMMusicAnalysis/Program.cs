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
            var files = new List<string>();//Directory.GetFiles("decompressed_music");
            var songProcessor = new SongProcessor();

            foreach (var file in files)
            {
                var str = "";
                //Console.WriteLine(file);
                var temp = songProcessor.ProcessSong(file);

                if (temp == null) continue;

                foreach(var (pos, song) in temp.SongPositions)
                {
                    var fbSong = ((FieldBattleSong)song);
                    
                    var notes = fbSong.Notes;
                    foreach(var note in notes)
                    {
                        if (note.Unk1 != 0) str += $"Unk1: {note.Unk1} - ";
                        if (note.Unk2 != 0) str += $"Unk2: {note.Unk2} - ";
                        if (note.Unk3 != 0) str += $"Unk3: {note.Unk3} - ";
                        if (note.Unk4 != 0) str += $"Unk4: {note.Unk4} - ";
                        if (note.Unk5 != 0) str += $"Unk5: {note.Unk5} - ";
                        if (note.Unk6 != 0) str += $"Unk6: {note.Unk6} - ";
                    }

                    var enemies = fbSong.FieldAnimations;
                    foreach (var enemy in enemies)
                    {
                        if (enemy.Unk1 != 0) str += $"Unk1: {enemy.Unk1} - ";
                        if (enemy.Unk2 != 0) str += $"Unk2: {enemy.Unk2} - ";
                        if (enemy.Unk3 != 0) str += $"Unk3: {enemy.Unk3} - ";
                        if (enemy.Unk4 != 0) str += $"Unk4: {enemy.Unk4} - ";
                        if (enemy.Unk5 != 0) str += $"Unk5: {enemy.Unk5} - ";
                        if (enemy.Unk6 != 0) str += $"Unk6: {enemy.Unk6} - ";
                        if (enemy.Unk7 != 0) str += $"Unk7: {enemy.Unk7} - ";
                        if (enemy.Unk8 != 0) str += $"Unk8: {enemy.Unk8} - ";
                    }

                    var assets = fbSong.FieldAssets;
                    foreach (var asset in assets)
                    {
                        if (asset.Unk1 != 0) str += $"Unk1: {asset.Unk1} - ";
                        if (asset.Unk2 != 0) str += $"Unk2: {asset.Unk2} - ";
                        if (asset.Unk3 != 0) str += $"Unk3: {asset.Unk3} - ";
                        if (asset.Unk4 != 0) str += $"Unk4: {asset.Unk4} - ";
                        if (asset.Unk5 != 0) str += $"Unk5: {asset.Unk5} - ";
                        if (asset.Unk6 != 0) str += $"Unk6: {asset.Unk6} - ";
                        if (asset.Unk7 != 0) str += $"Unk7: {asset.Unk7} - ";
                        if (asset.Unk8 != 0) str += $"Unk8: {asset.Unk8} - ";

                        if (asset.FF1 != -1) str += $"FF1: {asset.FF1} - ";
                        if (asset.FF2 != -1) str += $"FF2: {asset.FF2} - ";
                        if (asset.FF3 != -1) str += $"FF3: {asset.FF3} - ";
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
