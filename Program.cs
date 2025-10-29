using System.Diagnostics;
using App;

User? active_user = null;

List<User> users = new();
users.Add(new("000", "000", "Lina"));
users.Add(new("111", "111", "David"));

users[0].Permissions.Add(Permission.AddUser);
users[0].Permissions.Add(Permission.AddPermission);
users[0].Permissions.Add(Permission.AssignAdminRegion);
users[0].Permissions.Add(Permission.ViewAppointments);



bool running = true;

while (running)
{
    if (active_user == null)
    {
        tryClear();
        Console.WriteLine("=== Login page ===");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register as a patient");
        Console.WriteLine("3. Quit the program");
        string? loginInput = Console.ReadLine();

        switch (loginInput)
        {
            case "1":
                tryClear();
                Console.Write("SSN: ");
                string? ssn = Console.ReadLine();
                Console.Write("password: ");
                string? password = Console.ReadLine();

                Debug.Assert(ssn != null);
                Debug.Assert(password != null);


                foreach (User user in users)
                {
                    if (user.Trylogin(ssn, password))
                    {
                        active_user = user;
                        break;
                    }
                }
                if (active_user == null)
                {
                    Console.WriteLine("No matching user, try again or register an account, press ENTER to go back");
                    Console.ReadLine();
                }
                break;
            case "2":
                tryClear();
                Console.Write("Enter your SSN: ");
                string? newSsn = Console.ReadLine();

                Console.Write("Enter your name: ");
                string? newName = Console.ReadLine();

                Console.Write("Create a password: ");
                string? newPassword = Console.ReadLine();

                Debug.Assert(newSsn != null);
                Debug.Assert(newName != null);
                Debug.Assert(newPassword != null);

                users.Add(new User(newSsn, newName, newPassword));

                tryClear();
                Console.WriteLine("Account registerd succesfully, waiting for approval, press ENTER to go back");
                Console.ReadLine();
                break;

            case "3":
                running = false;
                break;
        }

    }
    else
    {
        tryClear();
        Console.WriteLine($"=== welcome {active_user.Name} to Lina's Health care ===");

        Dictionary<string, Permission> menuOptions = new();
        int index = 1;

        foreach (Permission permission in active_user.Permissions)
        {
            menuOptions[index.ToString()] = permission;
            string menuText = $"[{index}] - ";
            switch (permission)
            {
                case Permission.AddUser:
                    menuText += "Add new user";
                    break;
                case Permission.AddPermission:
                    menuText += "Add permissions to user";
                    break;
                case Permission.ViewPermissions:
                    menuText += "View permissions";
                    break;
                case Permission.ViewJournal:
                    menuText += "View journal";
                    break;
                case Permission.WriteJournal:
                    menuText += "Write in Journal";
                    break;
                case Permission.ViewAppointments:
                    menuText += "View appointments";
                    break;
                case Permission.ViewSchedule:
                    menuText += "View schedule";
                    break;
                case Permission.AssignAdminRegion:
                    menuText += "Assign an Admin to a region";
                    break;
                case Permission.AddLocation:
                    menuText += "Add a new hospital";
                    break;

            }
            Console.WriteLine(menuText);
            index += 1;
        }
        menuOptions[index.ToString()] = Permission.Logout;
        Console.WriteLine($"[{index}] - Logout");
        index += 1;
        menuOptions[index.ToString()] = Permission.Quit;
        Console.WriteLine($"[{index}] - Quit");

        string? input = Console.ReadLine();
        Debug.Assert(input != null);
        switch (menuOptions[input])
        {
            case Permission.AddUser:
                Console.WriteLine("The add user menu");
                Console.ReadLine();
                break;

            case Permission.AddPermission:
                break;

            case Permission.ViewPermissions:
                break;

            case Permission.ViewJournal:
                break;

            case Permission.WriteJournal:
                break;

            case Permission.ViewAppointments:
                break;

            case Permission.ViewSchedule:
                break;

            case Permission.AssignAdminRegion:
                break;

            case Permission.AddLocation:
                break;

            case Permission.Logout:
                active_user = null;
                break;

            case Permission.Quit:
                running = false;
                break;

        }
    }
}

static void tryClear()
{
    try { Console.Clear(); } catch { }
}