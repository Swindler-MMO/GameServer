using LiteNetLib.Utils;
using Swindler.GameServer.Utilities;
using Swindler.GameServer.Utilities.Extensions;

namespace Swindler.GameServer.Packets.Players
{
	public class PlayerInteractResourcePacket
	{

		public Vector2Int Position { get; private set; }
		public byte ResourceId { get; private set; }

		public PlayerInteractResourcePacket(NetDataReader r)
		{
			Position = r.GetVector2Int();
			ResourceId = r.GetByte();
			
		}
		
	}
}