var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    if (request.Path == "/api/user/")
    {
        var message = "Error";
        if (request.HasJsonContentType())
        {
            var person = await request.ReadFromJsonAsync<Person>();
            if (person != null)
                message = $"Name: {person.UserName} Phone: {person.UserPhone} Password: {person.UserPassword}";
        }

        
    }
    else
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/index.html");
    }
});

app.Run();

public record Person(string UserName, string UserPhone, string UserPassword);