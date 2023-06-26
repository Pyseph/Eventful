using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;
using System;

namespace Eventful
{
	public class GameSession : Game
	{
		public UserInputHandler UserInputHandler = new();
		public PhysicsHandler PhysicsHandler = new();
		public SpriteBatch SpriteBatch;
		public AppearanceHitboxes AppearanceHitboxes = new();

        public GameEvents.Event<double> PreRender = new(true);
		public GameEvents.Event<double> ProcessRender = new(true);
        public GameEvents.Event<double> PrePhysics = new(true);
        public GameEvents.Event<double> PostPhysics = new();
		public MapHitboxManager MapHitboxManager;

		private GraphicsDeviceManager _graphics;
		private double _elapsedTime = 0;

		public GameSession()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			base.Initialize();
			new TestCode();
			//new MapHitboxEditor();
			MapHitboxManager = new();
		}

		protected override void LoadContent()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);

		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			/*
			Each frame runs the following, in order:
			1. (Internal) User input logic is handled
			2. (Event) PreRender, which pauses the game until all callbacks finish running
			3. (Internal) The game is rendered in Draw()
			4. (Event) PrePhysics, which pauses the game until all callbacks finish running
			5. (Internal) Physics logic is ran
			6. (Event) PostPhysics, which runs all callbacks without pausing the game.
			*/

			double elapsedTime = gameTime.TotalGameTime.TotalSeconds;
			double timeSinceLastFrame = elapsedTime - _elapsedTime;

			// 1. User input logic
			UserInputHandler.Update();
			// 2. PreRender
			PreRender.Invoke(timeSinceLastFrame);
			// 3. Draw
			Draw(gameTime);
			// 4. PrePhysics
			PrePhysics.Invoke(timeSinceLastFrame);
			// 5. Physics
			PhysicsHandler.UpdateColliders(timeSinceLastFrame);
			// 6. PostPhysics
			PostPhysics.Invoke(timeSinceLastFrame);

			base.Update(gameTime);
			_elapsedTime = elapsedTime;
		}

		protected override void Draw(GameTime gameTime)
		{
			double elapsedTime = gameTime.TotalGameTime.TotalSeconds;
			double timeSinceLastFrame = elapsedTime - _elapsedTime;

			GraphicsDevice.Clear(Color.CornflowerBlue);

			SpriteBatch.Begin(SpriteSortMode.BackToFront, null);
			ProcessRender.Invoke(timeSinceLastFrame);
			SpriteBatch.End();

			base.Draw(gameTime);
		}
	}
}