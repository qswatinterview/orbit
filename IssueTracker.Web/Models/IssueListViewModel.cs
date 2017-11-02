using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Web.Models
{
    public class IssueListViewModel
    {
        public IEnumerable<Issue> Issues { get; set; }
    }
}
