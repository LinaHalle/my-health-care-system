namespace App;

public enum Region
{
    Skane,
    Uppsala,
    Norrbotten,
    Vasterbotten,
    Stockholm,
    Halland,
}

class Hospital
{
    public string Name;
    public Region Region;
    public List<Department> Departments = new();

    public Hospital(string name, Region region)
    {
        Name = name;
        Region = region;
    }
}

class Department
{
    public string Name;
    public Hospital? Hospital;
    public List<User> Personell = new();
    public List<User> Patients = new();

    public Department(string name)
    {
        Name = name;
    }
}