using System.Collections.Generic;
using Swindler.GameServer.Structures;

namespace Swindler.GameServer
{
	public static class Config
	{
		
		public static int UpdatesPerSeconds { get; private set; }
		public static int ResourceInteractCooldown { get; private set; }
		public static int ResourceRespawnTime { get; private set; }
		public static List<Resource> ResourcesList { get; private set; }
		public static Dictionary<byte, Resource> Resources { get; private set; }

		public static void FromView(ConfigView view)
		{
			UpdatesPerSeconds = view.UpdatesPerSeconds;
			ResourceInteractCooldown = view.ResourceInteractCooldown;
			ResourceRespawnTime = view.ResourceRespawnTime;
			ResourcesList = view.Resources;
			Resources = new Dictionary<byte, Resource>();
			foreach (Resource resource in ResourcesList)
			{
				Resources.Add(resource.Id, resource);
			}
		}

	}
}