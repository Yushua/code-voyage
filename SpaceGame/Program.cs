using System;
using System.Diagnostics;
using System.Threading;
using SpaceGameNamespace;
class SpaceGame
{
    static readonly int RealTimeStep = 2000; // in milliseconds (1 second)
    static int GameTimeStep = 10; // Each step represents 10 minutes of game time.
    static int gameTimeStep = 0;

    static int year = 0;
    static int month = 0;
    static int day = 0;
    static int hour = 0;
    static int minute = 0;

    static void Main()
    {
        Console.WriteLine("---Starting game loop---");
        Console.WriteLine("---create users---");
        List<User> users = createCharacters(5);
        Console.WriteLine("---finished creating---");
        while (true)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                ProcessGameTick();
                FTProcessGameTick(stopwatch);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }

            // Update game time step
            gameTimeStep++;
            
            // Call the function to update and display the current game time
            UpdateAndDisplayGameTime();
        }
    }

    static List<User> createCharacters(int numberOfUsers){
        List<User> users = new List<User>();

        // Generate users
        for (int i = 0; i < numberOfUsers; i++)
        {
            // Generate a random name for each user (can be improved)
            string name = $"UserFirstName{i}";
            string lastName = $"UserLastName{i}";

            User newUser = new User(name, lastName);
            users.Add(newUser);
        }

        // Display all users' information
        foreach (User user in users)
        {
            user.DisplayUserInfo();
        }
        return users;
    }
    // Game logic processing (everything that happens each 'tick')
    static void ProcessGameTick()
    {
        // Console.WriteLine($"Game time advances by {GameTimeStep} minutes.");

        // Example of workload that may vary
        Random random = new Random();
        int workDuration = random.Next(500, 1500); // Random workload between 500ms and 1500ms
        Thread.Sleep(workDuration); // Simulate work that takes time (replace this with actual logic)

        // Console.WriteLine($"Work took {workDuration}ms.");
    }

    // Function that processes the time step
    static void FTProcessGameTick(Stopwatch stopwatch)
    {
        stopwatch.Stop();
        int timeRemaining = RealTimeStep - (int)stopwatch.ElapsedMilliseconds;
        if (timeRemaining > 0)
        {
            Thread.Sleep(timeRemaining);
        }
    }

    // Function to update and display the game time
    static void UpdateAndDisplayGameTime()
    {
        // Update the minutes based on GameTimeStep
        minute += GameTimeStep;

        // Handle the overflow for minutes, hours, days, and months
        if (minute >= 60)
        {
            minute = 0;
            hour++;
        }
        if (hour >= 24)
        {
            hour = 0;
            day++;
        }
        if (day >= 30) // Assuming 30 days in a month for simplicity
        {
            day = 0;
            month++;
        }
        if (month >= 12)
        {
            month = 0;
            year++;
        }

        // Print out the updated game time
        Console.WriteLine($"Year: {year}, Month: {month}, Day: {day}, Hour: {hour}, Minute: {minute}");
    }
}
