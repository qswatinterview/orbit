using IssueTracker.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace IssueTracker.Tests.Services
{
    public class IssueServiceTest
    {
        [Fact]
        public async Task CreateIssueAsync_GivenIssueViewModelItAddsToDatabase()
        {
            using (var state = new IssueHelper())
            {
                var vm = new IssueEditorViewModel() { Title = "New Issue" };
                await state.issueService.CreateIssueAsync(vm);

                var issues = (await state.issueService.GetAllIssuesAsync()).Count();

                issues.Should().Be(2);
            }
        }

        [Fact]
        public async Task GetAllIssuesAsync_ItReturnsEachIssueInCollection()
        {
            using (var state = new IssueHelper())
            {
                var issues = (await state.issueService.GetAllIssuesAsync()).Count();

                issues.Should().Be(1);
            }
        }

        [Fact]
        public async Task GetIssueByIdAsync_ItReturnsIssueWithId()
        {
            using (var state = new IssueHelper())
            {
                var actual = await state.issueService.GetIssueByIdAsync(3);
                actual.Title.Should().NotBeEmpty();
            }
        }

        [Fact]
        public async Task GetIssueEditPageViewModelAsync_ItPopulatesUserNames_AndPopulatesIssueFields()
        {
            using (var state = new IssueHelper())
            {
                var actual = await state.issueService.GetIssueEditPageViewModelAsync(3);

                actual.UserNames.Should().ContainSingle(); // Usernames should be populated.
                actual.Issue.Title.Should().NotBeEmpty();
            }

        }

        [Fact]
        public async Task UpdateIssueAsync_ItUpdatesExistingIssueInPlace()
        {
            using (var state = new IssueHelper())
            {
                var edited = new IssueEditorViewModel() { Title = "NewTitle", Id = 3 };
                await state.issueService.UpdateIssueAsync(edited);
                var actual = await state.issueService.GetIssueByIdAsync(3);

                actual.Title.Should().Be("NewTitle");
            }
        }
    }
}
