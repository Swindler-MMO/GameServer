namespace Swindler.GameServer.Utilities
{
	public class Vector2
	{

		public float x;
		public float y;

		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public override string ToString()
		{
			return $"({x};{y})";
		}
	}
}