# SorcerIoAPI

This is the backend project for [SorcerIo](https://github.com/Thiago-Bastani/SourcerIo). Feel free to check it out!

## How it works

its ment to be a RESTful API (for creating accounts, players, etc) that also handles sockets for creating maps for the game.

Its an ASP.NET Core project, please, if you are not familiar with it, check the [documentation](https://learn.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-7.0) of the technology.

## How to Run it

Clone the source code, and follow the steps:

1. Install the [dependencies](#project-dependencies)
1. Update the connection string in the `appsettings.json` and `appsettings.Development.json` files ('SQLEXPRESS' is being used)
1. run the following command in the root folder:
    ```
    dotnet ef database update
    ```
1. run the command:

    ```
    dotnet run
    ```
1. Open the navigator on the link `http://localhost:5227/swagger` to check the endpoints.

### Project dependencies

1. .NET SDK
1. Git
1. VSCode (or another editor)

