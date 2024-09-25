using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

public class User
{
    public User(Guid id,
        string email,
        string password,
        string username,
        string fullName,
        string role,
        DateTimeOffset createdAt)
    {
        Id = id;
        Email = email;
        Username = username;
        FullName = fullName;
        Password = password;
        Role = role;
        CreatedAt = createdAt;
    }

    public User() { }

    public UserId Id { get; private set; }

    public Email Email { get; private set; }
    public Username Username { get; private set; }

    public FullName FullName { get; private set; }
    public Password Password { get; private set; }

    public Role Role { get; private set; }

    public Date CreatedAt { get; private set; }


}