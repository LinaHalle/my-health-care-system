using System.Diagnostics;
using System.Net;
using System.Security.Cryptography.X509Certificates;
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
users[0].Permissions.Add(Permission.AddLocation);
users[0].Permissions.Add(Permission.AddAdmin);
users[0].Permissions.Add(Permission.AddDepartment);




users[1].Permissions.Add(Permission.AcceptOrDenyAppointments);
users[1].Permissions.Add(Permission.RegisterAppointment);
users[1].Permissions.Add(Permission.ModifyAppointments);
users[1].Permissions.Add(Permission.WriteJournal);
users[1].Permissions.Add(Permission.ViewJournal);






users[3].Permissions.Add(Permission.RequestAppointment);
users[3].Permissions.Add(Permission.ViewMyJournal);
users[3].Permissions.Add(Permission.ViewAppointments);

List<Appointment> appointments = new();
List<JournalEntry> journals = new();
List<Hospital> hospitals = new();




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
                case Permission.AddAdmin:
                    menuText += "Add new Admin";
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
                case Permission.AddLocation:
                    menuText += "Add a new hospital";
                    break;
                case Permission.AddDepartment:
                    menuText += "Add a new department";
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
                {
                    //creating an user that is accepted (staff, admin, patient)
                    tryClear();
                    Console.WriteLine("=== Add New User ===");

                    Console.Write("New User's SSN: ");
                    string? newSsn = Console.ReadLine();
                    Debug.Assert(newSsn != null);

                    Console.Write("New User's Name: ");
                    string? newName = Console.ReadLine();
                    Debug.Assert(newName != null);

                    Console.Write("Create Password: ");
                    string? newPassword = Console.ReadLine();
                    Debug.Assert(newPassword != null);

                    Console.Write("New User's role? patient or personell: ");
                    string? newRoleInput = Console.ReadLine();
                    Debug.Assert(newRoleInput != null);

                    User.UserRole newRole;
                    if (newRoleInput.ToLower() == "patient") newRole = User.UserRole.patient;
                    else if (newRoleInput.ToLower() == "personell") newRole = User.UserRole.personell;

                    else
                    {
                        GoBack();
                        break;
                    }

                    User newUser = new User(newSsn.Trim(), newPassword.Trim(), newName.Trim(), newRole, User.UserStatus.accepted);

                    if (newRole == User.UserRole.personell)
                    {
                        Hospital? selectedHospital = SelectHospital(hospitals);
                        if (selectedHospital == null) break;

                        newUser.Hospital = selectedHospital;

                        if (selectedHospital.Departments.Count > 0)
                        {
                            Console.WriteLine("Select departments (comma separated numbers), or leave empty for none:");
                            for (int i = 0; i < selectedHospital.Departments.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {selectedHospital.Departments[i].Name}");
                            }

                            string? inputDeps = Console.ReadLine();
                            if (!string.IsNullOrEmpty(inputDeps))
                            {
                                string[] parts = inputDeps.Split(',');
                                for (int i = 0; i < parts.Length; i++)
                                {
                                    int depIndex;
                                    if (int.TryParse(parts[i].Trim(), out depIndex) && depIndex >= 1 && depIndex <= selectedHospital.Departments.Count)
                                    {
                                        Department dep = selectedHospital.Departments[depIndex - 1];
                                        newUser.Departments.Add(dep);
                                        dep.Personell.Add(newUser);
                                    }
                                }
                            }
                        }
                    }
                    else if (newRole == User.UserRole.patient)
                    {
                        Hospital? selectedHospital = SelectHospital(hospitals);
                        if (selectedHospital == null) break;

                        newUser.Hospital = selectedHospital;
                        selectedHospital.Patients.Add(newUser);
                    }

                    users.Add(newUser);

                    tryClear();
                    Console.WriteLine($"New {newRole} '{newUser.Name}' added! Press ENTER to continue");
                    Console.ReadLine();
                    break;
                }
            case Permission.AddAdmin:
                {
                    tryClear();
                    Console.WriteLine("=== Add Admin ===");

                    Console.Write("New Admin's SSN: ");
                    string? newSsn = Console.ReadLine();
                    Debug.Assert(newSsn != null);

                    Console.Write("New Admin's Name: ");
                    string? newName = Console.ReadLine();
                    Debug.Assert(newName != null);

                    Console.Write("Create Password: ");
                    string? newPassword = Console.ReadLine();
                    Debug.Assert(newPassword != null);

                    Console.WriteLine("Select Admin type:");
                    Console.WriteLine("1. Region Admin");
                    Console.WriteLine("2. Hospital Admin");

                    string? adminTypeInput = Console.ReadLine();
                    int adminTypeIndex;
                    if (!int.TryParse(adminTypeInput, out adminTypeIndex) || (adminTypeIndex != 1 && adminTypeIndex != 2))
                    {
                        Console.WriteLine("Invalid input");
                        GoBack();
                        break;
                    }

                    User newAdmin = new User(newSsn.Trim(), newPassword.Trim(), newName.Trim(), User.UserRole.admin, User.UserStatus.accepted);

                    if (adminTypeIndex == 1)
                    {
                        Region[] Regions = Enum.GetValues<Region>();

                        Console.WriteLine("Select Region:");
                        for (int i = 0; i < Regions.Length; i++)
                        {
                            Console.WriteLine($"{i + 1}. {Regions[i]}");
                        }
                        string? regionInput = Console.ReadLine();
                        int regionIndex;
                        if (!int.TryParse(regionInput, out regionIndex) || regionIndex < 1 || regionIndex > Regions.Length)
                        {
                            Console.WriteLine("Invalid input");
                            GoBack();
                            break;
                        }
                        newAdmin.Region = Regions[regionIndex - 1];
                        newAdmin.Hospital = null;
                    }
                    else
                    {
                        Hospital? selectedHospital = SelectHospital(hospitals);
                        if (selectedHospital == null) break;

                        newAdmin.Hospital = selectedHospital;
                        newAdmin.Region = null;
                    }

                    users.Add(newAdmin);

                    tryClear();
                    Console.WriteLine($"New Admin '{newAdmin.Name}' added successfully!");
                    GoBack();
                    break;
                }

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
                tryClear();
                Console.WriteLine("=== View journal entries ===");
                List<JournalEntry> writtenJournals = journals.Where(j => j.Author == active_user).ToList();

                if (writtenJournals.Count == 0)
                {
                    Console.WriteLine("You've not written any journalentries yet, contact department admin if you need approval for a patients journal");
                    Console.WriteLine("Press ENTER to go back");
                    Console.ReadLine();
                    break;
                }

                List<User> journalPatients = writtenJournals.Select(j => j.Patient).Distinct().ToList();

                for (int i = 0; i < journalPatients.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] - {journalPatients[i].Name} (SSN: {journalPatients[i].SSN})");
                }
                Console.Write("Select patient (number): ");
                string? choosenP = Console.ReadLine();
                Debug.Assert(choosenP != null);
                if (int.TryParse(choosenP, out int choosenIndex) && choosenIndex > 0 && choosenIndex <= journalPatients.Count)
                {
                    User selectedPatient = journalPatients[choosenIndex - 1];

                    List<JournalEntry> patientJournals = writtenJournals.Where(j => j.Patient == selectedPatient).ToList();

                    if (patientJournals.Count == 0)
                    {
                        Console.WriteLine("No journal entries found for the selected patient");
                        GoBack();
                        break;

                    }
                    else
                    {
                        Console.WriteLine($"\n=== Journal entries for {selectedPatient.Name} ===");
                        for (int i = 0; i < patientJournals.Count; i++)
                        {
                            JournalEntry entry = patientJournals[i];
                            Console.WriteLine($"\nDate: {entry.Date}");
                            Console.WriteLine($"\nAuthor: {entry.Author.Name}");
                            Console.WriteLine($"\nNote: {entry.Note}");
                            Console.WriteLine("----------------------------");

                        }
                    }
                    GoBack();

                }
                else
                {
                    Console.WriteLine("Invalid selection");
                    GoBack();
                }
                break;

            case Permission.WriteJournal:
                tryClear();
                Console.WriteLine("=== Write Journal entries ===");
                List<Appointment> myPatientsAppointments = appointments.Where(a => a.Personell == active_user && a.Status == Appointment.AppointmentStatus.Accepted).ToList();

                if (myPatientsAppointments.Count == 0)
                {
                    Console.WriteLine("You currently have no patients with accepted appointments");
                    GoBack();
                    break;
                }
                List<User> myPatients = myPatientsAppointments.Select(a => a.Patient).Distinct().ToList();

                for (int i = 0; i < myPatients.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] - {myPatients[i].Name}");
                }
                Console.Write("Select patient (number): ");
                string? choiceP = Console.ReadLine();
                Debug.Assert(choiceP != null);
                if (int.TryParse(choiceP, out int choiceIndex) && choiceIndex > 0 && choiceIndex <= myPatients.Count)
                {
                    User selectedPatient = myPatients[choiceIndex - 1];
                    Console.WriteLine($"Writing in {selectedPatient.Name}'s Journal.");
                    Console.WriteLine("Enter your note:");
                    string? note = Console.ReadLine();
                    Debug.Assert(note != null);
                    journals.Add(new JournalEntry(selectedPatient, active_user, note));
                    Console.WriteLine("Note added succesfully!");

                }
                else
                {
                    Console.WriteLine("Invalid choice");
                }
                GoBack();
                break;

            case Permission.ViewMyJournal:
                tryClear();
                Console.WriteLine("=== My journal entries");
                List<JournalEntry> myEntries = journals.Where(j => j.Patient == active_user).ToList();
                if (myEntries.Count == 0)
                {
                    Console.WriteLine("You have no journal entries yet");
                }
                else
                {
                    for (int i = 0; i < myEntries.Count; i++)
                    {
                        JournalEntry entry = myEntries[i];
                        Console.WriteLine($"[{i + 1}]. {entry}");
                    }
                }
                GoBack();
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
                GoBack();
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
                    Console.Write("Enter date for appointment (YYYY-MM-DD HH:mm): ");
                    String? dateInput = Console.ReadLine();

                    if (DateTime.TryParseExact(dateInput, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime date))
                    {
                        TimeSpan appointmentLength = TimeSpan.FromHours(1);
                        bool timeTaken = false;
                        foreach (Appointment appt in appointments)
                        {
                            if (appt.Personell == selectedStaff && appt.Status != Appointment.AppointmentStatus.Denied)
                            {
                                DateTime existingStart = appt.Date;
                                DateTime existingEnd = appt.Date.Add(appointmentLength);
                                DateTime newStart = date;
                                DateTime newEnd = date.Add(appointmentLength);

                                bool overlap = newStart < existingEnd && newEnd > existingStart;
                                if (overlap)
                                {
                                    timeTaken = true;
                                    break;
                                }

                            }
                        }
                        if (timeTaken)
                        {
                            Console.WriteLine($"{selectedStaff.Name} is already booked within that hour. Try another time.");
                        }
                        else
                        {
                            Appointment newAppointment = new Appointment(active_user, selectedStaff, date);
                            appointments.Add(newAppointment);

                            Console.WriteLine($"Appointment requested with {selectedStaff.Name} on {date:yyyy-MM-dd HH:mm}. (1 hour)");
                            Console.WriteLine("Status: Waiting for approval");
                        }

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

                GoBack();
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

                GoBack();
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
                        Console.Write("Enter the date you want to book (YYYY-MM-DD HH:mm): ");
                        string? dateInput = Console.ReadLine();
                        Debug.Assert(dateInput != null);
                        if (DateTime.TryParseExact(dateInput, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime date))
                        {
                            Appointment appointment = new Appointment(selectedP, active_user, date);
                            appointments.Add(appointment);
                            appointment.Status = Appointment.AppointmentStatus.Accepted;
                            Console.WriteLine($"Appointment registerd with patient: {selectedP.Name} on {date:yyyy-MM-dd HH:mm}. (1 hour)");
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
                GoBack();
                break;

            case Permission.ModifyAppointments:
                tryClear();
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
                        Console.WriteLine($"[{i + 1}] - {appointments1[i].Patient.Name} on {appointments1[i].Date:yyyy-MM-dd HH:mm}");
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
                            Console.Write("Select a new date and time (YYYY-MM-DD HH:mm): ");
                            string? dateInput = Console.ReadLine();
                            Debug.Assert(dateInput != null);
                            if (DateTime.TryParseExact(dateInput, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime date))
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
                GoBack();
                break;

            case Permission.ViewMySchedule:
                break;

            case Permission.ViewHospitalSchedule:
                break;

            case Permission.AddLocation:
                tryClear();
                Console.WriteLine("Choose region: ");
                Region[] allRegions = Enum.GetValues<Region>();

                for (int i = 0; i < allRegions.Length; i++)
                {
                    Console.WriteLine($"[{i + 1}]. {allRegions[i]}");
                }
                string? choosenRegion = Console.ReadLine();
                Debug.Assert(choosenRegion != null);

                int myIndex;
                bool success = int.TryParse(choosenRegion, out myIndex);

                if (!success || myIndex < 1 || myIndex > allRegions.Length)
                {
                    Console.WriteLine("Invalid input");
                    GoBack();
                    break;
                }

                Region selectedRegion = allRegions[myIndex - 1];

                Console.WriteLine("Write the name of the new hospital:");
                string? hospitalName = Console.ReadLine();
                Debug.Assert(hospitalName != null);

                Hospital newHospital = new Hospital(hospitalName, selectedRegion);
                hospitals.Add(newHospital);

                Console.WriteLine($"Hospital '{hospitalName}' added to {selectedRegion}");
                GoBack();
                break;

            case Permission.AddDepartment:
                {
                    tryClear();
                    Console.WriteLine("=== Add Department ===");
                    Hospital selectedHospital = SelectHospital(hospitals);
                    if (selectedHospital == null) break;

                    Console.WriteLine("Write the name of the new Department");
                    string? departmentName = Console.ReadLine();
                    if (departmentName == null || departmentName.Trim().Length == 0)
                    {
                        Console.WriteLine("Invalid input");
                        GoBack();
                        break;
                    }
                    Department department = new Department(departmentName.Trim(), selectedHospital);
                    selectedHospital.Departments.Add(department);
                    Console.WriteLine($"Department '{department.Name}' added to hospital '{selectedHospital}'");
                    GoBack();
                    break;
                }


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

static void GoBack()
{
    Console.WriteLine("Press ENTER to go back to main menu");
    Console.ReadLine();
}

static Hospital SelectHospital(List<Hospital> hospitals)
{
    if (hospitals.Count == 0)
    {
        Console.WriteLine("No hospitals found. Add a hospital first.");
        Console.WriteLine("Press ENTER to go back.");
        Console.ReadLine();
        return null; // eller throw om du föredrar det
    }

    Console.WriteLine("Select a hospital:");
    for (int i = 0; i < hospitals.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {hospitals[i].Name} ({hospitals[i].Region})");
    }

    string? input = Console.ReadLine();
    int index;

    if (!int.TryParse(input, out index) || index < 1 || index > hospitals.Count)
    {
        Console.WriteLine("Invalid input.");
        Console.WriteLine("Press ENTER to go back.");
        Console.ReadLine();
        return null;
    }

    return hospitals[index - 1];
}
