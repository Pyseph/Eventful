using Microsoft.Xna.Framework;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Eventful
{
	public class MapHitboxManager
	{
		public static GameSession CurrentSession = Program.CurrentSession;
		// add a table of hitboxes with id as the key
		public static Dictionary<int, Collider> Hitboxes = new();
		public MapHitboxManager()
		{
		}

		public void LoadHitboxes()
		{
			string hitboxData = FileManager.ReadFile("Content/Maps/Hitboxes.txt");
			// each line is a hitbox, stored as "{id, x, y, width, height}"
			string[] hitboxes = hitboxData.Split('\n');
			foreach (string hitbox in hitboxes)
			{
				string[] data = hitbox.Split(',');
				int id = int.Parse(data[0]);
				int x = int.Parse(data[1]);
				int y = int.Parse(data[2]);
				int width = int.Parse(data[3]);
				int height = int.Parse(data[4]);

				Collider collider = new(new Vector2(x, y), new Vector2(width, height));
				collider.Anchored = true;
				Hitboxes.Add(id, collider);
			}
		}

		public void UpdateHitboxData(int Id, Vector2 NewSize, Vector2 NewPosition)
		{
			Collider collider = Hitboxes[Id];
			collider.Size = NewSize;
			collider.Position = NewPosition;

			// update the hitbox data file
			string hitboxData = FileManager.ReadFile("Content/Maps/Hitboxes.txt");
			// find the line with the id with a regex (just id and anything before a newline)
			Regex regex = new(Id + ",.*\n");
			// replace the line with the new data
			hitboxData = regex.Replace(hitboxData, Id + "," + NewPosition.X + "," + NewPosition.Y + "," + NewSize.X + "," + NewSize.Y + "\n");
			FileManager.WriteFile("Content/Maps/Hitboxes.txt", hitboxData);
		}
	}
}