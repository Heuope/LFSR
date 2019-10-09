using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryOnWPFCS
{
    static class LFSR
    {
        // x^29 + x^2 + 1   
        public static byte[] Key;
        public static byte[] InitialFile;
        public static byte[] CipherFile;
        private const int BitInByte = 8;

        public static void MainPoint(string _key, string _filePathLoad, string _filePathSave)
        {
            InitialFile = System.IO.File.ReadAllBytes(_filePathLoad);
            KeyGenerator(_key);
            GenerateCipher();
            System.IO.File.WriteAllBytes(_filePathSave, CipherFile);
        }

        static private void KeyGenerator(string keyStream)
        {
            var _key = new byte[InitialFile.Length];

            //
            // TODO create block for small files without using generator 
            //

            var _keyStream = new byte[keyStream.Length];

            for (int i = 0; i < keyStream.Length; i++)
                _keyStream[i] = byte.Parse(keyStream[i].ToString());

            string _bitsKey = "";
            int _lastNumber;
            int k = 0; // index in _key

            for (Int64 i = 0; i < InitialFile.Length * BitInByte; i++)
            {
                _bitsKey += _keyStream[0].ToString();
                if (_bitsKey.Length == 8)
                {
                    _key[k++] = Convert.ToByte(_bitsKey, 2);
                    _bitsKey = "";
                }                   
                _lastNumber = _keyStream[0] ^ _keyStream[27];

                for (int j = 0; j < _keyStream.Length - 1; j++)
                    _keyStream[j] = _keyStream[j + 1];
                
                _keyStream[_keyStream.Length - 1] = (byte)_lastNumber;
            }

            Key = _key;
        }

        static private void GenerateCipher()
        {
            var _cipherFile = new byte[InitialFile.Length];
                       
            for (int i = 0; i < InitialFile.Length; i++)
                _cipherFile[i] = (byte)(InitialFile[i] ^ Key[i]);

            CipherFile = _cipherFile;
        }
    }
}
