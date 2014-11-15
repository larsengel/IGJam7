using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	void Update () {
		PlayerScript playerScript = this.GetComponent<PlayerScript> ();

		if(playerScript.playerNumber == 2)
			this.transform.Find ("Spaceman_Blue").GetComponent<Animator> ().SetFloat ("Speed", Mathf.Abs((float)playerScript.movingDirection));
		if(playerScript.playerNumber == 1)
			this.transform.Find ("Spaceman_Red").GetComponent<Animator> ().SetFloat ("Speed", Mathf.Abs((float)playerScript.movingDirection));
		
        // TODO only update if change
		if((float)playerScript.movingDirection != 0)
		{
			Vector3 theScale = transform.localScale;
			theScale.x = (float)playerScript.movingDirection;
            //theScale.x *= -1;
			transform.localScale = theScale;
		}

	}
}
