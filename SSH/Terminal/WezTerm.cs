using Wox.Infrastructure;

namespace Community.PowerToys.Run.Plugin.SSH.Terminal;

public class WezTerm : ITerminalHandler
{
	public static bool OpenTerminal(string host, string title, WindowMode mode, bool suppressTitleChange) =>
		mode switch
		{
			WindowMode.Default or WindowMode.Quake => Helper.OpenInShell("wezterm-gui", $"ssh {host}"),
			WindowMode.NewTab => Helper.OpenInShell("wezterm", $"cli spawn -- ssh {host}"),
			_ => throw new ArgumentOutOfRangeException(nameof(mode), "Impossible enum value"),
		};
}
