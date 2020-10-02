using System;
using System.Collections.Generic;
using Swindler.GameServer.Utilities;
using Swindler.GameServer.Utilities.Extensions;

namespace Swindler.GameServer.Structures
{
	public class ResourceInteraction
	{
		private static readonly int INTERACT_COOLDOWN = Config.ResourceInteractCooldown;

		private byte _id;
		private byte _hitsLeft;
		private DateTime _lastUpdate;
		private Dictionary<byte, Resource> _resources = Config.Resources;

		public ResourceInteraction(byte id)
		{
			_id = id;;
			_hitsLeft = _resources[id].HitsRequired;
			_lastUpdate = DateTime.UtcNow - TimeSpan.FromMilliseconds(INTERACT_COOLDOWN);
		}

		public bool Interact()
		{
			if ((DateTime.UtcNow - _lastUpdate).TotalMilliseconds < INTERACT_COOLDOWN)
			{
				$"Interacted too quickly {(DateTime.UtcNow - _lastUpdate).TotalMilliseconds}".Log();
				return false;
			}

			_lastUpdate = DateTime.UtcNow;

			_hitsLeft--;
			return _hitsLeft <= 0;
		}
		
	}
}