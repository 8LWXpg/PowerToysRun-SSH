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
}

public static class TerminalHelper
{
	public static bool OpenTerminal(string host, string title, WindowMode mode, TerminalType type)
	{
		ITerminalHandler handler = TerminalHandlerFactory.CreateHandler(type);
		return handler.OpenTerminal(host, title, mode);
	}
}
