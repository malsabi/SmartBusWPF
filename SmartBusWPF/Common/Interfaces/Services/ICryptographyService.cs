namespace SmartBusWPF.Common.Interfaces.Services
{
    public interface ICryptographyService
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}