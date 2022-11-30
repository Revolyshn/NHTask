namespace NHService;

public class StorageController : IStorageController
{
    private readonly List<string> _buffer;

    public StorageController()
    {
        _buffer = new List<string>();
    }

    public List<string> GetList()
    {
        return _buffer;
    }

    public void WriteToList(string buffer)
    {
        _buffer.Add(buffer);
        
        
    }
}