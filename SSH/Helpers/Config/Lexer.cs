using System.IO;
using System.Text.RegularExpressions;

namespace Community.PowerToys.Run.Plugin.SSH.Helpers.Config;

public partial class Lexer
{
	public List<KeyValuePair<string, string>> Nodes;
	public List<KeyValuePair<string, string>> GlobNodes;

	/// <summary>
	/// Convert ssh config file to dictionary<key, value>
	/// </summary>
	public Lexer(string configPath)
	{
		Nodes = [];
		GlobNodes = [];
		var lines = File.ReadAllLines(configPath);
		foreach (var line in lines)
		{
			Match match = NodeRegex().Match(line);
			if (match.Success)
			{
				var key = match.Groups[1].Value;
				var value = match.Groups[2].Value;
				if (HasGlobPattern(key))
				{
					Nodes.Add(new KeyValuePair<string, string>(key, value));
				}
				else
				{
					GlobNodes.Add(new KeyValuePair<string, string>(key, value));
				}
			}
		}
	}

	private static bool HasGlobPattern(string pattern)
	{
		char[] globCharacters = ['*', '?', '[', ']', '{', '}'];
		return pattern.Any(c => globCharacters.Contains(c));
	}

	[GeneratedRegex(@"^\s*(\S+)\s+(\S+)\s*$", RegexOptions.Compiled)]
	private static partial Regex NodeRegex();
}
