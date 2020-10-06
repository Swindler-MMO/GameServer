using System.Collections.Generic;
using Newtonsoft.Json;
using Swindler.GameServer.Structures;
using Swindler.Utilities;

namespace Swindler.GameServer
{
	internal static class Program
	{
		public static void Main(string[] args)
		{
			LoadConfig();
			
			GameServer gs = new GameServer(Config.UpdatesPerSeconds);
			
			gs.Start(2525);
			
		}

		private static void LoadConfig()
		{
			ConfigData view = HttpUtils.Get<ConfigData>("http://swindler.thebad.xyz/configs/server").Result;
			
			Config.FromView(view.Config);
		}
		
	}

	public class ConfigData
	{
		[JsonProperty("config")] public ConfigView Config { get; set; }
	}

	public class ConfigView
	{
		
		public int UpdatesPerSeconds { get; set; }
		public int ResourceInteractCooldown { get; set; }
		public int ResourceRespawnTime { get; set; }

		public List<Resource> Resources { get; set; }

	}

}