using System.Diagnostics;
using App;

User? active_user = null;

List<User> users = new();
users.Add(new("000", "000", "Lina", User.UserRole.admin, User.UserStatus.accepted)); //admin 
users.Add(new("111", "111", "David", User.UserRole.personell, User.UserStatus.accepted)); //staff
users.Add(new("555", "555", "Joel", User.UserRole.personell, User.UserStatus.accepted)); //staff
users.Add(new("222", "222", "Gustav", User.UserRole.patient, User.UserStatus.accepted)); //patient
users.Add(new("333", "333", "Harris", User.UserRole.patient, User.UserStatus.accepted)); //patient 
users.Add(new("444", "444", "Zomi", User.UserRole.patient, User.UserStatus.accepted)); //patient 


users[0].Permissions.Add(Permission.AddUser);
users[0].Permissions.Add(Permission.AddPermission);
users[0].Permissions.Add(Permission.AcceptOrDenyUser);




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
                    if (user.Trylogin(ssn, password) && user.Status == User.UserStatus.accepted)
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
                //requesting to be a patient, pending by deafult
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

                users.Add(new User(newSsn, newPassword, newName, User.UserRole.patient, User.UserStatus.pending));

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
                case Permission.AssignAdminRegion:
                    menuText += "Assign an Admin to a region";
                    break;
                case Permission.AddLocation:
                    menuText += "Add a new hospital";
                    break;
                case Permission.AcceptOrDenyUser:
                    menuText += "Accept or deny a user";
                    break;
                case Permission.ViewHospitalSchedule:
                    menuText += "View hospital schedule";
                    break;
                case Permission.ViewMySchedule:
                    menuText += "View my schedule";
                    break;
                case Permission.RequestAppointment:
                    menuText += "Request an appointment";
                    break;
                case Permission.RegisterAppointment:
                    menuText += "Register an appointment";
                    break;
                case Permission.AcceptOrDenyAppointments:
                    menuText += "Accept or deny appointment";
                    break;
                case Permission.ModifyAppointments:
                    menuText += "Modify an appointment";
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
                //creating an user that is accepted (staff, admin)
                tryClear();
                Console.Write("New User's SSN: ");
                string? newSsn = Console.ReadLine();
                Debug.Assert(newSsn != null);

                Console.Write("New user's name: ");
                string? newName = Console.ReadLine();
                Debug.Assert(newName != null);

                Console.Write("Create password: ");
                string? newPassword = Console.ReadLine();
                Debug.Assert(newPassword != null);

                Console.Write("New User's role? patient, personell or admin: ");
                string? newRole = Console.ReadLine();
                Debug.Assert(newRole != null);

                if (newRole == "patient")
                {
                    users.Add(new User(newSsn, newPassword, newName, User.UserRole.patient, User.UserStatus.accepted));
                }
                else if (newRole == "personell")
                {
                    users.Add(new User(newSsn, newPassword, newName, User.UserRole.personell, User.UserStatus.accepted));
                }
                else if (newRole == "admin")
                {
                    users.Add(new User(newSsn, newPassword, newName, User.UserRole.admin, User.UserStatus.accepted));
                }
                else
                {
                    Console.WriteLine("Wrong input, press ENTER to go back");
                    Console.ReadLine();
                    break;

                }
                tryClear();
                Console.WriteLine($"New {newRole} added! Press ENTER to continue");
                Console.ReadLine();
                break;

            case Permission.AddPermission:
                foreach (User user in users)
                {
                    Console.WriteLine($"[{user.SSN}] - {user.Name}");
                }
                Console.WriteLine("-----------------");
                Console.Write("Enter the users SSN: ");
                string? ssn = Console.ReadLine();
                Debug.Assert(ssn != null);
                User? userEdit = users.Find(u => u.SSN == ssn);
                if (userEdit != null)
                {
                    bool editing = true;
                    while (editing)
                    {
                        tryClear();
                        Console.WriteLine($"{userEdit.Name}'s permissions");
                        Permission[] permissionAll = Enum.GetValues<Permission>();
                        int optionMaxNum = 0;
                        for (int i = 0; i < permissionAll.Length; i++)
                        {
                            Permission permission = permissionAll[i];
                            if (permission != Permission.Logout && permission != Permission.Quit)
                            {
                                Console.WriteLine($"[{i + 1}] - {permission}\t is allowed: {userEdit.IsAllowed(permission)}");
                                optionMaxNum += 1;
                            }
                        }
                        Console.WriteLine($"[1 - {optionMaxNum}] - handle the permission");
                        Console.WriteLine("[f] - finish editing");
                        string? selectedOption = Console.ReadLine();
                        Debug.Assert(selectedOption != null);
                        if (selectedOption == "f")
                        {
                            editing = false;
                        }
                        else if (int.TryParse(selectedOption, out int selectedIndex))
                        {
                            Permission permission = permissionAll[selectedIndex - 1];
                            if (permission != Permission.Logout && permission != Permission.Quit)
                            {
                                userEdit.HandlePermission(permission);
                            }
                        }
                    }
                }
                break;

            case Permission.ViewPermissions:
                foreach (User user in users)
                {
                    Console.WriteLine(user.Name);
                    foreach (Permission permission in user.Permissions)
                    {
                        Console.WriteLine(permission);
                    }
                    Console.WriteLine("----------------------");
                }
                Console.WriteLine("Press ENTER to go back");
                Console.ReadLine();
                break;

            case Permission.ViewJournal:
                break;

            case Permission.WriteJournal:
                break;

            case Permission.ViewMyJournal:
                break;

            case Permission.ViewAppointments:
                break;

            case Permission.RequestAppointment:
                foreach (User user in users)
                {
                    if (user != active_user)
                    {
                        Console.WriteLine($"{user.Name}");
                    }
                }
                Console.WriteLine("Select which doctor you want to meet");
                break;

            case Permission.RegisterAppointment:
                break;

            case Permission.ModifyAppointments:
                break;

            case Permission.ViewMySchedule:
                break;

            case Permission.ViewHospitalSchedule:
                break;

            case Permission.AssignAdminRegion:
                break;

            case Permission.AddLocation:
                break;

            case Permission.AcceptOrDenyUser:
                foreach (User user in users)
                {
                    if (user.Status == User.UserStatus.pending)
                    {
                        Console.WriteLine(user.Name + ": " + user.SSN);
                    }
                }

                Console.WriteLine("Choose the SSN of the user you want to change status on");
                string? chosenssn = Console.ReadLine();
                Debug.Assert(chosenssn != null);

                User? selectedUser = null;

                foreach (User user1 in users)
                {
                    if (user1.SSN == chosenssn)
                    {
                        selectedUser = user1;
                        break;
                    }
                }

                if (selectedUser == null)
                {
                    tryClear();
                    Console.WriteLine("User not found, press ENTER to continue");
                    Console.ReadLine();
                }

                Console.WriteLine("Do you want to accept or deny user request?");
                Console.WriteLine("Press 1: To accept");
                Console.WriteLine("Press 2: To deny");
                string? aOrD = Console.ReadLine();
                Debug.Assert(aOrD != null);
                Debug.Assert(selectedUser != null);
                switch (aOrD)
                {
                    case "1":
                        selectedUser.Status = User.UserStatus.accepted;
                        Console.WriteLine("The user is now accepted, press ENTER to go back");
                        Console.ReadLine();
                        break;

                    case "2":
                        selectedUser.Status = User.UserStatus.declined;
                        Console.WriteLine("The user is now declined, press ENTER to go back");
                        Console.ReadLine();
                        break;
                }
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