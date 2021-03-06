﻿using UnityEngine;
using System.Collections;

public class GlewGunScript : MonoBehaviour 
{
	[SerializeField]
    private GameObject chargePrefab;
	public static AudioSource fireSound;
    public float timeNeed = 1f;
    public float timeCurrent = 0;
	public int playerNr;
    private Transform Spawn;

	public bool cooldown = false;

	public GameObject bar;

    void Start()
	{
        Spawn = this.gameObject.transform.GetChild(0).transform;
		fireSound = GameObject.Find("ScriptContainer/Laser").GetComponent<AudioSource>();
		if(playerNr == 1)
			bar = GameObject.Find("laser_red");
		else
			bar = GameObject.Find ("laser_blue");
	}

    void Update()
    {
        timeCurrent = timeCurrent>timeNeed? timeNeed : timeCurrent + Time.deltaTime;
		float sizex = bar.transform.localScale.x;
		if (sizex < 1) {
			sizex += 1*Time.deltaTime/timeNeed;
		}
		if (sizex > 1){
			GameObject.Find("RdyToShoot").GetComponent<AudioSource>().Play ();
			sizex = 1;
			bar.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
		}

		bar.transform.localScale = new Vector3(sizex, bar.transform.localScale.y, bar.transform.localScale.z);

    }

	public void Fire(PlayerScript.DIRECTION direction, float curDegrees, float addDegrees)
	{
        if (this.timeCurrent >= this.timeNeed)
		{
            this.timeCurrent = 0;
			fireSound.Play();
			bar.transform.localScale = new Vector3(0, bar.transform.localScale.y, bar.transform.localScale.z);
			timeNeed = 1f;
			cooldown = false;

            createShot(direction, curDegrees, addDegrees);
		}

		else
		{
			if (cooldown == false)
			{
				this.timeCurrent = 0;
				bar.transform.localScale = new Vector3(0, bar.transform.localScale.y, bar.transform.localScale.z);
				//bar.GetComponent<SpriteRenderer>().color = new Color(0.5F,0.5F,0.5F); // Does not work?!
                GameObject.Find("Cooldown").GetComponent<AudioSource>().Play();

				timeNeed = 2f;
				cooldown = true;

                createShot(direction, curDegrees, addDegrees);
			}
		}
	}

    private void createShot(PlayerScript.DIRECTION direction, float curDegrees, float addDegrees)
    {
        GameObject chargeObj = GameObject.Instantiate(chargePrefab, Spawn.position, this.transform.rotation) as GameObject;
        GlueCharge charge = chargeObj.GetComponent<GlueCharge>();
        charge.movementDirection = direction;
        charge.currentDegrees = curDegrees + addDegrees;

        if (addDegrees > 0)
        {
            Utility.DoTurn(chargeObj.transform, addDegrees * 2);
        }
    }

}
