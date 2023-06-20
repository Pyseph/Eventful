using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Eventful
{
	public class UserInputHandler
	{
		private Dictionary<Keys, bool> _keysDown = new();
		private Dictionary<Keys, bool> _mouseDown = new();

		public UserInputHandler()
		{

		}

		// Called as the first step of each frame
		public void Update()
		{
			MouseState mouseState = Mouse.GetState();
			KeyboardState keyboardState = Keyboard.GetState();

			// Find all keys that weren't pressed last frame, but are pressed this frame
			foreach (Keys key in keyboardState.GetPressedKeys())
			{
				if (!_keysDown.ContainsKey(key))
				{
					_keysDown.Add(key, true);
					GameEvents.InputBegan.Invoke(key);
				}
			}

			// Find all keys that were pressed last frame, but aren't pressed this frame
			foreach (Keys key in _keysDown.Keys)
			{
				if (!keyboardState.IsKeyDown(key))
				{
					_keysDown.Remove(key);
					GameEvents.InputEnded.Invoke(key);
				}
			}

			// Clear the keysDown dictionary, and add all keys that are currently pressed
			_keysDown.Clear();
			foreach (Keys key in keyboardState.GetPressedKeys())
			{
				_keysDown.Add(key, true);
			}
		}
	}
}