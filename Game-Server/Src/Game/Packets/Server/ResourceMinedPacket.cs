using LiteNetLib.Utils;
using Swindler.GameServer.Utilities;
using Swindler.GameServer.Utilities.Extensions;

namespace Swindler.GameServer.Packets.Server
{
	public class ServerGiveItems : SwindlerPacket {

		private const short PACKET_ID = 1;

		private readonly Vector2Int _position;
		private readonly ushort _itemId;
		private readonly ushort _amount;

		public ServerGiveItems(Vector2Int position, ushort itemId, ushort amount) : base(PACKET_ID)
		{
			_position = position;
			_itemId = itemId;
			_amount = amount;
		}

		protected override void PerformSerialization(NetDataWriter w)
		{
			w.Put(_position);
			w.Put(_itemId);
			w.Put(_amount);
		}
	}
}