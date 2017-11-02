using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IssueTracker.Web.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class

    public class ApplicationUser : IdentityUser
    {
        // TODO: We want a First / Last name, or at least a display name here, since it's kind of weird to reference
        // users by their email address. (e.g. "Assigned to 'corick2@gmail.com'" instead of 
        // "Assigned to Eckelman, Corey" looks funny)
        // - Need to Fix registration / Profile / ViewModels / Views etc so we can do this.
    }
}
