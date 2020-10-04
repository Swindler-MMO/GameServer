using LiteNetLib.Utils;
using Swindler.GameServer.Utilities;
using Swindler.GameServer.Utilities.Extensions;

namespace Swindler.GameServer.Packets.Server
{
	public class ResourceRespawnPacket : SwindlerPacket
	{
		
		private const short PACKET_ID = 2;

		private readonly Vector2Int _p;
		private readonly byte _type;

		public ResourceRespawnPacket(ResourceNode d) : base(PACKET_ID)
		{
			_p = d.Position;
			_type = d.ResourceType;
		}
		
		protected override void PerformSerialization(NetDataWriter w)
		{
			w.Put(_p);
			w.Put(_type);
		}
	}
}