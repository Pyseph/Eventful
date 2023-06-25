namespace Eventful
{
	public class FileManager
	{
		public FileManager()
		{
		}

		public static void WriteFile(string path, string contents)
		{
			System.IO.File.WriteAllText(path, contents);
		}
		public static void AppendFile(string path, string contents)
		{
			System.IO.File.AppendAllText(path, contents);
		}
		public static string ReadFile(string path)
		{
			return System.IO.File.ReadAllText(path);
		}
		public static bool FileExists(string path)
		{
			return System.IO.File.Exists(path);
		}
		public static void CreateFile(string path)
		{
			System.IO.File.Create(path);
		}
		public static void CreateFolder(string path)
		{
			System.IO.Directory.CreateDirectory(path);
		}
	}
}