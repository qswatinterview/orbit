using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Web.Models
{
    /// <summary>
    /// The severity of a given issue in the issue tracker.
    /// </summary>
    public enum IssueSeverity { Low, Medium, High, Critical }

    public static class IssueSeverityExtensions
    {
        public static string GetGlyphiconClassAndTextColorForSeverity(this IssueSeverity severity)
        {
            switch (severity)
            {
                case IssueSeverity.Low: return "glyphicon-menu-down text-success";
                case IssueSeverity.Medium: return "glyphicon-minus text-primary";
                case IssueSeverity.High: return "glyphicon-menu-up text-warning";
                case IssueSeverity.Critical: return "glyphicon-alert text-danger";
                default: throw new ArgumentException("Not a valid severity: " + severity);
            }
        }
    }
}
