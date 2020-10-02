using LiteNetLib.Utils;
using Swindler.GameServer.Game;
using Swindler.GameServer.Utilities.Extensions;

namespace Swindler.GameServer.Packets.Server
{
	public class PlayerJoinedPacket : SwindlerPacket
	{

		private const short PACKET_ID = 3;

		private readonly Player _p;
		
		public PlayerJoinedPacket(Player p) : base(PACKET_ID)
		{
			_p = p;
		}

		protected override void PerformSerialization(NetDataWriter w)
		{
			w.Put(_p.Id);
			w.Put(_p.Name);
			w.Put(_p.Position);
		}
	}
}