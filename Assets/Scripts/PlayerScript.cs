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

	public enum DIRECTION
	{
		LEFT = -1,
		NONE = 0,
		RIGHT = 1
	}
	private DIRECTION movingDirection;

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
		gun.Reloade();
	}


	private void GetInputs()
	{
        switch (playerNumber)
        {
            case 1:
			    movingDirection=(DIRECTION)Input.GetAxis("PlayerAControll");
                if (Input.GetKeyDown(KeyCode.Joystick2Button0))
                    Catch();
                break;
            case 2:
			    movingDirection=(DIRECTION)Input.GetAxis("PlayerBControll");
                if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                    Catch();
                break;
        }
	}

	private void Move()
	{

        //move left or right...
        Vector3 positionSide = (movingDirection == DIRECTION.LEFT) ? -this.transform.right * speed * 0.01f : (movingDirection == DIRECTION.RIGHT) ? this.transform.right * speed * 0.01f : Vector3.zero;
        this.transform.position += positionSide;

        //correct orientation angle...
        Vector2 up = -(planet.GetComponent<CircleCollider2D>().center - new Vector2(this.transform.position.x, this.transform.position.y)).normalized;
        this.gameObject.transform.up = up;

        //correct position (distance to planet-center)...
        Vector3 positionDown = -transform.up * ((Vector2.Distance(this.planet.transform.position, new Vector2(this.transform.position.x, this.transform.position.y)) + this.transform.localScale.y) - (planet.GetComponent<CircleCollider2D>().radius * 2 - this.transform.localScale.y * 2));
        this.gameObject.transform.position += positionDown;

        if (this.catchObject != null && catchFollowing == true)
        {
            this.catchObject.transform.position += positionSide;
            this.catchObject.transform.up = up;
            this.catchObject.transform.position += positionDown;
        }
    }

    private void Catch()
    {
        if (this.catchObject != null && catchFollowing == false)
        {
            catchFollowing = true;
        }
        else if(catchFollowing == true)
        {
            catchFollowing = false;
        }
    }
}
