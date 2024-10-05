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
				var key = match.Groups[1].Value;
				var value = match.Groups[2].Value;
				Nodes.Add(new KeyValuePair<string, string>(key, value));
			}
		}
	}

	[GeneratedRegex(@"^\s*(\S+)\s+(\S+)\s*$", RegexOptions.Compiled)]
	private static partial Regex NodeRegex();
}
