using System.Diagnostics;

namespace asyncLearn
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");

            Task<Egg> eggsTask = FryEggs(2);
            Task<Bacon> baconTask = FryBacon(3);
            Task<Toast> toastTask = makeToast(2);

            Juice oj = PourOJ();
            Console.WriteLine("oj is ready");
            
            var breakfastTasks = new List<Task> { eggsTask, baconTask, toastTask };
            while (breakfastTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(breakfastTasks);
                if (finishedTask == eggsTask)
                {
                    Console.WriteLine("Eggs are ready");
                }
                else if (finishedTask == baconTask)
                {
                    Console.WriteLine("Bacon is ready");
                }
                else if (finishedTask == toastTask)
                {
                    Console.WriteLine("Toast is ready");
                }
                breakfastTasks.Remove(finishedTask);
            }
            Console.WriteLine("Breakfast is ready!");
            
            stopwatch.Stop();
            Console.WriteLine($"time is {stopwatch.ElapsedMilliseconds}");
        }

        private static async Task<Toast> makeToast(int slices)
        {
            Toast toast = await ToastBread(slices);
            ApplyButter(toast);
            ApplyJam(toast);
            return toast;
        }

        private static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        private static void ApplyJam(Toast toast) => 
            Console.WriteLine("Putting jam on the toast");

        private static void ApplyButter(Toast toast) => 
            Console.WriteLine("Putting butter on the toast");

        private static async Task<Toast> ToastBread(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }

        private static async Task<Bacon> FryBacon(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            await Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);
            Console.WriteLine("Put bacon on plate");

            return new Bacon();
        }

        private static async Task<Egg> FryEggs(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        private static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }
    }

    internal class Juice
    {
    }

    internal class Toast
    {
    }

    internal class Bacon
    {
    }

    internal class Egg
    {
    }

    internal class Coffee
    {
    }
}