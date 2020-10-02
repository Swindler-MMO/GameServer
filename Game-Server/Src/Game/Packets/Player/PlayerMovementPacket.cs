using LiteNetLib.Utils;
using Swindler.GameServer.Utilities;
using Swindler.GameServer.Utilities.Extensions;

namespace Swindler.GameServer.Packets.Players
{
	public class PlayerMovementPacket
	{
		
		public Vector2 Position { get; }

		public PlayerMovementPacket(NetDataReader r)
		{
			Position = r.GetVector2();
		}
		
	}
}