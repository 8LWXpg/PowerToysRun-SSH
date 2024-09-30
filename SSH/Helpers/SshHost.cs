using System;
using Community.PowerToys.Run.Plugin.SSH.Helpers.Config;

namespace Community.PowerToys.Run.Plugin.SSH.Helpers;

public class SshHost(ConfigNode node)
{
	public string Host { get; set; } = node.Host;
	public string HostName { get; set; } = node.Properties.TryGetValue("HostName", out string? hostName) ? hostName : "";
	public string User { get; set; } = node.Properties.TryGetValue("User", out string? user) ? user : "";
}
