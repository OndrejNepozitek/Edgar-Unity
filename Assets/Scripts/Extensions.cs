namespace Assets.Scripts
{
	using System.Collections.Generic;
	using System.Linq;

	public static class Extensions
	{
		public static int GetNextId<T>(this Dictionary<int, T> dictionary)
		{
			if (dictionary.Count == 0)
			{
				return 1;
			}

			return dictionary.Keys.Max() + 1;
		}
	}
}