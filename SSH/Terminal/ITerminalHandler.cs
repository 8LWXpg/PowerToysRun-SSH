namespace Community.PowerToys.Run.Plugin.SSH.Terminal;

public interface ITerminalHandler
{
	bool OpenTerminal(string host, string title, WindowMode mode);
}
