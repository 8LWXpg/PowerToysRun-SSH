namespace Community.PowerToys.Run.Plugin.SSH.Terminal;

public class TerminalHandlerFactory
{
	private static readonly Lazy<ITerminalHandler> WindowsTerminalHandler =
		new(() => new WindowsTerminal());

	public static ITerminalHandler CreateHandler(TerminalType terminalType)
	{
		return terminalType switch
		{
			TerminalType.WindowsTerminal => WindowsTerminalHandler.Value,
			_ => throw new ArgumentOutOfRangeException(nameof(terminalType), "Unsupported terminal type")
		};
	}
}
