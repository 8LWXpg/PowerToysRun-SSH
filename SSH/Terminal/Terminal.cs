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
	public static bool OpenTerminal(string host, string title, WindowMode mode, TerminalType type)
	{
		return type switch
		{
			TerminalType.WindowsTerminal => WindowsTerminal.OpenTerminal(host, title, mode),
			TerminalType.WezTerm => WezTerm.OpenTerminal(host, title, mode),
			_ => throw new ArgumentOutOfRangeException(nameof(type), "Impossible enum value"),
		};
	}
}
