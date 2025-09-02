using Community.PowerToys.Run.Plugin.SSH.Classes.Config;
using System.IO;

namespace Community.PowerToys.Run.Plugin.SSH.Classes;

public static class SshProfile
{
	public static readonly string ConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".ssh", "config");
	/// <summary>
	/// Cached hosts that only updates when config changes
	/// </summary>
	private static List<SshHost>? CachedHosts;
	private static readonly Lock Lock = new();
	private static readonly FileSystemWatcher[] FileWatchers;
	public static List<SshHost> Hosts
	{
		get
		{
			lock (Lock)
			{
				CachedHosts ??= new Parser(ConfigPath).Nodes.ConvertAll(node => new SshHost(node));
				return CachedHosts;
			}
		}
	}

	/// <summary>
	/// Parse config and initialize file watchersx
	/// </summary>
	static SshProfile()
	{
		var config = new Parser(ConfigPath);
		CachedHosts = config.Nodes.ConvertAll(node => new SshHost(node));
		HashSet<string> includes = config.Includes;
		FileWatchers = [.. includes.Select(inc =>
		{
			var fileWatcher = new FileSystemWatcher
			{
				Path = Path.GetDirectoryName(inc) ?? string.Empty,
				Filter = Path.GetFileName(inc),
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
		})];
	}
}
