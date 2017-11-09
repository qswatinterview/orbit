using IssueTracker.Web.Controllers;
using IssueTracker.Web.Data;
using IssueTracker.Web.Models;
using IssueTracker.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IssueTracker.Tests
{
    /// <summary>
    /// Helps ensure things get disposed when they need to, so we can properly use InMemoryDatabase.
    /// </summary>
    internal class IssueHelper : IDisposable
    {
        public readonly ApplicationDbContext context;
        public readonly UserManager<ApplicationUser> userManager;
        public readonly IIssueService issueService;
        public readonly IssueController controller;

        public IssueHelper()
        {

            context = CreateSingleIssueContext();
            userManager = CreateUserManager();
            issueService = new IssueService(context, userManager);
            controller = new IssueController(context, userManager, issueService);
        }

        public void Dispose()
        {
            controller.Dispose();
            userManager.Dispose();
            context.Dispose();
        }

        private ApplicationDbContext CreateSingleIssueContext()
        {
            // Ensure the test DB is torn down between tests properly.
            // See: https://stackoverflow.com/questions/38890269/how-to-isolate-ef-inmemory-database-per-xunit-test

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Test")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            ApplicationDbContext context = new ApplicationDbContext(options);

            context.Add(new Issue() { Id = 3, Title = "Fake" });
            context.SaveChanges();

            return context;
        }

        private UserManager<ApplicationUser> CreateUserManager()
        {
            var user = new ApplicationUser()
            {
                UserName = "a@b.c",
                Email = "a@b.c",
                Id=Guid.NewGuid().ToString()
            };
            var manager = new Mock<UserManager<ApplicationUser>>(new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null);
            manager.Setup(x => x.Users).Returns(() => new List<ApplicationUser>() { user }.AsQueryable());

            return manager.Object;
        }

    }
}
