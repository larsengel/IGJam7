using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public int playerNumber;
	public string playerName;
	public GameObject planet;
	[SerializeField]
	private float speed;

	public enum DIRECTION
	{
		LEFT = -1,
		NONE = 0,
		RIGHT = 1
	}
	public DIRECTION movingDirection;


	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update() 
	{
		GetInputs();
		Move();
	}


	private void GetInputs()
	{
		if(playerNumber==1)
		{
			movingDirection=(DIRECTION)Input.GetAxis("PlayerAControll");
		}
		else
		{
			movingDirection=(DIRECTION)Input.GetAxis("PlayerBControll");
		}
	}

	private void Move()
	{
		Vector2 direction;
		this.transform.position += (movingDirection == DIRECTION.LEFT)? -this.transform.right*speed :(movingDirection==DIRECTION.RIGHT)? this.transform.right * speed:Vector3.zero;
		
		this.gameObject.transform.up = -(direction = ( planet.GetComponent<CircleCollider2D>().center - new Vector2(this.transform.position.x, this.transform.position.y) ).normalized);
		this.gameObject.transform.position += -transform.up * ( ( Vector2.Distance(this.planet.transform.position, new Vector2(this.transform.position.x, this.transform.position.y)) + this.transform.localScale.y ) - ( planet.GetComponent<CircleCollider2D>().radius*2 - this.transform.localScale.y*2 ) );
	}
}
