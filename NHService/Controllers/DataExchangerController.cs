using Microsoft.AspNetCore.Mvc;

namespace NHService.Controllers;

public class DataExchangerController : Controller
{
    private readonly IStorageController _storageController;

    public DataExchangerController(IStorageController storageController)
    {
        _storageController = storageController;
    }

    [HttpPost("/userPost")]
    public async void UserMethod()
    {
        var reader = new StreamReader(Request.Body);
        var message = await reader.ReadToEndAsync();

        _storageController.WriteToList(message);
    }

    [HttpGet("/userGet")]
    public List<string> GetUsers()
    {
        return _storageController.GetList();
    }
    
}