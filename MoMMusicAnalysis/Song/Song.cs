using System;
using System.Collections.Generic;
using System.IO;

namespace MoMMusicAnalysis
{
    public class Song<TNote, TPerformerLane> : ISong where TPerformerLane : Enum
    {
        public string Name { get; set; }
        public Difficulty Difficulty { get; set; }
        public int Length { get; set; }
        public SongType SongType { get; set; }
        public int Unk1 { get; set; } // Maybe Count Section?
        public int Unk2 { get; set; }
        public int Unk3 { get; set; }
        public int Unk4 { get; set; }
        public int NoteCount { get; set; }
        public int PerformerCount { get; set; }
        public int TimeShiftCount { get; set; }

        public List<TNote> Notes = new List<TNote>();
        public List<PerformerNote<TPerformerLane>> PerformerNotes = new List<PerformerNote<TPerformerLane>>();
        public List<TimeShift> TimeShifts = new List<TimeShift>();

        public ISong ProcessSong(FileStream musicReader)
        {
            return null;
            throw new System.NotImplementedException();

        }

        public List<byte> RecompileSong()
        {
            throw new System.NotImplementedException();
        }
    }
}