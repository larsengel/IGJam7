using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GlueCharge : MonoBehaviour
{
	[SerializeField]
    public float speed = 10.0f;
    public PlayerScript.DIRECTION movementDirection = PlayerScript.DIRECTION.RIGHT;

	void Start ()
	{
	}

	void Update()
	{
		Flow();
	}

    public void Flow()
	{

        Utility.DoAroundMovement(this.transform, movementDirection, speed, false);

	}

    void OnTriggerEnter2D(Collider2D other)
	{
        //Debug.Log(other.tag);
        // Player hit
        if (other.tag == "Player")
        {
            // Destroy Hit Object
            GameObject.Destroy(this.gameObject);

            // Get Player
            PlayerScript player = other.GetComponent<PlayerScript>();

            // Through item away
            if (player.catchObject != null & player.catchFollowing == true)
                player.ThroughItemAway();

            // Hit Player -> through back - slow movement
            player.EnableDamage();

        }
		if (other.tag == "Rocket" && other.GetComponent<RocketBase>().isCountdownStarted)
		{
			List<GameObject> _placedItems = other.GetComponent<RocketBase>().placedItems;
			GameObject.Destroy(this.gameObject);
			//GameObject.Destroy(
			GameObject lastItem = _placedItems[_placedItems.Count-1];
			_placedItems.RemoveAt(_placedItems.Count-1);
			GameObject.Destroy(lastItem);

		}

	}
}
