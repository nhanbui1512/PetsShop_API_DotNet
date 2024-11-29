using BCrypt.Net;

public class PasswordService
{
    public string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.");

        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        if (string.IsNullOrEmpty(hashedPassword) || string.IsNullOrEmpty(password))
            return false;

        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
