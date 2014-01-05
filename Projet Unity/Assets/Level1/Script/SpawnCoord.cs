using UnityEngine;
using System.Collections;

public class SpawnCoord : MonoBehaviour 
{
	private Vector3[] Coord = new []
	{	new Vector3(40f , 30.5f, 1250f),
		new Vector3(108f, 30.5f, 1368f),
		new Vector3(110f, 30.5f, 1310f),
		new Vector3(156f, 30.5f, 1280f),
		new Vector3(165f, 30.5f, 1122f),
		new Vector3(206f, 30.5f, 1365f),
		new Vector3(210f, 30.5f, 860f),
		new Vector3(212f, 30.5f, 690f),
		new Vector3(258f, 30.5f, 1192f),
		new Vector3(275f, 30.5f, 1042f),
		new Vector3(360f, 30.5f, 240f),
		new Vector3(362f, 30.5f, 1164f),
		new Vector3(380f, 30.5f, 385f),
		new Vector3(430f, 30.5f, 630f),
		new Vector3(465f, 30.5f, 588f),
		new Vector3(467f, 30.5f, 1067f),
		new Vector3(527f, 30.5f, 1150f),
		new Vector3(615f, 30.5f, 440f),
		new Vector3(615f, 30.5f, 498f),
		new Vector3(665f, 30.5f, 240f),
		new Vector3(675f, 30.5f, 735f),
		new Vector3(690f, 30.5f, 1040f),
		new Vector3(697f, 30.5f, 1200f),
		new Vector3(748f, 30.5f, 900f),
		new Vector3(815f, 30.5f, 540f),
		new Vector3(900f, 30.5f, 690f),
		new Vector3(925f, 30.5f, 345f),
		new Vector3(950f, 30.5f, 1130f),
		new Vector3(980f, 30.5f, 950f),
		new Vector3(1050f, 30.5f, 842f),
		new Vector3(1068f, 30.5f, 515f),
		new Vector3(1156f, 30.5f, 988f),
		new Vector3(1170f, 30.5f, 250f),
		new Vector3(1236f, 30.5f, 500f),
		new Vector3(1240f, 30.5f, 655f)};

	public Vector3 Get_Coord(int nb)
	{
		return Coord[nb];
	}
}
