using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MoMMusicAnalysis
{
    public static class Extensions
    {
        public static List<byte> ReadBytesFromFileStream(this FileStream musicReader, int length)
        {
            if (musicReader.Position + length > musicReader.Length)
                return null;

            var data = new List<byte>();

            for (int i = 0; i < length; ++i)
            {
                var t = musicReader.ReadByte();
                if (t == -1)
                    return null;
                data.Add((byte)t);
            }

            return data;
        }

        /// <summary>
        /// Reads in data for the current object until we find the next length for the next object name.
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        public static List<byte> FindDataFromFileStream(this FileStream fileStream, int nextId = -1, int nextCount = -1)
        {
            var data = new List<byte>();

            while (CleanInput(fileStream.GetStringFromFileStream(160, true)) && !CheckForNextId(fileStream, nextId) && !CheckForNextCount(fileStream, nextCount))
            {
                var t = fileStream.ReadByte();
                if (t == -1)
                    return null;

                data.Add((byte)t);
            }

            return data;
        }

        public static bool CheckForNextId(this FileStream fileStream, int nextId)
        {
            if (nextId == -1)
                return false;

            var id = fileStream.GetIdFromFileStream();
            fileStream.Position -= 5;

            if (id == nextId)
                return true;

            if (Math.Abs(id - nextId) < 250)
                return true;

            id = fileStream.ReadByte();
            fileStream.Position -= 1;

            if (id == nextId)
                return true;

            return false;
        }

        public static bool CheckForNextCount(this FileStream fileStream, int nextCount)
        {
            if (nextCount == -1)
                return false;

            var count = fileStream.GetCountFromFileStream();
            fileStream.Position -= 3;

            if (count == nextCount)
                return true;

            count = fileStream.ReadByte();
            fileStream.Position -= 1;

            return count == nextCount;
        }

        /// <summary>
        /// Handles of the Id length and gets the Id.
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        public static int GetIdFromFileStream(this FileStream fileStream)
        {
            // Get Length
            var length = fileStream.ReadByte();

            // Get Id
            var id = fileStream.ReadBytesFromFileStream(4);
            id.Reverse();
            
            return BitConverter.ToInt32(id.ToArray());
        }

        /// <summary>
        /// Handles of the Id length and gets the Count.
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        public static int GetCountFromFileStream(this FileStream fileStream)
        {
            // Get Length
            var length = fileStream.ReadByte();

            // Get Id
            var id = fileStream.ReadBytesFromFileStream(2);
            id.Reverse();

            return BitConverter.ToInt16(id.ToArray());
        }

        /// <summary>
        /// Handles the look up of the name length and then gets the actual length using the offset provided.
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="offset"></param>
        /// <param name="resetPosition"></param>
        /// <returns></returns>
        public static string GetStringFromFileStream(this FileStream fileStream, int offset = 0, bool resetPosition = false)
        {
            var nameLength = fileStream.ReadBytesFromFileStream(1);
            var tempLength = nameLength.FirstOrDefault() - offset;

            if (tempLength < 100 && tempLength > 0)
            {
                var name = Encoding.UTF8.GetString(fileStream.ReadBytesFromFileStream(tempLength).ToArray());

                if (resetPosition)
                    fileStream.Position -= 1 + tempLength;

                return name;
            }

            fileStream.Position -= 1;
            return string.Empty;
        }

        static bool CleanInput(string strIn)
        {
            if (strIn.Equals(string.Empty))
                return true;

            // Replace invalid characters with empty strings.
            try
            {
                var match = Regex.Match(strIn, @"^[_A-Za-z]+$");
                var isMatch = match.Success && match.Value.Length == strIn.Length;

                return !isMatch;
            }
            // If we timeout when replacing invalid characters,
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static string Display(this List<byte> data)
        {
            return string.Join("", data);
        }

        public static double ConvertHexToTime(this List<byte> data)
        {
            var t = BitConverter.ToInt32(data.ToArray(), 0);

            return t / 1000.0;
        }
    }
}