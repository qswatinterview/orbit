using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Web.Models
{
    /// <summary>
    /// View model for the issue editor. 
    /// </summary>
    public class IssueEditorViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Assignee { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public IssueSeverity Severity { get; set; }
        public IssueStatus Status { get; set; }
    }
}
