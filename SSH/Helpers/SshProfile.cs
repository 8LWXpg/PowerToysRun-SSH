using Community.PowerToys.Run.Plugin.SSH.Helpers.Config;
using System.IO;

namespace Community.PowerToys.Run.Plugin.SSH.Helpers;

public static class SshProfile
{
	public static readonly string ConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".ssh", "config");
	private static List<SshHost>? CachedHosts;
	private static readonly object Lock = new();
	private static readonly FileSystemWatcher[] FileWatchers;
	public static List<SshHost> Hosts
	{
		get
		{
			lock (Lock)
			{
				CachedHosts ??= new Parser(ConfigPath).Nodes.Select(node => new SshHost(node)).ToList();
				return CachedHosts;
			}
		}
	}

	static SshProfile()
	{
		var config = new Parser(ConfigPath);
		CachedHosts = config.Nodes.Select(node => new SshHost(node)).ToList();
		var includes = config.Includes;
		FileWatchers = includes.Select(inc =>
		{
			var fileWatcher = new FileSystemWatcher
			{
				Path = Path.GetDirectoryName(ConfigPath) ?? string.Empty,
				Filter = Path.GetFileName(ConfigPath),
				NotifyFilter = NotifyFilters.LastWrite
			};

			fileWatcher.Changed += (_, _) =>
			{
				lock (Lock)
				{
					CachedHosts = null;
				}
			};
			fileWatcher.EnableRaisingEvents = true;

			return fileWatcher;
		}).ToArray();
	}
}
