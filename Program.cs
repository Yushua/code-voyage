using System;
using System.Diagnostics;
using System.Threading;

class SpaceGame
{
    static readonly int RealTimeStep = 1000; // in milliseconds (1 second)
    static int GameTimeStep = 60; // Game time increment in minutes (60 minutes per hour)
    static gameTimeStep = 0;
    static void Main()
    {
        Console.WriteLine("Starting game loop...");
        List<User> users = new List<User>();
        for (int i = 0; i < numberOfUsers; i++)
        {
            // Generate a random name for each user (can be improved)
            string name = $"UserFirstName{i}";
            string lastName = $"UserLastName{i}";

            User newUser = new User(name, lastName);
            users.Add(newUser);
        }
        foreach (User user in users)
        {
            user.DisplayUserInfo();
        }
        Console.WriteLine("finished creating users...");
        // Main game loop
        while (true)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                ProcessGameTick();
                FTProcessGameTick();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
            GameTimeStep++;
        }
    }

    // Game logic processing (everything that happens each 'tick')
    static void ProcessGameTick()
    {
        // Simulate some work - replace this with actual game logic
        Console.WriteLine($"Game time advances by {GameTimeStep} minutes.");

        // Example of workload that may vary
        Random random = new Random();
        int workDuration = random.Next(500, 1500); // Random workload between 500ms and 1500ms
        Thread.Sleep(workDuration); // Simulate work that takes time (replace this with actual logic)

        Console.WriteLine($"Work took {workDuration}ms.");
    }
    static void FTProcessGameTick(){
        stopwatch.Stop();
        if (stopwatch.ElapsedMilliseconds > RealTimeStep)
        {
            Console.WriteLine($"Error: Game tick took too long! {stopwatch.ElapsedMilliseconds}ms");
        }
        int timeRemaining = RealTimeStep - (int)stopwatch.ElapsedMilliseconds;
        if (timeRemaining > 0)
        {
            Thread.Sleep(timeRemaining);
        }
    }
}