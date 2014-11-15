using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

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
            if (player.catchObject == this)
            {
                player.catchObject = null;
                player.catchFollowing = false;
            }
        }
    }

}
