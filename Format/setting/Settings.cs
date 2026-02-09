using System.Text;
using System.Runtime.InteropServices;

namespace Format.setting;

internal class Settings
{
    private static string path = "format.config";
    private static Dictionary<string, string> settings = new();

    public static string BaseDir { get; set; } = AppDomain.CurrentDomain.BaseDirectory;

    public static bool Initialize(string config = "format.config")
    {
        path = Path.IsPathRooted(config) ? config : Path.GetFullPath(Path.Combine(BaseDir, "data", config));
        try
        {
            var lines = File.ReadAllLines(path);
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

    public static void AddPathOptionIfMissing(string key, string p)
    {
        if (Path.IsPathRooted(p) && p.StartsWith(BaseDir))
        {
            p = Path.GetRelativePath(BaseDir, p).Replace('\\', '/');
        }
        Add(key, p);
    }

    public static bool Add(string key, string value)
    {
        if (!settings.ContainsKey(key))
        {
            settings[key] = value;
            return Save();
        }
        return false;
    }

    public static string EnvPathOption(string key, string defaultVaule = "")
    {
        if (settings.ContainsKey(key))
        {
            string p = settings[key];
            if (!Path.IsPathRooted(p))
            {
                return Path.GetFullPath(Path.Combine(BaseDir, p));
            }
            return NormalizeCrossPlatformPath(p);
        }
        return defaultVaule;
    }

    private static string NormalizeCrossPlatformPath(string p)
    {
        if (string.IsNullOrEmpty(p)) return p;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // Se siamo su Windows e il path sembra uno stile Linux WSL (/mnt/c/...)
            if (p.StartsWith("/mnt/") && p.Length >= 7 && p[6] == '/')
            {
                char drive = p[5];
                return $"{drive}:{p.Substring(6).Replace('/', '\\')}";
            }
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            // Se siamo su Linux e il path sembra uno stile Windows (C:\...)
            if (p.Length >= 3 && p[1] == ':' && (p[2] == '\\' || p[2] == '/'))
            {
                char drive = char.ToLower(p[0]);
                return $"/mnt/{drive}{p.Substring(2).Replace('\\', '/')}";
            }
        }
        return p;
    }

    public static bool? DebugOverride { get; set; } = null;

    public static bool? EnvBoolOption(string key, bool? defaultValue = null) {
        if (key == "debug" && DebugOverride.HasValue)
        {
            return DebugOverride.Value;
        }
        if (bool.TryParse(env(key), out bool value))
        {
            return value;
        }
        return defaultValue;
    }

    public static string env(string key, string defaultValue = "")
    {
        if (settings.ContainsKey(key))
        {
            return settings[key];
        }
        return defaultValue;
    }

    public static bool Set(string key, bool value)
    {
        return Set(key, value.ToString());
    }

    public static bool Set(string key, string value)
    {
        settings[key] = value;
        Save();
        return true;
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
            File.WriteAllText(path, sb.ToString());
            return true;

        }
        catch (Exception)
        {
            return false;
        }
    }

    public static Dictionary<string, string> List()
    {
        return new Dictionary<string, string>(settings);
    }

}
