namespace Assets.Scripts.TileMapping
{
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public class WallsCorrectionScript : MonoBehaviour
	{
		public GameObject WallTiles;

		public GameObject GoToCorrect;

		private readonly WallsCorrection wallsCorrection = new WallsCorrection();

		public void Execute()
		{
			var wallTiles = WallTiles.GetComponentInChildren<Tilemap>();
			wallsCorrection.CorrectWalls(GoToCorrect, wallTiles);
		}
	}
}