using System;
using System.Collections.Generic;
using System.Text;

namespace MoMMusicAnalysis
{
    public class MusicFile
    {
        public string FileName { get; set; }
        public Header Header { get; set; }

        public Dictionary<int, ISong> SongPositions { get; set; } = new Dictionary<int, ISong>();
        public Dictionary<int, List<byte>> AssetPositions { get; set; } = new Dictionary<int, List<byte>>();

        public bool AssetHasEmptyData { get; set; }


        public List<byte> RecompileMusicFile(MusicFile originalMusicFile = null)
        {
            var data = new List<byte>();

            if (originalMusicFile != null)
            {
                this.Header = originalMusicFile.Header;
                //this.AssetPositions = 
            }

            data.AddRange(this.Header.RecompileHeader());

            for (int i = 0; i < 4; ++i)
            {
                if (this.AssetPositions.ContainsKey(i))
                {
                    data.AddRange(BitConverter.GetBytes(0x2E));

                    data.AddRange(this.AssetPositions[i]);

                    if (this.AssetHasEmptyData)
                        data.AddRange(BitConverter.GetBytes(0));
                }
                else if (this.SongPositions.ContainsKey(i))
                {
                    var song = this.SongPositions[i];
                    switch (song.SongType)
                    {
                        case SongType.FieldBattle:
                            data.AddRange(((FieldBattleSong)song).RecompileSong());

                            break;
                        case SongType.BossBattle:
                            data.AddRange(((BossBattleSong)song).RecompileSong());
                            
                            break;
                        case SongType.MemoryDive:
                            data.AddRange(((MemoryDiveSong)song).RecompileSong());
                            
                            break;
                        default:
                            break;
                    }
                }
            }

            return data;
        }
    }
}