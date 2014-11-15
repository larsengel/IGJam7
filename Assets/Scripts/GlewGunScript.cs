using UnityEngine;
using System.Collections;

public class GlewGunScript : MonoBehaviour 
{
	[SerializeField]
    private GameObject chargePrefab;

    public float timeNeed = 1f;
    private float timeCurrent = 0;
    private Transform Spawn;

    void Start()
	{
        Spawn = this.gameObject.transform.GetChild(0).transform;
	}

    void Update()
    {
        timeCurrent += Time.deltaTime;
    }

    public void Fire(PlayerScript.DIRECTION direction)
	{
        if (this.timeCurrent >= this.timeNeed)
		{
            this.timeCurrent = 0;

            GameObject charge = GameObject.Instantiate(chargePrefab, Spawn.position, this.transform.rotation) as GameObject;
            charge.GetComponent<GlueCharge>().movementDirection = direction;
		}
	}

}
