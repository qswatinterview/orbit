using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Web.Data
{
    public static class SeedDatabase
    {
        /// <summary>
        /// Adds some sample items to the database.
        /// </summary>
        /// <param name="context">The DB context to use to add the issues.</param>
        public static void WithSampleRecords(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            #region Test Data 
            var testUser = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "test@test.com",
                Email = "test@test.com"
            };

            var otherUser = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "test2@test.com",
                Email = "test2@test.com"
            };

            var issues = new List<Issue>()
            {
                new Issue() {
                    Id=1,
                    Assignee = testUser,
                    Title = "Add DB Issue Service",
                    Description = "Create a service class for interacting with issues through the DB",
                    DueDate = DateTimeOffset.Now.AddDays(2),
                    Severity = IssueSeverity.Medium,
                    Status = IssueStatus.New,
                },

                new Issue() {
                    Id=2,
                    Assignee = testUser,
                    Title = "Seed the Database",
                    Description = "Seed the database with some sample values for testing purposes.",
                    DueDate = DateTimeOffset.Now.AddDays(2),
                    Severity = IssueSeverity.Low,
                    Status = IssueStatus.Development,
                },

                new Issue() {
                    Id=3,
                    Assignee = testUser,
                    Title = "Add Issue Editor View",
                    Description = "Add a view for editing issues",
                    DueDate = DateTimeOffset.Now.AddDays(3),
                    Severity = IssueSeverity.High,
                    Status = IssueStatus.Requirements,
                },

                new Issue() {
                    Id=4,
                    Assignee = otherUser,
                    Title = "Walk Dog",
                    Description = "Walk the puppy around the block. (This sample issue is assigned to another user.)",
                    DueDate = DateTimeOffset.Now.AddDays(3),
                    Severity = IssueSeverity.Critical,
                    Status = IssueStatus.Deployment,
                },

                new Issue() {
                    Id=5,
                    Assignee = otherUser,
                    Title = "Set up .net core and ASP.NET core",
                    Description = "Set up the development environment. (This sample issue is one that's been completed.)",
                    DueDate = DateTimeOffset.Now.AddDays(1),
                    Severity = IssueSeverity.Medium,
                    Status = IssueStatus.Resolved,
                },
            };

            #endregion testdata

            if (context.Issues.Any()) return; // Don't re-create these.

            userManager.CreateAsync(testUser, "Abc!23");
            userManager.CreateAsync(otherUser, "Abc!23");

            foreach (var i in issues) {
                context.Issues.Add(i);
            }

            context.SaveChanges();
        }

    }
}
