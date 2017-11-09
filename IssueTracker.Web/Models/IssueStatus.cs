using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Web.Models
{
    /// <summary>
    /// The status of a given issue in the issue tracker. 
    /// Issues in our workflow have a pre-defined workflow of: 
    /// 1. Somebody creates the issue (New)
    /// 2. Somebody researches the issue (e.g. how to reproduce it) (Requirements)
    /// 3. Somebody then develops a fix for the issue (Development)
    /// 4. Somebody validates that the fix works (Validation)
    /// 5. Somebody then publishes the fix (Deployment)
    /// 6. Finally, when the issue is resolved we close it out (Resolved)
    ///  - OR if the issue wasn't fixed, we can mark the issue with the reason it wasn't. (WontFix, NotABug, ...)
    /// 
    /// In the future, we could allow for users to define custom workflows, and then we'd need to swap the Status enum
    /// for a Status model.
    /// </summary>
    public enum IssueStatus { New, Requirements, Development, Validation, Deployment, Resolved, WontFix, NotABug, CanNotReproduce }

    /// <summary>
    /// Extension methods ...
    /// </summary>
    public static class IssueStatusExtensions
    {
        public static bool IsClosed(this IssueStatus status)
        {
            // Note: Resolved / Fixed / NotABug / CanNotReproduce are all closed.

            return status == IssueStatus.Resolved
                || status == IssueStatus.WontFix
                || status == IssueStatus.NotABug
                || status == IssueStatus.CanNotReproduce;
        }

    }
}
