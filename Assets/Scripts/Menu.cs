using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	GameObject bgImg;

	GameObject buttonsInstructions;
	GameObject buttonsMenu;

	int mode = 0;

	public Sprite spriteInstructions;
	public Sprite spriteMenu;

	// Use this for initialization
	void Start () {
		bgImg = GameObject.Find ("Background");
		buttonsInstructions = GameObject.Find ("ButtonsInstr");
		buttonsMenu = GameObject.Find ("ButtonsMenu");
		buttonsInstructions.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.JoystickButton0))
		{
			Application.LoadLevel("level1");
		
		}
		if(Input.GetKeyUp(KeyCode.JoystickButton1))
		{

			if(mode == 0)
			{

			}
			else if(mode == 1){
				mode = 0;
				bgImg.GetComponent<SpriteRenderer>().sprite = spriteMenu;
				buttonsMenu.SetActive(true);
				buttonsInstructions.SetActive(false);
			}

		}
		if(Input.GetKeyUp(KeyCode.JoystickButton3))
		{
			if(mode == 0)
			{
				mode = 1;
				bgImg.GetComponent<SpriteRenderer>().sprite = spriteInstructions;
				buttonsMenu.SetActive(false);
				buttonsInstructions.SetActive(true);
			}
		}
	}
}
