using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public int playerNumber;
	public string playerName;
	private GlewGunScript gun;
    private GameObject planet;
	public Transform catchObject = null;
    public bool catchFollowing = false;
	[SerializeField]
	private float speed;

	public RocketBase rocketBase = null;

	public enum DIRECTION
	{LEFT = -1,	NONE = 0, RIGHT = 1	}
	[SerializeField]
    private DIRECTION movingDirection;
    private DIRECTION lookAtDirection;

	public enum AIM
	{ DOWN = -1, NONE = 0, UP = 1 }
	[SerializeField]
	internal AIM gunDirection;

	void Start () 
	{
		planet = GameMaster.Earth;
		gun = this.gameObject.transform.GetChild(0).gameObject.GetComponent<GlewGunScript>();
	}
	
	// Update is called once per frame
	void Update() 
	{
		GetInputs();
		Move();
		//gun.Reloade();
	}


	private void GetInputs()
	{
        switch (playerNumber)
        {
            case 1:
			    movingDirection=(DIRECTION)Input.GetAxis("PlayerAControll");
				gunDirection = (AIM)Input.GetAxis("AimA");
				if(Input.GetKeyDown(KeyCode.Joystick1Button0)) // A
					Catch();
                if(Input.GetKeyDown(KeyCode.Joystick1Button1)) // B
                    Jump();
				if(Input.GetKeyDown(KeyCode.Joystick1Button2)) // X
					Fire();
                break;
            case 2:
			    movingDirection=(DIRECTION)Input.GetAxis("PlayerBControll");
				gunDirection = (AIM)Input.GetAxis("AimB");
				if(Input.GetKeyDown(KeyCode.Joystick2Button0))
                    Catch();
                if(Input.GetKeyDown(KeyCode.Joystick2Button1))
                    Jump();
				if(Input.GetKeyDown(KeyCode.Joystick2Button2))
					Fire();
                break;
        }
        if (movingDirection != 0)
            lookAtDirection = movingDirection;
	}

	private void Move()
	{
        // Player Movement
        Utility.DoAroundMovement(this.transform, movingDirection, speed);

        if (this.catchObject != null && catchFollowing == true)
        {
            // Item Movement
            Utility.DoAroundMovement(this.catchObject, movingDirection, speed);
        }
    }

    private void Catch()
    {

		if (this.catchObject != null && catchFollowing == false && this.catchObject.GetComponent<Item> ().isLocked == false)	//Aufnehmen
        {
            catchFollowing = true;
            this.catchObject.transform.Translate(Vector3.up * 0.4f);
			this.catchObject.GetComponent<Item>().isLocked = true;

        }
        else if (this.catchObject != null && catchFollowing == true) //Ablegen
        {
			if(this.rocketBase != null && this.rocketBase.GetComponent<RocketBase>().getModuleCnt() < 12)
			{
				rocketBase.placeItem(this.catchObject.gameObject);

			}
			else
			{

            	this.catchObject.transform.Translate(Vector3.up * -0.4f);
				this.catchObject.GetComponent<Item>().isLocked = false;
			}
			catchFollowing = false;
			this.catchObject = null;
        }
    }

    private void Jump()
    {

    }

    private void Fire()
    {
        gun.Fire(lookAtDirection);
    }

	void Launch()
	{
		if(this.rocketBase != null && this.rocketBase.GetComponent<RocketBase>().isStartable())
		{
			this.rocketBase.GetComponent<RocketBase>().startCountdown();
		}

	}

}
