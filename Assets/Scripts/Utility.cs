using UnityEngine;
using System.Collections;

public class Utility : MonoBehaviour {

    public static void DoAroundMovement(Transform transform, PlayerScript.DIRECTION movingDirection, float speed, bool gravity = true)
    {
        transform.position += (movingDirection == PlayerScript.DIRECTION.LEFT) ? -transform.right * speed * 0.01f : (movingDirection == PlayerScript.DIRECTION.RIGHT) ? transform.right * speed * 0.01f : Vector3.zero;
        transform.up = -(GameMaster.Earth.GetComponent<CircleCollider2D>().center - new Vector2(transform.position.x, transform.position.y)).normalized;
        if (gravity == true)
        {
            transform.position += -transform.up * ((Vector2.Distance(GameMaster.Earth.transform.position, new Vector2(transform.position.x, transform.position.y)) + transform.localScale.y) - (GameMaster.Earth.GetComponent<CircleCollider2D>().radius * 2 - transform.localScale.y * 2));
        }
    }

	//public static void DoAroundMovement(Transform transform, PlayerScript.DIRECTION movingDirection, float speed, bool gravity = true)
	//{
	//	transform.position += ( movingDirection == PlayerScript.DIRECTION.LEFT ) ? -transform.right * speed * 0.01f : ( movingDirection == PlayerScript.DIRECTION.RIGHT ) ? transform.right * speed * 0.01f : Vector3.zero;
	//	transform.up = -( GameMaster.Earth.GetComponent<CircleCollider2D>().center - new Vector2(transform.position.x, transform.position.y) ).normalized;
	//	if(gravity == true)
	//	{
	//		transform.position += -transform.up * ( ( Vector2.Distance(GameMaster.Earth.transform.position, new Vector2(transform.position.x, transform.position.y)) + transform.localScale.y ) - ( GameMaster.Earth.GetComponent<CircleCollider2D>().radius * 2 - transform.localScale.y * 2 ) );
	//	}
	//}
}
