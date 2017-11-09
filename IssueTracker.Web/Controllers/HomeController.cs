using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IssueTracker.Web.Models;
using IssueTracker.Web.Services;
using Microsoft.AspNetCore.Identity;

namespace IssueTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIssueService _issueService;

        public HomeController(UserManager<ApplicationUser> userManager, IIssueService issueService)
        {
            _userManager = userManager;
            _issueService = issueService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                // Get the incomplete issues that are assigned to the current user.
                var myIssues = await _issueService.GetIncompleteIssuesAssignedToMeAsync(currentUser);

                var viewModel = new IssueListViewModel() { Issues = myIssues };
                return View(viewModel);
            }
            else
            {
                return View(new IssueListViewModel()); 
            }

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
