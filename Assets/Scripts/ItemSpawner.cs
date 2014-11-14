using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

	public int itemCounter = 0;

	public Transform item;

	public Transform earth;

	public float timer = 10f; 

	public int pLength = 5;

	public float randMin = 10f, randMax = 10f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		float angle = Random.value * 2*Mathf.PI;

		float randomX = pLength * Mathf.Cos (angle) + earth.transform.position.x;
		float randomY = pLength * Mathf.Sin (angle) + earth.transform.position.y;

		if(timer < 0)
		{
			Transform clone = Instantiate(item, new Vector3(randomX, randomY, 0), Quaternion.identity) as Transform;
			clone.LookAt(earth.transform.position);
			timer = randMin + Random.value *randMax;
			itemCounter++;
		}
	}
}
