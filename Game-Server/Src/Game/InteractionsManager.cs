using System;
using System.Collections.Generic;
using LiteNetLib;
using Swindler.GameServer;
using Swindler.GameServer.Game;
using Swindler.GameServer.Packets.Players;
using Swindler.GameServer.Packets.Server;
using Swindler.GameServer.Structures;
using Swindler.GameServer.Utilities;
using Swindler.GameServer.Utilities.Extensions;

namespace Game_Server.Game
{
	public class InteractionsManager
	{
		private readonly Dictionary<Vector2Int, ResourceInteraction> _interactions;
		private readonly Player _p;

		public InteractionsManager(Player p)
		{
			_interactions = new Dictionary<Vector2Int, ResourceInteraction>();
			_p = p;
		}

		public void InteractResource(Vector2Int position, byte resourceId)
		{
			if (ResourceSpawner.IsOnCooldown(position, resourceId))
				return;

			if (!_interactions.ContainsKey(position))
				_interactions.Add(position, new ResourceInteraction(resourceId));

			//Resource has been mined until the end, giving player resource
			if (!_interactions[position].Interact()) return;

			OnMined(position, resourceId);
		}

		private void OnMined(Vector2Int position, byte resourceId)
		{
			$"Has mined resource {resourceId}".Log(_p);

			Resource r = Config.Resources[resourceId];
			
			_p.Send(new ServerGiveItems(position, resourceId, (ushort) new Random().Next(r.MinAmount, r.MaxAmount)),
				DeliveryMethod.ReliableUnordered);
			
			_interactions.Remove(position);
			
			GameManager.GameServer.Broadcast(new ResourceRemovedPacket(position));
			
			ResourceSpawner.Add(position, resourceId);
		}
		
	}
}