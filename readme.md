// efcore.tools in api project
// efcore and efcore.sqlite in infrastructure proj

Add-Migration InitialCreate -p CleanUsers.Infrastructure -s CleanUsers.Api
Update-Database