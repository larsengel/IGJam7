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
    public float CatchSlowmo = 0.7f;
    public float JumpSlowmo = 0.8f;
    

    // Jump
    private bool enableJump = false;
    private float jumpTime = 1.5f;
    private float jumpCurrent = 0;
    private float gravityDefault = 0;
    private float gravityCurrent = 0;
    private float jumpSpeed = 3.0f;

	public enum AIM
	{ DOWN = -1, NONE = 0, UP = 1 }
	[SerializeField]
	internal AIM gunDirection;

	void Start () 
	{
		planet = GameMaster.Earth;
		gun = this.gameObject.transform.GetChild(0).gameObject.GetComponent<GlewGunScript>();
        this.gravityDefault = this.jumpTime / 2;
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
                    StartJump();
				if (Input.GetKeyDown(KeyCode.Joystick1Button3)) // Y - Launch
					Launch();
                break;
            case 2:
                PlayerCode = "B";
                if (Input.GetKeyDown(KeyCode.Joystick2Button0)) // A - Jump
                    StartJump();
				if (Input.GetKeyDown(KeyCode.Joystick2Button3)) // Y - Launch
					Launch();
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
        float tmpSpeed = speed;
        if (this.catchFollowing == true) // slow down if catch item
            tmpSpeed *= CatchSlowmo;
        if (this.enableJump == true) // slow down if Jump
            tmpSpeed *= JumpSlowmo;
        Utility.DoAroundMovement(this.transform, movingDirection, tmpSpeed, !this.enableJump);

        if (this.catchObject != null && catchFollowing == true)
        {
            // Item Movement
            Utility.DoAroundMovement(this.catchObject, movingDirection, tmpSpeed, !this.enableJump);
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

    private void StartJump()
    {
        if (this.enableJump == false)
        {
            this.enableJump = true;
            this.gravityCurrent = this.gravityDefault;
        }
    }

    private void Jump()
    {
        if(this.enableJump == true)
        {
            this.jumpCurrent += Time.deltaTime;
            this.gravityCurrent -= Time.deltaTime;
            if(this.jumpCurrent <= this.jumpTime)
            {
                Vector3 speedJump = Vector3.up * this.jumpSpeed * Time.deltaTime * (this.gravityCurrent / this.gravityDefault);

                this.transform.Translate(speedJump);
                if (this.catchObject != null && this.catchFollowing == true)
                    this.catchObject.transform.Translate(speedJump);
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
