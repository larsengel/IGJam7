using UnityEngine;
using System.Collections;

public class Endscreen : MonoBehaviour {

	GameObject bgImg;

	GameObject buttonsInstructions;
	GameObject buttonsMenu;

	public Sprite player1Win;
	public Sprite player2Win;

	public static int winner = 0;


	// Use this for initialization
	void Start () {
		bgImg = GameObject.Find ("Background");

		if (winner == 1)
			bgImg.GetComponent<SpriteRenderer> ().sprite = player1Win;
		else if(winner == 2)
			bgImg.GetComponent<SpriteRenderer> ().sprite = player2Win;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.JoystickButton0) || Input.GetKey (KeyCode.Return))
		{
			Application.LoadLevel("menu");
			
		}
	}
}
