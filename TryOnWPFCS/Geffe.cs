using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryOnWPFCS
{
    static class Geffe
    {
        // x^29 + x^2 + 1
        // x^27 + x^8 + x^7 + x + 1 
        // x^37 + x^12 + x^10 + x^2 + 1
        public static byte[] Key { get; set; }
        public static byte[] InitialFile { get; set; }
        public static byte[] CipherFile { get; set; }        

        public static void Start(string _firstKey, string _secondKey, string _thirdKey, string _loadFilePath)
        {                  
            InitialFile = System.IO.File.ReadAllBytes(_loadFilePath);

            byte[] _fKey, _sKey, _tKey;
            _fKey = LFSR.KeyGenerator(_firstKey, InitialFile.Length, new int[] { 27 });
            _sKey = LFSR.KeyGenerator(_secondKey, InitialFile.Length, new int[] { 26, 19, 20 });
            _tKey = LFSR.KeyGenerator(_thirdKey, InitialFile.Length, new int[] { 25, 27, 35 });

            Key = KeyGenerator(_fKey, _sKey, _tKey);

            CipherFile = LFSR.GenerateCipher(InitialFile, Key);
        }

        public static void SaveFile(string _saveFilePath)
        {            
            System.IO.File.WriteAllBytes(_saveFilePath, CipherFile);
        }

        private static byte[] KeyGenerator(byte[] firstKey, byte[] secondKey, byte[] thirdKey)
        {
            var _key = new List<byte>();
            byte temp;

            for (Int64 i = 0; i < firstKey.Length; i++)
            {
                temp = (byte)((firstKey[i] & secondKey[i]) | (~firstKey[i] & thirdKey[i]));
                _key.Add(temp);
            }

            return _key.ToArray();
        }        
    }
}
