using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryOnWPFCS
{
    interface IFieldsCipher
    {
        byte[] Key { get; set; }
        byte[] InitialFile { get; set; }
        byte[] CipherFile { get; set; }
        byte[] FirstKey { get; set; }
        byte[] SecondKey { get; set; }
        byte[] ThirdKey { get; set; }
    }
}
