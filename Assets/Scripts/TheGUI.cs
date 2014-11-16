using UnityEngine;
using System.Collections;

public class TheGUI : MonoBehaviour
{
	
	public enum THE_MODE
	{ THE_NONE = 0, THE_GAME = 1, THE_MENU = 2, THE_INSTRUCTIONS = 3, THE_PLAYER1_WINSCREEN = 4, THE_PLAYER2_WINSCREEN = 5, THE_EXIT = -1 }

	public static THE_MODE TheMode
	{
		get { return (THE_MODE)( TheScreens.TheActive > 0 ? TheScreens.TheActive : TheHud.TheActive > 0 ? TheHud.TheActive : TheMenu.TheActive > 0 ? TheMenu.TheActive : 0 ); }
		set
		{
			if(value == THE_MODE.THE_EXIT)
			{
				Application.Quit();
			}
			else
			{
				TheHud.TheActive = value;
				if(value >= TheGUI.THE_MODE.THE_INSTRUCTIONS)
					GameMaster.GameIsRunning = false;
				TheScreens.TheActive = value;
				TheMenu.TheActive = value;
			}
		}
	}

	public static TheSCREENS TheScreens;
	public static TheHUD TheHud;
	public static TheMENU TheMenu;

	void Start () 
	{
		TheScreens = GameObject.Find("TheSCREENS").gameObject.GetComponent<TheSCREENS>();
		TheHud = GameObject.Find("TheHUD").gameObject.GetComponent<TheHUD>();
		TheMenu = GameObject.Find("TheMENU").gameObject.GetComponent<TheMENU>();
		TheMode = THE_MODE.THE_MENU;
	}

	public static void TheUpdate()
	{
		TheMenu.TheUpdate();
		TheHud.TheUpdate();
		TheScreens.TheUpdate();
	}

	void Update()
	{
		TheUpdate();
	}
}
