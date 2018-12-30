namespace Assets.Scripts.RoomTemplates.Transformations
{
	using Doors;
	using GeneralAlgorithms.DataStructures.Common;
	using UnityEngine;

	public class RoomTransformationsScript : MonoBehaviour
	{
		public Transformation Transformation;

		public bool TransformDoors = true;

		private readonly RoomTransformations roomTransformations = new RoomTransformations();

		public void Transform()
		{
			roomTransformations.Transform(gameObject, Transformation);

			// Transform doors if enabled
			if (TransformDoors)
			{
				var doors = gameObject.GetComponent<Doors>();

				if (doors != null)
				{
					doors.Transform(Transformation);
				}
			}
		}
	}
}