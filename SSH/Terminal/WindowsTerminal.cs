using Wox.Infrastructure;

namespace Community.PowerToys.Run.Plugin.SSH.Terminal;

public class WindowsTerminal : ITerminalHandler
{
	public static bool OpenTerminal(string host, string title, WindowMode mode)
	{
		var arguments = mode switch
		{
			WindowMode.Default => $"--title {title} ssh {host}",
			WindowMode.NewTab => $"-w 0 nt --title {title} ssh {host}",
			WindowMode.Quake => $"-w _quake --title {title} ssh {host}",
			_ => throw new ArgumentOutOfRangeException(nameof(mode), "Impossible enum value"),
		};

		return Helper.OpenInShell("wt", arguments);
	}
}
