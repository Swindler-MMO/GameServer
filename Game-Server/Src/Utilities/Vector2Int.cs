namespace Swindler.GameServer.Utilities
{
	public class Vector2Int
	{
		public int x;
		public int y;

		public Vector2Int(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public override string ToString()
		{
			return $"({x};{y})";
		}

		public override bool Equals(object other)
		{
			return other is Vector2Int && Equals((Vector2Int)other);
		}
		
		public bool Equals(Vector2Int other)
		{
			return x == other.x && y == other.y;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ (y.GetHashCode() << 2);
		}
	}
}