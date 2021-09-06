using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoMMusicAnalysis.SaveDataInfo
{
    public class MuseumTopInfo
    {
        public int ObjectCount { get; set; }
        public byte IsTheaterChecked { get; set; }
        public string Version { get; set; }

        public MuseumTopInfo Process(FileStream saveDataReader)
        {
            // Get Name
            var name = saveDataReader.GetStringFromFileStream(160);

            // Get Object Count
            this.ObjectCount = saveDataReader.ReadByte() - 128;

            // Get Is Theater Checked Name
            var isTheaterCheckedName = saveDataReader.GetStringFromFileStream(160);

            // Get Is Theater Checked Value
            this.IsTheaterChecked = saveDataReader.ReadBytesFromFileStream(1).FirstOrDefault();

            // Get Version Name
            var versionName = saveDataReader.GetStringFromFileStream(160);

            // Get Version
            this.Version = saveDataReader.GetStringFromFileStream(160);

            return this;
        }

        public string Display()
        {
            return @$"
    #region MuseumTopInfo
    
    Object Count: {this.ObjectCount}
    Is Theater Checked: {this.IsTheaterChecked}
    Version: {this.Version}
    
    #endregion MuseumTopInfo
";
        }
    }
}