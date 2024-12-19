using System;
using Wox.Infrastructure;

namespace Community.PowerToys.Run.Plugin.SSH.Helpers;

public static class TerminalHelper
{
	public static bool OpenTerminal(string host, bool quakeMode, bool newTab)
	{
		if (quakeMode)
		{
			return Helper.OpenInShell("wt", $"-w _quake ssh {host}");
		}
		else if (newTab)
		{
			return Helper.OpenInShell("wt", $"-w 0 nt ssh {host}");
		}
		else
		{
			return Helper.OpenInShell("wt", $"ssh {host}");
		}
	}
}
