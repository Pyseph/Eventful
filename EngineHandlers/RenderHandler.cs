using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eventful
{
	public class RenderHandler
	{
		public static GameSession CurrentSession = Program.CurrentSession;
		private static Texture2D _hitboxTexture = CurrentSession.Content.Load<Texture2D>("BoxOutline");

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

		public static void DrawBox(Vector2 Position, Vector2 Size, object sender = null)
		{
			// Generate a unique color for each object using the golden ratio
			float goldenRatio = (float)(1 + System.Math.Sqrt(5)) / 2;
			float hue = sender != null ? sender.GetHashCode() * goldenRatio : 0;
			Color color = colorFromHSV(hue, 1, 1);

			CurrentSession.SpriteBatch.Draw(
				texture: _hitboxTexture,
				destinationRectangle: new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y),
				color: color,
				effects: SpriteEffects.None,
				layerDepth: 0.1f,
				rotation: 0,
				origin: new Vector2(0, 0),
				sourceRectangle: null
			);
		}
	}
}