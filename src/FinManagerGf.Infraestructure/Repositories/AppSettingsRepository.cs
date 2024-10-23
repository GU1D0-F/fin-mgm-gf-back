using FinManagerGf.Application.Repositories;

namespace FinManagerGf.Infraestructure.Repositories
{
    public class AppSettingsRepository(Dictionary<string, string> settings) : IAppSettingsRepository
    {
        public string GetValue(string key)
        {
            if (settings.TryGetValue(key, out var value))
            {
                return value;
            }
            throw new KeyNotFoundException($"Key '{key}' not found in settings dictionary.");
        }

        public Dictionary<string, string> GetAll()
        {
            return settings;
        }
    }
}
