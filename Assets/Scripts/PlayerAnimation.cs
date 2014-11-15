using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	float oldMovingDirection;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		PlayerScript playerScript = this.GetComponent<PlayerScript> ();

		if(playerScript.playerNumber == 2)
			this.transform.Find ("Spaceman_Blue").GetComponent<Animator> ().SetFloat ("Speed", Mathf.Abs((float)playerScript.movingDirection));

		if((float)playerScript.movingDirection != 0)
		{
			Vector3 theScale = transform.localScale;
			theScale.x = (float)playerScript.movingDirection;
			transform.localScale = theScale;
		}

		oldMovingDirection = (float)playerScript.movingDirection;
	}
}
