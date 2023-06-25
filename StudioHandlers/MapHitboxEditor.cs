using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Collections.Generic;

namespace Eventful
{
	public class MapHitboxEditor
	{
		public static GameSession CurrentSession = Program.CurrentSession;
		public static UserInputHandler UserInputHandler = CurrentSession.UserInputHandler;
		private Connection _updateConnection;
		private Collider _currentHitbox;
		public MapHitboxEditor()
		{
			CurrentSession.PhysicsHandler.DebugMode = true;
			UserInputHandler.MouseButton1Down.Connect((Vector2 Position) => {
				startDrawingHitbox(Position);
			});
			UserInputHandler.MouseButton1Up.Connect((Vector2 Position) => {
				stopDrawingHitbox();
			});
		}

		private void startDrawingHitbox(Vector2 startPosition)
		{
			_currentHitbox = new(startPosition, new Vector2(0, 0));
			_currentHitbox.Anchored = true;
			CurrentSession.MapHitboxManager.AddHitbox(_currentHitbox);

			this._updateConnection = CurrentSession.PreRender.Connect((double step) => {
				Vector2 endPosition = UserInputHandler.GetMouseLocation();
				_currentHitbox.Size = endPosition - startPosition;
			});
		}
		private void stopDrawingHitbox()
		{
			this._updateConnection.Disconnect();
			this._updateConnection = null;

			CurrentSession.MapHitboxManager.SaveHitboxData(MapHitboxManager.Hitboxes.Count - 1, _currentHitbox.Size, _currentHitbox.Position);
			_currentHitbox = null;
		}
	}
}