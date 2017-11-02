using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Web.Models
{
    public class IssueEditPageViewModel
    {
        public IssueEditorViewModel Issue { get; set; }
        public IEnumerable<string> UserNames { get; set; }
    }

}
