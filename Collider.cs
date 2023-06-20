using Microsoft.Xna.Framework;
using System;

namespace Eventful
{
	public class Collider
	{
		public Vector2 Position;
		public Vector2 Size;
		public static PhysicsHandler PhysicsHandler = Program.CurrentSession.PhysicsHandler;

		public Collider(Vector2 InitialPosition, Vector2 InitialSize)
		{
			Position = InitialPosition;
			Size = InitialSize;

			PhysicsHandler.AddCollider(this);
		}

		public void UpdatePosition(Vector2 NewPosition)
		{
			// All colliders are AABBs
			foreach (Collider other in PhysicsHandler.Colliders)
			{
				if (this == other) continue;

				if (NewPosition.X < other.Position.X + other.Size.X &&
					NewPosition.X + Size.X > other.Position.X &&
					NewPosition.Y < other.Position.Y + other.Size.Y &&
					NewPosition.Y + Size.Y > other.Position.Y)
				{
					// Resolve the collision
					ResolveCollision(other);
					return;
				}
			}

			Position = NewPosition;
		}

        private void ResolveCollision(Collider other)
        {
            // Calculate the displacement vector between the two colliders
            Vector2 displacement = Position - other.Position;

            // Calculate the minimum translation vector (MTV)
            float xOverlap = (Size.X + other.Size.X) / 2 - Math.Abs(displacement.X);
            float yOverlap = (Size.Y + other.Size.Y) / 2 - Math.Abs(displacement.Y);

            if (xOverlap < yOverlap)
            {
                // Horizontal collision
                if (displacement.X < 0)
                    Position.X -= xOverlap;
                else
                    Position.X += xOverlap;
            }
            else
            {
                // Vertical collision
                if (displacement.Y < 0)
                    Position.Y -= yOverlap;
                else
                    Position.Y += yOverlap;
            }
        }
	}
}