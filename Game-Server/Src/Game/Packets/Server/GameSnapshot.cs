using System.Collections.Generic;
using System.Linq;
using LiteNetLib.Utils;
using Swindler.GameServer.Game;
using Swindler.GameServer.Utilities;
using Swindler.GameServer.Utilities.Extensions;

namespace Swindler.GameServer.Packets.Server
{
	public class GameSnapshot : SwindlerPacket
	{

		private const short PACKET_ID = 4;

		private readonly Dictionary<int, Player> _players;
		private readonly Dictionary<int, Vector2> _positions;
		public GameSnapshot(Dictionary<int, Player> players) : base(PACKET_ID)
		{
			_players = players;
			_positions = new Dictionary<int, Vector2>();

			//There is probably a better way than this whole mess
			foreach (Player p in _players.Values)
			{
				if(!p.HasMoved)
					continue;
				_positions.Add(p.Id, p.Position);
				p.HasMoved = false;
			}
		}

		protected override void PerformSerialization(NetDataWriter w)
		{
			w.Put(_positions.Count);
			
			foreach (var kv in _positions)
			{
				w.Put(kv.Key);
				w.Put(kv.Value);
			}
		}
	}
}