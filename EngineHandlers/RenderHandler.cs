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

		public static void DrawBox(Vector2 Position, Vector2 Size)
		{
			CurrentSession.SpriteBatch.Draw(
				texture: _hitboxTexture,
				destinationRectangle: new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y),
				color: Color.White,
				effects: SpriteEffects.None,
				layerDepth: 0.1f,
				rotation: 0,
				origin: new Vector2(0, 0),
				sourceRectangle: null
			);
		}
	}
}