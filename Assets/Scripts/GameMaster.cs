using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour 
{

	public static GameObject Earth;
	public static bool GameIsRunning = false;
	// Use this for initialization
	void Start()
	{
		Earth = GameObject.FindGameObjectWithTag("Earth");
	}
}
