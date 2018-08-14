namespace Assets.Scripts
{
	public class DataController
	{
		public static DataController Instance { get; }

		static DataController()
		{
			Instance = new DataController();
		}


	}
}