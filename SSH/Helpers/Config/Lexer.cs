using System.IO;
using System.Text.RegularExpressions;

namespace Community.PowerToys.Run.Plugin.SSH.Helpers.Config;

public partial class Lexer
{
	public List<KeyValuePair<string, string>> Nodes;

	/// <summary>
	/// Convert ssh config file to dictionary<key, value>
	/// </summary>
	public Lexer(string configPath)
	{
		Nodes = [];
		var lines = File.ReadAllLines(configPath);
		foreach (var line in lines)
		{
			Match match = NodeRegex().Match(line);
			if (match.Success)
			{
				Nodes.Add(new KeyValuePair<string, string>(match.Groups[1].Value, match.Groups[2].Value));
			}
		}
	}

	[GeneratedRegex(@"^\s*(\S+)\s+([^\s\*\?!,'""]*)\s*$", RegexOptions.Compiled)]
	private static partial Regex NodeRegex();
}
