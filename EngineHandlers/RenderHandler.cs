using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace Eventful
{
	public class RenderHandler
	{
		public static GameSession CurrentSession = Program.CurrentSession;
		private static Dictionary<string, Texture2D> _loadedTextures = new();

		private static float _goldenRatio = (float)(1 + System.Math.Sqrt(5)) / 2;

		public RenderHandler()
		{
		}

		private static Color colorFromHSV(float hue, float saturation, float value)
		{
			int hi = (int)(System.Math.Floor(hue / 60)) % 6;
			float f = hue / 60 - (float)System.Math.Floor(hue / 60);

			value = value * 255;
			int v = (int)(value);
			int p = (int)(value * (1 - saturation));
			int q = (int)(value * (1 - f * saturation));
			int t = (int)(value * (1 - (1 - f) * saturation));

			return hi switch
			{
				0 => new Color(v, t, p),
				1 => new Color(q, v, p),
				2 => new Color(p, v, t),
				3 => new Color(p, q, v),
				4 => new Color(t, p, v),
				_ => new Color(v, p, q),
			};
		}
		public static Texture2D LoadTexture(string path)
		{
			if (_loadedTextures.ContainsKey(path))
			{
				return _loadedTextures[path];
			}
			else
			{
				Texture2D texture = CurrentSession.Content.Load<Texture2D>(path);
				_loadedTextures.Add(path, texture);
				return texture;
			}
		}

		public static void DrawBox(Vector2 Position, Vector2 Size, object sender = null)
		{
			float hue = sender != null ? sender.GetHashCode() * _goldenRatio : 0;
			Color color = colorFromHSV(hue, 1, 1);

			int lineThickness = 2;
			// negative sizes aren't supported, so if the size is negative, flip it and the position
			if (Size.X < 0)
			{
				Size.X *= -1;
				Position.X -= Size.X;
			}
			if (Size.Y < 0)
			{
				Size.Y *= -1;
				Position.Y -= Size.Y;
			}

			var whiteSquare = LoadTexture("WhiteSquare");
			CurrentSession.SpriteBatch.Draw(whiteSquare, Position, new Rectangle(0, 0, (int)Size.X, lineThickness), color);
			CurrentSession.SpriteBatch.Draw(whiteSquare, Position, new Rectangle(0, 0, lineThickness, (int)Size.Y), color);
			CurrentSession.SpriteBatch.Draw(whiteSquare, Position + new Vector2(0, Size.Y - lineThickness), new Rectangle(0, 0, (int)Size.X, lineThickness), color);
			CurrentSession.SpriteBatch.Draw(whiteSquare, Position + new Vector2(Size.X - lineThickness, 0), new Rectangle(0, 0, lineThickness, (int)Size.Y), color);
		}

		public static void DrawTexture(Texture2D Texture, Vector2 Position, Rectangle SourceRectangle, int ZIndex)
		{
			if (Texture == null) throw new System.ArgumentNullException(nameof(Texture));

			CurrentSession.SpriteBatch.Draw(
				texture: Texture,
				position: Position,
				sourceRectangle: SourceRectangle,
				color: Color.White,
				rotation: 0,
				origin: Vector2.Zero,
				scale: 1,
				effects: SpriteEffects.None,
				layerDepth: (float)-ZIndex
			);
		}
	}
}