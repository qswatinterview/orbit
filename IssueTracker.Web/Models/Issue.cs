using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Web.Models
{
    /// <summary>
    /// An issue in the issue database. 
    /// Issues have a title, status, description, severity, assignee, and a due date.
    /// </summary>
    public class Issue
    {
        /// <summary>
        /// The issue's ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // TODO: We may want to add a Created and Updated field here.

        /// <summary>
        /// The title of the issue.
        /// </summary>
        [Required]
        [MaxLength(140)]
        public String Title { get; set; }

        /// <summary>
        /// The (plain-text for now) description of the issue.
        /// </summary>
        [MaxLength(10_000)]
        public String Description { get; set; }

        /// <summary>
        /// The issue's status. Each issue will always have a status, though that status can be the default status of 'New'. 
        /// </summary>
        public IssueStatus Status { get; set; } = IssueStatus.New;

        /// <summary>
        /// The severity of the issue.
        /// </summary>
        [Required]
        public IssueSeverity Severity { get; set; } 

        /// <summary>
        /// The issue's assigned user. This is optional, as we do not necessarily know who will work on an issue when
        /// we create it.
        /// </summary>
        public ApplicationUser Assignee { get; set; }

        /// <summary>
        /// The date that the issue is due. This is optional because a user may not know when the issue should be complete
        /// at creation time.
        /// </summary>
        public DateTimeOffset? DueDate { get; set; }
    }
}
