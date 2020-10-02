namespace Swindler.GameServer.Structures
{
	public class Resource
	{
		public byte Id { get; set; }
		public short MinAmount { get; set; }
		public short MaxAmount { get; set; }
		public byte HitsRequired { get; set; }
	}
}