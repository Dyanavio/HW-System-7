namespace HW_System_7
{
    internal class Program
    {
        const int MaxCapacitance = 16;
        static int onStation;
        static AutoResetEvent autoResetEvent = new AutoResetEvent(true);
        static ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        static Random random = new Random();
        public static void BusStation(string bus, ConsoleColor color)
        {
            int freePlaces = random.Next(0, MaxCapacitance + 1); // places that are free

            while(!manualResetEvent.WaitOne(0))
            {
                autoResetEvent.WaitOne();

                int taken = 0;
                if (onStation > freePlaces)
                {
                    taken = freePlaces;
                    onStation -= freePlaces;
                    freePlaces = 0;
                }
                else
                {
                    taken = onStation;
                    freePlaces -= taken;
                    onStation = 0;
                }
                Console.ForegroundColor = color;
                Console.WriteLine($"{bus} arrived at the station and took {taken} passengers. For now {onStation} remained");
                int added = random.Next(10, 20);
                onStation += added;
                Console.WriteLine($"{added} people came to station. Now there are {onStation} waiting");
                freePlaces = random.Next(0, MaxCapacitance + 1);

                Thread.Sleep(2000);
                autoResetEvent.Set();
                
            }
        }
        static void Main(string[] args)
        {
            Thread busThread1 = new Thread(() => BusStation("#168", ConsoleColor.Red));
            Thread busThread2 = new Thread(() => BusStation("#222", ConsoleColor.Green));
            Thread busThread3 = new Thread(() => BusStation("#175", ConsoleColor.DarkCyan));

            onStation = random.Next(10, 25);


            Console.WriteLine("Press Enter to end the day");

            busThread1.Start();
            busThread2.Start();
            busThread3.Start();

            Console.ReadLine();

            manualResetEvent.Set();

            Console.ResetColor();
            Console.WriteLine("Day will end soon\n\n\n");
        }
    }
}
