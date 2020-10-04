using System.Collections.Generic;
using Swindler.GameServer.Packets.Server;
using Swindler.GameServer.Utilities;
using Swindler.GameServer.Utilities.Extensions;

namespace Swindler.GameServer
{
	public static class ResourceSpawner
	{
		private static readonly float RESPAWN_COOLDOWN = Config.ResourceRespawnTime;

		private static readonly Queue<ResourceNode> _minedNodes = new Queue<ResourceNode>();

		private static float _lastRespawn = 0f;
		
		public static void Add(Vector2Int position, byte resourceType)
		{
			_minedNodes.Enqueue(new ResourceNode(position, resourceType));
		}

		public static bool IsOnCooldown(Vector2Int position, byte type)
		{
			return _minedNodes.Contains(new ResourceNode(position, type));
		}

		public static void Update()
		{

			if (_minedNodes.Count <= 0)
				return;
			
			_lastRespawn += Time.deltaTime;
			//_lastRespawn.Log();
			
			if (!(_lastRespawn >= RESPAWN_COOLDOWN)) return;
			
			$"Respawning a node (nodes left: {_minedNodes.Count - 1})".Log();
			_lastRespawn = 0f;
			GameManager.GameServer.Broadcast(new ResourceRespawnPacket(_minedNodes.Dequeue()));
		}

		public static ResourceNode[] List()
		{
			return _minedNodes.ToArray();
		}

		public static bool HasNodes()
		{
			return _minedNodes.Count > 0;
		}
	}
	
	public class ResourceNode
	{
		public Vector2Int Position;
		public byte ResourceType;

		public ResourceNode(Vector2Int position, byte resourceType)
		{
			Position = position;
			ResourceType = resourceType;
		}

		public override bool Equals(object other)
		{
			return other is ResourceNode && Equals((ResourceNode)other);
		}
		
		public bool Equals(ResourceNode other)
		{
			return other.Position.Equals(Position) && other.ResourceType == ResourceType;
		}
	}
	
}