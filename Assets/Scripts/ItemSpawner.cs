﻿using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

	public int itemCounter = 0;

	public Transform item;

	public float timer = 10f; 

	public int pLength = 5;

	public float randMin = 10f, randMax = 10f;

	public Sprite engineSprite, moduleSprite;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update() 
	{
		timer -= Time.deltaTime;
		if(timer < 0)
		{
            Spawn();
		}
	}

    private void Spawn()
    {
		float angle;
		float rnd = Random.value;
		Debug.Log (rnd);
		if(rnd > 0.5f)
			angle = Random.Range ((float)0.0,(float)0.25f) - 0.125f;
		else
			angle = Random.Range ((float)(0.375f), (float)(0.625f));
		Debug.Log (angle);
		angle *= 2 * Mathf.PI;

        float randomX = pLength * Mathf.Cos(angle) + GameMaster.Earth.transform.position.x;
        float randomY = pLength * Mathf.Sin(angle) + GameMaster.Earth.transform.position.y;

        Transform clone = Instantiate(item, new Vector3(randomX, randomY, 0), Quaternion.identity) as Transform;
        //clone.LookAt(GameMaster.Earth.transform.position);
        clone.transform.up = GameMaster.Earth.transform.position - clone.transform.position;
		clone.GetComponent<Item> ().type = Random.value > 0.8 ? 1 : 0;
		clone.GetComponent<SpriteRenderer> ().sprite = clone.GetComponent<Item> ().type == 1 ? engineSprite : moduleSprite;

        timer = randMin + Random.value * randMax;
        itemCounter++;
    }
}
