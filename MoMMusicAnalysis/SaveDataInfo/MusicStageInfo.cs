using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class MusicStageInfo
    {
        public int ObjectCount { get; set; }
        public PlayInfo NormalPlayInfo { get; set; } = new PlayInfo();
        public PlayInfo SimplePlayInfo { get; set; } = new PlayInfo();
        public List<byte> PartySelectedValue { get; set; }
        public List<byte> ChangeCharacterPartyNumber { get; set; }
        public List<byte> TotalScore { get; set; }
        public List<byte> FriendTrigerInputSuccessCount { get; set; } // Mispelled for data in savedata
        public List<byte> RhythmPrizeInputSuccessCount { get; set; }
        public List<byte> TotalPointHPRecoveryInt { get; set; }
        public List<byte> TotalCountJump { get; set; }
        public List<byte> TotalCountFriendAppeared { get; set; }
        public List<byte> TotalKingEntryTime { get; set; }
        public List<byte> TotalKingEntryCount { get; set; }
        public string Version { get; set; }

        public MusicStageInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Normal Play Info
            this.NormalPlayInfo.Process(saveDataReader);

            // Get Simple Play Info
            this.SimplePlayInfo.Process(saveDataReader);

            // Get Party Selected Value Name
            var partySelectedValueName = saveDataReader.GetStringFromFileStream(160);

            // Get Music Select Unlock State
            this.PartySelectedValue = saveDataReader.FindDataFromFileStream();

            // Get Change Character Party Number Name
            var changeCharacterPartyNumberName = saveDataReader.GetStringFromFileStream(160);

            // Get Change Character Party Number
            this.ChangeCharacterPartyNumber = saveDataReader.FindDataFromFileStream();

            // Get Total Score Name
            var totalScoreName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Score
            this.TotalScore = saveDataReader.FindDataFromFileStream();

            // Get Friend Triger Input Success Count Name
            var friendTrigerInputSuccessCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Friend Triger Input Success Count
            this.FriendTrigerInputSuccessCount = saveDataReader.FindDataFromFileStream();

            // Get Rhythm Prize Input Success Count Name
            var rhythmPrizeInputSuccessCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Rhythm Prize Input Success Count
            this.RhythmPrizeInputSuccessCount = saveDataReader.FindDataFromFileStream();

            // Get Total Point HP Recovery Int Name
            var totalPointHPRecoveryIntName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Point HP Recovery Int
            this.TotalPointHPRecoveryInt = saveDataReader.FindDataFromFileStream();

            // Get Total Count Jump Name
            var totalCountJumpName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Count Jump Int
            this.TotalCountJump = saveDataReader.FindDataFromFileStream();

            // Get Total Count Friend Appeared Name
            var totalCountFriendAppearedName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Count Friend Appeared
            this.TotalCountFriendAppeared = saveDataReader.FindDataFromFileStream();

            // Get Total King Entry Count Name
            var totalKingEntryCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Total King Entry Count
            this.TotalKingEntryCount = saveDataReader.FindDataFromFileStream();

            // Get Total King Entry Time Name
            var totalKingEntryTimeName = saveDataReader.GetStringFromFileStream(160);

            // Get Total King Entry Time
            this.TotalKingEntryTime = saveDataReader.FindDataFromFileStream();

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            return @$"
    #region MusicStageInfo

    Normal Play Info: {this.NormalPlayInfo.Display()}
    Simple Play Info: {this.SimplePlayInfo.Display()}
    Party Selected Value: {this.PartySelectedValue.Display()}
    Change Character Party Number: {this.ChangeCharacterPartyNumber.Display()}
    Total Score: {this.TotalScore.Display()}
    Friend Triger Input Success Count: {this.FriendTrigerInputSuccessCount.Display()}
    Rhythm Prize Input Success Count: {this.RhythmPrizeInputSuccessCount.Display()}
    Total Point HP Recovery Int: {this.TotalPointHPRecoveryInt.Display()}
    Total Count Friend Appeared: {this.TotalCountFriendAppeared.Display()}
    Total Count Jump: {this.TotalCountJump.Display()}
    Total King Entry Time: {this.TotalKingEntryTime.Display()}
    Total King Entry Count: {this.TotalKingEntryCount.Display()}
    Version: {this.Version}
    
    #endregion MusicStageInfo
";
        }
    }

    public class PlayInfo
    {
        public int ObjectCount { get; set; }
        public List<byte> InputSuccessCount { get; set; }
        public List<byte> TotalCountEnemyKilled { get; set; }
        public List<byte> TotalCountNormalItemGot { get; set; }
        public List<byte> TotalCountLastChestItemGot { get; set; }

        public PlayInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Input Success Count Name
            var inputSuccessCountName = saveDataReader.GetStringFromFileStream(160);

            // Get Input Success Count
            this.InputSuccessCount = saveDataReader.FindDataFromFileStream();

            // Get Total Count Enemy Killed Name
            var totalCountEnemyKilledName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Count Enemy Killed
            this.TotalCountEnemyKilled = saveDataReader.FindDataFromFileStream();

            // Get Total Count Normal Item Got Name
            var totalCountNormalItemGotName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Count Normal Item Got
            this.TotalCountNormalItemGot = saveDataReader.FindDataFromFileStream();

            // Get Total Count Last Chest Item Got Name
            var totalCountLastChestItemGotName = saveDataReader.GetStringFromFileStream(160);

            // Get Total Count Last Chest Item Got
            this.TotalCountLastChestItemGot = saveDataReader.FindDataFromFileStream();

            return this;
        }

        public string Display()
        {
            return @$"
    #region MainMenuInfo

    Object Count: {this.ObjectCount}
    Input Success Count: {this.InputSuccessCount.Display()}
    Total Count Enemy Killed: {this.TotalCountEnemyKilled.Display()}
    Total Count Normal Item Got: {this.TotalCountNormalItemGot.Display()}
    Total Count Last Chest Item Got: {this.TotalCountLastChestItemGot.Display()}
    
    #endregion MainMenuInfo
";
        }
    }
}