using Wox.Infrastructure;

namespace Community.PowerToys.Run.Plugin.SSH.Terminal;

public class WezTerm : ITerminalHandler
{
	public static bool OpenTerminal(string host, string title, WindowMode mode) =>
		mode switch
		{
			WindowMode.Default => Helper.OpenInShell("wezterm-gui", $"ssh {host}"),
			WindowMode.NewTab => Helper.OpenInShell("wezterm", $"cli spawn -- ssh {host}"),
			WindowMode.Quake => false,
			_ => throw new ArgumentOutOfRangeException(nameof(mode), "Impossible enum value"),
		};
}
