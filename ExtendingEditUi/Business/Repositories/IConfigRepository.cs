namespace ExtendingEditUi.Business.Repositories
{
    public interface IConfigRepository
    {
        string GetConnectionString(string key);
    }
}