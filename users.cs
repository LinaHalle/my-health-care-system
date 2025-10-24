using System.Security;

namespace App;

class User
{
    public string SSN;
    public string Name;
    string _password;

    public User(string ssn, string name, string password)
    {
        SSN = ssn;
        Name = name;
        _password = password;
    }

    public bool Trylogin(string ssn, string password)
    {
        return ssn == SSN && password == _password;
    }

}