using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public int playerNumber;
	public string playerName;
	private GlewGunScript gun;
	public GameObject planet;
	public GameObject catchObject=null;
	[SerializeField]
	private float speed;

	public enum DIRECTION
	{LEFT = -1,	NONE = 0, RIGHT = 1	}
	[SerializeField]
	private DIRECTION movingDirection;

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
		gun.Reloade();
	}


	private void GetInputs()
	{
		if(playerNumber==1)
		{
			movingDirection=(DIRECTION)Input.GetAxis("PlayerAControll");
			gunDirection = (AIM)Input.GetAxis("AimA");
			if(Input.GetButtonDown("Fire1"))
				gun.Fire();
		}
		else
		{
			movingDirection=(DIRECTION)Input.GetAxis("PlayerBControll");
			gunDirection = (AIM)Input.GetAxis("AimB");
		}
	}

	private void Move()
	{
		//move left or right...
		this.transform.position += ( movingDirection == DIRECTION.LEFT ) ? -this.transform.right * speed : ( movingDirection == DIRECTION.RIGHT ) ? this.transform.right * speed : Vector3.zero;
		//correct orientation angle...
		this.gameObject.transform.up = -( planet.GetComponent<CircleCollider2D>().center - new Vector2(this.transform.position.x, this.transform.position.y) ).normalized;
		//correct position (distance to planet-center)...
		this.gameObject.transform.position += -transform.up * ( ( Vector2.Distance(this.planet.transform.position, new Vector2(this.transform.position.x, this.transform.position.y)) + this.transform.localScale.y ) - ( planet.GetComponent<CircleCollider2D>().radius * 2 - this.transform.localScale.y * 2 ) );
	}

}
