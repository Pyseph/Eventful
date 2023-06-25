
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace Eventful
{
	public class AppearanceHitboxes
	{
		// key is AppearanceName and values are {HitboxSize, HitboxOffset, PositionOffset}
		public static readonly Dictionary<string, Tuple<Vector2, Vector2, Vector2>> Data = new()
		{
			{ "TestBox", new(new(32, 32), new(0, 0), new(0, 0)) }
		};
	}
}