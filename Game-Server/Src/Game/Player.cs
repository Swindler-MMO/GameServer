using System;
using System.Collections.Generic;
using Game_Server.Game;
using LiteNetLib;
using LiteNetLib.Utils;
using Swindler.GameServer.Errors;
using Swindler.GameServer.Packets;
using Swindler.GameServer.Packets.Players;
using Swindler.GameServer.Packets.Server;
using Swindler.GameServer.Structures;
using Swindler.GameServer.Utilities;
using Swindler.GameServer.Utilities.Extensions;

namespace Swindler.GameServer.Game
{
	public class Player
	{
		public int Id { get; }
		public string Name { get; }
		public int Ping { get; set; }

		public Vector2 Position { get; private set; }
		public bool HasMoved { get; set; }

		private NetPeer _peer;
		private InteractionsManager _interactions;

		public Player(NetPeer peer, PlayerAuth auth)
		{
			Id = peer.Id;
			Name = auth.Name;
			Ping = peer.Ping;
			
			//Should be invalid as game is (0;0) (10k;10k)
			Position = new Vector2(-12, -12);

			_peer = peer;
			_interactions = new InteractionsManager(this);
		}

		public void SetPosition(Vector2 pos)
		{
			Position = pos;
			HasMoved = true;
			$"Is at position: {pos}".Log(this);
		}

		public void InteractResource(PlayerInteractResourcePacket p)
		{
			_interactions.InteractResource(p.Position, p.ResourceId);
		}

		public void Send(SwindlerPacket p, DeliveryMethod method)
		{
			//"Was sent a packet".Log(this);
			_peer.Send(p.Serialize(), method);
		}
	}
	
	public class PlayerAuth
	{
		private const uint PROTOCOL_KEY = 0xCAFEBABE;

		public string Name { get; }

		public PlayerAuth(NetDataReader reader)
		{
			if (reader.GetUInt() != PROTOCOL_KEY)
				throw new PlayerAuthException("Invalid protocol version, try updating your game");

			Name = reader.GetString();
		}

		public void Validate()
		{
			if (Name.Length < 3 || Name.Length > 15)
				throw new PlayerAuthException("Invalid username, name must be between 3 and 15 characters");
		}
	}
}