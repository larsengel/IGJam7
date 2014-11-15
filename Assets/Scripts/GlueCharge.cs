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

        Utility.DoAroundMovement(this.transform, movementDirection, speed);

	}

    void OnTriggerEnter2D(Collider2D other)
	{
        //Debug.Log(other.tag);
        // Player hit
        if (other.tag == "Player")
        {
            GameObject.Destroy(this.gameObject);

            // Through item away

            // Hit Player -> through back - slow movement


        }

	}
}
