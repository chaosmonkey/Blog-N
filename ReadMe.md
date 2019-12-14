# Blog-n

An ASP.NET Core CMS/Blog Platform

## CI Builds & Deployments

Development Environment: (develop banch)

[![Build status](https://dev.azure.com/chaosmonkey/Blog-n/_apis/build/status/Blog-n%20CI%20Build)](https://dev.azure.com/chaosmonkey/Blog-n/_build/latest?definitionId=24)

Dev/Test Site: [https://blogn.azurewebsites.net/](https://blogn.azurewebsites.net/)

## Getting Started

Before getting started working with Blog-n locally you will need to create a secrets file. To do this:

1. Open the source in Visual Studio 2019
1. Right-Click on the Blogn project (in the src folder).
1. Select "Manage User Secrets" (A secrets.json file will be created and opened in Visual Studio)
1. Add the following to the file and save it.

<pre>
{
  "BlognConnectionString": "Server=LOCALHOST;Database=Blogn;User Id=USERNAME;Password=PASSWORD;",
  "Migrations": {
    "DefaultAdminUserName": "blog.admin@example.com",
    "DefaultAdminPassword": "AdminPassword123" 
  }
}
</pre>

The connection string will be used by the application for your blog data. You can set the values to anything you like, but:

1. The User must have rights to create a new database, OR
1. You must create an empty database with the same name.

Either way the account will need rights to create and modify the schema of the database if you run with migrations enabled.

DefaultAdminUserName & DefaultAdminPassword will be used to create an initial administrator account for your site so you can log in and make changes. It is highly recommended that you change the password on this account on your first login! (This will only be done if there are no accounts in your database.)

If you are not using Visual Studio (For example if you prefer to use Visual Studio Code), you can achieve the same thing from the command line.
See the [documentation for detailed instructions](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.0&tabs=windows).

ex.

<pre>
    dotnet user-secrets set "Migrations:DefaultAdminPassword" "AdminPassword123" --project PATH\TO\PROJECT\DIRECTORY
</pre>

**Note**: If values are not provided for DefaultAdminUserName & DefaultAdminPassword, they will default to the above values.
