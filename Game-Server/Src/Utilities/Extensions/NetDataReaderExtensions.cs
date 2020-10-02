﻿using LiteNetLib.Utils;

namespace Swindler.GameServer.Utilities.Extensions
{
	public static class NetDataReaderExtensions
	{
		
		public static Vector2 GetVector2(this NetDataReader r)
		{
			return new Vector2(r.GetFloat(), r.GetFloat());
		}
		public static Vector2Int GetVector2Int(this NetDataReader r)
		{
			return new Vector2Int(r.GetInt(), r.GetInt());
		}
		
		public static Vector3Int GetVector3Int(this NetDataReader r)
		{
			return new Vector3Int(r.GetInt(), r.GetInt(), r.GetInt());
		}
		
	}
}