using System;
using System.Threading.Tasks;
using Alus.Client;
using Alus.Core.Models;
using static System.Console;

namespace Alus.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var client = new AlusClient();
            var feedback = new Feedback()
            {
                EMail = "andrius.bentkus@gmail.com",
                Text = "Cia kazkas blogai",
                Type = FeedbackType.Bugs
            };
            await client.AddAsync(feedback);

            foreach (var f in await client.GetFeedbackListAsync())
            {
                WriteLine($"{f.EMail}: ({f.Type}) {f.Text}");
            }
        }
    }
}
