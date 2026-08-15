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
	private static Dictionary<string, FileSystemWatcher> FileWatchers = [];
	public static List<SshHost> Hosts
	{
		get
		{
			lock (Lock)
			{
				if (CachedHosts is null)
				{
					var config = new Parser(ConfigPath);
					CachedHosts = config.Nodes.ConvertAll(node => new SshHost(node));
					SyncFileWatchers(config.Includes);
				}

				return CachedHosts;
			}
		}
	}

	/// <summary>
	/// Parse config and initialize file watchers
	/// </summary>
	static SshProfile()
	{
		var config = new Parser(ConfigPath);
		CachedHosts = config.Nodes.ConvertAll(node => new SshHost(node));
		SyncFileWatchers(config.Includes);
	}

	/// <summary>
	/// Reconcile <see cref="FileWatchers"/> with the current set of included files, reusing
	/// watchers for paths that are still included and disposing ones that no longer are.
	/// Must be called while holding <see cref="Lock"/>.
	/// </summary>
	private static void SyncFileWatchers(HashSet<string> includes)
	{
		var updated = new Dictionary<string, FileSystemWatcher>(includes.Count);
		foreach (var inc in includes)
		{
			updated[inc] = FileWatchers.Remove(inc, out var existing) ? existing : CreateWatcher(inc);
		}

		foreach (FileSystemWatcher stale in FileWatchers.Values)
		{
			stale.Dispose();
		}

		FileWatchers = updated;
	}

	private static FileSystemWatcher CreateWatcher(string path)
	{
		var fileWatcher = new FileSystemWatcher
		{
			Path = Path.GetDirectoryName(path) ?? string.Empty,
			Filter = Path.GetFileName(path),
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
	}

	public static void Dispose()
	{
		lock (Lock)
		{
			foreach (FileSystemWatcher fileWatcher in FileWatchers.Values)
			{
				fileWatcher.Dispose();
			}

			FileWatchers = [];
			CachedHosts = null;
		}
	}
}
