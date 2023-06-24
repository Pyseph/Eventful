using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Eventful
{
	public class UserInputHandler
	{
		private Dictionary<Keys, bool> _keysDown = new();
		private Dictionary<string, bool> _mouseDown = new();

        public static GameEvents.Event<Keys> InputBegan = new();
        public static GameEvents.Event<Keys> InputEnded = new();

        public static GameEvents.Event<Vector2> MouseButton1Down = new();
        public static GameEvents.Event<Vector2> MouseButton1Up = new();
        public static GameEvents.Event<Vector2> MouseButton2Down = new();
        public static GameEvents.Event<Vector2> MouseButton2Up = new();
        public static GameEvents.Event<Vector2> MouseButton3Down = new();
        public static GameEvents.Event<Vector2> MouseButton3Up = new();

		public UserInputHandler()
		{

		}

		public Vector2 GetMouseLocation()
		{
			return new(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
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
					InputBegan.Invoke(key);
				}
			}
			// Find all keys that were pressed last frame, but aren't pressed this frame
			foreach (Keys key in _keysDown.Keys)
			{
				if (!keyboardState.IsKeyDown(key))
				{
					_keysDown.Remove(key);
					InputEnded.Invoke(key);
				}
			}

			// Clear the keysDown dictionary, and add all keys that are currently pressed
			_keysDown.Clear();
			foreach (Keys key in keyboardState.GetPressedKeys())
			{
				_keysDown.Add(key, true);
			}

			// Dictionary of all mouse buttons and their current state
			Dictionary<string, ButtonState> mouseButtons = new()
			{
				{ "MouseButton1", mouseState.LeftButton },
				{ "MouseButton2", mouseState.RightButton },
				{ "MouseButton3", mouseState.MiddleButton },
			};
			// Dictionary of all mouse buttons and their corresponding events
			Dictionary<string, object> mouseButtonEvents = new()
			{
				{ "MouseButton1Down", MouseButton1Down },
				{ "MouseButton1Up", MouseButton1Up },
				{ "MouseButton2Down", MouseButton2Down },
				{ "MouseButton2Up", MouseButton2Up },
				{ "MouseButton3Down", MouseButton3Down },
				{ "MouseButton3Up", MouseButton3Up },
			};

			// Find all mouse buttons that weren't pressed last frame, but are pressed this frame
			foreach (KeyValuePair<string, ButtonState> button in mouseButtons)
			{
				if (!_mouseDown.ContainsKey(button.Key) && button.Value == ButtonState.Pressed)
				{
					_mouseDown.Add(button.Key, true);
					((GameEvents.Event<Vector2>)mouseButtonEvents[button.Key + "Down"]).Invoke(new Vector2(mouseState.X, mouseState.Y));
				}
			}
			// Find all mouse buttons that were pressed last frame, but aren't pressed this frame
			foreach (KeyValuePair<string, bool> button in _mouseDown)
			{
				if (mouseButtons[button.Key] == ButtonState.Released)
				{
					_mouseDown.Remove(button.Key);
					((GameEvents.Event<Vector2>)mouseButtonEvents[button.Key + "Up"]).Invoke(new Vector2(mouseState.X, mouseState.Y));
				}
			}

			// Clear the mouseDown dictionary, and add all mouse buttons that are currently pressed
			_mouseDown.Clear();
			foreach (KeyValuePair<string, ButtonState> button in mouseButtons)
			{
				if (button.Value == ButtonState.Pressed)
				{
					_mouseDown.Add(button.Key, true);
				}
			}
		}
	}
}