using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eventful
{
	public class Object
	{
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
			set => Collider.Anchored = value;
		}
		public bool CanCollide
		{
			get => _canCollide;
			set
			{
				_canCollide = value;
				if (_canCollide) Program.CurrentSession.PhysicsHandler.AddCollider(this.Collider);
				else Program.CurrentSession.PhysicsHandler.RemoveCollider(this.Collider);
			}
		}
		public Collider Collider;
		public int ZIndex = 1;
		private bool _canCollide = true;
		private bool _visible = true;
		public Appearance Appearance;
		public Object(Vector2 Position, Vector2 Size)
		{
			this.Collider = new Collider(Position, Size);
			this.Position = Position;
			this.Size = Size;
			this.CanCollide = true;
		}

		public void Destroy()
		{
			this.Appearance.Destroy();
			this.Collider.Destroy();
		}
	}
}