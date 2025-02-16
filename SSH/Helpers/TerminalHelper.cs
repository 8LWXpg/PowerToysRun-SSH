using System;
using Wox.Infrastructure;

namespace Community.PowerToys.Run.Plugin.SSH.Helpers;

public enum WindowMode
{
	Default,
	NewTab,
	Quake,
}

public static class TerminalHelper
{
	public static bool OpenTerminal(string host, string title, WindowMode mode)
	{
		return mode switch
		{
			WindowMode.Default => Helper.OpenInShell("wt", $"--title {title} ssh {host}"),
			WindowMode.NewTab => Helper.OpenInShell("wt", $"-w 0 nt --title {title} ssh {host}"),
			WindowMode.Quake => Helper.OpenInShell("wt", $"-w _quake --title {title} ssh {host}"),
			_ => throw new ArgumentOutOfRangeException(nameof(mode), "Impossible enum value"),
		};
	}
}
