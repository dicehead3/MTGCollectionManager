namespace Infrastructure.Encryption
{
    public interface IEncryptor
    {
        string MakeSha512Hash(string phrase);
    }
}
