namespace WebAuthExample.Data.Account;

public enum Role
{
    User,
    Admin
}

public class CustomUser
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserName { get; set; } = string.Empty;
    public string PreName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Role UserRole { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
}