using System;
using System.IO;
using System.Text.RegularExpressions;
using Community.PowerToys.Run.Plugin.SSH.Helpers.Config;

namespace Community.PowerToys.Run.Plugin.SSH.Helpers;

public partial class SshProfile
{
	public static readonly string ConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".ssh", "config");
	public static List<SshHost> Hosts
	{
		get
		{
			return new Parser(ConfigPath).Nodes.Select(node => new SshHost(node)).ToList();
		}
	}
}
