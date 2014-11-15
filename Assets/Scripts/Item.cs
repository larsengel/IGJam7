using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public bool isLocked = false;
	public int type;	// 1 = Engine, 0 = Module

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
Debug.Log("enter");
            PlayerScript player = other.GetComponent<PlayerScript>();
            if (player.catchFollowing == false)
            {
                player.catchObject = this.transform;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
Debug.Log("leave");
        if (other.tag == "Player")
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            if (player.catchObject == this)
            {
                player.catchObject = null;
                player.catchFollowing = false;
            }
        }
    }

}
