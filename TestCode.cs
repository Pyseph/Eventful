using Microsoft.Xna.Framework;
using System.Diagnostics;
using System;
using System.Threading;
namespace Eventful
{
	public class TestCode
	{
		public static GameSession CurrentSession = Program.CurrentSession;
		public TestCode()
		{
			var random = new Random();
			// fill the screen with grass tiles from numbers [0, 32] and 3d noise to make it look more natural
			Noise2d.Reseed();
			for (int x = 0; x < 32; x++)
			{
				for (int y = 0; y < 18; y++)
				{
					// use random to offset the coordinates so that the noise is different for each tile
					var xCoord = (float)(x + random.NextDouble());
					var yCoord = (float)(y + random.NextDouble());

					var noise = Noise2d.Noise(xCoord, yCoord); // this is between [-1, 1]
					noise = (noise + 1) / 2; // change the domain to [0, 1]
					var tileIndex = (int)(noise * 32);

					Object Tile = AddTile(new Vector2(x * 32, y * 32), "Tileset_Grass", "Grass" + tileIndex);
					Tile.ZIndex = 0;
				}
			}

			//AddTile(new Vector2(100, 100), "Tileset_Walls", "Wall1");
			System.Threading.Thread.Sleep(2000);
			Object previousWall = null;
			for (int i = 1; i <= 11; i++)
			{
				var wall = AddTile(new Vector2(100, 100), "Tileset_Walls", "Wall" + i);
				wall.ZIndex = 1;
				if (previousWall != null) previousWall.Destroy();
				previousWall = wall;

				// sleep 1 second before spawning the next wall
				System.Threading.Thread.Sleep(1000);
			}
		}
		public Object AddTestBox(Vector2 Size, Vector2 Position, Vector2 Velocity, string Tileset, string TileName)
		{
			Object TestBox = new(Position, Size);
			Appearance TestBoxAppearance = new(Tileset, TileName);
			TestBoxAppearance.Parent = TestBox;

			CurrentSession.PrePhysics.Connect((double step) => {
				//TestBox.Position += Velocity * (float)step;
			});
			return TestBox;
		}
		public Object AddTile(Vector2 Position, string Tileset, string TileName)
		{
			Appearance TileAppearance = new(Tileset, TileName);

			Object Tile = new(Position, TileAppearance.TextureSize);
			Tile.Anchored = true;
			Tile.CanCollide = false;

			TileAppearance.Parent = Tile;
			return Tile;
		}
	}
}