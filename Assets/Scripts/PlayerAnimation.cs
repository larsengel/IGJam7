using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

    void Start()
    {

    }

	public void UpdateAnimator () {
		PlayerScript playerScript = this.GetComponent<PlayerScript> ();

		this.transform.Find ("Spaceman_Blue").GetComponent<Animator> ().SetFloat ("Speed", Mathf.Abs((float)playerScript.movingDirection));

        if(playerScript.movingDirection != 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = (float)playerScript.movingDirection;
            transform.localScale = theScale;
        }

	}
}
