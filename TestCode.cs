using Microsoft.Xna.Framework;
using System.Threading;

namespace Eventful
{
	public class TestCode
	{
		public static GameSession CurrentSession = Program.CurrentSession;
		public TestCode()
		{
			AddTestBox(new Vector2(32, 32), new Vector2(500, 0), new Vector2(0, 0)).Anchored = true;
			AddTestBox(new Vector2(32, 32), new Vector2(600, 0), new Vector2(-55, 5)).Anchored = false;
		}
		public Object AddTestBox(Vector2 Size, Vector2 Position, Vector2 Velocity)
		{
			Object TestBox = new(Position, Size);

			CurrentSession.PrePhysics.Connect((double step) => {
				TestBox.Position += Velocity * (float)step;
			});
			return TestBox;
		}
	}
}