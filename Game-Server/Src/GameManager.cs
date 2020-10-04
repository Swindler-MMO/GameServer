using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;
using Swindler.GameServer.Errors;
using Swindler.GameServer.Packets.Players;
using Swindler.GameServer.Utilities;
using Swindler.GameServer.Game;
using Swindler.GameServer.Packets.Server;
using Swindler.GameServer.Utilities.Extensions;

namespace Swindler.GameServer
{
	public class GameManager
	{

		public static GameManager Instance { get; private set; }
		public static GameServer GameServer { get; private set; }
		
		private Dictionary<int, Player> _players;

		public GameManager(GameServer gameServer)
		{
			Instance = this;
			GameServer = gameServer;
			_players = new Dictionary<int, Player>();
		}
		
		public void Update()
		{
			ResourceSpawner.Update();
			SendSnapshot();
		}

		private void SendSnapshot()
		{
			if (_players.Count == 0)
				return;
			
			GameServer.Broadcast(new GameSnapshot(_players));
		}

		public void OnPlayerConnected(int peerId)
		{
			Player p = _players[peerId];
			"Connected".Log(p);
			
			p.Send(new InitialSetupPacket(_players.Values.ToList(), peerId), DeliveryMethod.ReliableOrdered);
			GameServer.Broadcast(new PlayerJoinedPacket(p));
		}

		public void OnPlayerDisconnected(int peerId, DisconnectInfo disconnectInfo)
		{
			$"Disconnected ({disconnectInfo.Reason})".Log(_players[peerId]);
			_players.Remove(peerId);
			GameServer.Broadcast(new PlayerLeftPacket(peerId));
		}

		public void SetPing(int peerId, int latency)
		{
			//Got an error at some point after disconnect, that's weird
			if(_players.TryGetValue(peerId, out Player p))
				p.Ping = latency;
		}

		public void AddPlayer(NetPeer peer, PlayerAuth data)
		{
			_players.Add(peer.Id, new Player(peer, data));
		}
		
		public void HandleMovePacket(int playerId, NetDataReader reader)
		{
			PlayerMovementPacket packet = new PlayerMovementPacket(reader);
			
			_players[playerId].SetPosition(packet.Position);
		}

		public void HandleInteractResourcePacket(int playerId, NetDataReader reader)
		{
			_players[playerId].InteractResource(new PlayerInteractResourcePacket(reader));
		}
		
		
	}
}