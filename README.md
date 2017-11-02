#Issue Tracker:

An issue tracker written in ASP.net MVC core. 

##Running the project:

In order to run the project you will need to first install the .NET Core 2.0 SDK
The installer can be found at https://github.com/dotnet/core/blob/master/release-notes/download-archives/2.0.0-download.md.

###Run the server:

First, Open a cmd window, and cd to the IssueTracker.Web directory.
Then, run 'dotnet run' to start the server.
Copy and paste the url from the console output into your browser ("Now listening on: http://localhost:59147")

You can register your own user, or use the pre-set test user "test@test.com" with the password "Abc!23"

## Potential Next Things to Do:
1. The application right now uses a SQLite database. In order to deploy this I would have to set it to connect to a database server instead.
2. Currently all changes to the issues have to happen on the edit page. 
   It would be nice to be able to change the status and assignee on the display page as well.
3. I could add user-defined filters on the dashboard and issue list pages. Additionally, I could add sorting and pagination to the issue lists.  
4. I could also add functionality for searching for issues by title, description, assignee, etc.

