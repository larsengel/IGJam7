using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GlueCharge : MonoBehaviour
{
	[SerializeField]
    public float speed = 15.0f;
    public PlayerScript.DIRECTION movementDirection = PlayerScript.DIRECTION.RIGHT;
    public float currentDegrees = 0;

    private float timer;

	void Start ()
	{
	}

	void Update()
	{
        Flow();

        // Destroy after 10 Sek
        timer += Time.deltaTime;
        if(timer >= 5f)
        {
            GameObject.Destroy(this.gameObject);
        }
	}

    public void Flow()
	{
        // Shot Movement
        this.currentDegrees = Utility.DoAroundMovement(this.transform, this.currentDegrees, movementDirection, speed, false);
	}

    void OnTriggerEnter2D(Collider2D other)
	{
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

            // Rotate Player if he looks not into Hit direction
            if (player.lookAtDirection == movementDirection)
            {
                player.LookOtherWay();
            }
        }

        if (other.tag == "Shot")
        {
            // Destroy other shot
            GameObject.Destroy(other.gameObject);

            // Destroy this shot
            GameObject.Destroy(this.gameObject);
        }

		if (other.tag == "Rocket" && other.GetComponent<RocketBase>().isCountdownStarted)
		{
			List<GameObject> _placedItems = other.GetComponent<RocketBase>().placedItems;
			GameObject.Destroy(this.gameObject);
			GameObject lastItem = _placedItems[_placedItems.Count-1];
			_placedItems.RemoveAt(_placedItems.Count-1);
			GameObject.Destroy(lastItem);
			other.transform.Find("RocketHit").GetComponent<Animator>().SetTrigger("Hit");
			GameObject.Find ("BoxGone").GetComponent<AudioSource>().Play ();
		}

	}
}
