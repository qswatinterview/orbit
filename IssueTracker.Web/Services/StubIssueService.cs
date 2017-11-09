using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Web.Models;

namespace IssueTracker.Web.Services
{
    /// <summary>
    /// Temporary implementation of IIssueService which returns some hard-coded issues.
    /// </summary>
    public class StubIssueService
        : IIssueService
    {
        private readonly ApplicationUser _user = new ApplicationUser() { Email = "fdfd@fdfd.com", UserName = "fdfdf" };
        private readonly ApplicationUser _userTwo = new ApplicationUser() { Email = "asdf@fdfd.com", UserName = "dfdfd" };

        private IEnumerable<Issue> GetAllIssuesStub()
        {
            return new List<Issue>() {
                new Issue() { Title = "Issue Title A", Description = "Issue Description", Assignee = _user, DueDate = DateTimeOffset.Now.AddDays(2), Id = 1, Severity = IssueSeverity.Low, Status = IssueStatus.New},
                new Issue() { Title = "Issue Title B", Description = "Issue Description", Assignee = _user, DueDate = DateTimeOffset.Now.AddDays(2), Id = 2, Severity = IssueSeverity.Critical, Status = IssueStatus.Development},
                new Issue() { Title = "Issue Title C", Description = "Issue Description", Assignee = _user, DueDate = DateTimeOffset.Now.AddDays(2), Id = 3, Severity = IssueSeverity.Medium, Status = IssueStatus.Development},
                new Issue() { Title = "Issue Title D", Description = "Issue Description", Assignee = _user, DueDate = DateTimeOffset.Now.AddDays(2), Id = 4, Severity = IssueSeverity.Critical, Status = IssueStatus.Validation},
                new Issue() { Title = "Issue Title E", Description = "Issue Description", Assignee = _user, DueDate = DateTimeOffset.Now.AddDays(2), Id = 5, Severity = IssueSeverity.Critical, Status = IssueStatus.WontFix},
                new Issue() { Title = "Issue Title F", Description = "Issue Description", Assignee = _userTwo, DueDate = DateTimeOffset.Now.AddDays(2), Id = 6, Severity = IssueSeverity.Critical, Status = IssueStatus.Deployment},
                new Issue() { Title = "Issue Title G", Description = "Issue Description", Assignee = _userTwo, DueDate = DateTimeOffset.Now.AddDays(2), Id = 7, Severity = IssueSeverity.Critical, Status = IssueStatus.New}
            };
        }

        public Task<IEnumerable<Issue>> GetAllIssuesAsync()
        {
            return Task.FromResult(GetAllIssuesStub());
        }

        public Task<IEnumerable<Issue>> GetIncompleteIssuesAssignedToMeAsync(ApplicationUser me)
        {
            return Task.FromResult(GetAllIssuesStub().Where(x => x.Assignee == _user));
        }

        public Task<Issue> GetIssueByIdAsync(int ID)
        {
            return Task.FromResult(GetAllIssuesStub().First());
        }

        public Task<Issue> CreateIssueAsync(IssueEditorViewModel issue)
        {
            throw new NotImplementedException();
        }

        public Task<Issue> UpdateIssueAsync(IssueEditorViewModel issue)
        {
            throw new NotImplementedException();
        }

        public Task<IssueEditPageViewModel> GetIssueEditPageViewModelAsync(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
