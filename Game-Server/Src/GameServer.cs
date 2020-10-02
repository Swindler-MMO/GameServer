using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using LiteNetLib;
using Swindler.GameServer.Packets;
using Swindler.GameServer.Utilities;
using Swindler.GameServer.Utilities.Extensions;

namespace Swindler.GameServer
{
	public class GameServer
	{

		public int TickPerSeconds { get; private set; }
		
		private readonly NetManager _manager;
		private readonly GameManager _game;

		public GameServer(int updatesPerSecond)
		{
			TickPerSeconds = updatesPerSecond;
			_game = new GameManager(this);
			_manager = new NetManager(new EventListener(_game));
		}

		public void Start(int port)
		{
			_manager.Start(port);
			$"Starting server on port {port}".Log();
			Run();
		}

		private void Run()
		{
			Stopwatch watch = new Stopwatch();

			int deltaTime = 0;
			int tickTime = 1000 / TickPerSeconds;

			while (true)
			{

				watch.Restart();

				Update(deltaTime);

				watch.Stop();

				deltaTime = tickTime - (int)watch.ElapsedMilliseconds;

				if (deltaTime > 0)
					Thread.Sleep(deltaTime);
			}
			// ReSharper disable once FunctionNeverReturns
		}

		private void Update(int deltaTime)
		{
			//$"Updating server after {deltaTime}".Log();
			
			Time.deltaTime = deltaTime;
			
			_manager.PollEvents();

			_game.Update();
		}

		public void Broadcast(SwindlerPacket p)
		{
			_manager.SendToAll(p.Serialize(), DeliveryMethod.ReliableOrdered);
		}
	}
}