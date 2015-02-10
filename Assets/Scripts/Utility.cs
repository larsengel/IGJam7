using UnityEngine;
using System.Collections;

public class Utility : MonoBehaviour {

    private static float worldSize = 3.9f;

    //public static void DoAroundMovement(Transform transform, PlayerScript.DIRECTION movingDirection, float speed, bool gravity = true)
    //{
        /*
         * OLD MOVEMENT -> PROBLEMS WITH Vector-Switching
         * 
        transform.position += (movingDirection == PlayerScript.DIRECTION.LEFT) ? -transform.right * speed * 0.01f : (movingDirection == PlayerScript.DIRECTION.RIGHT) ? transform.right * speed * 0.01f : Vector3.zero;
        transform.up = -(GameMaster.Earth.GetComponent<CircleCollider2D>().center - new Vector2(transform.position.x, transform.position.y)).normalized;
        if (gravity == true)
        {
            transform.position += -transform.up * ((Vector2.Distance(GameMaster.Earth.transform.position, new Vector2(transform.position.x, transform.position.y)) + transform.localScale.y) - (GameMaster.Earth.GetComponent<CircleCollider2D>().radius * 2 - transform.localScale.y * 2));
        }*/
    //}

    public static float DoAroundMovement(Transform transform, float currentDegrees, PlayerScript.DIRECTION movingDirection, float speed, bool gravity = true)
    {
        // do kreismovement
        float addDegrees = speed * (int)movingDirection * 5.0f * Time.deltaTime;
        currentDegrees += addDegrees;
        float angle = Mathf.PI * currentDegrees / 180.0f;
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        float distance = Vector2.Distance(GameMaster.Earth.transform.position,new Vector2(transform.position.x, transform.position.y));

        // set kreismovement
        Vector3 pos = new Vector3(sin * distance, cos * distance, 0);
        transform.position = pos;

        // do drehnung
        transform.Rotate(new Vector3(0, 0, 1), addDegrees*-1);

        // gravity
        if (gravity == true)
        {
            transform.position += -transform.up * ((distance + transform.localScale.y) - (GameMaster.Earth.GetComponent<CircleCollider2D>().radius * 2 - transform.localScale.y * 2));
        }

        // return new Degrees of object
        return currentDegrees;
    }

}
