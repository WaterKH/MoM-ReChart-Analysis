using System.Collections.Generic;
using System.IO;

namespace MoMMusicAnalysis
{
    public interface ISong
    {
        public string Name { get; set; }
        public Difficulty Difficulty { get; set; }
        public int Length { get; set; }
        public SongType SongType { get; set; }
        public int Unk1 { get; set; }
        public int Unk2 { get; set; }
        public int Unk3 { get; set; }
        public int Unk4 { get; set; }
        public int NoteCount { get; set; }
        public int PerformerCount { get; set; }


        public ISong ProcessSong(FileStream musicReader);

        public List<byte> RecompileSong();
    }
}