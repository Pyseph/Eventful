using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
namespace Eventful
{
	public class Collider
	{
		public Vector2 Position {
			get => _position;
			set {
				if (Anchored) _nextPosition = value;
				_position = value;
			}
		}
		public Vector2 Size;
		public bool Anchored;
		public static PhysicsHandler PhysicsHandler = Program.CurrentSession.PhysicsHandler;
		private Vector2 _nextPosition;
		private Vector2 _position;

		public Collider(Vector2 InitialPosition, Vector2 InitialSize)
		{
			Position = InitialPosition;
			Size = InitialSize;
			Anchored = false;
			_position = InitialPosition;
			_nextPosition = InitialPosition;

			PhysicsHandler.AddCollider(this);
		}

		private void postPhysicsUpdate(double step)
		{
			Position = _nextPosition;
		}
		public void UpdateCollision(double Step)
		{
			Vector2 NewPosition = this.Position;
			bool didCollide = false;

			// Split the movement into smaller steps to prevent tunneling
			int numSubSteps = 4;
			for (int i = 0; i < numSubSteps; i++)
			{
				Vector2 subStepPosition = Position + (NewPosition - Position) * (float)i / numSubSteps;
				if (detectCollision(Step, NewPosition))
				{
					didCollide = true;
					break;
				}
			}

			if (!didCollide)
			{
				this._nextPosition = NewPosition;
			}
		}

		public void Destroy()
		{
			PhysicsHandler.RemoveCollider(this);
		}
		public void EnableDebug()
		{
			Program.CurrentSession.DrawQueue.Add(this, (double timeSinceLastFrame) =>
			{
				RenderHandler.DrawBox(this.Position, this.Size, this);
			});
		}
		public void DisableDebug()
		{
			Program.CurrentSession.DrawQueue.Remove(this);
		}

		private bool detectCollision(double Step, Vector2 NewPosition)
		{
			bool didCollide = false;
			foreach (Collider other in PhysicsHandler.Colliders)
			{
				if (this == other) continue;

				if (NewPosition.X < other.Position.X + other.Size.X &&
					NewPosition.X + Size.X > other.Position.X &&
					NewPosition.Y < other.Position.Y + other.Size.Y &&
					NewPosition.Y + Size.Y > other.Position.Y)
				{
					// Resolve the collision
					Debug.WriteLine("Collision detected");
					resolveCollision(Step, other);
					didCollide = true;
				}
			}

			return didCollide;
		}
        private void resolveCollision(double Step, Collider other)
		{
			// there needs to be support for sliding when colliding with two objects
			// to do this, we need to know the direction of the collision
			Vector2 NewPosition = this.Position;
			Vector2 otherNewPosition = other.Position;

			Vector2 thisCenter = this.Position + this.Size / 2;
			Vector2 otherCenter = other.Position + other.Size / 2;

			Vector2 thisCenterToOtherCenter = otherCenter - thisCenter;

			// if the x distance is greater than the y distance, then the collision is horizontal
			// otherwise, the collision is vertical
			if (Math.Abs(thisCenterToOtherCenter.X) > Math.Abs(thisCenterToOtherCenter.Y))
			{
				// horizontal collision
				if (thisCenterToOtherCenter.X > 0)
				{
					// this is to the left of other
					NewPosition.X = other.Position.X - this.Size.X;
				}
				else
				{
					// this is to the right of other
					NewPosition.X = other.Position.X + other.Size.X;
				}
			}
			else
			{
				// vertical collision
				if (thisCenterToOtherCenter.Y > 0)
				{
					// this is above other
					NewPosition.Y = other.Position.Y - this.Size.Y;
				}
				else
				{
					// this is below other
					NewPosition.Y = other.Position.Y + other.Size.Y;
				}
			}

			this._nextPosition = NewPosition;
		}
	}
}