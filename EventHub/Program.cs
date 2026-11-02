using EventHub.Data;

namespace EventHub
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (EventHubDbContext context =
                   new EventHubDbContext())
            {
                context.Database.EnsureCreated();

                Console.WriteLine("EventHub Database Created Successfully!");
            }
        }
    }
}