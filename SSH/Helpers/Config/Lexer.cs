using GlobExpressions;
using System.IO;
using System.Text.RegularExpressions;

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
		Includes = [configPath];
		var lines = File.ReadAllLines(configPath);
		foreach (var line in lines)
		{
			Match match = NodeRegex().Match(line);
			if (!match.Success)
			{
				continue;
			}

			var key = match.Groups[1].Value;
			var value = match.Groups[2].Value;

			if (key.StartsWith('#'))
			{
				continue;
			}

			// trim comment
			var comment = value.IndexOf('#');
			if (comment != -1)
			{
				value = value[..comment];
			}

			if (key == "Include")
			{
				value = value.Replace("~", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
				var cwd = Path.IsPathRooted(value)
					? Path.GetDirectoryName(value)!
					: Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".ssh", Path.GetDirectoryName(value));
				var files = Glob.Files(cwd, Path.GetFileName(value)).Select(f => Path.Join(cwd, f)).ToArray();

				foreach (var f in files)
				{
					_ = Includes.Add(f);
					var lexer = new Lexer(f);
					Nodes.AddRange(lexer.Nodes);
					foreach (var inc in lexer.Includes)
					{
						_ = Includes.Add(inc);
					}
				}
			}
			else
			{
				Nodes.Add(new KeyValuePair<string, string>(key, value));
			}
		}
	}

	[GeneratedRegex(@"^\s*(\S+)\s+([\S\s]+)\s*$", RegexOptions.Compiled)]
	private static partial Regex NodeRegex();
}
