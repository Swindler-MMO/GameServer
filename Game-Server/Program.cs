using System.Collections.Generic;
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
			ConfigView view = HttpUtils.Get<ConfigView>("http://swindler.thebad.xyz/config/server").Result;
			
			Config.FromView(view);
		}
		
	}

	public class ConfigView
	{
		
		public int UpdatesPerSeconds { get; set; }
		public int ResourceInteractCooldown { get; set; }
		public int ResourceRespawnTime { get; set; }

		public List<Resource> Resources { get; set; }

	}

}