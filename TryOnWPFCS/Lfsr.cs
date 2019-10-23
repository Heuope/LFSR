using System.IO;

namespace TryOnWpfCs
{
    internal class Lfsr : IFieldsCipher
    {
        public static Lfsr Create(string key, string loadFilePath, string saveFilePath)
        {
            var initialFile = File.ReadAllBytes(loadFilePath);
            var lfsrKey = CipherHelpers.GenerateKey(key, initialFile.Length, new[] { 27 });
            var cipherFile = CipherHelpers.GenerateCipher(initialFile, lfsrKey);
            var lfsr = new Lfsr(lfsrKey, initialFile, cipherFile);
            File.WriteAllBytes(saveFilePath, lfsr.CipherFile);
            return lfsr;
        }

        private Lfsr(byte[] key, byte[] initialFile, byte[] cipherFile)
        {
            Key = key;
            InitialFile = initialFile;
            CipherFile = cipherFile;
        }

        public byte[] Key { get; }
        public byte[] InitialFile { get; }
        public byte[] CipherFile { get; }
    }
}
