using Community.PowerToys.Run.Plugin.SSH.Helpers.Config;

namespace Community.PowerToys.Run.Plugin.SSH.Helpers;

public record SshHost
{
	public string Host { get; }
	public string HostName { get; }
	public string User { get; }
	public Dictionary<string, string> Properties { get; }

	public SshHost(ConfigNode node)
	{
		Host = node.Host;
		HostName = node.Properties.TryGetValue("HostName", out var hostName) ? hostName : "";
		User = node.Properties.TryGetValue("User", out var user) ? user : "";
		Properties = node.Properties;
	}

	public override string ToString() => string.Join("\n", Properties.Select(kv => $"{kv.Key} {kv.Value}"));
}
