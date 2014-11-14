using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public int playerNumber;
	public string playerName;

	[SerializeField]
	private float speed;

	public enum DIRECTION
	{LEFT,RIGHT}
	private DIRECTION movingDirection;


	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update() 
	{
	//	Input.GetAxis();
	}
}
