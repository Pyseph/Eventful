using Microsoft.Xna.Framework;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Diagnostics;

namespace Eventful
{
	public class MapHitboxManager
	{
		public static GameSession CurrentSession = Program.CurrentSession;
		// add a table of hitboxes with id as the key
		public static Dictionary<int, Collider> Hitboxes = new();
		public MapHitboxManager()
		{
			// Content folder check
			if (!FileManager.FileExists("Content"))
				FileManager.CreateFolder("Content");
			// Maps folder check
			if (!FileManager.FileExists("Content/Maps"))
				FileManager.CreateFolder("Content/Maps");

			if (!FileManager.FileExists("Content/Maps/Hitboxes.dat"))
				FileManager.CreateFile("Content/Maps/Hitboxes.dat");

			LoadHitboxes();
		}

		public void LoadHitboxes()
		{
			string hitboxData = FileManager.ReadFile("Content/Maps/Hitboxes.dat");
			// each line is a hitbox, stored as "{id, x, y, width, height}"
			string[] hitboxes = hitboxData.Split('\n');
			foreach (string hitbox in hitboxes)
			{
				string[] data = hitbox.Split(',');
				if (data.Length != 5)
					continue;
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

		public void AddHitbox(Collider hitbox)
		{
			// add the hitbox to the table
			Hitboxes.Add(Hitboxes.Count, hitbox);
		}
		public void SaveHitboxData(int Id, Vector2 NewSize, Vector2 NewPosition)
		{
			Collider collider = Hitboxes[Id];
			collider.Size = NewSize;
			collider.Position = NewPosition;

			string hitboxData = FileManager.ReadFile("Content/Maps/Hitboxes.dat");
			string newData = Id + "," + NewPosition.X + "," + NewPosition.Y + "," + NewSize.X + "," + NewSize.Y + ";";
			// check if the id exists
			if (Regex.IsMatch(hitboxData, "\n" + Id + ",.*;")) {
				// find the line with the id with a regex (just id and anything before a newline)
				Regex regex = new("\n" + Id + ",.*;");
				// replace the line with the new data
				hitboxData = regex.Replace(hitboxData, newData);
				FileManager.WriteFile("Content/Maps/Hitboxes.dat", hitboxData);
			} else {
				FileManager.AppendFile("Content/Maps/Hitboxes.dat", "\n" + newData);
			}
		}
	}
}