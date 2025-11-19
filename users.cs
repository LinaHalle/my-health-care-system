
namespace App;

class User
{
    public string SSN;
    string _password;
    public string Name;
    public UserStatus Status;
    public UserRole Role;

    public List<Permission> Permissions = new();

    public User(string ssn, string password, string name, UserRole role, UserStatus status)
    {
        SSN = ssn;
        Name = name;
        _password = password;
        Status = status;
        Role = role;

    }

    public bool Trylogin(string ssn, string password)
    {
        return ssn == SSN && password == _password;
    }

    public bool IsAllowed(Permission permission)
    {
        return Permissions.Contains(permission);
    }

    public void HandlePermission(Permission permission)
    {
        if (Permissions.Contains(permission))
        {
            Permissions.Remove(permission);
        }
        else
        {
            Permissions.Add(permission);
        }
    }

    public enum UserStatus
    {
        pending,
        declined,
        accepted,
    }

    public enum UserRole
    {
        patient,
        personell,
        admin,
    }

}