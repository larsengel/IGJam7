using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void callback()
	{
		//this.GetComponent<Animator>().SetBool("explosion", false);
		Debug.Log (this.name);
		if(this.name == "ExplosionA")
				TheGUI.TheMode = TheGUI.THE_MODE.THE_PLAYER1_WINSCREEN;
		else if(this.name == "ExplosionB")
				TheGUI.TheMode = TheGUI.THE_MODE.THE_PLAYER2_WINSCREEN;

		//Time.timeScale = 0;
	}
}
