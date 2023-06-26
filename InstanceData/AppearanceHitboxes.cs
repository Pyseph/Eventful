
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace Eventful
{
	public class AppearanceHitboxes
	{
		// key is AppearanceName and values are {HitboxSize, HitboxOffset, PositionOffset}
		public Dictionary<string, Tuple<Vector2, Vector2, Vector2>> Data = new()
		{
			//{ "Example", new(new(32, 32), new(0, 0), new(0, 0)) }
			{ "Tileset_Grass", new(new(256, 256), new(0, 0), new(0, 0)) }
		};
		public AppearanceHitboxes()
		{
			addTilesetTextures("Grass", 32, 64);
		}
		private void addTilesetTextures(string BaseName, int TileSize, int TileCount)
		{
			for (int i = 0; i < TileCount; i++)
			{
				int x = i % 8;
				int y = i / 8;
				Data.Add(BaseName + i, new(new(TileSize, TileSize), new(x * TileSize, y * TileSize), new(0, 0)));
			}
		}
	}
}