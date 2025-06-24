namespace Community.PowerToys.Run.Plugin.SSH.Terminal;

public enum WindowMode
{
	Default,
	NewTab,
	Quake,
}

public enum TerminalType
{
	WindowsTerminal,
	WezTerm,
}

public static class TerminalHelper
{
	public static bool OpenTerminal(string host, string title, WindowMode mode, TerminalType type, bool suppressTitleChange)
	{
		return type switch
		{
			TerminalType.WindowsTerminal => WindowsTerminal.OpenTerminal(host, title, mode, suppressTitleChange),
			TerminalType.WezTerm => WezTerm.OpenTerminal(host, title, mode, suppressTitleChange),
			_ => throw new ArgumentOutOfRangeException(nameof(type), "Impossible enum value"),
		};
	}
}
