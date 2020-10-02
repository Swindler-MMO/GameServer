using System;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;
using Swindler.GameServer.Errors;
using Swindler.GameServer.Game;

namespace Swindler.GameServer
{
	public class EventListener : INetEventListener
	{

		private GameManager _gm;

		public EventListener(GameManager gm)
		{
			_gm = gm;
		}
		
		public void OnPeerConnected(NetPeer peer)
		{
			_gm.OnPlayerConnected(peer.Id);
		}

		public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
		{
			_gm.OnPlayerDisconnected(peer.Id, disconnectInfo);
		}
		
		public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
		{
			//"Got packet".Log();

			short packetId = reader.GetShort();

			switch (packetId)
			{
				case 1: _gm.HandleMovePacket(peer.Id, reader); break;
				case 2: _gm.HandleInteractResourcePacket(peer.Id, reader); break;
			}
		}

		public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
		{
			_gm.SetPing(peer.Id, peer.Ping);
		}

		public void OnConnectionRequest(ConnectionRequest rq)
		{
			try
			{

				PlayerAuth data = new PlayerAuth(rq.Data);
				//This will throw an exception in case of protocol error
				data.Validate();
				
				_gm.AddPlayer(rq.Accept(), data);
			}
			catch (Exception e)
			{

				if (e is PlayerAuthException)
				{
					rq.Reject(NetDataWriter.FromString(e.Message));
					return;
				}
				
				Console.WriteLine(e);
				rq.Reject(NetDataWriter.FromString("An unknown error occured"));
			}

		}
		
		public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
		{
		}
		public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
		{
		}
	}
}