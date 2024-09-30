using System;

namespace Community.PowerToys.Run.Plugin.SSH.Helpers.Config;

public class Parser
{
	public List<ConfigNode> Nodes;

	/// <summary>
	/// Convert Lexer nodes to ConfigNode
	/// </summary>
	public Parser(string configPath)
	{
		Nodes = [];
		var nodes = new Lexer(configPath).Nodes;

		ConfigNode? current = null;
		foreach (var node in nodes)
		{
			// First node must be Host
			if (node.Key == "Host")
			{
				if (current != null)
				{
					Nodes.Add(current);
				}
				current = new ConfigNode
				{
					Host = node.Value,
					Properties = []
				};
			}
			else
			{
				if (!current!.Properties.ContainsKey(node.Key))
				{
					current!.Properties.Add(node.Key, node.Value);
				}
			}
		}
		if (current != null)
		{
			Nodes.Add(current);
		}
	}
}
