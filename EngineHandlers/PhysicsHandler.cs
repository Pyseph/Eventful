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
		public void UpdateColliders()
		{
			foreach (Collider collider in Colliders)
			{
				collider.UpdatePosition(collider.Position);
			}
		}
	}
}