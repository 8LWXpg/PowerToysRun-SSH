namespace Community.PowerToys.Run.Plugin.SSH.Terminal;

public interface ITerminalHandler
{
	/// <summary>
	/// Defines how terminal should be opened
	/// </summary>
	/// <param name="host">Host name showed in search</param>
	/// <param name="title">Window title for terminal</param>
	/// <param name="mode">Open mode for terminal emulator</param>
	/// <returns>Return value of <c>Wox.Infrastructure.Helper.OpenInShell</c></returns>
	static abstract bool OpenTerminal(string host, string title, WindowMode mode);
}
