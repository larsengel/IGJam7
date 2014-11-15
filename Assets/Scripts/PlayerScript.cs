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
    private float axisDiff = 0.7f;
    private enum PICKUPSTATUS { NONE, DOWNTHISFRAME, WASDOWN };
    private PICKUPSTATUS pickup = PICKUPSTATUS.NONE;

    // Jump
    private bool enableJump = false;
    public float jumpTime = 1.0f;
    private float jumpCurrent = 0;

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
        Jump();
	}


	private void GetInputs()
	{
        if (pickup == PICKUPSTATUS.DOWNTHISFRAME)
            pickup = PICKUPSTATUS.WASDOWN;

        string PlayerCode = "A";
        switch (playerNumber)
        {
            case 1:
                PlayerCode = "A";
                if (Input.GetKeyDown(KeyCode.Joystick1Button0)) // A - Jump
                    this.enableJump = true;
                break;
            case 2:
                PlayerCode = "B";
                if (Input.GetKeyDown(KeyCode.Joystick2Button0)) // A - Jump
                    this.enableJump = true;
                break;
        }

		// Movement Controller Axis X + Y + A/D + Left/Right
        float axis = Input.GetAxis("Player"+PlayerCode+"Control");
        if (axis < axisDiff * -1)
            movingDirection = DIRECTION.LEFT;
        else if (axis > axisDiff)
            movingDirection = DIRECTION.RIGHT;
        else
            movingDirection = DIRECTION.NONE;
        if (movingDirection != 0)
            lookAtDirection = movingDirection;

		gunDirection = (AIM)Input.GetAxis("AimA");

        // Reset LT Pickup'ing
        if (Input.GetAxis("Player" + PlayerCode + "ControlLTRT") > 0.6f && pickup == PICKUPSTATUS.NONE) // LR
        {
            pickup = PICKUPSTATUS.DOWNTHISFRAME;
            Catch();
        }
        if (Input.GetAxis("Player" + PlayerCode + "ControlLTRT") <= 0 && pickup == PICKUPSTATUS.WASDOWN) // LR - Reset Pickup
        {
            pickup = PICKUPSTATUS.NONE;
        }

        // RT - Fire
        if (Input.GetAxis("Player" + PlayerCode + "ControlLTRT") < -0.6f) // RT
            Fire();
	}

	private void Move()
	{
        // Player Movement
        Utility.DoAroundMovement(this.transform, movingDirection, speed, !this.enableJump);

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

    private void Fire()
    {
        gun.Fire(lookAtDirection);
    }

    private void Jump()
    {
        if(this.enableJump == true)
        {
            this.jumpCurrent += Time.deltaTime;
            if(this.jumpCurrent <= this.jumpTime)
            {
                this.transform.Translate(Vector3.up * 1.0f * Time.deltaTime);
            }
            else
            {
                this.jumpCurrent = 0;
                this.enableJump = false;
            }
        }

    }

	void Launch()
	{
		if(this.rocketBase != null && this.rocketBase.GetComponent<RocketBase>().isStartable())
		{
			this.rocketBase.GetComponent<RocketBase>().startCountdown();
		}

	}

}
