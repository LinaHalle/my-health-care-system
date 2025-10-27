using System.Diagnostics;
using App;

User? active_user = null;

List<User> users = new();

bool running = true;

while (running)
{
    if (active_user == null)
    {
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
        Console.WriteLine("=== welcome to Lina's Health care ===");

        Console.ReadLine();
    }
}

static void tryClear()
{
    try { Console.Clear(); } catch { }
}