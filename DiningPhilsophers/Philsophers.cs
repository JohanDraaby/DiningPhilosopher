using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiningPhilsophers
{
    class Philsophers
    {
        public string Name { get; set; }
        public int LocationAtTable { get; set; }
        private int tempLocationAtTable { get; set; }
        private Random Random { get; set; } = new Random();

        public Philsophers(string name, int locationAtTable)
        {
            Name = name;
            LocationAtTable = locationAtTable;
        }

        public Philsophers()
        {
                
        }
        

        public void Work()
        {

            while (true)
            {
                try
                {
                    // Attempt to get the left fork..
                    if (Monitor.TryEnter(Program.Forks[LocationAtTable], 1500))
                    {
                        // Write message to screen..
                        Console.WriteLine("Philosopher {0} har fået venstre gaffel.", LocationAtTable);
                        Thread.Sleep(1500);

                        // If we got the left fork, try to grab the right fork as well.
                        if (Monitor.TryEnter(Program.Forks[LocationAtTable + 1], 1500))
                        {
                            // Write message to screen...
                            Console.WriteLine("Philosopher {0} Har fået sin højre gaffel.", LocationAtTable);
                            Thread.Sleep(1500);

                            // Set Philosopher to eat...
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.WriteLine("Philosopher {0} Har sat sig til at spise", LocationAtTable);
                            Thread.Sleep(Random.Next(1000, 5000));      // The Philosopher eat at random duration.
                            Console.ResetColor();
                        }
                    }
                    else if (Monitor.TryEnter(Program.Forks[LocationAtTable +1], 1500))
                    {
                        // Write message to screen...
                        Console.WriteLine("Philosopher {0} Har fået sin højre gaffel.", LocationAtTable);
                        Thread.Sleep(1500);

                        // If it succeded to take first fork try next one...
                        if (Monitor.TryEnter(Program.Forks[LocationAtTable], 1500))
                        {
                            // Write message to screen...
                            Console.WriteLine("Philosopher {0} Har fået sin venstre gaffel.", LocationAtTable);
                            Thread.Sleep(1500);

                            // Set philosopher to sleep...
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.WriteLine("Philosopher {0} Har sat sig til at spise.", LocationAtTable);
                            Thread.Sleep(Random.Next(1000, 5000));
                            Console.ResetColor();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
                finally
                {
                    // Check if fork are taken and lay it back on table...
                    if (Monitor.IsEntered(Program.Forks[LocationAtTable]))
                    {
                        // Lay the fork on table and write message...
                        Monitor.Exit(Program.Forks[LocationAtTable]);
                        Console.WriteLine("Philosopher {0} Er færdig med at spise og har lagt sin venstre gaffel.", LocationAtTable);
                        Thread.Sleep(1500);
                    }
                    if (Monitor.IsEntered(Program.Forks[LocationAtTable]))
                    {
                        // Lay the fork on table and write message...
                        Monitor.Exit(Program.Forks[LocationAtTable +1]);
                        Console.WriteLine("Philosopher {0} Er færdig med at spise og har lagt sin højre gaffel.", LocationAtTable);
                        Thread.Sleep(1500);
                    }
                }
            }
        }
    }
}
