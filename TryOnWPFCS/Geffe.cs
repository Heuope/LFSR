using System.Collections.Generic;

namespace TryOnWPFCS
{
    class Geffe : LFSR
    {
        // x^29 + x^2 + 1
        // x^27 + x^8 + x^7 + x + 1 
        // x^37 + x^12 + x^10 + x^2 + 1

        public void Start(string firstKeyStream, string secondKeyStream, string thirdKeyStream, string loadFilePath)
        {                  
            InitialFile = System.IO.File.ReadAllBytes(loadFilePath);

            FirstKey = KeyGenerator(firstKeyStream, InitialFile.Length, new int[] { 27 });            
            SecondKey = KeyGenerator(secondKeyStream, InitialFile.Length, new int[] { 26, 19, 20 });            
            ThirdKey = KeyGenerator(thirdKeyStream, InitialFile.Length, new int[] { 25, 27, 35 });     

            Key = KeyGenerator(FirstKey, SecondKey, ThirdKey);

            CipherFile = GenerateCipher(InitialFile, Key);
        }       

        private byte[] KeyGenerator(byte[] firstKey, byte[] secondKey, byte[] thirdKey)
        {
            var key = new List<byte>();
            //
            //  (x1 and x2) or (not x1 and x3)
            //
            for (long i = 0; i < firstKey.Length; i++)
                key.Add(
                    (byte)
                    ((firstKey[i] & secondKey[i]) | (~firstKey[i] & thirdKey[i]))
                    );

            return key.ToArray();
        }        
    }
}
