using Microsoft.AspNetCore.DataProtection;

public class CookieProtector : IDataProtector
{
    public IDataProtector CreateProtector(string purpose)
    {
        return new CookieProtector();
    }

    public byte[] Protect(byte[] plaintext)
    {
        return plaintext;
    }

    public byte[] Unprotect(byte[] protectedData)
    {
        return protectedData;
    }
}