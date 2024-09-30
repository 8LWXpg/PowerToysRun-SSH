using Community.PowerToys.Run.Plugin.SSH.Helpers;
using Community.PowerToys.Run.Plugin.SSH.Properties;
using ManagedCommon;
using Microsoft.PowerToys.Settings.UI.Library;
using System.Windows.Controls;
using Wox.Infrastructure;
using Wox.Plugin;

namespace Community.PowerToys.Run.Plugin.SSH;
public class Main : IPlugin, IPluginI18n, ISettingProvider, IReloadable, IDisposable
{
	private const string OpenQuake = nameof(OpenQuake);
	private const string OpenNewTab = nameof(OpenNewTab);

	private PluginInitContext? _context;
	private bool _openQuake;
	private bool _openNewTab;
	private string? _iconPath;
	private bool _disposed;
	public string Name => Resources.plugin_name;
	public string Description => Resources.plugin_description;
	public static string PluginID => "ca17a4e0a0b54d2a8c2994b6f866e082";

	public IEnumerable<PluginAdditionalOption> AdditionalOptions =>
	[
		new ()
		{
			Key = OpenQuake,
			DisplayLabel = Resources.open_quake,
			DisplayDescription = Resources.open_quake_description,
			Value = false,
		},
		new ()
		{
			Key = OpenNewTab,
			DisplayLabel = Resources.open_new_tab,
			Value = false,
		},
	];

	public void UpdateSettings(PowerLauncherPluginSettings settings)
	{
		_openQuake = settings?.AdditionalOptions?.FirstOrDefault(x => x.Key == OpenQuake)?.Value ?? false;
		_openNewTab = settings?.AdditionalOptions?.FirstOrDefault(x => x.Key == OpenNewTab)?.Value ?? false;
	}

	public List<Result> Query(Query query)
	{
		ArgumentNullException.ThrowIfNull(query);

		var results = SshProfile.Hosts.ConvertAll(host =>
		{
			var match = StringMatcher.FuzzySearch(query.Search, host.Host);
			return new Result
			{
				Title = host.Host,
				SubTitle = $"{host.User}@{host.HostName}",
				IcoPath = _iconPath,
				Score = match.Score,
				TitleHighlightData = match.MatchData,
				Action = _ =>
				{
					TerminalHelper.OpenTerminal(host.Host, _openQuake, _openNewTab);
					return true;
				},
			};
		});

		if (!string.IsNullOrEmpty(query.Search))
		{
			results.RemoveAll(x => x.Score <= 0);
		}

		return results;
	}

	public void Init(PluginInitContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
		_context.API.ThemeChanged += OnThemeChanged;
		UpdateIconPath(_context.API.GetCurrentTheme());
	}

	public string GetTranslatedPluginTitle() => Resources.plugin_name;

	public string GetTranslatedPluginDescription() => Resources.plugin_description;

	private void OnThemeChanged(Theme oldtheme, Theme newTheme) => UpdateIconPath(newTheme);

	private void UpdateIconPath(Theme theme) => _iconPath = theme is Theme.Light or Theme.HighContrastWhite ? "Images/SSH.light.png" : "Images/SSH.dark.png";

	public Control CreateSettingPanel() => throw new NotImplementedException();

	public void ReloadData()
	{
		if (_context is null)
		{
			return;
		}

		UpdateIconPath(_context.API.GetCurrentTheme());
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed && disposing)
		{
			if (_context != null && _context.API != null)
			{
				_context.API.ThemeChanged -= OnThemeChanged;
			}

			_disposed = true;
		}
	}
}
