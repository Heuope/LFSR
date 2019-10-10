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

        private static void KeyGenerator(string keyStream)
        {        
            var _key = new List<byte>();

            var _elementLFSR = new byte[keyStream.Length]; // LFSR element

            // initialize element                        

            for (int i = 0; i < keyStream.Length; i++)
                _elementLFSR[i] = byte.Parse(keyStream[i].ToString());

            int _lastNumber;          
            int z = 0; // counter bit in byte
            byte _byteInKey = 0;            

            for (Int64 i = 0; i < InitialFile.Length * BitInByte; i++)
            {                
                _byteInKey = (byte)(_byteInKey << 1);
                _byteInKey = (byte)(_byteInKey & 0b_1111_1110);
                _byteInKey = (byte)(_byteInKey | _elementLFSR[0]);

                z++;
                if (z == 8)
                {
                    _key.Add(_byteInKey);                   
                    z = 0;
                }

                _lastNumber = _elementLFSR[0] ^ _elementLFSR[27];

                for (int j = 0; j < _elementLFSR.Length - 1; j++)
                    _elementLFSR[j] = _elementLFSR[j + 1];

                _elementLFSR[_elementLFSR.Length - 1] = (byte)_lastNumber;
            }

            Key = _key.ToArray();
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
