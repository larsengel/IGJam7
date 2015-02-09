using UnityEngine;
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
		}

		bar.transform.localScale = new Vector3(sizex, bar.transform.localScale.y, bar.transform.localScale.z);

    }

	public void Fire(PlayerScript.DIRECTION direction)
	{
        if (this.timeCurrent >= this.timeNeed)
		{
            this.timeCurrent = 0;
			fireSound.Play();
			bar.transform.localScale = new Vector3(0, bar.transform.localScale.y, bar.transform.localScale.z);
			timeNeed = 1f;
			cooldown = false;

            GameObject charge = GameObject.Instantiate(chargePrefab, Spawn.position, this.transform.rotation) as GameObject;
            charge.GetComponent<GlueCharge>().movementDirection = direction;
		}

		else
		{
			if (cooldown == false)
			{
				this.timeCurrent = 0;
				bar.transform.localScale = new Vector3(0, bar.transform.localScale.y, bar.transform.localScale.z);

				timeNeed = 2f;
				cooldown = true;
				
				GameObject charge = GameObject.Instantiate(chargePrefab, Spawn.position, this.transform.rotation) as GameObject;
				charge.GetComponent<GlueCharge>().movementDirection = direction;

				//fireSound.Play();
				GameObject.Find("Cooldown").GetComponent<AudioSource>().Play();
			}
		}
	}

}
