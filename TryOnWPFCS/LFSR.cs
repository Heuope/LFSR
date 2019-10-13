using System;
using System.Collections.Generic;

namespace TryOnWPFCS
{
    class LFSR
    {   
        public byte[] Key { get; set; }
        public byte[] InitialFile { get; set; }
        public byte[] CipherFile { get; set; }

        private const int BitInByte = 8;

        public void Start(string key, string loadFilePath)
        {            
            InitialFile = System.IO.File.ReadAllBytes(loadFilePath);
            Key =  KeyGenerator(key, InitialFile.Length, new int[] { 27 });            
            CipherFile = GenerateCipher(InitialFile, Key);            
        }

        public void SaveFile(string filePathSave)
        {         
            System.IO.File.WriteAllBytes(filePathSave, CipherFile);
        }

        public byte[] KeyGenerator(string keyStream, Int64 sizeofInitialFile, int[] xorElements)
        {            
            var key = new List<byte>();

            if (keyStream.Length >= sizeofInitialFile * BitInByte)
            {
                for (int i = 0; i < sizeofInitialFile * BitInByte; i += BitInByte)
                    key.Add(Convert.ToByte(keyStream.Substring(i, BitInByte), 2));

                return key.ToArray();
            }

            var elementLFSR = new List<byte>(); // LFSR element

            // initialize element                                   

            for (int i = 0; i < keyStream.Length; i++)
                elementLFSR.Add(byte.Parse(keyStream[i].ToString()));

            byte lastNumber;          
            byte calculatedElement = 0;            
            int bitCounter = 0;

            for (Int64 i = 0; i < sizeofInitialFile * BitInByte; i++)
            {                
                calculatedElement = (byte)(calculatedElement << 1);
                calculatedElement = (byte)(calculatedElement & 0b_1111_1110);
                calculatedElement = (byte)(calculatedElement | elementLFSR[0]);

                bitCounter++;
                if (bitCounter == BitInByte)
                {
                    key.Add(calculatedElement);                   
                    bitCounter = 0;
                }

                lastNumber = elementLFSR[0];

                foreach (int index in xorElements)
                    lastNumber = (byte)(lastNumber ^ elementLFSR[index]);

                elementLFSR.RemoveAt(0);

                elementLFSR.Add(lastNumber);
            }

            return key.ToArray();
        }      

        public byte[] GenerateCipher(byte[] initialFile, byte[] key)
        {
            var cipherFile = new byte[initialFile.Length];
                       
            for (int i = 0; i < initialFile.Length; i++)
                cipherFile[i] = (byte)(initialFile[i] ^ key[i]);

            return cipherFile;
        }
    }
}
