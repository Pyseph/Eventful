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
		public Vector2 TextureSize;
		public Vector2 TextureOffset;
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
			var Data = CurrentSession.TextureData.Data[TileName ?? TextureName];
			var TextureSize = Data.Item1;
			var TextureOffset = Data.Item2;

			this.TextureSize = TextureSize;
			this.TextureOffset = TextureOffset;
			this.Texture = RenderHandler.LoadTexture(TextureName);

			this.RenderBounds = new Rectangle((int)TextureOffset.X, (int)TextureOffset.Y, (int)TextureSize.X, (int)TextureSize.Y);
		}
		private void addRender()
		{
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