﻿namespace Swindler.GameServer.Utilities
{
	public class Vector3Int
	{
		public int x;
		public int y;
		public int z;

		public Vector3Int(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public override string ToString()
		{
			return $"({x};{y};{z})";
		}
	}
}