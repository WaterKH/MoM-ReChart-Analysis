using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis
{
    public class FieldBattleSong : Song<FieldNote, FieldLane>
    {
        public int AnimationCount { get; set; }
        public int AssetCount { get; set; }

        public List<FieldAnimation> FieldAnimations { get; set; } = new List<FieldAnimation>();
        public List<FieldAsset> FieldAssets { get; set; } = new List<FieldAsset>();
        //public List<PerformerNote<FieldLane>> PerformerNotes { get; set; } = new List<PerformerNote<FieldLane>>();
        //public List<TimeShift> TimeShifts { get; set; } = new List<TimeShift>();

        public bool HasEmptyData { get; set; }

        public FieldBattleSong(Difficulty difficulty, int length, SongType songType)
        {
            this.Difficulty = difficulty;
            this.Length = length;
            this.SongType = songType;
        }

        public new FieldBattleSong ProcessSong(FileStream musicReader)
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

            // Get Animation Count
            this.AnimationCount = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Asset Count
            this.AssetCount = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Performer Count
            this.PerformerCount = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Time Shift Count
            this.TimeShiftCount = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            for (int i = 0; i < this.NoteCount; ++i)
            {
                var fieldNote = new FieldNote();
                
                fieldNote.ProcessNote(musicReader);

                this.Notes.Add(fieldNote);
            }

            for (int i = 0; i < this.AnimationCount; ++i)
            {
                var fieldEnemy = new FieldAnimation();

                fieldEnemy.ProcessNote(musicReader);
                
                this.FieldAnimations.Add(fieldEnemy);
            }

            for (int i = 0; i < this.AssetCount; ++i)
            {
                var fieldAsset = new FieldAsset();

                fieldAsset.ProcessNote(musicReader);

                this.FieldAssets.Add(fieldAsset);
            }

            for (int i = 0; i < this.PerformerCount; ++i)
            {
                var performerNote = new PerformerNote<FieldLane>();

                performerNote.ProcessNote(musicReader);

                this.PerformerNotes.Add(performerNote);
            }


            for (int i = 0; i < this.TimeShiftCount; ++i)
            {
                var timeShift = new TimeShift<FieldLane>();

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
            
            // Pair Notes and Assets to Animations
            for (int i = 0; i < this.Notes.Count; ++i)
            {
                var anim = this.FieldAnimations[this.Notes[i].AnimationReference];
                if (anim.Previous == -1)
                {
                    if (anim.Next != -1)
                        this.Notes[i].Animations.AddRange(this.RetrieveAnimations(anim));
                    else
                        this.Notes[i].Animations.Add(anim);
                }
            }

            for (int i = 0; i < this.FieldAssets.Count; ++i)
            {
                var anim = this.FieldAnimations[this.FieldAssets[i].AnimationReference];
                if (anim.Previous == -1)
                {
                    if (anim.Next != -1)
                        this.FieldAssets[i].Animations.AddRange(this.RetrieveAnimations(anim));
                    else
                        this.FieldAssets[i].Animations.Add(anim);
                }
            }

            //this.FieldAnimations.Clear(); // We don't need the animations list anymore since they are paired with the notes and assets right?

            //var count = this.FieldNotes.Select(x => x.Animations.Count).Sum() + this.FieldAssets.Select(x => x.Animations.Count).Sum();

            //if (this.FieldAnimations.Count != count)
            //    Console.WriteLine($"Counts mismatched: {this.FieldAnimations.Count} - {count}");

            return this;
        }
        
        private List<FieldAnimation> RetrieveAnimations(FieldAnimation fieldAnimation)
        {
            var anims = new List<FieldAnimation> { fieldAnimation };
            if (fieldAnimation.Next == -1)
                return anims;

            anims.AddRange(RetrieveAnimations(this.FieldAnimations[fieldAnimation.Next]));

            return anims;
        }

        public new List<byte> RecompileSong()
        {
            var data = new List<byte>();

            var anims = this.Notes.SelectMany(x => x.Animations).Concat(this.FieldAssets.SelectMany(x => x.Animations));

            data.AddRange(BitConverter.GetBytes(0x10));

            // Recompile Header
            var diff = (this.Difficulty == Difficulty.Beginner) ? 1 : (this.Difficulty == Difficulty.Standard) ? 2 : 3;
            data.AddRange(Encoding.UTF8.GetBytes($"trigger_std_0{diff}01"));

            data.AddRange(BitConverter.GetBytes(this.Length));
            data.AddRange(BitConverter.GetBytes((int)this.SongType));
            data.AddRange(BitConverter.GetBytes(this.Unk1));
            data.AddRange(BitConverter.GetBytes(this.Unk2));
            data.AddRange(BitConverter.GetBytes(this.Unk3));
            data.AddRange(BitConverter.GetBytes(this.Unk4));
            data.AddRange(BitConverter.GetBytes(this.Notes.Count));
            data.AddRange(BitConverter.GetBytes(anims.ToList().Count));
            data.AddRange(BitConverter.GetBytes(this.FieldAssets.Count));
            data.AddRange(BitConverter.GetBytes(this.PerformerNotes.Count));
            data.AddRange(BitConverter.GetBytes(this.TimeShifts.Count));

            // Recompile All Field Notes
            foreach(var note in this.Notes.OrderBy(x => x.HitTime))
                data.AddRange(note.RecompileNote());

            // Recompile All Field Animations
            foreach (var anim in anims.OrderBy(x => x.AnimationEndTime).ThenBy(x => x.AnimationStartTime))
                data.AddRange(anim.RecompileNote());

            // Recompile All Field Assets
            foreach (var note in this.FieldAssets.OrderBy(x => x.HitTime))
                data.AddRange(note.RecompileNote());

            // Recompile All Performer Notes
            foreach (var note in this.PerformerNotes)
                data.AddRange(note.RecompileNote());

            //var anims = .Concat(this.FieldAssets.SelectMany(x => x.Animations));

            // Recompile All Field Animations (Notes)
            //foreach (var anim in this.FieldNotes.SelectMany(x => x.Animations).OrderBy(x => x.AnimationEndTime))
            //    data.AddRange(anim.RecompileNote());

            //// Recompile All Field Animations (Assets)
            //foreach (var anim in this.FieldAssets.SelectMany(x => x.Animations).OrderBy(x => x.AnimationEndTime))
            //    data.AddRange(anim.RecompileNote());

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
            var header = @$"
    Name: {this.Name}
    Difficulty: {this.Difficulty}
    Length: {this.Length} (in bytes)
    Song Type: {this.SongType}
    Unk: {this.Unk1} - {this.Unk2} - {this.Unk3} - {this.Unk4}
    Note Count: {this.NoteCount}
    Animation Count: {this.AnimationCount}
    Asset Count: {this.AssetCount}
    Performer Count: {this.PerformerCount}
    Time Shift Count: {this.TimeShiftCount}
    Has Empty Data At End: {this.HasEmptyData}
            ";

            var fieldNotes = "";
            int i = 1;
            foreach(var fieldNote in this.Notes)
            {
                // Get anims
                var fieldAnims = "";
                int j = 1;
                foreach (var fieldAnim in fieldNote.Animations)
                {
                    var fieldAnimString = @$"
    #region Field Animation {j} For Field Note {i}
    
    Note Type: {fieldAnim.NoteType}
    Animation End Time: {fieldAnim.AnimationEndTime} ({fieldAnim.AnimationEndTime / 1000.0})
    Lane: {fieldAnim.Lane}
    Animation Start Time: {fieldAnim.AnimationStartTime} ({fieldAnim.AnimationStartTime / 1000.0})
    Aerial Flag: {fieldAnim.AerialFlag}
    Previous (Potential End): {fieldAnim.Previous}
    Next (Potential Beginning): {fieldAnim.Next}
    Unk1: {fieldAnim.Unk1}
    Unk2: {fieldAnim.Unk2}
    Unk3: {fieldAnim.Unk3}
    Unk4: {fieldAnim.Unk4}
    Unk5: {fieldAnim.Unk5}
    Unk6: {fieldAnim.Unk6}
    Unk7: {fieldAnim.Unk7}
    Unk8: {fieldAnim.Unk8}

    #endregion
                ";

                    fieldAnims += fieldAnimString;
                    ++j;
                }


                var fieldNoteString = @$"
    #region Field Note {i}
    
    Note Type: {fieldNote.NoteType}
    Hit Time: {fieldNote.HitTime} ({fieldNote.HitTime / 1000.0})
    Lane: {fieldNote.Lane}
    Aerial Flag: {fieldNote.AerialFlag}
    Animation Reference: {fieldNote.AnimationReference}
    Projectile Origin Note: {fieldNote.ProjectileOriginNoteIndex}
    Previous Enemy Note: {fieldNote.PreviousEnemyNoteIndex}
    Next Enemy Note: {fieldNote.NextEnemyNoteIndex}
    Aerial And Crystal Counter: {fieldNote.AerialAndCrystalCounter}
    Model Type: {fieldNote.ModelType}
    Star Flag: {fieldNote.StarFlag}
    Party Flag: {fieldNote.PartyFlag}
    Unk1: {fieldNote.Unk1}
    Unk2: {fieldNote.Unk2}
    Unk3: {fieldNote.Unk3} (Model Alt)
    Unk4: {fieldNote.Unk4}
    Unk5: {fieldNote.Unk5}
    Unk6: {fieldNote.Unk6}

    {fieldAnims}

    #endregion
                ";

                fieldNotes += fieldNoteString;
                ++i;
            }

            var fieldAssets = "";
            i = 1;
            foreach (var fieldAsset in this.FieldAssets)
            {
                // Get anims

                var fieldAnims = "";
                int j = 1;
                foreach (var fieldAnim in fieldAsset.Animations)
                {
                    var fieldAnimString = @$"
    #region Field Animation {j} For Field Note {i}
    
    Note Type: {fieldAnim.NoteType}
    Animation End Time: {fieldAnim.AnimationEndTime} ({fieldAnim.AnimationEndTime / 1000.0})
    Lane: {fieldAnim.Lane}
    Animation Start Time: {fieldAnim.AnimationStartTime} ({fieldAnim.AnimationStartTime / 1000.0})
    Aerial Flag: {fieldAnim.AerialFlag}
    Previous (Potential End): {fieldAnim.Previous}
    Next (Potential Beginning): {fieldAnim.Next}
    Unk1: {fieldAnim.Unk1}
    Unk2: {fieldAnim.Unk2}
    Unk3: {fieldAnim.Unk3}
    Unk4: {fieldAnim.Unk4}
    Unk5: {fieldAnim.Unk5}
    Unk6: {fieldAnim.Unk6}
    Unk7: {fieldAnim.Unk7}
    Unk8: {fieldAnim.Unk8}

    #endregion
                ";

                    fieldAnims += fieldAnimString;
                    ++j;
                }

                var fieldAssetString = @$"
    #region Field Assets {i}
    
    Note Type: {fieldAsset.JumpFlag} (Jump Flag?)
    Hit Time: {fieldAsset.HitTime} ({fieldAsset.HitTime / 1000.0})
    Lane: {fieldAsset.Lane}
    Animation Reference: {fieldAsset.AnimationReference}
    FF1: {fieldAsset.FF1}
    FF2: {fieldAsset.FF2}
    FF3: {fieldAsset.FF3}
    Model Type: {fieldAsset.ModelType}
    Unk1: {fieldAsset.Unk1}
    Unk2: {fieldAsset.Unk2}
    Unk3: {fieldAsset.Unk3}
    Unk4: {fieldAsset.Unk4}
    Unk5: {fieldAsset.Unk5}
    Unk6: {fieldAsset.Unk6}
    Unk7: {fieldAsset.Unk7}
    Unk8: {fieldAsset.Unk8}
    Unk8: {fieldAsset.Unk9}

    {fieldAnims}

    #endregion
                ";

                fieldAssets += fieldAssetString;
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
            foreach(var timeShift in this.TimeShifts)
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

            var formattedString = header + fieldNotes + fieldAssets + performerNotes + timeShifts;

            File.AppendAllText($"{destination}-{this.Name.Split('\\')[^1]}.cs", formattedString);
        }
    }
}