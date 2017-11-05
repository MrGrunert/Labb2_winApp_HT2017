using System.Collections.Generic;
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

            context.SaveChanges();

            context.FriendPhoneNumbers.AddOrUpdate(p => p.Number,
               new FriendPhoneNumber {Number = "+46 12345678", FriendId = context.Friends.First().Id } );

            context.Meetings.AddOrUpdate(m => m.Title,
                new Meeting
                {
                    Title = "Hunting Monsters",
                    DateFrom = new DateTime(2018, 5, 26),
                    DateTo = new DateTime(2018, 5, 26),
                    Friends = new List<Friend>
                    {
                        context.Friends.Single(f => f.FirstName == "Dean" && f.LastName == "Winchester"),
                        context.Friends.Single(f => f.FirstName == "Sam" && f.LastName == "Winchester")
                    }
                });
        }
    }
}

