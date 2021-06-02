using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MoMMusicAnalysis
{
    public class Header
    {
        public List<byte> UnityVersion { get; set; } // Always 33 bytes it seems
        public int EntireFileSize { get; set; } // (Reversed?)
        public int NextSize1 { get; set; } // This is the next portion (Reversed?)
        public int NextSize2 { get; set; } // This is the same as the previous value for some reason? (Reversed?)
        public int NextSubSize { get; set; } // If the file size > 128KBs, this adds 10 more bytes (Assume this is true for every next ceiling?)
        public int Unk1 { get; set; }
        public int Unk2 { get; set; }
        public int Unk3 { get; set; }
        public int Unk4 { get; set; }
        public int FileSizeCount { get; set; }
        public List<FileSize> FileSizes { get; set; } = new List<FileSize>();
        public int Unk5 { get; set; } // Usually 1?
        public int Unk6 { get; set; }
        public int Unk7 { get; set; }
        public int Unk8 { get; set; }
        public int CompleteFileSize { get; set; } // File Size Compiled
        public int Unk9 { get; set; } // Usually 4?
        public List<byte> UnityId { get; set; } // 0x29? bytes in length
        public int FinalFileSize { get; set; } // Final Time we see this size
        public int AnotherUnityVersionSize { get; set; }
        public List<byte> AnotherUnityVersion { get; set; } // Has 00 13 00 trailing after, will this be captured in string?
        public List<byte> DuplicateData { get; set; } = new List<byte>(); // Seems to be the same info (Just out of order) 9A6
        public Section[] Sections { get; set; } = new Section[4]; // 3 songs and an asset list
        public List<byte> EmptyData { get; set; } = new List<byte>(); // Empty Data of length 0x5EC

        public Header ProcessHeader(FileStream musicReader)
        {
            // Get Unity Version
            this.UnityVersion = musicReader.ReadBytesFromFileStream(33);

            // Get Entire File Size
            var reversedData = musicReader.ReadBytesFromFileStream(4);
            reversedData.Reverse();
            this.EntireFileSize = BitConverter.ToInt32(reversedData.ToArray());

            // Get NextSize1
            reversedData = musicReader.ReadBytesFromFileStream(4);
            reversedData.Reverse();
            this.NextSize1 = BitConverter.ToInt32(reversedData.ToArray());

            // Get NextSize2
            reversedData = musicReader.ReadBytesFromFileStream(4);
            reversedData.Reverse();
            this.NextSize2 = BitConverter.ToInt32(reversedData.ToArray());

            // Get NextSubSize
            reversedData = musicReader.ReadBytesFromFileStream(4);
            reversedData.Reverse();
            this.NextSubSize = BitConverter.ToInt32(reversedData.ToArray());

            // Get Unk1
            this.Unk1 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk2
            this.Unk2 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk3
            this.Unk3 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk4
            this.Unk4 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get File Size Count
            reversedData = musicReader.ReadBytesFromFileStream(4);
            reversedData.Reverse();
            this.FileSizeCount = BitConverter.ToInt16(reversedData.ToArray());

            for (int i = 0; i < this.FileSizeCount; ++i)
            {
                var fileSize = new FileSize();

                reversedData = musicReader.ReadBytesFromFileStream(4);
                reversedData.Reverse();
                fileSize.MainFileSize1 = BitConverter.ToInt32(reversedData.ToArray());

                reversedData = musicReader.ReadBytesFromFileStream(4);
                reversedData.Reverse();
                fileSize.MainFileSize2 = BitConverter.ToInt32(reversedData.ToArray());

                reversedData = musicReader.ReadBytesFromFileStream(2);
                reversedData.Reverse();
                fileSize.EmptyData = BitConverter.ToInt16(reversedData.ToArray());

                this.FileSizes.Add(fileSize);
            }

            // Get Unk5
            this.Unk5 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk6
            this.Unk6 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk7
            this.Unk7 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unk8
            this.Unk8 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Complete File Size
            reversedData = musicReader.ReadBytesFromFileStream(4);
            reversedData.Reverse();
            this.CompleteFileSize = BitConverter.ToInt32(reversedData.ToArray());

            // Get Unk9
            this.Unk9 = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray());

            // Get Unity Version
            this.UnityId = musicReader.ReadBytesFromFileStream(41);

            // Get Final File Size
            reversedData = musicReader.ReadBytesFromFileStream(4);
            reversedData.Reverse();
            this.FinalFileSize = BitConverter.ToInt32(reversedData.ToArray());

            // Get Unity Another Version Size
            reversedData = musicReader.ReadBytesFromFileStream(4);
            reversedData.Reverse();
            this.AnotherUnityVersionSize = BitConverter.ToInt32(reversedData.ToArray());

            // Get Unity Another Version
            this.AnotherUnityVersion = musicReader.ReadBytesFromFileStream(this.AnotherUnityVersionSize);

            // Get Duplicate Data
            this.DuplicateData = musicReader.ReadBytesFromFileStream(0x9A3);

            for (int i = 0; i < Sections.Length; ++i)
            {
                Sections[i] = new Section
                {
                    Id = musicReader.ReadBytesFromFileStream(8),
                    Offset = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray()),
                    Size = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray()),
                    EmptyData = BitConverter.ToInt32(musicReader.ReadBytesFromFileStream(4).ToArray())
                };
            }

            // Get Empty Data
            this.EmptyData = musicReader.ReadBytesFromFileStream(0x5EC);

            return this;
        }

        public List<byte> RecompileHeader()
        {
            var data = new List<byte>();

            data.AddRange(this.UnityVersion);
            var reversedData = BitConverter.GetBytes(this.EntireFileSize);
            Array.Reverse(reversedData);
            data.AddRange(reversedData);
            
            reversedData = BitConverter.GetBytes(this.NextSize1);
            Array.Reverse(reversedData);
            data.AddRange(reversedData);

            reversedData = BitConverter.GetBytes(this.NextSize2);
            Array.Reverse(reversedData);
            data.AddRange(reversedData);

            reversedData = BitConverter.GetBytes(this.NextSubSize);
            Array.Reverse(reversedData);
            data.AddRange(reversedData);

            data.AddRange(BitConverter.GetBytes(this.Unk1));
            data.AddRange(BitConverter.GetBytes(this.Unk2));
            data.AddRange(BitConverter.GetBytes(this.Unk3));
            data.AddRange(BitConverter.GetBytes(this.Unk4));

            reversedData = BitConverter.GetBytes(this.FileSizeCount);
            Array.Reverse(reversedData);
            data.AddRange(reversedData);

            foreach (var fileSize in this.FileSizes)
                data.AddRange(fileSize.RecompileFileSize());

            data.AddRange(BitConverter.GetBytes(this.Unk5));
            data.AddRange(BitConverter.GetBytes(this.Unk6));
            data.AddRange(BitConverter.GetBytes(this.Unk7));
            data.AddRange(BitConverter.GetBytes(this.Unk8));

            reversedData = BitConverter.GetBytes(this.CompleteFileSize);
            Array.Reverse(reversedData);
            data.AddRange(reversedData);

            data.AddRange(BitConverter.GetBytes(this.Unk9));
            data.AddRange(this.UnityId);
            
            reversedData = BitConverter.GetBytes(this.FinalFileSize);
            Array.Reverse(reversedData);
            data.AddRange(reversedData);

            reversedData = BitConverter.GetBytes(this.AnotherUnityVersionSize);
            Array.Reverse(reversedData);
            data.AddRange(reversedData);

            data.AddRange(this.AnotherUnityVersion);

            data.AddRange(DuplicateData);

            foreach (var section in this.Sections)
                data.AddRange(section.RecompileSection());

            data.AddRange(EmptyData);

            return data;
        }
    }
}