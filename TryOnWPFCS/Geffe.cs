using System.Collections.Generic;
using System.IO;

namespace TryOnWpfCs
{
    internal class Geffe : IGeffe
    {
        public static Geffe Create(string firstKeyStream, string secondKeyStream, string thirdKeyStream, string loadFilePath, string saveFilePath)
        {
            var initialFile = File.ReadAllBytes(loadFilePath);
            var firstKey = CipherHelpers.GenerateKey(firstKeyStream, initialFile.Length, new int[] { 27 });
            var secondKey = CipherHelpers.GenerateKey(secondKeyStream, initialFile.Length, new int[] { 26, 19, 20 });
            var thirdKey = CipherHelpers.GenerateKey(thirdKeyStream, initialFile.Length, new int[] { 25, 27, 35 });
            var key = GenerateGeffeKey(firstKey, secondKey, thirdKey);
            var cipherFile = CipherHelpers.GenerateCipher(initialFile, key);
            var geffe = new Geffe(firstKey, secondKey, thirdKey, key, initialFile, cipherFile);
            File.WriteAllBytes(saveFilePath, geffe.CipherFile);
            return geffe;
        }

        private Geffe(byte[] firstKey, byte[] secondKey, byte[] thirdKey, byte[] key, byte[] initialFile, byte[] cipherFile)
        {
            Key = key;
            InitialFile = initialFile;
            CipherFile = cipherFile;
            FirstKey = firstKey;
            SecondKey = secondKey;
            ThirdKey = thirdKey;
        }

        public byte[] Key { get; }
        public byte[] InitialFile { get; }
        public byte[] CipherFile { get; }
        public byte[] FirstKey { get; }
        public byte[] SecondKey { get; }
        public byte[] ThirdKey { get; }

        private static byte[] GenerateGeffeKey(byte[] firstKey, byte[] secondKey, byte[] thirdKey)
        {
            var key = new List<byte>();
            //
            //  (x1 and x2) or (not x1 and x3)
            //
            for (long i = 0; i < firstKey.Length; i++)
            {
                key.Add((byte) ((firstKey[i] & secondKey[i]) | (~firstKey[i] & thirdKey[i])));
            }

            return key.ToArray();
        }
    }
}
