using UnityEngine;
using System.Collections;

public class GlewGunScript : MonoBehaviour 
{
	[SerializeField]
    private GameObject chargePrefab;
	public static AudioSource fireSound;
    public float timeNeed = 1f;
    public float timeCurrent = 0;
    private Transform Spawn;

    void Start()
	{
        Spawn = this.gameObject.transform.GetChild(0).transform;
		fireSound = GameObject.Find("ScriptContainer/Laser").GetComponent<AudioSource>();
	}

    void Update()
    {
        timeCurrent = timeCurrent>timeNeed? timeNeed : timeCurrent + Time.deltaTime;
    }

    public void Fire(PlayerScript.DIRECTION direction)
	{
        if (this.timeCurrent >= this.timeNeed)
		{
            this.timeCurrent = 0;
			fireSound.Play();

            GameObject charge = GameObject.Instantiate(chargePrefab, Spawn.position, this.transform.rotation) as GameObject;
            charge.GetComponent<GlueCharge>().movementDirection = direction;
		}
	}

}
