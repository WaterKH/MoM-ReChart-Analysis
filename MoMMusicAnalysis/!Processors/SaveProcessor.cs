using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace MoMMusicAnalysis
{
    public class SaveProcessor
    {
        [DllImport("IL2CppDLL.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern string Backup_get_encryptionKey(IntPtr methodInfo);

        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("IL2CppDLL.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public static extern IntPtr BackupCryptography_Decrypt(IntPtr encrypted, string password, MethodInfo methodInfo);


        public SaveData ProcessSaveData(string fileName, bool debug = false)
        {
            if (File.Exists($"TEST-SaveData.cs"))
                File.Delete($"TEST-SaveData.cs");

            using var saveDataReader = File.OpenRead(fileName);

            var saveData = new SaveData().Process(saveDataReader);

            if (debug)
                saveData.WriteToFile("TEST");

            return saveData;
        }

        public void Test()
        {
            var password = "b0e71f606410d4ef2972ca89455eb4fa723bfa379d29db1ac18b2819631d7703";
            var encrypted = new IntPtr();

            var password_test = Backup_get_encryptionKey(IntPtr.Zero);
            BackupCryptography_Decrypt(encrypted, password, null);
        }
    }
}