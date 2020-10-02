using LiteNetLib.Utils;

namespace Swindler.GameServer.Utilities.Extensions
{
	public static class NetDataWriterExtensions
	{
		
		public static void Put(this NetDataWriter w, Vector2 p)
		{
			w.Put(p.x);
			w.Put(p.y);
		}
		public static void Put(this NetDataWriter w, Vector2Int p)
		{
			w.Put(p.x);
			w.Put(p.y);
		}
		
		public static void Put(this NetDataWriter w, Vector3Int p)
		{
			w.Put(p.x);
			w.Put(p.y);
			w.Put(p.z);
		}
		
		
		
	}
}