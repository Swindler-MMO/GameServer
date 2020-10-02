using System;
using LiteNetLib;
using Swindler.GameServer.Game;

namespace Swindler.GameServer.Utilities.Extensions
{
	public static class LogExtensions
	{

		public const string PREFIX = "[Swindler]";

		public static void Log(this string value, string name = null)
		{
			Print(value, name);
		}
		
		public static void Log(this string value, Player player)
		{
			PrintPlayer(value, player);
		}
		
		public static void Log(this int value, string name = null)
		{
			Print(value, name);
		}

		public static void Log(this float value, string name = null)
		{
			Print(value, name);
		}
		public static void Log(this double value, string name = null)
		{
			Print(value, name);
		}
		
		public static void Log(this bool value, string name = null)
		{
			Print(value, name);
		}

		public static void Log(this NetPeer v, string name = null)
		{
			Print($"Peer #{v.Id} at {v.EndPoint}", name);
		}

		private static void Print(object value, string name)
		{
			if(string.IsNullOrEmpty(name))
			{
				Console.WriteLine($"{PREFIX} {value}");
				return;
			}

			Console.WriteLine($"{PREFIX} {name} :>> {value}");
		}
		
		private static void PrintPlayer(object value, Player p)
		{
			if(p == null)
			{
				Console.WriteLine($"{PREFIX} {value}");
				return;
			}

			Console.WriteLine($"{PREFIX} [Player #{p.Id}] {value}");
		}

	}
}