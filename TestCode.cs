using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Eventful
{
	public class TestCode
	{
		public static GameSession CurrentSession = Program.CurrentSession;
		public TestCode()
		{
			AddTestBox(new Vector2(32, 32), new Vector2(0, 0), new Vector2(50, 0));
			AddTestBox(new Vector2(32, 32), new Vector2(800, 0), new Vector2(-50, 0));
		}
		public void AddTestBox(Vector2 Size, Vector2 Position, Vector2 Velocity)
		{
			Collider TestBox = new Collider(Position, Size);

			CurrentSession.DrawQueue.Add(TestBox, (double step) => {
				RenderHandler.DrawBox(TestBox.Position, TestBox.Size);
			});

			GameEvents.PreRender.Invoked += (double step) => {
				TestBox.UpdatePosition(TestBox.Position + Velocity*(float)step);
			};
		}
	}
}