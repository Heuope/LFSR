using System;
using System.Collections.Generic;

namespace TryOnWpfCs
{
    internal static class CipherHelpers
    {
        private const int BitInByte = 8;

        public static byte[] GenerateKey(string keyStream, long sizeofInitialFile, int[] xorElements)
        {
            var key = new List<byte>();

            if (keyStream.Length >= sizeofInitialFile * BitInByte)
            {
                for (var i = 0; i < sizeofInitialFile * BitInByte; i += BitInByte)
                {
                    key.Add(Convert.ToByte(keyStream.Substring(i, BitInByte), 2));
                }

                return key.ToArray();
            }

            var elementLfsr = new List<byte>(); // LFSR element

            // initialize element

            for (var i = 0; i < keyStream.Length; i++)
            {
                elementLfsr.Add(byte.Parse(keyStream[i].ToString()));
            }

            byte calculatedElement = 0;
            var bitCounter = 0;

            for (long i = 0; i < sizeofInitialFile * BitInByte; i++)
            {
                calculatedElement = (byte) (calculatedElement << 1);
                calculatedElement = (byte) (calculatedElement & 0b_1111_1110);
                calculatedElement = (byte) (calculatedElement | elementLfsr[0]);

                bitCounter++;
                if (bitCounter == BitInByte)
                {
                    key.Add(calculatedElement);
                    bitCounter = 0;
                }

                var lastNumber = elementLfsr[0];

                foreach (var index in xorElements)
                {
                    lastNumber = (byte) (lastNumber ^ elementLfsr[index]);
                }

                elementLfsr.RemoveAt(0);
                elementLfsr.Add(lastNumber);
            }

            return key.ToArray();
        }

        public static byte[] GenerateCipher(byte[] initialFile, byte[] key)
        {
            var cipherFile = new byte[initialFile.Length];

            for (var i = 0; i < initialFile.Length; i++)
            {
                cipherFile[i] = (byte) (initialFile[i] ^ key[i]);
            }

            return cipherFile;
        }
    }
}