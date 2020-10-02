using LiteNetLib.Utils;
using Swindler.GameServer.Utilities;
using Swindler.GameServer.Utilities.Extensions;

namespace Swindler.GameServer.Packets.Server
{
	public class ResourceRemovedPacket : SwindlerPacket
	{
		
		private const short PACKET_ID = 6;

		private Vector2Int _position;
		
		public ResourceRemovedPacket(Vector2Int position) : base(PACKET_ID)
		{
			_position = position;
		}

		protected override void PerformSerialization(NetDataWriter w)
		{
			w.Put(_position);
		}
	}
}