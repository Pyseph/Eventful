
namespace Eventful
{
	public class Program
	{
		public static GameSession CurrentSession = new();
		public static void Main(string[] args)
		{
			CurrentSession.Run();
		}
	}
}
