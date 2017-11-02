using IssueTracker.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Web.Services
{
    /// <summary>
    /// Interface for interacting with issues.
    /// </summary>
    public interface IIssueService
    {
        /// <summary>
        /// Gets each issue from the database.
        /// </summary>
        /// <returns>Returns each issue in the database.</returns>
        Task<IEnumerable<Issue>> GetAllIssuesAsync();

        /// <summary>
        /// Gets only issues that are incomplete and assigned to the given user. This is used to display a dashboard.
        /// </summary>
        /// <param name="me">The current user.</param>
        /// <returns>Returns filtered issues.</returns>
        Task<IEnumerable<Issue>> GetIncompleteIssuesAssignedToMeAsync(ApplicationUser me);

        /// <summary>
        /// Gets a specific issue by its ID
        /// </summary>
        /// <param name="id">The issue's ID</param>
        /// <returns>Returns the issue</returns>
        Task<Issue> GetIssueByIdAsync(int id);

        /// <summary>
        /// Creates an issue with the given values.
        /// </summary>
        /// <param name="issue">Values for the issue to create.</param>
        /// <returns>Returns the database issue object.</returns>
        Task<Issue> CreateIssueAsync(IssueEditorViewModel issue); 

        /// <summary>
        /// Updates an existing issue with the given values.
        /// </summary>
        /// <param name="issue">Values for the issue to create.</param>
        /// <returns>Returns the database issue object.</returns>
        Task<Issue> UpdateIssueAsync(IssueEditorViewModel issue);

        /// <summary>
        /// Gets the view model for a issue editor page. 
        /// </summary>
        /// <param name="id">The ID of the issue to pre-populate fields from, or null if we creating a new issue.</param>
        /// <returns>Returns the created issue editor view model.</returns>
        Task<IssueEditPageViewModel> GetIssueEditPageViewModelAsync(int? id);

        // TODO: We should be able to delete issues.
    }
}
