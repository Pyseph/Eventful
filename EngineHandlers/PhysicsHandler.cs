using System.Collections.Generic;
using System.Diagnostics;
namespace Eventful
{
	public class PhysicsHandler
	{
		public List<Collider> Colliders = new();
		public bool DebugMode = false;
		public PhysicsHandler()
		{

		}

		public void AddCollider(Collider collider)
		{
			if (Colliders.Contains(collider)) return;
			Debug.WriteLine("Added collider");
			if (DebugMode) collider.EnableDebug();
			Colliders.Add(collider);
		}
		public void RemoveCollider(Collider collider)
		{
			if (!Colliders.Contains(collider)) return;
			Debug.WriteLine("Removed collider");
			if (DebugMode) collider.DisableDebug();
			Colliders.Remove(collider);
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