using FriendOrganizer2.Model;

namespace FriendOrganizer2.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FriendOrganizer2.DataAccess.FriendOrganizerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FriendOrganizer2.DataAccess.FriendOrganizerDbContext context)
        {
            context.Friends.AddOrUpdate(f => f.FirstName,
             new Friend { FirstName = "Dean", LastName = "Winchester" },
             new Friend { FirstName = "Sam", LastName = "Winchester" },
             new Friend { FirstName = "Mr", LastName = "Crowley" },
             new Friend { FirstName = "Castiel", LastName = "H" }
             );

            context.ProgrammingLanguages.AddOrUpdate(p => p.Name,
               new ProgrammingLanguage { Name = "C#" },
               new ProgrammingLanguage { Name = "TypeScript" },
               new ProgrammingLanguage { Name = "F#" },
               new ProgrammingLanguage { Name = "Swift" },
               new ProgrammingLanguage { Name = "Java" }
               );
        }
    }
}

