namespace Community.PowerToys.Run.Plugin.SSH.Terminal;

public interface ITerminalHandler
{
	static abstract bool OpenTerminal(string host, string title, WindowMode mode);
}
