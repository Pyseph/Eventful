using System.Collections.Generic;

namespace Eventful
{
	public class PhysicsHandler
	{
		public List<Collider> Colliders = new();
		public PhysicsHandler()
		{

		}

		public void AddCollider(Collider collider)
		{
			Colliders.Add(collider);
		}
		public void UpdateColliders(double step)
		{
			foreach (Collider collider in Colliders)
			{
				if (collider.Anchored) continue;
				collider.UpdateCollision(step);
			}
		}
	}
}