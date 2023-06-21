using Microsoft.Xna.Framework;

namespace Eventful
{
	public class Object
	{
		// get and set position and size directly to collider
		public Vector2 Position
		{
			get => Collider.Position;
			set => Collider.Position = value;
		}
		public Vector2 Size
		{
			get => Collider.Size;
			set => Collider.Size = value;
		}
		public bool Anchored
		{
			get => Collider.Anchored;
			set => Collider.Anchored = Anchored;
		}
		public Collider Collider;
		public bool Visible
		{
			get => _visible;
			set
			{
				_visible = value;
				if (_visible)
				{
					addToRenderQueue();
				}
				else
				{
					removeFromRenderQueue();
				}
			}
		}
		private bool _visible = true;
		public Object(Vector2 Position, Vector2 Size)
		{
			this.Collider = new Collider(Position, Size);
			this.Position = Position;
			this.Size = Size;

			addToRenderQueue();
		}

		private void addToRenderQueue()
		{
			Program.CurrentSession.DrawQueue.Add(this, (double timeSinceLastFrame) =>
			{
				RenderHandler.DrawBox(this.Position, this.Size);
			});
		}
		private void removeFromRenderQueue()
		{
			Program.CurrentSession.DrawQueue.Remove(this);
		}

		public void Destroy()
		{
			this.Visible = false;
			this.Collider.Destroy();
		}
	}
}