namespace NHService;

public interface IStorageController
{
    public List<string> GetList();
    public void WriteToList(string buffer);
}