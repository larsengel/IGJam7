using UnityEngine;
using System.Collections;

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

	}
}
