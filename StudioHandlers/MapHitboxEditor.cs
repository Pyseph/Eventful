using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Eventful
{
	public class MapHitboxEditor
	{
		public static GameSession CurrentSession = Program.CurrentSession;
		public static UserInputHandler UserInputHandler = CurrentSession.UserInputHandler;
		private Connection updateConnection;
		public MapHitboxEditor()
		{
			CurrentSession.PhysicsHandler.DebugMode = true;
			UserInputHandler.MouseButton1Down.Connect((Vector2 Position) => {
				startDrawingHitbox();
			});
			UserInputHandler.MouseButton1Up.Connect((Vector2 Position) => {
				stopDrawingHitbox();
			});
		}

		private void startDrawingHitbox()
		{
			Vector2 startPosition = UserInputHandler.GetMouseLocation();
			Collider hitbox = new(startPosition, new Vector2(0, 0));
			hitbox.Anchored = true;

			this.updateConnection = CurrentSession.PreRender.Connect((double step) => {
				Vector2 endPosition = UserInputHandler.GetMouseLocation();
				hitbox.Size = endPosition - startPosition;
			});
		}
		private void stopDrawingHitbox()
		{
			this.updateConnection.Disconnect();
			this.updateConnection = null;
		}
	}
}