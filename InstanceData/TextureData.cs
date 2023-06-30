
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace Eventful
{
	public class TextureData
	{
		// key is AppearanceName and values are {TextureSize, TextureOffset}
		public Dictionary<string, Tuple<Vector2, Vector2>> Data = new()
		{
			//{ "Example", new(new(32, 32), new(0, 0), new(0, 0)) }
			{ "Tileset_Grass", new(new(256, 256), new(0, 0)) },

			{"Wall1", new(new Vector2(127, 159) - new Vector2(32, 32), new(32, 32))},
			{"Wall2", new(new Vector2(265, 135) - new Vector2(152, 32), new(152, 32))},
			{"Wall3", new(new Vector2(297, 127) - new Vector2(288, 32), new(288, 32))},
			{"Wall4", new(new Vector2(351, 127) - new Vector2(344, 32), new(344, 32))},
			{"Wall5", new(new Vector2(479, 39) - new Vector2(384, 32), new(384, 32))},
			{"Wall6", new(new Vector2(297, 127) - new Vector2(288, 32), new(288, 32))},
			{"Wall7", new(new Vector2(447, 159) - new Vector2(384, 64), new(384, 64))},
			{"Wall8", new(new Vector2(159, 255) - new Vector2(32, 192), new(32, 192))},
			{"Wall9", new(new Vector2(223, 255) - new Vector2(192, 192), new(192, 192))},
			{"Wall10", new(new Vector2(95, 351) - new Vector2(32, 286), new(32, 286))},
			{"Wall11", new(new Vector2(191, 351) - new Vector2(128, 288), new(128, 288))},
		};
		public TextureData()
		{
			// 64 tiles, 32x32 px each
			addTilesetTextures("Grass", 32, 64);
		}
		private void addTilesetTextures(string BaseName, int TileSize, int TileCount)
		{
			for (int i = 0; i < TileCount; i++)
			{
				int x = i % 8;
				int y = i / 8;
				Data.Add(BaseName + i, new(new(TileSize, TileSize), new(x * TileSize, y * TileSize)));
			}
		}
	}
}