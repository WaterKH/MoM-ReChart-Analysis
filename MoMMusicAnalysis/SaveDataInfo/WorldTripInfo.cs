using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class WorldTripInfo
    {
        public int ObjectCount { get; set; }
        public List<GummiWorld> WorldDictionary { get; set; } = new List<GummiWorld>();
        public List<GummiDoor> DoorDictionary { get; set; } = new List<GummiDoor>();
        public List<GummiMusic> TripMusicDictionary { get; set; } = new List<GummiMusic>();
        public int LastSelectedTripWorldIDValue { get; set; }
        public byte LastSelectedTripWorldState { get; set; }
        public int LastPlayedTripMusicIDValue { get; set; }
        public byte LastPlayedTripMusicCleared { get; set; }
        public List<byte> LastPlayedTripMusicMissionCleareds { get; set; } = new List<byte>();
        public byte PlayedStaffCredit { get; set; }
        public byte OpenedSecretArea { get; set; }
        public byte GetClearAllMSReward { get; set; }
        public byte GetCompleteAllMissionReward { get; set; }
        public List<byte> PlayTime { get; set; }
        public List<byte> PlayTimeDouble { get; set; }
        public byte TotalClearCount { get; set; }
        public byte ClearedWorldTripMode { get; set; }
        public string Version { get; set; }

        public WorldTripInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.GetCountFromFileStream();

            // Get World Dictionary Name
            var worldDictionaryName = saveDataReader.GetStringFromFileStream(160);

            // Get World Dictionary Count
            var worldDictionaryCount = saveDataReader.GetCountFromFileStream();

            // Get World Dictionary
            for (int i = 0; i < worldDictionaryCount; ++i)
            {
                this.WorldDictionary.Add(new GummiWorld().Process(saveDataReader));
            }

            // Get Door Dictionary Name
            var doorDictionaryName = saveDataReader.GetStringFromFileStream(160);

            // Get Door Dictionary Count
            var doorDictionaryCount = saveDataReader.GetCountFromFileStream();

            // Get Door Dictionary
            for (int i = 0; i < doorDictionaryCount; ++i)
            {
                this.DoorDictionary.Add(new GummiDoor().Process(saveDataReader));
            }

            // Get Trip Music Dictionary Name
            var tripMusicDictionaryName = saveDataReader.GetStringFromFileStream(160);

            // Get Trip Music Dictionary Count
            var tripMusicDictionaryCount = saveDataReader.GetCountFromFileStream();

            // Get Trip Music Dictionary
            for (int i = 0; i < tripMusicDictionaryCount; ++i)
            {
                this.TripMusicDictionary.Add(new GummiMusic().Process(saveDataReader));
            }

            // Get Last Selected World Trip ID Value Name
            var lastSelectedWorldTripIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Last Selected World Trip ID Value
            this.LastSelectedTripWorldIDValue = saveDataReader.GetIdFromFileStream();

            // Get Last Selected World Trip State Name
            var lastSelectedWorldTripStateName = saveDataReader.GetStringFromFileStream(160);

            // Get Last Selected World Trip State
            this.LastSelectedTripWorldState = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Last Played Trip Music ID Value Name
            var lastPlayedTripMusicIDValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Last Played Trip Music ID Value
            this.LastPlayedTripMusicIDValue = saveDataReader.GetIdFromFileStream();

            // Get Last Played Trip Music Cleared Name
            var lastPlayedTripMusicClearedName = saveDataReader.GetStringFromFileStream(160);

            // Get Last Played Trip Music Cleared
            this.LastPlayedTripMusicCleared = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Last Played Trip Music Mission Cleareds Name
            var lastPlayedTripMusicMissionClearedsName = saveDataReader.GetStringFromFileStream(160);

            // Get Last Played Trip Music Cleareds
            this.LastPlayedTripMusicMissionCleareds = saveDataReader.FindDataFromFileStream();

            // Get Played Staff Credit Name
            var playedStaffCreditName = saveDataReader.GetStringFromFileStream(160);

            // Get Played Staff Credit
            this.PlayedStaffCredit = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Opened Secret Area Name
            var openedSecretAreaName = saveDataReader.GetStringFromFileStream(160);

            // Get Opened Secret Area
            this.OpenedSecretArea = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Get Clear All Ms Reward Name
            var getClearAllMsRewardName = saveDataReader.GetStringFromFileStream(160);

            // Get Get Clear All Ms Reward
            this.GetClearAllMSReward = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Play Time Name
            var playTimeName = saveDataReader.GetStringFromFileStream(160);

            // Get Play Time Value Length
            var playTimeValueLength = saveDataReader.ReadBytesFromFileStream(1); // CA == 4 bytes?

            // Get Play Time Value
            this.PlayTime = saveDataReader.ReadBytesFromFileStream(4);

            // Get Play Time Double Name
            var playTimeDoubleName = saveDataReader.GetStringFromFileStream(160);

            // Get Play Time Value Double Length
            var playTimeDoubleValueLength = saveDataReader.ReadBytesFromFileStream(1); // CB == 8 bytes?

            // Get Play Time Double Value
            this.PlayTimeDouble = saveDataReader.ReadBytesFromFileStream(8);

            // Get Total Clear Count Name
            var totalClearCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Clear Count
            this.TotalClearCount = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Cleared World Trip Mode Name
            var clearedWorldTripName = saveDataReader.GetStringFromFileStream(160);

            // Get Cleared World Trip Mode
            this.ClearedWorldTripMode = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            var gummiWorlds = "";
            this.WorldDictionary.ForEach(x => gummiWorlds += $"\n{x.Display()}");
            var gummiDoors = "";
            this.DoorDictionary.ForEach(x => gummiDoors += $"\n{x.Display()}");
            var gummiMusics = "";
            this.TripMusicDictionary.ForEach(x => gummiMusics += $"\n{x.Display()}");

            return @$"
    #region WorldTripInfo

    Object Count: {this.ObjectCount}
    
    Gummi Worlds: 
    #region WorldDictionary
    {gummiWorlds}
    #endregion WorldDictionary

    Gummi Doors: 
    #region DoorDictionary
    {gummiDoors}
    #endregion DoorDictionary

    Gummi Musics: 
    #region TripMusicDictionary
    {gummiMusics}
    #endregion TripMusicDictionary

    Last Selected Trip World ID Value: {this.LastSelectedTripWorldIDValue}
    Last Selected Trip World State: {this.LastSelectedTripWorldState}
    Last Played Trip Music ID Value: {this.LastPlayedTripMusicIDValue}
    Last Played Trip Music Cleared: {this.LastPlayedTripMusicCleared}
    Last Played Trip Music Mission Cleareds: {this.LastPlayedTripMusicMissionCleareds.Display()}
    Played Staff Credit: {this.PlayedStaffCredit}
    Opened Secret Area: {this.OpenedSecretArea}
    Get Clear All MS Reward: {this.GetClearAllMSReward}
    Play Time: {this.PlayTime}
    Play Time Double: {this.PlayTimeDouble}
    Total Clear Count: {this.TotalClearCount}
    Cleared World Trip Mode: {this.ClearedWorldTripMode}
    Version: {this.Version}
    
    #endregion WorldTripInfo
";
        }
    }

    public class GummiWorld
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public byte State { get; set; }
        public byte GetClearReward { get; set; }

        public GummiWorld Process(FileStream saveDataReader)
        {
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get State Name
            var stateName = saveDataReader.GetStringFromFileStream(160);

            // Get State Value
            this.State = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Get Clear Reward Name
            var getClearRewardName = saveDataReader.GetStringFromFileStream(160);

            // Get Get Clear Reward Value
            this.GetClearReward = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }

        public string Display()
        {
            return @$"
    #region GummiWorld {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    State: {this.State}
    Get Clear Reward: {this.GetClearReward}
    
    #endregion GummiWorld {this.Id}
";
        }
    }

    public class GummiDoor
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public byte LookedUnlockEffect { get; set; }

        public GummiDoor Process(FileStream saveDataReader)
        {
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Looked Unlock Effect Name
            var lookedUnlockEffectName = saveDataReader.GetStringFromFileStream(160);

            // Get Looked Unlock Effect Value
            this.LookedUnlockEffect = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            return this;
        }

        public string Display()
        {
            return @$"
    #region GummiDoor {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    Looked Unlock Effect: {this.LookedUnlockEffect}
    
    #endregion GummiDoor {this.Id}
";
        }
    }

    public class GummiMusic
    {
        public int Id { get; set; }
        public int ObjectCount { get; set; }
        public byte LookedPlaySetting { get; set; }
        public byte Cleared { get; set; }
        public List<MissionRecord> MissionRecords { get; set; } = new List<MissionRecord>();

        public GummiMusic Process(FileStream saveDataReader)
        {
            this.Id = saveDataReader.GetIdFromFileStream();

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Looked Play Setting Name
            var lookedPlaySettingName = saveDataReader.GetStringFromFileStream(160);

            // Get Looked Play Setting Value
            this.LookedPlaySetting = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Cleared Name
            var clearedName = saveDataReader.GetStringFromFileStream(160);

            // Get Cleared Value
            this.Cleared = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Mission Record Name
            var missionRecordsName = saveDataReader.GetStringFromFileStream(160);

            // Get Mission Record Count
            var missionRecordsCount = saveDataReader.ReadByte() - 144;

            // Get Trip Music Dictionary
            for (int i = 0; i < missionRecordsCount; ++i)
            {
                if (i == missionRecordsCount - 1)
                    this.MissionRecords.Add(new MissionRecord().Process(saveDataReader, this.Id + 1));
                else
                    this.MissionRecords.Add(new MissionRecord().Process(saveDataReader));
            }

            return this;
        }

        public string Display()
        {
            var missionRecordsString = "";
            this.MissionRecords.ForEach(x => missionRecordsString += $"\n{x.Display()}");

            return @$"
    #region GummiMusic {this.Id}

    Id: {this.Id}
    Object Count: {this.ObjectCount}
    Looked Play Setting: {this.LookedPlaySetting}
    Cleared: {this.Cleared}

    Mission Records:
    #region MissionRecords
    {missionRecordsString}
    #endregion MissionRecords
    
    #endregion GummiMusic {this.Id}
";
        }
    }
    
    public class MissionRecord
    {
        public int ObjectCount { get; set; }
        public byte Cleared { get; set; }
        public List<byte> ProgressValue { get; set; } = new List<byte>();

        public MissionRecord Process(FileStream saveDataReader, int id = -1)
        {
            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Cleared Name
            var clearedName = saveDataReader.GetStringFromFileStream(160);

            // Get Cleared Value
            this.Cleared = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Progress Value Name
            var prgoressValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Progress Value
            if (id != -1)
                this.ProgressValue = saveDataReader.FindDataFromFileStream(nextId: id);
            else
                this.ProgressValue = saveDataReader.FindDataFromFileStream(nextCount: 130);

            return this;
        }

        public string Display()
        {
            return @$"
    #region MissionRecord

    Object Count: {this.ObjectCount}
    Cleared: {this.Cleared}
    Progress Value: {this.ProgressValue.Display()}
    
    #endregion MissionRecord
";
        }
    }
}