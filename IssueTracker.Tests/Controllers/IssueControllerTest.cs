using FluentAssertions;
using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace IssueTracker.Tests.Controllers
{
    /// <summary>
    /// Tests for IssueController
    /// </summary>
    public class IssueControllerTest 
    {
        [Fact]
        public async void Index_WhenViewingIndexThenItShowsListOfIssues()
        {
            using (var state = new IssueHelper())
            {
                var controller = state.controller;
                var view = (await controller.Index()) as ViewResult;
                var model = view.Model as IssueListViewModel;
                model.Issues.Count().Should().Be(1, because: "There is one item in the database.");
            }
        }

        [Fact]
        public async void Details_WhenViewingDetailsThenItShowsIssue()
        {
            using (var state = new IssueHelper())
            {
                var controller = state.controller;
                var view = (await controller.Details(3)) as ViewResult;
                var model = view.Model as Issue;

                model.Title.Should().NotBeEmpty();
            }
        }

        [Fact]
        public async void Details_WhenIssueDoesNotExistIt404s()
        {
            using (var state = new IssueHelper())
            {
                var controller = state.controller;
                var result = (await controller.Details(404)) as StatusCodeResult;
                result.StatusCode.Should().Be(404);
            }
        }

        [Fact]
        public async void Create_WhenSubmittedItCreatesNewIssue()
        {
            using (var state = new IssueHelper())
            {
                var controller = state.controller;
                var context = state.context;
                var vm = new IssueEditPageViewModel() {
                    Issue = new IssueEditorViewModel() { Title = "New", Assignee = "a@b.c", Description = "Desc", Severity = IssueSeverity.Low, Status = IssueStatus.New }
                };

                await controller.Create(vm);

                context.Issues.Count().Should().Be(2);
            }
        }

        [Fact]
        public async void Update_WhenSubmittedItOverwritesExistingIssue()
        {
            using (var state = new IssueHelper())
            {
                var controller = state.controller;
                var context = state.context;
                var vm = new IssueEditPageViewModel() {
                    Issue = new IssueEditorViewModel() { Title = "NewTitle", Assignee = "a@b.c", Description = "Desc", Severity = IssueSeverity.Low, Status = IssueStatus.New, Id = 3 }
                };

                await controller.Edit(vm);

                context.Issues.Count().Should().Be(1);
                context.Issues.First().Title.Should().Be("NewTitle");
            }
        }
    }
}
