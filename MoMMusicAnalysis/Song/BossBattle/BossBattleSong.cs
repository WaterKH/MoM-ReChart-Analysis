using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis
{
    public class BossBattleSong : Song<BossNote, BossLane>
    {
        public int DarkZoneCount { get; set; }
        public int Unk5 { get; set; }
        public bool HasEmptyData { get; set; }

        public List<BossDarkZone> DarkZones = new List<BossDarkZone>();

        public BossBattleSong(Difficulty difficulty, int length, SongType songType)
        {
            this.Difficulty = difficulty;
            this.Length = length;
            this.SongType = songType;
        }

        public new BossBattleSong ProcessSong(FileStream musicReader)
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

            // Get Dark Zone Count
            this.DarkZoneCount = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            for (int i = 0; i < this.NoteCount; ++i)
            {
                var bossNote = new BossNote();

                bossNote.ProcessNote(musicReader);

                this.Notes.Add(bossNote);
            }

            for (int i = 0; i < this.PerformerCount; ++i)
            {
                var performerNote = new PerformerNote<BossLane>();

                performerNote.ProcessNote(musicReader);

                this.PerformerNotes.Add(performerNote);
            }

            for (int i = 0; i < this.TimeShiftCount; ++i)
            {
                var timeShift = new TimeShift<BossLane>();

                timeShift.ProcessTimeShift(musicReader);

                this.TimeShifts.Add(timeShift);
            }

            for (int i = 0; i < this.DarkZoneCount; ++i)
            {
                var darkZone = new BossDarkZone();

                darkZone.ProcessDarkZone(musicReader);

                this.DarkZones.Add(darkZone);
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
            data.AddRange(Encoding.UTF8.GetBytes($"trigger_bos_0{diff}01"));

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
            data.AddRange(BitConverter.GetBytes(this.DarkZoneCount));

            // Recompile All Memory Notes
            foreach (var note in this.Notes.OrderBy(x => x.HitTime))
                data.AddRange(note.RecompileNote());

            // Recompile All Performer Notes
            foreach (var note in this.PerformerNotes)
                data.AddRange(note.RecompileNote());

            // Recompile Time Shifts
            foreach (var timeShift in this.TimeShifts)
                data.AddRange(timeShift.RecompileTimeShift());

            // Recompile Dark Zones
            foreach (var darkZone in this.DarkZones)
                data.AddRange(darkZone.RecompileDarkZone());

            if (this.HasEmptyData)
                data.AddRange(BitConverter.GetBytes(0));

            return data;
        }

        // TODO Move string construction to subclasses
        public void WriteToFile(string destination)
        {
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
    Dark Zone Count: {this.DarkZoneCount}
    Has Empty Data At End: {this.HasEmptyData}
            ";

            var notes = "";
            int i = 1;
            foreach (var note in this.Notes)
            {
                var noteString = @$"
    #region Boss Note {i}
    
    Note Type: {note.BossNoteType}
    Hit Time: {note.HitTime} ({note.HitTime / 1000.0})
    Lane: {note.Lane}
    Aerial Flag: {note.AerialFlag}
    Swipe Direction: {note.SwipeDirection} (degrees)
    Start Hold Note: {note.StartHoldNote}
    End Hole Note: {note.EndHoldNote}
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

            var darkZones = "";
            i = 1;
            foreach (var darkZone in this.DarkZones)
            {

                var darkZoneStr = @$"
    #region Dark Zone {i}
    
    Start Time (For Notes): {darkZone.HitTime} ({darkZone.HitTime / 1000.0})
    End Time (For Notes - Start Time For Attack): {darkZone.EndTime} ({darkZone.EndTime / 1000.0})
    End Attack Time (For Attack): {darkZone.EndTime} ({darkZone.EndAttackTime / 1000.0})
    Empty Data: {darkZone.EmptyData}

    #endregion
";

                darkZones += darkZoneStr;
                ++i;
            }

            var formattedString = header + notes + performerNotes + timeShifts + darkZones;

            File.AppendAllText($"{destination}-{this.Name}.cs", formattedString);
        }
    }
}