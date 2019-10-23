namespace TryOnWpfCs
{
    internal interface IFieldsCipher
    {
        byte[] Key { get; }
        byte[] InitialFile { get; }
        byte[] CipherFile { get; }
    }

    internal interface IGeffe : IFieldsCipher
    {
        byte[] FirstKey { get; }
        byte[] SecondKey { get; }
        byte[] ThirdKey { get; }
    }
}
