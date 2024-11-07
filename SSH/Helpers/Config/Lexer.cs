using System.IO;
using System.Text.RegularExpressions;
using GlobExpressions;

namespace Community.PowerToys.Run.Plugin.SSH.Helpers.Config;

public partial class Lexer
{
	public List<KeyValuePair<string, string>> Nodes;
	public HashSet<string> Includes;

	/// <summary>
	/// Convert ssh config file to dictionary<key, value>
	/// </summary>
	public Lexer(string configPath)
	{
		Nodes = [];
		Includes = [];
		var lines = File.ReadAllLines(configPath);
		foreach (var line in lines)
		{
			Match match = NodeRegex().Match(line);
			if (match.Success)
			{
				var key = match.Groups[1].Value;
				var value = match.Groups[2].Value;
				if (key == "Include")
				{
					value = value.Replace("~", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
					Includes.Add(value);
					Nodes.AddRange(Glob.Files(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".ssh"), value).SelectMany(f => new Lexer(f).Nodes));
				}
				else
				{
					Nodes.Add(new KeyValuePair<string, string>(key, value));
				}
			}
		}
	}

	[GeneratedRegex(@"^\s*(\S+)\s+([\S\s]+)\s*$", RegexOptions.Compiled)]
	private static partial Regex NodeRegex();
}
