using System;
using System.Linq;
using JotBotNg2Core.Models;

namespace JotBotNg2Core.Data
{
    public class DbInitializer
    {
        private static void Log(string message)
        {
            Console.ForegroundColor = System.ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ForegroundColor = System.ConsoleColor.White;
        }
        public static void Initialize(ApiDbContext context)
        {
            var creationTesting = false; // set false when done database creation
            if (creationTesting)
            {
                // dev-mode, kill database, then recreate
                context.Database.EnsureDeleted();
            }

            context.Database.EnsureCreated();

            if (creationTesting)
            {
                // testing...
                // seeding a few quicknotes
                var quickNotes = new QuickNote[]
                {
                new QuickNote{Name="Quick Note 1", Description="QuickNote1 with no spaces"},
                new QuickNote{Name="Quick Note 2", Description="Prepare for deletion!"}
                };
                foreach (var qn in quickNotes)
                {
                    context.Insert(qn);
                }
                context.SaveChanges();
                Log("Quick Notes Seeded!");

                Log("GetAll Test!!");
                var dbQuickNotes = context.GetAll<QuickNote>();
                Log($"Found: {dbQuickNotes.Count()} records");
                Log("---");

                Log("Find Test!!");
                var findQn = context.Find<QuickNote>(1);
                Log($"{findQn.Name}");
                Log("---");

                Log("Insert Test!!");
                var insQn = new QuickNote { Name = "Test Quick Note", Description = "Hoping for an insert!" };
                var dbQn = context.Insert(insQn);
                context.SaveChanges();
                Log($"Saved! {dbQn.Name}, Created date: {dbQn.CreatedDate}");
                Log("---");

                Log("Modify Test!!");
                var modQn = context.Find<QuickNote>(1);
                Log($"Description before modification: {modQn.Description}");
                modQn.Description = "Quick Note 1 with spaces!";
                var dbModQn = context.Modify(modQn, modQn.Id);
                context.SaveChanges();
                Log($"Description before modification: {dbModQn.Description}");
                Log($"Modified date: {dbModQn.ModifiedDate}");
                Log("---");

                Log("Delete Test!!");
                var delQn = context.Find<QuickNote>(2);
                var delQnId = delQn.Id;
                Log($"Preparing to delete quicknote id: {delQn.Id}");
                context.Delete<QuickNote>(delQnId);
                context.SaveChanges();
                var quickNoteExist = context.GetAll<QuickNote>().Any(o => o.Id == delQnId);
                Log($"quick note id {delQnId} exists? {quickNoteExist}");
                Log("---");
                Log("End of Test!!");

                // more seeding for the other tables
                Log("Seeding more data...");
                var directory = new Directory
                {
                    Name = "Brian Byrdsong",
                    Email = "brian.byrdsong@ey.com",
                    Phone = "404-353-9560",
                    Url = "http://www.byrdsong.io",
                    Group = "Global IT Services",
                    Description = "Application Engineer"
                };
                context.Insert(directory);

                var document = new Document
                {
                    Name = "About dotnet core...",
                    Body = "This will eventually become a long article on how dotnet core has started to reshape how I feel about .net..."
                };
                context.Insert(document);

                var workTask = new WorkTask
                {
                    Name = "Completion of JotBotsNg2Core!",
                    DueDate = new DateTime(2017, 3, 3),
                    Description = "An updated POC to dotnet core with angular 2 versus just html."
                };
                context.Insert(workTask);
                context.SaveChanges();
                Log("Data seeded!");
            }
        }
    }
}