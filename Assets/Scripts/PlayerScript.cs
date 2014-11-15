using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public int playerNumber;
	public string playerName;
	private GlewGunScript gun;
	public Transform catchObject = null;
    public bool catchFollowing = false;
	[SerializeField]
	private float speed;

	public RocketBase rocketBase = null;

	public enum DIRECTION
	{LEFT = -1,	NONE = 0, RIGHT = 1	}
	[SerializeField]
    public DIRECTION movingDirection = DIRECTION.NONE;
    public DIRECTION lookAtDirection = DIRECTION.RIGHT;
    private float axisDiff = 0.7f; // controls must not be val 1. they could be pressed a little bit
    private enum PICKUPSTATUS { NONE, DOWNTHISFRAME, WASDOWN };
    private PICKUPSTATUS pickup = PICKUPSTATUS.NONE;
    private float CatchSlowmo = 0.6f;
    private float JumpSlowmo = 0.8f;

    // Jump
    private bool enableJump = false;
    private float jumpTime = 1.5f;
    private float jumpCurrent = 0;
    private float gravityDefault = 0.75f;
    private float gravityCurrent = 0;
    private float jumpSpeed = 3.0f;

    // Damage
    private bool enableDamage = false;
    private float damageTime = 0.3f;
    private float damageCurrent = 0;
    private float damageHeight = 3.0f;
    private float damageSpeed = 6.0f;
    private float damageGravity = 0.1f;

	public enum AIM
	{ DOWN = -1, NONE = 0, UP = 1 }
	[SerializeField]
	internal AIM gunDirection;

	void Start () 
	{
		gun = this.gameObject.transform.GetChild(0).gameObject.GetComponent<GlewGunScript>();
        this.gravityDefault = this.jumpTime / 2;
	}
	
	// Update is called once per frame
	void Update() 
	{
        if (this.enableDamage == false)
        {
            GetInputs();
            Move();
            Jump();
        }
        Damage();
	}


	private void GetInputs()
	{
        DIRECTION oldMovement = movingDirection;
        if (pickup == PICKUPSTATUS.DOWNTHISFRAME)
            pickup = PICKUPSTATUS.WASDOWN;

        string PlayerCode = "A";
        switch (playerNumber)
        {
            case 1:
                PlayerCode = "A";
                if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.E)) // A - Jump
                    StartJump();
				if (Input.GetKeyDown(KeyCode.Joystick1Button3)  || Input.GetKeyDown(KeyCode.Q)) // Y - Launch
					Launch();
                if (Input.GetKeyDown(KeyCode.Joystick1Button2)) // X
                    Catch();
                break;
            case 2:
                PlayerCode = "B";
                if (Input.GetKeyDown(KeyCode.Joystick2Button0)  || Input.GetKeyDown(KeyCode.O)) // A - Jump
                    StartJump();
				if (Input.GetKeyDown(KeyCode.Joystick2Button3)  || Input.GetKeyDown(KeyCode.P)) // Y - Launch
					Launch();
                if (Input.GetKeyDown(KeyCode.Joystick2Button2)) // X
                    Catch();
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
        if (Input.GetAxis("Player" + PlayerCode + "ControlLTRT") < -0.6f && this.catchFollowing == false) // RT - Shot
            Fire();

        // Animator - Update Direction and Speed
        if(oldMovement != movingDirection)
        {
            if (this.playerNumber == 2)
            {
                PlayerAnimation animation = this.GetComponent<PlayerAnimation>();
                animation.UpdateAnimator();
            }
        }

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
                this.ThroughItemAway();
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
			if(playerNumber == 2)
				this.transform.Find ("Spaceman_Blue").GetComponent<Animator> ().SetTrigger ("Jump");
			if(playerNumber == 1)
				this.transform.Find ("Spaceman_Red").GetComponent<Animator> ().SetTrigger ("Jump");
			GameObject.Find ("ScriptContainer/Jump").GetComponent<AudioSource> ().Play ();
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

	private void Launch()
	{
		if(this.rocketBase != null && this.rocketBase.GetComponent<RocketBase>().isStartable())
		{
			this.rocketBase.GetComponent<RocketBase>().startCountdown();
		}
	}

    public void ThroughItemAway()
    {
        this.catchObject.transform.Translate(Vector3.up * -0.4f);
        this.catchObject.GetComponent<Item>().isLocked = false;
    }

    public void EnableDamage()
    {
        this.enableDamage = true;
        this.gravityCurrent = this.damageGravity;
        this.enableJump = false;
        
		if(playerNumber == 2)
			this.transform.Find ("Spaceman_Blue").GetComponent<Animator> ().SetTrigger ("Hitted");
		if(playerNumber == 1)
			this.transform.Find ("Spaceman_Red").GetComponent<Animator> ().SetTrigger ("Hitted");
		GameObject.Find ("ScriptContainer/Hit").GetComponent<AudioSource> ().Play ();

    }

    public void Damage()
    {
        if(this.enableDamage)
        {
            this.damageCurrent += Time.deltaTime;
            this.gravityCurrent -= Time.deltaTime;

            if(this.damageCurrent <= this.damageTime)
            {
                // Jump top
                Vector3 damageJump = new Vector3(0, 1, 0) * this.damageHeight * Time.deltaTime * (this.gravityCurrent / this.damageGravity);
                // Jump backwords
                damageJump += new Vector3(1, 0, 0) * this.damageSpeed * Time.deltaTime;
                this.transform.Translate(damageJump);
            }
            else
            {
                this.enableDamage = false;
                this.damageCurrent = 0;
            }
            
        }
    }

    public void LookOtherWay()
    {
        this.movingDirection = (DIRECTION)((float)this.lookAtDirection * -1);
    }


}
