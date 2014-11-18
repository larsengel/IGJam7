using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public bool isLocked = false;
	public int type;	// 1 = Engine, 0 = Module

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            if (player.catchFollowing == false)
            {
                player.catchObject = this.transform;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            if (player.catchObject == this.transform && player.catchFollowing == false)
            {
                player.catchObject = null;
                player.catchFollowing = false;
            }
        }
    }

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			PlayerScript player = other.GetComponent<PlayerScript>();
			if (player.catchFollowing == false && player.catchObject == null)
			{
				player.catchObject = this.transform;
			}
		}
		
	}
	
}
