using MoMMusicAnalysis.SaveDataInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis
{
    public class SaveData
    {
        public int ObjectCount { get; set; }
        public UserInfo UserInfo { get; set; } = new UserInfo();
        public MainMenuInfo MainMenuInfo { get; set; } = new MainMenuInfo();
        public MusicStageInfo MusicStageInfo { get; set; } = new MusicStageInfo();
        public MusicPlayerInfo MusicPlayerInfo { get; set; } = new MusicPlayerInfo();
        public ItemMixInfo ItemMixInfo { get; set; } = new ItemMixInfo();
        public CollectionCardInfo CollectionCardInfo { get; set; } = new CollectionCardInfo();
        public TheaterInfo TheaterInfo { get; set; } = new TheaterInfo();
        public ConfigInfo ConfigInfo { get; set; } = new ConfigInfo();
        public PartyInfo PartyInfo { get; set; } = new PartyInfo();
        public ItemInfo ItemInfo { get; set; } = new ItemInfo();
        public MuseumTopInfo MuseumTopInfo { get; set; } = new MuseumTopInfo();
        public WorldTripInfo WorldTripInfo { get; set; } = new WorldTripInfo();
        public MusicSelectInfo MusicSelectInfo { get; set; } = new MusicSelectInfo();
        public BattleCPUInfo BattleCPUInfo { get; set; } = new BattleCPUInfo();
        public PairMusicInfo PairMusicInfo { get; set; } = new PairMusicInfo();
        public MusicInfo MusicInfo { get; set; } = new MusicInfo();
        public BattleVsInfo BattleVsInfo { get; set; } = new BattleVsInfo();
        public RhythmPointInfo RhythmPointInfo { get; set; } = new RhythmPointInfo();
        public ProfileCardInfo ProfileCardInfo { get; set; } = new ProfileCardInfo();
        public AchievementInfo AchievementInfo { get; set; } = new AchievementInfo();
        public TutorialInfo TutorialInfo { get; set; } = new TutorialInfo();
        public HelpInfo HelpInfo { get; set; } = new HelpInfo();
        public MemoryMovieInfo MemoryMovieInfo { get; set; } = new MemoryMovieInfo();
        public string Version { get; set; }

        public SaveData Process(FileStream saveDataReader)
        {
            // Get Header
            saveDataReader.ReadBytesFromFileStream(2); // DE 00

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte();

            this.UserInfo.Process(saveDataReader);
            this.MainMenuInfo.Process(saveDataReader);
            this.MusicStageInfo.Process(saveDataReader);
            this.MusicPlayerInfo.Process(saveDataReader);
            this.ItemMixInfo.Process(saveDataReader);
            this.CollectionCardInfo.Process(saveDataReader);
            this.TheaterInfo.Process(saveDataReader);
            this.ConfigInfo.Process(saveDataReader);
            this.PartyInfo.Process(saveDataReader);
            this.ItemInfo.Process(saveDataReader);
            this.MuseumTopInfo.Process(saveDataReader);
            this.WorldTripInfo.Process(saveDataReader);
            this.MusicSelectInfo.Process(saveDataReader);
            this.BattleCPUInfo.Process(saveDataReader);
            this.PairMusicInfo.Process(saveDataReader);
            this.MusicInfo.Process(saveDataReader);
            this.BattleVsInfo.Process(saveDataReader);
            this.RhythmPointInfo.Process(saveDataReader);
            this.ProfileCardInfo.Process(saveDataReader);
            this.AchievementInfo.Process(saveDataReader);
            this.TutorialInfo.Process(saveDataReader);
            this.HelpInfo.Process(saveDataReader);
            this.MemoryMovieInfo.Process(saveDataReader);

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public void WriteToFile(string destination)
        {
            var formattedString = @$"
    Object Count: {this.ObjectCount}

    {this.UserInfo.Display()}
    {this.MainMenuInfo.Display()}
    {this.MusicStageInfo.Display()}
    {this.MusicPlayerInfo.Display()}
    {this.ItemMixInfo.Display()}
    {this.CollectionCardInfo.Display()}
    {this.TheaterInfo.Display()}
    {this.ConfigInfo.Display()}
    {this.PartyInfo.Display()}
    {this.ItemInfo.Display()}
    {this.MuseumTopInfo.Display()}
    {this.WorldTripInfo.Display()}
    {this.MusicSelectInfo.Display()}
    {this.BattleCPUInfo.Display()}
    {this.PairMusicInfo.Display()}
    {this.MusicInfo.Display()}
    {this.BattleVsInfo.Display()}
    {this.RhythmPointInfo.Display()}
    {this.ProfileCardInfo.Display()}
    {this.AchievementInfo.Display()}
    {this.TutorialInfo.Display()}
    {this.HelpInfo.Display()}
    {this.MemoryMovieInfo.Display()}
";

            File.AppendAllText($"{destination}-SaveData.cs", formattedString);
        }
    }
}