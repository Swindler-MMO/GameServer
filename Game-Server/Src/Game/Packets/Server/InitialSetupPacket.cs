using System.Collections.Generic;
using LiteNetLib.Utils;
using Swindler.GameServer.Game;
using Swindler.GameServer.Utilities.Extensions;

namespace Swindler.GameServer.Packets.Server
{
	public class InitialSetupPacket : SwindlerPacket
	{

		private static short PACKET_ID = 0;
		
		private readonly List<Player> _players;
		private readonly int _playerId;
		
		public InitialSetupPacket(List<Player> players, int playerId) : base(PACKET_ID)
		{
			_players = players;
			_playerId = playerId;
		}
		
		protected override void PerformSerialization(NetDataWriter w)
		{

			w.Put(_playerId);
			//We do not send the current player
			w.Put(_players.Count - 1);
			
			foreach (Player p in _players)
			{
				if(p.Id == _playerId)
					continue;

				w.Put(p.Id);
				w.Put(p.Name);
				w.Put(p.Position);

			}
			
			var nodes = ResourceNodes.List();
			w.Put(nodes.Length);
			foreach (ResourceNodeData node in nodes)
			{
				w.Put(node.Position);
			}
		}
	}
}