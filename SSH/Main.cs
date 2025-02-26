using Community.PowerToys.Run.Plugin.SSH.Helpers;
using Community.PowerToys.Run.Plugin.SSH.Properties;
using Community.PowerToys.Run.Plugin.SSH.Terminal;
using ManagedCommon;
using Microsoft.PowerToys.Settings.UI.Library;
using System.Windows.Controls;
using Wox.Infrastructure;
using Wox.Plugin;

namespace Community.PowerToys.Run.Plugin.SSH;
public class Main : IPlugin, IPluginI18n, ISettingProvider, IReloadable, IDisposable
{
	private const string OpenMode = nameof(OpenMode);
	private const string Terminal = nameof(Terminal);

	private PluginInitContext? _context;
	private WindowMode _openMode;
	private TerminalType _terminalType;
	private string? _iconPath;
	private bool _disposed;
	public string Name => Resources.plugin_name;
	public string Description => Resources.plugin_description;
	public static string PluginID => "ca17a4e0a0b54d2a8c2994b6f866e082";

	public IEnumerable<PluginAdditionalOption> AdditionalOptions =>
	[
		new()
		{
			PluginOptionType = PluginAdditionalOption.AdditionalOptionType.Combobox,
			Key = OpenMode,
			DisplayLabel = Resources.open_mode,
			DisplayDescription = Resources.open_mode_desc,
			ComboBoxItems =
			[
				new(Resources.open_mode_default, "0"),
				new(Resources.open_mode_new_tab, "1"),
				new(Resources.open_mode_quake, "2"),
			],
		},
		new()
		{
			PluginOptionType = PluginAdditionalOption.AdditionalOptionType.Combobox,
			Key = Terminal,
			DisplayLabel = Resources.terminal,
			DisplayDescription = Resources.terminal_desc,
			ComboBoxItems =
			[
				new(Resources.windows_terminal, "0"),
			],
		},
	];

	public void UpdateSettings(PowerLauncherPluginSettings settings)
	{
		_openMode = (WindowMode)(settings?.AdditionalOptions?.FirstOrDefault(x => x.Key == OpenMode)?.ComboBoxValue ?? 0);
		_terminalType = (TerminalType)(settings?.AdditionalOptions?.FirstOrDefault(x => x.Key == Terminal)?.ComboBoxValue ?? 0);
	}

	public List<Result> Query(Query query)
	{
		ArgumentNullException.ThrowIfNull(query);

		List<Result> results = SshProfile.Hosts.ConvertAll(host =>
		{
			MatchResult match = StringMatcher.FuzzySearch(query.Search, host.Host);
			return new Result
			{
				Title = host.Host,
				SubTitle = $"{host.User}@{host.HostName}",
				ToolTipData = new ToolTipData(host.Host, host.ToString()),
				IcoPath = _iconPath,
				Score = match.Score,
				TitleHighlightData = match.MatchData,
				Action = _ => TerminalHelper.OpenTerminal(host.Host, host.Host, _openMode, _terminalType)
			};
		});

		if (!string.IsNullOrEmpty(query.Search))
		{
			_ = results.RemoveAll(x => x.Score <= 0);
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

	private void OnThemeChanged(Theme oldTheme, Theme newTheme) => UpdateIconPath(newTheme);

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
