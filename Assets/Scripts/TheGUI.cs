using UnityEngine;
using System.Collections;

public class TheGUI : MonoBehaviour
{
    /*
	public enum THE_MODE
	{NONE=0,THE_HUD,THE_CREDSCREEN}
	public static TheSCREENS TheScreens;
	public static TheHUD TheHud;
	public static THE_MODE TheMode
	{
		get { return (THE_MODE)( TheScreens.TheActive > 0 ? TheScreens.TheActive : TheHud.TheActive > 0 ? TheHud.TheActive : 0 ); }
		set
		{
			TheScreens.TheActive = value;
			TheHud.TheActive = value;
		}
	}



	void Start () 
	{
		TheScreens = GameObject.Find("TheSCREENS").gameObject.GetComponent<TheSCREENS>();
		TheHud = GameObject.Find("TheHUD").gameObject.GetComponent<TheHUD>();
		TheMode = THE_MODE.NONE;
	}

	public static void TheUpdate()
	{
		TheHud.TheUpdate();
		TheScreens.TheUpdate();
	}
    */
}
