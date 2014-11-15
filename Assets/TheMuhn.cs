using UnityEngine;
using System.Collections;

public class TheMuhn : MonoBehaviour {

	public float rotator=0.01f;
	// Use this for initialization
	void Start() 
	{
	
	}
	
	// Update is called once per frame
	void Update() 
	{
		this.transform.Rotate(new Vector3(0,0,1) ,rotator);
	}
}
