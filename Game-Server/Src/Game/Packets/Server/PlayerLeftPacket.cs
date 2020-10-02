using LiteNetLib.Utils;

namespace Swindler.GameServer.Packets.Server
{
	public class PlayerLeftPacket : SwindlerPacket
	{

		private const short PACKET_ID = 5;
		
		public int _id;


		public PlayerLeftPacket(int playerId) : base(PACKET_ID)
		{
			_id = playerId;
		}

		protected override void PerformSerialization(NetDataWriter w)
		{
			w.Put(_id);
		}
	}
}