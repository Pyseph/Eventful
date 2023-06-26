using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Eventful
{
	public class Appearance
	{
		public static GameSession CurrentSession = Program.CurrentSession;
		public Texture2D Texture;
		public Rectangle RenderBounds;
		public Vector2 PositionOffset;
		public Object Parent
		{
			get => _parent;
			set
			{
				_parent = value;
				if (_parent != null) addRender();
				else _renderConnection.Disconnect();
			}
		}
		private Object _parent;
		private Connection _renderConnection;
		public Appearance(string TextureName, string TileName = null)
		{
			var Data = CurrentSession.AppearanceHitboxes.Data[TileName ?? TextureName];
			var HitboxSize = Data.Item1;
			var HitboxOffset = Data.Item2;
			var PositionOffset = Data.Item3;

			this.PositionOffset = PositionOffset;
			this.Texture = RenderHandler.LoadTexture(TextureName);

			this.RenderBounds = new Rectangle((int)HitboxOffset.X, (int)HitboxOffset.Y, (int)HitboxSize.X, (int)HitboxSize.Y);
		}
		private void addRender()
		{
			Debug.WriteLine("Adding render" + RenderBounds);
			this.Parent.Appearance = this;
			_renderConnection = CurrentSession.ProcessRender.Connect((double timeSinceLastFrame) =>
			{
				RenderHandler.DrawTexture(this.Texture, this.Parent.Position, RenderBounds, 0);
			});
		}
		public void Destroy()
		{
			if (this.Parent != null) this.Parent.Appearance = null;
			if (_renderConnection != null) _renderConnection.Disconnect();
		}
	}
}