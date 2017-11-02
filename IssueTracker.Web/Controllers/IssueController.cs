using IssueTracker.Web.Data;
using IssueTracker.Web.Models;
using IssueTracker.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace IssueTracker.Web.Controllers
{
    [Authorize]
    public class IssueController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IIssueService _issueService;

        public IssueController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IIssueService issueService)
        {
            _dbContext = dbContext;
            _issueService = issueService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var issues = await _issueService.GetAllIssuesAsync();
            var issueList = new IssueListViewModel() { Issues = issues };
            return View(issueList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var issue = await _issueService.GetIssueByIdAsync(id);
            if (issue == null)
                return NotFound();
            else
                return View(issue);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = await _issueService.GetIssueEditPageViewModelAsync(null);
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await _issueService.GetIssueEditPageViewModelAsync(id);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IssueEditPageViewModel issuePageVm)
        {
            var issue = await _issueService.CreateIssueAsync(issuePageVm.Issue);
            return RedirectToAction("Details", new { id = issue.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IssueEditPageViewModel issuePageVm)
        {
            var issue = await _issueService.UpdateIssueAsync(issuePageVm.Issue);
            return RedirectToAction("Details", new { id = issue.Id });
        }
    }
}
