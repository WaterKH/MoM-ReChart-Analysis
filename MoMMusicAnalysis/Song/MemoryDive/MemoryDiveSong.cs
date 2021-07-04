using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis
{
    public class MemoryDiveSong : Song<MemoryNote, MemoryLane>
    {
        public int Unk5 { get; set; }
        public bool HasEmptyData { get; set; }


        public MemoryDiveSong(Difficulty difficulty, int length, SongType songType)
        {
            this. Difficulty = difficulty;
            this.Length = length;
            this.SongType = songType;
        }

        public new MemoryDiveSong ProcessSong(FileStream musicReader)
        {
            // Get Unk1
            this.Unk1 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk2
            this.Unk2 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk3
            this.Unk3 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk4
            this.Unk4 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Note Count
            this.NoteCount = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk5 - Asset Count?
            this.Unk5 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Performer Count
            this.PerformerCount = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Time Shift Count
            this.TimeShiftCount = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            for (int i = 0; i < this.NoteCount; ++i)
            {
                var memoryNote = new MemoryNote();

                memoryNote.ProcessNote(musicReader);

                this.Notes.Add(memoryNote);
            }

            for (int i = 0; i < this.PerformerCount; ++i)
            {
                var performerNote = new PerformerNote<MemoryLane>();

                performerNote.ProcessNote(musicReader);

                this.PerformerNotes.Add(performerNote);
            }


            for (int i = 0; i < this.TimeShiftCount; ++i)
            {
                var timeShift = new TimeShift<MemoryLane>();

                timeShift.ProcessTimeShift(musicReader);

                this.TimeShifts.Add(timeShift);
            }

            if (musicReader.Length != musicReader.Position)
            {
                if (BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4)?.ToArray()) == 0)
                {
                    this.HasEmptyData = true;
                }
                else
                {
                    musicReader.Position -= 4;
                }
            }

            return this;
        }

        public new List<byte> RecompileSong()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(0x10));

            // Recompile Header
            var diff = (this.Difficulty == Difficulty.Beginner) ? 1 : (this.Difficulty == Difficulty.Standard) ? 2 : 3;
            data.AddRange(Encoding.UTF8.GetBytes($"trigger_evt_0{diff}01"));

            data.AddRange(BitConverter.GetBytes(this.Length));
            data.AddRange(BitConverter.GetBytes((int)this.SongType));
            data.AddRange(BitConverter.GetBytes(this.Unk1));
            data.AddRange(BitConverter.GetBytes(this.Unk2));
            data.AddRange(BitConverter.GetBytes(this.Unk3));
            data.AddRange(BitConverter.GetBytes(this.Unk4));
            data.AddRange(BitConverter.GetBytes(this.NoteCount));
            data.AddRange(BitConverter.GetBytes(this.Unk5));
            data.AddRange(BitConverter.GetBytes(this.PerformerCount));
            data.AddRange(BitConverter.GetBytes(this.TimeShiftCount));

            // Recompile All Memory Notes
            foreach (var note in this.Notes.OrderBy(x => x.HitTime))
                data.AddRange(note.RecompileNote());

            // Recompile All Performer Notes
            foreach (var note in this.PerformerNotes)
                data.AddRange(note.RecompileNote());

            // Recompile Time Shifts
            foreach (var timeShift in this.TimeShifts)
                data.AddRange(timeShift.RecompileTimeShift());

            if (this.HasEmptyData)
                data.AddRange(BitConverter.GetBytes(0));

            return data;
        }

        // TODO Move string construction to subclasses
        public void WriteToFile(string destination)
        {
            if (File.Exists($"{destination}-{this.Name.Split('\\')[^1]}.cs"))
                File.Delete($"{destination}-{this.Name.Split('\\')[^1]}.cs");


            var header = @$"
    Name: {this.Name}
    Difficulty: {this.Difficulty}
    Length: {this.Length} (in bytes)
    Song Type: {this.SongType}
    Unk: {this.Unk1} - {this.Unk2} - {this.Unk3} - {this.Unk4}
    Note Count: {this.NoteCount}
    Unk Count: {this.Unk5}
    Performer Count: {this.PerformerCount}
    Time Shift Count: {this.TimeShiftCount}
    Has Empty Data At End: {this.HasEmptyData}
            ";

            var notes = "";
            int i = 1;
            foreach (var note in this.Notes)
            {
                var noteString = @$"
    #region Memory Note {i}
    
    Note Type: {note.MemoryNoteType}
    Hit Time: {note.HitTime} ({note.HitTime / 1000.0})
    Lane: {note.Lane}
    Aerial Flag: {note.AerialFlag}
    Swipe Direction: {note.SwipeDirection}
    Start Hold Note: {note.StartHoldNoteIndex}
    End Hold Note: {note.EndHoldNoteIndex}
    Unk FF: {note.UnkFF}
    Unk1: {note.Unk1}
    Unk2: {note.Unk2}
    Unk3: {note.Unk3}
    Unk4: {note.Unk4}
    Unk5: {note.Unk5}
    Unk6: {note.Unk6}
    Unk7: {note.Unk7}
    Unk8: {note.Unk8}

    #endregion
                ";

                notes += noteString;
                ++i;
            }

            var performerNotes = "";
            i = 1;
            foreach (var note in this.PerformerNotes)
            {
                var fieldAssetString = @$"
    #region Performer Notes {i}
    
    Performer Note Type: {note.PerformerType}
    Hit Time: {note.HitTime} ({note.HitTime / 1000.0})
    Lane: {note.Lane}
    Unk1: {note.Unk1}
    Unk2: {note.Unk2}
    Unk3: {note.Unk3}
    Unk4: {note.Unk4}
    Unk5: {note.Unk5}
    Duplicate Type: {note.DuplicateType}
    Unk7: {note.Unk7}
    Unk8: {note.Unk8}

    #endregion
                ";

                performerNotes += fieldAssetString;
                ++i;
            }


            var timeShifts = "";
            i = 1;
            foreach (var timeShift in this.TimeShifts)
            {

                var timeStr = @$"
    #region Time Shift {i}
    
    Change Time: {timeShift.HitTime}
    Speed: {timeShift.Speed}

    #endregion
";

                timeShifts += timeStr;
                ++i;
            }

            var formattedString = header + notes + performerNotes + timeShifts;

            File.AppendAllText($"{destination}-{this.Name.Split('\\')[^1]}.cs", formattedString);
        }
    }
}