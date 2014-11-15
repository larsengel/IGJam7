using UnityEngine;
using System.Collections;

public class ItemMovement : MonoBehaviour {

	private GameObject _earth;
	// Use this for initialization
	void Start() 
	{
		_earth = GameMaster.Earth;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		float x1 = transform.position.x;
		float y1 = transform.position.y;

		float x2 = _earth.transform.position.x;
		float y2 = _earth.transform.position.y;

		float d = Vector2.Distance (new Vector2 (x1, y1), new Vector2 (x2, y2));

	
		if(d > 3.5f)
        {
			this.transform.up = ( this.transform.position - _earth.transform.position ).normalized;
            this.transform.Translate(-this.transform.up * Time.deltaTime);
        }
        else
        {
            // delete script
            Component.Destroy(this);
        }

	}
}
