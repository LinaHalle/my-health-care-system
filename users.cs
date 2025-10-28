
namespace App;

class User
{
    public string SSN;
    string _password;
    public string Name;

    public List<Permission> Permissions = new();

    public User(string ssn, string password, string name)
    {
        SSN = ssn;
        Name = name;
        _password = password;
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

}