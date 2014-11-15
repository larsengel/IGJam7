using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

	public int itemCounter = 0;

	public Transform item;

	public float timer = 10f; 

	public int pLength = 5;

	public float randMin = 10f, randMax = 10f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer < 0)
		{
            Spawn();
		}
	}

    private void Spawn()
    {
        float angle = Random.value * 2 * Mathf.PI;
        float randomX = pLength * Mathf.Cos(angle) + GameMaster.Earth.transform.position.x;
        float randomY = pLength * Mathf.Sin(angle) + GameMaster.Earth.transform.position.y;

        Transform clone = Instantiate(item, new Vector3(randomX, randomY, 0), Quaternion.identity) as Transform;
        clone.LookAt(GameMaster.Earth.transform.position);
        timer = randMin + Random.value * randMax;
        itemCounter++;
    }
}
