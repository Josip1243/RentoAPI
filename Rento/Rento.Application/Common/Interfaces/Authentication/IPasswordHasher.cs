namespace Rento.Application.Common.Interfaces.Authentication
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string hash, string password);
    }
}
