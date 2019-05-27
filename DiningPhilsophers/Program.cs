using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DiningPhilsophers
{
    class Program
    {
        // Create a bool array of forks...
        public static bool[] Forks = new bool[5];       // Contains 5 forks - one for each philosopher..
        public static Philsophers[] Philsophers = new Philsophers[5];
        public static Thread[] Threads = new Thread[5];


        public static void Main(string[] args)
        {
            // Loop for each philosopher to add...
            for (int i = 0; i < 5; i++)
            {
                // Create new philosopher and add to Philosophers array...
                Philsophers[i] = new Philsophers
                {
                    Name = i.ToString(),
                    LocationAtTable = i
                    
                };

                try
                {
                    // Create thread for each philosophers work...
                    Threads[i] = new Thread(Philsophers[i].Work)
                    {
                        Name = $"Philosopher {i.ToString()}"
                    };
                }
                catch (Exception ex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
                
            }

            // Loop through created threads and start and join them...
            foreach (var thread in Threads)
            {
                thread.Start();     // Start the thread and do work..
                //thread.Join();      // Join the thread.
            }
        }
    }
}
