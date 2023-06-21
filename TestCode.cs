using Microsoft.Xna.Framework;
using System.Threading;

namespace Eventful
{
	public class TestCode
	{
		public static GameSession CurrentSession = Program.CurrentSession;
		public TestCode()
		{
			AddTestBox(new Vector2(32, 32), new Vector2(200, 0), new Vector2(50, 0));
			AddTestBox(new Vector2(32, 32), new Vector2(600, 0), new Vector2(-50, 0));
		}
		public void AddTestBox(Vector2 Size, Vector2 Position, Vector2 Velocity)
		{
			Object TestBox = new(Position, Size);

			GameEvents.PrePhysics.Invoked += (double step) => {
				TestBox.Position += Velocity * (float)step;
			};
		}
	}
}