using System.Text;

namespace Format.utils;

internal class Settings
{
    private static Dictionary<string, string> settings = new();

    public static bool Initialize()
    {
        try
        {
            var lines = File.ReadAllLines("format.config");
            foreach (var line in lines)
            {
                if (line.StartsWith("#") || string.IsNullOrWhiteSpace(line))
                {
                    continue; // Skip comments and empty lines
                }
                var parts = line.Split('=', 2);
                if (parts.Length == 2)
                {
                    settings[parts[0].Trim()] = parts[1].Trim();
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static void AddIfMissing(string key, string value)
    {
        if (!settings.ContainsKey(key))
        {
            settings[key] = value;
            Save();
        }
    }

    public static string env(string key, string defaultValue = "")
    {
        if (settings.ContainsKey(key))
        {
            return settings[key];
        }
        return defaultValue;
    }

    public static bool Save()
    {
        try
        {
            var sb = new StringBuilder();
            foreach (var kvp in settings)
            {
                sb.AppendLine($"{kvp.Key}={kvp.Value}");
            }
            return true;

        }
        catch (Exception)
        {
            return false;
        }
    }
}
