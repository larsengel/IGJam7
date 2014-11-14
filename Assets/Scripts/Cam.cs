using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

	private float speed = -5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.Rotate(new Vector3(0, 0, 1f * speed * Time.deltaTime));

	}
}
