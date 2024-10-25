using System.IO;
using Community.PowerToys.Run.Plugin.SSH.Helpers.Config;

namespace Community.PowerToys.Run.Plugin.SSH.Helpers;

public class SshProfile
{
	public static readonly string ConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".ssh", "config");
	private static List<SshHost>? _cachedHosts;
	private static readonly object _lock = new();
	private static readonly FileSystemWatcher? _fileWatcher;
	public static List<SshHost> Hosts
	{
		get
		{
			lock (_lock)
			{
				_cachedHosts ??= new Parser(ConfigPath).Nodes.Select(node => new SshHost(node)).ToList();
				return _cachedHosts;
			}
		}
	}

	static SshProfile()
	{
		_fileWatcher = new FileSystemWatcher
		{
			Path = Path.GetDirectoryName(ConfigPath) ?? string.Empty,
			Filter = Path.GetFileName(ConfigPath),
			NotifyFilter = NotifyFilters.LastWrite
		};

		_fileWatcher.Changed += (_, _) =>
		{
			lock (_lock)
			{
				_cachedHosts = null;
			}
		};
		_fileWatcher.EnableRaisingEvents = true;
	}
}
