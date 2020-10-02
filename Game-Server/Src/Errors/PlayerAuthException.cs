using System;

namespace Swindler.GameServer.Errors
{
	public class PlayerAuthException : Exception
	{
		public PlayerAuthException(string message) : base(message)
		{
		}
	}
}