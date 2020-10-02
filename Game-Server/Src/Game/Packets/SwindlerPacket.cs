using LiteNetLib.Utils;

namespace Swindler.GameServer.Packets
{
	public abstract class SwindlerPacket
	{

		private short packetId;
		private readonly NetDataWriter w;

		protected SwindlerPacket(short packetId)
		{
			this.packetId = packetId;
			
			w = new NetDataWriter();
		}

		public NetDataWriter Serialize()
		{
			w.Put(packetId);
			PerformSerialization(w);
			return w;
		}
		
		protected abstract void PerformSerialization(NetDataWriter w);
		
	}
}