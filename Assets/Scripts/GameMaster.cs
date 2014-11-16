using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour 
{
	public static Utility utility;
	public static GameObject Earth;
	public static bool GameIsRunning = false;

	// Use this for initialization
	void Start()
	{
		Earth = GameObject.FindGameObjectWithTag("Earth");
		utility = GameObject.Find("ScriptContainer").gameObject.GetComponent<Utility>();
	}
}
