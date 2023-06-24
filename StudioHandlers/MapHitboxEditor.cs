using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Eventful
{
	public class MapHitboxEditor
	{
		public static GameSession CurrentSession = Program.CurrentSession;
		public static UserInputHandler UserInputHandler = CurrentSession.UserInputHandler;
		public bool IsDrawingHitbox = false;
		private Connection updateConnection;
		public MapHitboxEditor()
		{
			CurrentSession.PhysicsHandler.DebugMode = true;
			UserInputHandler.MouseButton1Down.Connect((Vector2 Position) => {
				StartDrawingHitbox();
			});
			UserInputHandler.MouseButton1Up.Connect((Vector2 Position) => {
				StopDrawingHitbox();
			});
		}

		public void StartDrawingHitbox()
		{
			IsDrawingHitbox = true;
			Vector2 startPosition = UserInputHandler.GetMouseLocation();

			Collider hitbox = new(startPosition, new Vector2(0, 0));
			hitbox.Anchored = true;

			this.updateConnection = CurrentSession.PreRender.Connect((double step) => {
				Vector2 endPosition = UserInputHandler.GetMouseLocation();
				hitbox.Size = endPosition - startPosition;
			});
		}
		public void StopDrawingHitbox()
		{
			IsDrawingHitbox = false;
			this.updateConnection.Disconnect();
			this.updateConnection = null;
		}
	}
}