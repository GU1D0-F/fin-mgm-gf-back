namespace FinManagerGf.Application.Repositories
{
    public interface IAppSettingsRepository
    {
        string GetValue(string key);
        Dictionary<string, string> GetAll();
    }
}
