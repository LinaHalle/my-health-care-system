using System.Diagnostics;
using System.Net;
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

users[1].Permissions.Add(Permission.AcceptOrDenyAppointments);
users[1].Permissions.Add(Permission.RegisterAppointment);
users[1].Permissions.Add(Permission.ModifyAppointments);




users[3].Permissions.Add(Permission.RequestAppointment);
users[3].Permissions.Add(Permission.ViewMyJournal);
users[3].Permissions.Add(Permission.ViewAppointments);

List<Appointment> appointments = new();




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
                case Permission.ViewMyJournal:
                    menuText += "View my journal";
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
                //creating an user that is accepted (staff, admin, patient)
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
                tryClear();
                Console.WriteLine("=== Your Appointments ===");
                List<Appointment> myAppointments = appointments.Where(a => a.Patient == active_user).ToList();

                if (myAppointments.Count == 0)
                {
                    Console.WriteLine("You have no appointments yet.");
                }
                else
                {
                    foreach (Appointment a in myAppointments)
                    {
                        Console.WriteLine(a);
                    }
                }
                Console.WriteLine("Press ENTER to go back");
                Console.ReadLine();
                break;

            case Permission.RequestAppointment:
                tryClear();
                Console.WriteLine("=== Request an appointment ===");
                Console.WriteLine("Available medical staff:");

                List<User> availableStaff = users.Where(u => u.Role == User.UserRole.personell).ToList();

                for (int i = 0; i < availableStaff.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] - {availableStaff[i].Name}");
                }
                Console.Write("Select which medical staff you want to meet (number): ");
                string? staffChoice = Console.ReadLine();
                Debug.Assert(staffChoice != null);
                if (int.TryParse(staffChoice, out int staffIndex) && staffIndex > 0 && staffIndex <= availableStaff.Count)
                {
                    User selectedStaff = availableStaff[staffIndex - 1];
                    Console.Write("Enter date for appointment (YYYY-MM-DD): ");
                    String? dateInput = Console.ReadLine();

                    if (DateTime.TryParse(dateInput, out DateTime date))
                    {
                        Appointment newAppointment = new Appointment(active_user, selectedStaff, date);
                        appointments.Add(newAppointment);

                        Console.WriteLine($"Appointment requested with {selectedStaff.Name} on {date.ToShortDateString()}.");
                        Console.WriteLine("Status: Waiting for approval");
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection");
                }

                Console.WriteLine("Press ENTER to go back");
                Console.ReadLine();
                break;

            case Permission.AcceptOrDenyAppointments:
                tryClear();
                Console.WriteLine("=== Pending appointment requests ===");
                List<Appointment> pending = appointments.Where(a => a.Personell == active_user && a.Status == Appointment.AppointmentStatus.Pending).ToList();
                if (pending.Count == 0)
                {
                    Console.WriteLine("No pending requests");
                }
                else
                {
                    for (int i = 0; i < pending.Count; i++)
                    {
                        Appointment appointment = pending[i];
                        Console.WriteLine($"[{i + 1}] - {appointment.Patient.Name} on {appointment.Date.ToShortDateString()}");
                    }
                    Console.Write("Select appointment to handle: ");
                    string? choice = Console.ReadLine();
                    Debug.Assert(choice != null);

                    if (int.TryParse(choice, out int appoIndex) && appoIndex > 0 && appoIndex <= pending.Count)
                    {
                        Appointment selected = pending[appoIndex - 1];
                        Console.WriteLine("1. Accept");
                        Console.WriteLine("2. Deny");
                        string? action = Console.ReadLine();
                        Debug.Assert(action != null);
                        if (action == "1")
                        {
                            selected.Status = Appointment.AppointmentStatus.Accepted;
                        }
                        else if (action == "2")
                        {
                            selected.Status = Appointment.AppointmentStatus.Denied;
                        }
                        Console.WriteLine("Updated successfully!");
                    }
                }

                Console.WriteLine("Press ENTER to go back");
                Console.ReadLine();
                break;

            case Permission.RegisterAppointment:
                tryClear();
                Console.WriteLine("=== Register a booking ===");
                Console.WriteLine("Here's all your patients");
                List<User> patients = users.Where(u => u.Role == User.UserRole.patient).ToList();
                if (patients.Count == 0)
                {
                    Console.WriteLine("There's no patients listed here");
                }
                else
                {
                    for (int i = 0; i < patients.Count; i++)
                    {
                        Console.WriteLine($"[{i + 1}] - {patients[i].Name} - [{patients[i].SSN}]");
                    }
                    Console.WriteLine("Select which patient you want to schedule an appointment");
                    string? choice = Console.ReadLine();
                    Debug.Assert(choice != null);
                    if (int.TryParse(choice, out int result) && result > 0 && result <= patients.Count)
                    {
                        User selectedP = patients[result - 1];
                        Console.Write("Enter the date you want to book (YYYY-MM-DD): ");
                        string? dateInput = Console.ReadLine();
                        Debug.Assert(dateInput != null);
                        if (DateTime.TryParse(dateInput, out DateTime date))
                        {
                            Appointment appointment = new Appointment(selectedP, active_user, date);
                            appointments.Add(appointment);
                            appointment.Status = Appointment.AppointmentStatus.Accepted;
                            Console.WriteLine($"Appointment registerd with patient: {selectedP.Name} on {date.ToShortDateString()}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid date input");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection of patient");
                    }
                }
                Console.WriteLine("Press ENTER to go back");
                Console.ReadLine();
                break;

            case Permission.ModifyAppointments:
                Console.WriteLine("=== Modify appointment ===");
                Console.WriteLine("Here's all your accepted patient appointments");
                List<Appointment> appointments1 = appointments.Where(a => a.Personell == active_user && a.Status == Appointment.AppointmentStatus.Accepted).ToList();
                if (appointments1.Count == 0)
                {
                    Console.WriteLine("You have no appointments yet");
                }
                else
                {
                    for (int i = 0; i < appointments1.Count; i++)
                    {
                        Console.WriteLine($"[{i + 1}] - {appointments1[i].Patient.Name} on {appointments1[i].Date.ToShortDateString()}");
                    }
                    Console.Write("Select appointment to modify (number): ");
                    string? appointmentChoice = Console.ReadLine();
                    Debug.Assert(appointmentChoice != null);
                    if (int.TryParse(appointmentChoice, out int result) && result > 0 && result <= appointments1.Count)
                    {
                        Appointment appointment = appointments1[result - 1];
                        Console.WriteLine("1. Change date");
                        Console.WriteLine("2. Cancel appointment");
                        //Console.WriteLine("3. Change time");
                        string? choice = Console.ReadLine();
                        Debug.Assert(choice != null);
                        if (choice == "1")
                        {
                            Console.Write("Select a new date (YYYY-MM-DD): ");
                            string? dateInput = Console.ReadLine();
                            Debug.Assert(dateInput != null);
                            if (DateTime.TryParse(dateInput, out DateTime date))
                            {
                                appointment.Date = date;
                                Console.WriteLine($"Date updated to: {dateInput}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid date input");
                            }
                        }
                        else if (choice == "2")
                        {
                            appointment.Status = Appointment.AppointmentStatus.Denied;
                            Console.WriteLine("Appointment status changed to denied");
                        }
                        else
                        {
                            Console.WriteLine("Invalid input");
                        }


                    }
                }
                Console.WriteLine("Press ENTER to go back");
                Console.ReadLine();
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