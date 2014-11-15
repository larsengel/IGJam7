using UnityEngine;
using System.Collections;

public class RocketBase : MonoBehaviour {

	public int engines;
	public int modules;

	public Vector2 moduleOffsets;

	public Vector2 moduleCoords;

	int getModuleCnt()
	{
		return engines+modules;
	}

	Vector2 getModuleCoords()
	{
		int cnt = getModuleCnt ();
		Vector2 newCoords;

		newCoords.x = moduleCoords.x + moduleOffsets.x * cnt%3;
		newCoords.y = moduleCoords.y + moduleOffsets.y * (int)(cnt/3);

		return newCoords;
	}

	void placeItem()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{

		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{

		}
	}
}
