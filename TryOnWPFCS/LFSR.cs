using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryOnWPFCS
{
    static class LFSR
    {   
        public static byte[] Key { get; set; }
        public static byte[] InitialFile { get; set; }
        public static byte[] CipherFile { get; set; }

        private const int BitInByte = 8;

        public static void Start(string _key, string _loadFilePath)
        {            
            InitialFile = System.IO.File.ReadAllBytes(_loadFilePath);
            Key =  KeyGenerator(_key, InitialFile.Length, new int[] { 27 });            
            CipherFile = GenerateCipher(InitialFile, Key);            
        }

        public static void SaveFile(string _filePathSave)
        {         
            System.IO.File.WriteAllBytes(_filePathSave, CipherFile);
        }

        public static byte[] KeyGenerator(string keyStream, Int64 sizeofInitialFile, int[] xorElements)
        {            
            var _key = new List<byte>();

            if (keyStream.Length >= sizeofInitialFile * BitInByte)
            {
                for (int i = 0; i < sizeofInitialFile * BitInByte; i += BitInByte)
                    _key.Add(Convert.ToByte(keyStream.Substring(i, BitInByte), 2));

                return _key.ToArray();
            }

            var _elementLFSR = new List<byte>(); // LFSR element

            // initialize element                                   

            for (int i = 0; i < keyStream.Length; i++)
                _elementLFSR.Add(byte.Parse(keyStream[i].ToString()));

            byte _lastNumber;          
            int z = 0; // counter bit in byte
            byte _byteInKey = 0;            

            for (Int64 i = 0; i < sizeofInitialFile * BitInByte; i++)
            {                
                _byteInKey = (byte)(_byteInKey << 1);
                _byteInKey = (byte)(_byteInKey & 0b_1111_1110);
                _byteInKey = (byte)(_byteInKey | _elementLFSR[0]);

                z++;
                if (z == BitInByte)
                {
                    _key.Add(_byteInKey);                   
                    z = 0;
                }

                _lastNumber = _elementLFSR[0];

                foreach (var item in xorElements)
                    _lastNumber = (byte)(_lastNumber ^ _elementLFSR[item]);

                _elementLFSR.RemoveAt(0);

                _elementLFSR.Add(_lastNumber);
            }

            return _key.ToArray();
        }      

        public static byte[] GenerateCipher(byte[] _initialFile, byte[] _key)
        {
            var _cipherFile = new byte[_initialFile.Length];
                       
            for (int i = 0; i < _initialFile.Length; i++)
                _cipherFile[i] = (byte)(_initialFile[i] ^ _key[i]);

            return _cipherFile;
        }
    }
}
