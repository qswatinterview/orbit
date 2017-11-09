using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Web.Models;
using IssueTracker.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace IssueTracker.Web.Services
{
    public class IssueService: IIssueService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public IssueService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
        }

        public async Task<Issue> CreateIssueAsync(IssueEditorViewModel issue)
        {
            return await CreateOrUpdate(issue, creating: true);
        }

        public async Task<IEnumerable<Issue>> GetAllIssuesAsync()
        {
            return await _dbContext.Issues
                .Include(x => x.Assignee)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Issue>> GetIncompleteIssuesAssignedToMeAsync(ApplicationUser me)
        {
            return await _dbContext.Issues
                .Where(x => x.Assignee.UserName == me.UserName)
                .Where(x => !x.Status.IsClosed())
                .ToArrayAsync();
        }

        public async Task<Issue> GetIssueByIdAsync(int id)
        {
            return await _dbContext.Issues
                .Include(x => x.Assignee)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IssueEditPageViewModel> GetIssueEditPageViewModelAsync(int? id)
        {
            Issue issue;

            if (id.HasValue)
            {
                issue = await this.GetIssueByIdAsync(id.Value);

                if (issue == null)
                    throw new ArgumentException("Could not find given issue.");
            }
            else
            {
                issue = new Issue();
            }

            // Get all of the possible usernames, to use in the dropdown. 
            var allUsernames = _userManager.Users
                .Select(x => x.UserName)
                .ToArray();

            var issueVm = new IssueEditorViewModel()
            {
                Assignee = issue.Assignee?.UserName,
                Description = issue.Description,
                DueDate = issue.DueDate,
                Id = issue.Id,
                Severity = issue.Severity,
                Status = issue.Status,
                Title = issue.Title,
            };

            return new IssueEditPageViewModel()
            {
                Issue = issueVm,
                UserNames = allUsernames,
            };
        }

        public async Task<Issue> UpdateIssueAsync(IssueEditorViewModel issueVm)
        {
            return await CreateOrUpdate(issueVm, creating: false);
        }

        private async Task<Issue> CreateOrUpdate(IssueEditorViewModel issueVm, bool creating)
        { 
            Issue issue;

            if (creating)
            {
                issue = new Issue();
                _dbContext.Issues.Add(issue);
            }
            else
            {
                issue = await GetIssueByIdAsync(issueVm.Id);

                if (issue == null)
                    throw new ArgumentException("Issue not found.");
            }

            var user = await _userManager.FindByNameAsync(issueVm.Assignee);

            issue.Assignee = user;
            issue.Description = issueVm.Description;
            issue.DueDate = issueVm.DueDate;
            issue.Severity = issueVm.Severity;
            issue.Status = issueVm.Status;
            issue.Title = issueVm.Title;

            var result = await _dbContext.SaveChangesAsync();

            if (result != 1)
                throw new InvalidOperationException("Issue was not written.");

            return issue;
        }
    }
}
