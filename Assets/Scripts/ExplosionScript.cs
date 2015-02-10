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
		if(this.name == "ExplosionB")
			Endscreen.winner = 1;
		else if(this.name == "ExplosionA")
			Endscreen.winner = 2;
		
		Application.LoadLevel("endscreen");
		//Time.timeScale = 0;
	}
}
