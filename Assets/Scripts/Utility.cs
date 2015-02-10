using UnityEngine;
using System.Collections;

public class Utility : MonoBehaviour {

    public static float DoAroundMovement(Transform transform, float currentDegrees, PlayerScript.DIRECTION movingDirection, float speed, bool gravity = true)
    {
        // do kreismovement
        float addDegrees = speed * (int)movingDirection * 8.0f * Time.deltaTime;
        currentDegrees += addDegrees;
        float angle = Mathf.PI * currentDegrees / 180.0f;
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        float distance = Vector2.Distance(GameMaster.Earth.transform.position,new Vector2(transform.position.x, transform.position.y));

        // set kreismovement
        Vector3 pos = new Vector3(sin * distance, cos * distance, 0);
        transform.position = pos;

        // do turn
        Utility.DoTurn(transform, addDegrees);

        // gravity
        if (gravity == true)
        {
            // sets player and items to world subface
            transform.position += -transform.up * ((distance + transform.localScale.y) - (GameMaster.Earth.GetComponent<CircleCollider2D>().radius * 2 - transform.localScale.y * 2));
        }

        // return new Degrees of object
        return currentDegrees;
    }

    public static void DoTurn(Transform transform, float addDegrees)
    {
        // do turn
        transform.Rotate(new Vector3(0, 0, 1), addDegrees * -1);
    }

}
