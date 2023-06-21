using Microsoft.Xna.Framework;
using System;

namespace Eventful
{
	public class Collider
	{
		public Vector2 Position;
		public Vector2 Size;
		public static PhysicsHandler PhysicsHandler = Program.CurrentSession.PhysicsHandler;
		private Vector2 _nextPosition;

		public Collider(Vector2 InitialPosition, Vector2 InitialSize)
		{
			Position = InitialPosition;
			Size = InitialSize;
			_nextPosition = Position;

			PhysicsHandler.AddCollider(this);
			GameEvents.PostPhysics.Invoked += postPhysicsUpdate;
		}

		private void postPhysicsUpdate(double step)
		{
			Position = _nextPosition;
		}
		public void UpdateCollision(Vector2 NewPosition)
		{
			bool didCollide = false;

			// Split the movement into smaller steps to prevent tunneling
			int numSubSteps = 10;
			for (int i = 0; i < numSubSteps; i++)
			{
				Vector2 subStepPosition = Position + (NewPosition - Position) * (float)i / numSubSteps;
				if (checkCollision(subStepPosition))
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
			PhysicsHandler.Colliders.Remove(this);
			GameEvents.PostPhysics.Invoked -= postPhysicsUpdate;
		}

		private bool checkCollision(Vector2 NewPosition)
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
					resolveCollision(other);
					didCollide = true;
				}
			}

			return didCollide;
		}
        private void resolveCollision(Collider other)
		{
			// Calculate the overlap between the two colliders
			float xOverlap = Math.Min(Position.X + Size.X, other.Position.X + other.Size.X) - Math.Max(Position.X, other.Position.X);
			float yOverlap = Math.Min(Position.Y + Size.Y, other.Position.Y + other.Size.Y) - Math.Max(Position.Y, other.Position.Y);

			// If the x overlap is smaller than the y overlap, move the x position
			if (xOverlap < yOverlap)
			{
				// If the x position is to the left of the other collider, move it to the left
				if (Position.X < other.Position.X)
				{
					_nextPosition.X -= xOverlap;
				}
				// If the x position is to the right of the other collider, move it to the right
				else
				{
					_nextPosition.X += xOverlap;
				}
			}
			// If the y overlap is smaller than the x overlap, move the y position
			else
			{
				// If the y position is above the other collider, move it up
				if (Position.Y < other.Position.Y)
				{
					_nextPosition.Y -= yOverlap;
				}
				// If the y position is below the other collider, move it down
				else
				{
					_nextPosition.Y += yOverlap;
				}
			}
		}
	}
}