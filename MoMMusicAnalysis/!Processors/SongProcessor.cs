using SevenZip.Compression.LZMA;
using System;
using System.Collections.Generic;
using System.IO;

namespace MoMMusicAnalysis
{
    public class SongProcessor
    {
        // TODO Move this up a layer - Want to process the file so we can package it back up and send it back
        public MusicFile ProcessSong(string fileName, bool debug = false)
        {
            if (File.Exists($"TEST-{fileName.Split('\\')[^1]}.cs"))
                File.Delete($"TEST-{fileName.Split('\\')[^1]}.cs");

            var newFileName = $"{fileName}.decomp";
            if (!File.Exists(newFileName))
            {
                try
                {
                    using var initialMusicReader = File.OpenRead(fileName);
                    using var tempReader = Decompress(initialMusicReader);

                    tempReader.WriteTo(File.OpenWrite(newFileName));
                }
                catch (Exception e)
                {

                }
            }

            using var musicReader = File.OpenRead(newFileName);

            var musicFile = new MusicFile
            {
                FileName = fileName,
            };

            musicFile.Header = new Header().ProcessHeader(musicReader);

            var songs = new Dictionary<int, ISong>();
            var assetPositions = new Dictionary<int, List<byte>>();
            // For all 3 difficulties (and the 1 asset list), generate a song for each
            for (int i = 0; i < musicFile.Header.Sections.Length; i++)
            {
                // TODO Fix this to make it less gross
                var length = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

                //if(length == 0)
                //    length = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

                if (length == 46 || songs.Count == 3)
                {
                    if(songs.Count == 3)
                        length = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

                    assetPositions.Add(i, musicReader.ReadBytesFromFileStream(0x250)); // TODO Process asset list

                    if (musicReader.Length != musicReader.Position)
                    {
                        if (BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray()) == 0)
                        {
                            musicFile.AssetHasEmptyData = true;
                        }
                        else
                        {
                            musicReader.Position -= 4;
                        }
                    }

                    continue;
                }

                var song = GetSongInformation(musicReader);
                song.Name = fileName;

                switch (song.SongType)
                {
                    case SongType.FieldBattle:
                        ((FieldBattleSong)song).ProcessSong(musicReader);

                        if (debug)
                            ((FieldBattleSong)song).WriteToFile("TEST");

                        songs.Add(i, (FieldBattleSong)song);
                        break;
                    case SongType.BossBattle:
                        ((BossBattleSong)song).ProcessSong(musicReader);

                        if (debug)
                            ((BossBattleSong)song).WriteToFile("TEST");

                        songs.Add(i, (BossBattleSong)song);
                        break;
                    case SongType.MemoryDive:
                        ((MemoryDiveSong)song).ProcessSong(musicReader);
                        
                        if (debug)
                            ((MemoryDiveSong)song).WriteToFile("TEST");

                        songs.Add(i, (MemoryDiveSong)song);
                        break;
                    default:
                        break;
                }

                if (songs.Count != 3)
                {
                    var temp = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

                    if (temp != 0)
                        musicReader.Position -= 4;
                }
            }

            musicFile.AssetPositions = assetPositions;
            musicFile.SongPositions = songs;

            return musicFile;
        }

        public ISong GetSongInformation(FileStream musicReader)
        {
            var difficulty = (Difficulty)musicReader.ReadBytesFromFileStream(16)[13];
            var length = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());
            var songType = (SongType)BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            return songType switch
            {
                SongType.FieldBattle => new FieldBattleSong(difficulty, length, songType),
                SongType.BossBattle => new BossBattleSong(difficulty, length, songType),
                SongType.MemoryDive => new MemoryDiveSong(difficulty, length, songType),
                _ => null,
            };
        }

        public static MemoryStream Decompress(FileStream inStream)
        {
            var decoder = new Decoder();

            var properties = new byte[5];
            if (inStream.Read(properties, 0, 5) != 5)
                throw (new Exception("input .lzma is too short"));
            decoder.SetDecoderProperties(properties);

            long outSize = 0;
            for (var i = 0; i < 8; i++)
            {
                var v = inStream.ReadByte();
                if (v < 0)
                    break;
                outSize |= ((long)(byte)v) << (8 * i);
            }
            var compressedSize = inStream.Length - inStream.Position;

            var outStream = new MemoryStream();
            decoder.Code(inStream, outStream, compressedSize, outSize, null);
            outStream.Flush();
            outStream.Position = 0;
            return outStream;
        }
    }
}